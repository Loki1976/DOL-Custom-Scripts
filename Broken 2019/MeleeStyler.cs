using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using DOL.GS.Styles;
using DOL.GS.Effects;
using DOL.Database;

namespace DOL.AI.Brain
{
    class MeleeStyler : StandardMobBrain
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Style m_style;
        private AttackData m_lastAD;
        private AttackData m_targetLastAD;

        public override bool Start()
        {
            if (!base.Start())
                return false;

            if (Body != null)
                Body.NextCombatBackupStyle = StyleChooser.GetBestStyle(Body, null, null);

            return true;
        }

        public Style CurrentStyle
        {
            get { return m_style; }
        }

        public override void Notify(DOL.Events.DOLEvent e, object sender, EventArgs args)
        {
            base.Notify(e, sender, args);

            // Some interesting events we might want to respond to:
            //GameObjectEvent.TakeDamage
            //GameLivingEvent.AttackedByEnemy
            //GameLivingEvent.AttackFinished
            //GameLivingEvent.Dying
            //GameLivingEvent.EnemyKilled
            //GameLivingEvent.Moving
            //GameNPCEvent.TurnTo
            //GameNPCEvent.TurnToHeading

            if (sender != Body)
                return;

            if (e == GameLivingEvent.AttackFinished)
            {
                AttackData ad = (args as AttackFinishedEventArgs).AttackData;

                m_lastAD = ad;
                m_style = StyleChooser.GetBestStyle(Body, m_lastAD, m_targetLastAD);
                Body.NextCombatStyle = m_style;

                GameLiving target = ad.Target;

                // Maneuvering may fail if the target is up against a wall or something - then again, mobs can generally walk through walls
                // Let it fail? Or maneuver closer (say, 5 units instead of 65)?
                if (m_style != null && m_style.OpeningRequirementType == Style.eOpening.Positional)
                {
                    float angle = target.GetAngle(Body);
                    Point2D positionalPoint;

                    switch (m_style.OpeningRequirementValue)
                    {
                        case (int)Style.eOpeningPosition.Front:
                            if (!(angle >= 315 || angle < 45))
                            {
                                positionalPoint = target.GetPointFromHeading(Body.Heading, 65);
                                Body.WalkTo(positionalPoint.X, positionalPoint.Y, target.Z, 1250);
                            }
                            break;
                        case (int)Style.eOpeningPosition.Side:
                            if (!(angle >= 45 && angle < 150) && !(angle >= 210 && angle < 315))
                            {
                                positionalPoint = target.GetPointFromHeading((ushort)(target.Heading + (110.0 * (4096.0 / 360.0))), 65);
                                Body.WalkTo(positionalPoint.X, positionalPoint.Y, target.Z, 1250);
                            }
                            break;
                        case (int)Style.eOpeningPosition.Back:
                            if (!(angle >= 150 && angle < 210))
                            {
                                positionalPoint = target.GetPointFromHeading((ushort)((target.Heading + 2048) & 0xFFF), 65);
                                Body.WalkTo(positionalPoint.X, positionalPoint.Y, target.Z, 1250);
                            }
                            break;
                    }

                    Body.TurnTo(target);
                }

                // mobs often "forget" to wield their weapon after getting stunned - this is an attempt to get them to wield it
                if (m_style != null)
                {
                    if (Body.ActiveWeaponSlot == GameLiving.eActiveWeaponSlot.TwoHanded && Body.Inventory != null && Body.Inventory.GetItem(eInventorySlot.TwoHandWeapon) != null)
                        Body.SwitchWeapon(GameLiving.eActiveWeaponSlot.TwoHanded);
                    else
                        Body.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard);
                }
            }
            else
            {
                base.Notify(e, sender, args);

            }
        }

        protected override void OnAttackedByEnemy(DOL.GS.AttackData ad)
        {
            base.OnAttackedByEnemy(ad);

            if (ad.Attacker == Body.TargetObject)
            {
                m_targetLastAD = ad;

                m_style = StyleChooser.GetBestStyle(Body, m_lastAD, m_targetLastAD);
                Body.NextCombatStyle = m_style;
            }
        }
    }


    public class StyleChooser
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static bool AI_LOGGING = false;

        /// <summary>
        /// Keeps all styles from db cached, with only the relevant info
        /// </summary>
        // DOL.GS.SkillBase has a cache of styles, but protected
        // Access is provided only through GetStyleByID(int styleID, int classId)
        // and GetStyleList(string specID, int classId)
        // Initialization of this can take a long time (>100ms), but of course it only happens once
        // If it's problematic, it could be done away with and styles fetched from SkillBase's cache
        // This cache uses less memory/time since it's only the info relevant for the StyleChooser
        //private static readonly Dictionary<int, Dictionary<int, SCStyle>> m_allStyles;

        private static readonly Dictionary<long, Style> m_styleCache;
        private static readonly Dictionary<int, Spell> m_spellCache;

        private static readonly Dictionary<int, Dictionary<int, WeightInfo>> m_styleWeights;


        static StyleChooser()
        {
            m_styleCache = new Dictionary<long, Style>(150);
            m_spellCache = new Dictionary<int, Spell>(50);
            m_styleWeights = new Dictionary<int, Dictionary<int, WeightInfo>>(10);

            #region Style cache initialization
            /*
            SCStyle scStyle;
            DataObject[] allDBStyles = GameServer.Database.SelectAllObjects( typeof( DBStyle ) );
            DBStyleXSpell styleXSpell;
            DBSpell spell;

            m_allStyles = new Dictionary<int, Dictionary<int, SCStyle>>( 110 );

            foreach ( DBStyle dbStyle in allDBStyles )
            {
                scStyle = new SCStyle( dbStyle.ID );
                scStyle.WeaponTypeRequirement = dbStyle.WeaponTypeRequirement;
                scStyle.GrowthRate = dbStyle.GrowthRate;
                scStyle.OpeningRequirementType = (Style.eOpening)dbStyle.OpeningRequirementType;
                scStyle.OpeningRequirementValue = dbStyle.OpeningRequirementValue;
                scStyle.AttackResultRequirement = (Style.eAttackResult)dbStyle.AttackResultRequirement;

                styleXSpell = GameServer.Database.SelectObject( typeof( DBStyleXSpell ), "StyleID = " + dbStyle.ID ) as DBStyleXSpell;

                if ( styleXSpell != null )
                {
                    spell = GameServer.Database.SelectObject( typeof( DBSpell ), "SpellID = " + styleXSpell.SpellID ) as DBSpell;

                    if ( spell != null )
                    {
                        scStyle.Effect = spell.Type;
                        scStyle.EffectChance = (short)styleXSpell.Chance;
                        scStyle.EffectDuration = spell.Duration;
                        scStyle.EffectDamage = spell.Damage;
                        scStyle.EffectValue = spell.Value;
                    }
                }

                if ( !m_allStyles.ContainsKey( dbStyle.ClassId ) )
                {
                    m_allStyles.Add( dbStyle.ClassId, new Dictionary<int, SCStyle>( 16 ) );
                }

                if ( m_allStyles[dbStyle.ClassId].ContainsKey( scStyle.ID ) )
                {
                    log.Warn( "Duplicate style found in DB: ClassId = " + dbStyle.ClassId + "; StyleID = " + scStyle.ID );
                }
                else
                {
                    m_allStyles[dbStyle.ClassId].Add( scStyle.ID, scStyle );
                }
            }
            */
            #endregion Style cache initialization
        }


        public static Style GetBestStyle(GameNPC styleUser, AttackData ad, AttackData targetAD)
        {
            // TODO:  It would be good to analyze the whole style chain and adjust priority accordingly

            if (styleUser == null)
                return null;

            try
            {
                AddStylesToCache(styleUser.Styles);

                Style bestStyle = null;

                GameLiving target = styleUser.TargetObject as GameLiving;

                // allow null target - used to get best anytime style for NextCombatBackupStyle
                /*if ( target == null )
                {
                    log.Warn( "GameNPC has invalid or no target." );
                    return null;
                }*/

                if (styleUser.Styles == null || styleUser.Styles.Count < 1)
                {
                    return null;
                }

                InventoryItem rightHandWeapon = null;
                InventoryItem leftHandWeapon = null;
                InventoryItem twoHandWeapon = null;

                if (styleUser.Inventory != null)
                {
                    rightHandWeapon = styleUser.Inventory.GetItem(eInventorySlot.RightHandWeapon);
                    leftHandWeapon = styleUser.Inventory.GetItem(eInventorySlot.LeftHandWeapon);
                    twoHandWeapon = styleUser.Inventory.GetItem(eInventorySlot.TwoHandWeapon);
                }

                if (rightHandWeapon == null && twoHandWeapon == null)
                {
                    log.Warn("StyleChooser:  " + styleUser.Name + " (" + styleUser.GetType().Name + ") has no weapons equipped.");
                    // go ahead and continue - return best style and leave it to calling method to give NPC a weapon
                    //return null;
                }

                if (styleUser.Styles.Count == 1)
                {
                    return styleUser.Styles[0] as Style;
                }

                // always finish style chains... not always the best idea, but what the heck
                if (ad != null && ad.AttackResult == GameLiving.eAttackResult.HitStyle)
                {
                    foreach (Style userStyle in styleUser.Styles)
                    {
                        if (userStyle.OpeningRequirementType == Style.eOpening.Offensive && userStyle.OpeningRequirementValue == ad.Style.ID)
                        {
                            bestStyle = userStyle;
                            if (AI_LOGGING) log.Info("BEST STYLE = " + bestStyle.Name);
                            return bestStyle;
                        }
                    }
                }

                //GameSpellEffect.Spell.SpellType = "CombatSpeedBuff" "HealOverTime" etc
                //GameSpellEffect.Name = complete proper spell name

                Dictionary<eStyleFX, double> weightModifiers = new Dictionary<eStyleFX, double>(8);

                weightModifiers.Add(eStyleFX.StyleStun, 1.0);
                weightModifiers.Add(eStyleFX.StyleCombatSpeedDebuff, 1.0);
                weightModifiers.Add(eStyleFX.Heal, 1.0);
                weightModifiers.Add(eStyleFX.HealOverTime, 1.0);
                weightModifiers.Add(eStyleFX.Lifedrain, 1.0);
                weightModifiers.Add(eStyleFX.StrengthConstitutionDebuff, 1.0);
                weightModifiers.Add(eStyleFX.StrengthDebuff, 1.0);
                weightModifiers.Add(eStyleFX.StyleBleeding, 1.0);

                if (styleUser.Health > styleUser.MaxHealth * 0.9)
                {
                    weightModifiers[eStyleFX.Heal] = 0.25;
                    weightModifiers[eStyleFX.HealOverTime] = 0.25;
                    weightModifiers[eStyleFX.Lifedrain] = 0.75;
                }

                GameSpellEffect spellEffect;
                //GameSpellAndImmunityEffect immunityEffect;

                if (target != null)
                {
                    lock (target.EffectList)
                    {
                        foreach (IGameEffect effect in target.EffectList)
                        {
                            spellEffect = effect as GameSpellEffect;

                            if (spellEffect != null)
                            {
                                // Don't really need this first part - if the spell effect contains "stun" then they are either stunned
                                // or have stun immunity - either way, no point in trying to stun them
                                /*immunityEffect = spellEffect as GameSpellAndImmunityEffect;
                                if ( immunityEffect != null && immunityEffect.Spell.SpellType.ToLower().Contains( "stun" ) && immunityEffect.ImmunityState == true )
                                {
                                    styleEffectWeight[eStyleFX.StyleStun] = 0;
                                }
                                else*/
                                if (spellEffect.Spell.SpellType.ToLower().Contains("stun"))
                                {
                                    weightModifiers[eStyleFX.StyleStun] = 0.0;
                                }
                                else if (spellEffect.Spell.SpellType.ToLower().Contains("combatspeeddebuff"))
                                {
                                    weightModifiers[eStyleFX.StyleCombatSpeedDebuff] = 0.5;
                                }
                                else if (spellEffect.Spell.SpellType == eStyleFX.StrengthConstitutionDebuff.ToString())
                                {
                                    weightModifiers[eStyleFX.StrengthConstitutionDebuff] = 0.5;
                                }
                                else if (spellEffect.Spell.SpellType == eStyleFX.StrengthDebuff.ToString())
                                {
                                    weightModifiers[eStyleFX.StrengthDebuff] = 0.5;
                                }
                                else if (spellEffect.Spell.SpellType == eStyleFX.StyleBleeding.ToString())
                                {
                                    weightModifiers[eStyleFX.StyleBleeding] = 0.75;
                                }
                            }
                        }
                    }
                }

                List<SCStyle> possibleStyles = new List<SCStyle>(styleUser.Styles.Count);

                foreach (Style style in styleUser.Styles)
                {
                    if (style.StealthRequirement == true && styleUser.IsStealthed == false)
                    {
                        if (AI_LOGGING) log.Warn("DROP Stealth Style:  " + style.Name);
                        continue;
                    }

                    #region Unnecessary weapon type checks
                    // For now, mobs don't have real weapons (no Object_Type etc) and the StyleProcessor skips the weapon type check for GameNPCs
                    // So we don't need to check for shield, dual wield, weapon type
                    /*
                    // if no shield equipped, can't use shield style
                    if ( style.WeaponTypeRequirement == (int)eObjectType.Shield && ( leftHandWeapon == null || leftHandWeapon.Object_Type != (int)eObjectType.Shield ) )
                    {
                        addStyle = false;
                        continue;
                    }
                    // if not dual-wielding, can't use dual wield styles
                    else if ( style.WeaponTypeRequirement == Style.SpecialWeaponType.DualWield && ( leftHandWeapon == null || leftHandWeapon.Object_Type == (int)eObjectType.Shield ) )
                    {
                        addStyle = false;
                        continue;
                    }

                    // if neither the right nor two-hand weapon matches the required weapon type, can't use the style (shield & DW checks were already done)
                    // we know that both right and two-hand weapons can't be null, or we would have exited the function near the beginning
                    if ( style.WeaponTypeRequirement != (int)eObjectType.Shield && style.WeaponTypeRequirement != Style.SpecialWeaponType.AnyWeapon && style.WeaponTypeRequirement != Style.SpecialWeaponType.DualWield )
                    {
                        if ( rightHandWeapon == null && ( GameServer.ServerRules.IsObjectTypesEqual( (eObjectType)style.WeaponTypeRequirement, (eObjectType)twoHandWeapon.Object_Type ) == false ) )
                        {
                            
                            addStyle = false;
                            continue;
                        }
                        else if ( GameServer.ServerRules.IsObjectTypesEqual( (eObjectType)userStyle.WeaponTypeRequirement, (eObjectType)rightHandWeapon.Object_Type ) == false )
                        {
                            if ( twoHandWeapon == null || ( GameServer.ServerRules.IsObjectTypesEqual( (eObjectType)style.WeaponTypeRequirement, (eObjectType)twoHandWeapon.Object_Type ) == false ) )
                            {
                                addStyle = false;
                                continue;
                            }
                        }
                    }
                    */
                    #endregion Unnecessary weapon type checks

                    // To be fair, even though a GameNPC is not prevented from executing a dual-wield style with only one 1H weapon, let's prevent it here
                    // Unfortunately, we can't prevent it from being done with 1H+shield
                    if (style.WeaponTypeRequirement == Style.SpecialWeaponType.DualWield && leftHandWeapon == null)
                    {
                        if (AI_LOGGING) log.Warn("DROP Dual Style:  " + style.Name);
                        continue;
                    }

                    if (target == null && (style.OpeningRequirementType == Style.eOpening.Defensive || style.OpeningRequirementType == Style.eOpening.Offensive || style.OpeningRequirementType == Style.eOpening.Positional))
                    {
                        continue;
                    }
                    else
                    {
                        // if enemy has us targeted, assume they are /facing us and we can't use positionals if they are not stunned
                        if (style.OpeningRequirementType == Style.eOpening.Positional && target.IsStunned == false && target.TargetObject == styleUser)
                        {
                            if (style.OpeningRequirementValue != (int)Style.eOpeningPosition.Front)
                            {
                                if (AI_LOGGING) log.Warn("DROP Positional Style:  " + style.Name);
                                continue;
                            }
                        }
                        else if (style.OpeningRequirementType == Style.eOpening.Offensive)
                        {
                            if (ad == null || ad.Target != styleUser.TargetObject)
                            {
                                if (AI_LOGGING) log.Warn("DROP Offensive (bad target):  " + style.Name);
                                continue;
                            }

                            if (style.OpeningRequirementValue != 0 && (
                                ad.AttackResult != GameLiving.eAttackResult.HitStyle ||
                                ad.Style == null ||
                                ad.Style.ID != style.OpeningRequirementValue))
                            {
                                if (AI_LOGGING) log.Warn("DROP Offensive (bad prereq):  " + style.Name);
                                continue;
                            }
                        }
                        else if (style.OpeningRequirementType == Style.eOpening.Defensive)
                        {
                            if (targetAD == null || targetAD.Target != styleUser)
                            {
                                if (AI_LOGGING) log.Warn("DROP Defensive (bad target):  " + style.Name);
                                continue;
                            }

                            switch (style.AttackResultRequirement)
                            {
                                case Style.eAttackResult.Any:
                                    if (AI_LOGGING) log.Warn("Adding Anytime style " + style.Name);
                                    break;
                                case Style.eAttackResult.Block:
                                    if (targetAD.AttackResult != GameLiving.eAttackResult.Blocked)
                                        continue;
                                    if (AI_LOGGING) log.Warn("Adding Block style " + style.Name);
                                    break;
                                case Style.eAttackResult.Evade:
                                    if (targetAD.AttackResult != GameLiving.eAttackResult.Evaded)
                                        continue;
                                    if (AI_LOGGING) log.Warn("Adding Evade style " + style.Name);
                                    break;
                                case Style.eAttackResult.Fumble:
                                    if (targetAD.AttackResult != GameLiving.eAttackResult.Fumbled)
                                        continue;
                                    break;
                                case Style.eAttackResult.Parry:
                                    if (targetAD.AttackResult != GameLiving.eAttackResult.Parried)
                                        continue;
                                    if (AI_LOGGING) log.Warn("Adding Parry style " + style.Name);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }

                    possibleStyles.Add(new SCStyle(style));
                }

                SCStyle scStyle;

                for (int i = 0; i < possibleStyles.Count; i++)
                {
                    scStyle = possibleStyles[i];

                    if (scStyle.EffectType != eStyleFX.None && scStyle.EffectType != eStyleFX.Other && scStyle.EffectType != eStyleFX.DirectDamage)
                    {
                        scStyle.Weight = scStyle.DamageWeight + (scStyle.EffectWeight * weightModifiers[scStyle.EffectType]);
                    }
                    else
                    {
                        scStyle.Weight = scStyle.DamageWeight;
                    }

                    possibleStyles[i] = scStyle;
                }

                if (possibleStyles.Count < 1)
                {
                    return null;
                }

                possibleStyles.Sort(new SCStyleComparer());

                if (AI_LOGGING)
                {
                    foreach (SCStyle currentStyle in possibleStyles)
                    {
                        log.Warn(String.Format("{0,5} {1}", currentStyle.Weight, currentStyle.Name));
                    }
                }

                // determine how many, if any, styles at the top ended up with nearly the same weight
                int sameWeightStyles = 0;
                for (int i = possibleStyles.Count - 1; i > 0; i--)
                {
                    if (possibleStyles[i].Weight - possibleStyles[i - 1].Weight < 15)
                        sameWeightStyles++;
                    else
                        break;
                }

                // if multiple styles near the top (end of array) have very similar weights, choose one randomly
                if (sameWeightStyles > 0)
                    scStyle = possibleStyles[Util.Random(possibleStyles.Count - sameWeightStyles, possibleStyles.Count - 1)];
                else
                    scStyle = possibleStyles[possibleStyles.Count - 1];

                bestStyle = GetStyle(scStyle.ID, scStyle.ClassID);

                if (AI_LOGGING) log.Info("BEST STYLE = " + bestStyle.Name);
                return bestStyle;
            }
            catch (Exception e)
            {
                log.Error("Error: " + e.Message);
                log.Error("StackTrace: " + e.StackTrace);
                return null;
            }
        }


        // Apparently the frequent calls to SkillBase.GetStyleByID() caused its list of styles to get corrupted
        // So, we're keeping a local cache of styles here
        private static void AddStylesToCache(System.Collections.IList styleList)
        {
            long key;

            foreach (Style style in styleList)
            {
                key = ((long)style.ID << 32) | (uint)style.ClassID;

                if (!m_styleCache.ContainsKey(key))
                    m_styleCache.Add(key, style);
            }
        }


        private static Style GetStyle(int styleID, int classID)
        {
            Style style;
            long key = ((long)styleID << 32) | (uint)classID;

            if (m_styleCache.TryGetValue(key, out style))
                return style;
            else
                return SkillBase.GetStyleByID(styleID, classID);
        }


        // Also caching spells used for style procs
        private static Spell GetSpell(int spellID)
        {
            Spell spell;

            if (!m_spellCache.TryGetValue(spellID, out spell))
            {
                spell = SkillBase.GetSpellByID(spellID);
                m_spellCache.Add(spellID, spell);
            }

            return spell;
        }


        /*private double GetDamageWeight( Style style )
        {
            if ( m_styleWeights.ContainsKey( style.ID ) == false || m_styleWeights[style.ID].ContainsKey( style.ClassID ) == false )
            {
                AddStyleWeight( style );
            }

            return m_styleWeights[style.ID][style.ClassID].dmgWeight;
        }

        private double GetEffectWeight( Style style )
        {
            if ( m_styleWeights.ContainsKey( style.ID ) == false || m_styleWeights[style.ID].ContainsKey( style.ClassID ) == false )
            {
                AddStyleWeight( style );
            }

            return m_styleWeights[style.ID][style.ClassID].fxWeight;
        }

        private eStyleFX GetEffectType( Style style )
        {
            if ( m_styleWeights.ContainsKey( style.ID ) == false || m_styleWeights[style.ID].ContainsKey( style.ClassID ) == false )
            {
                AddStyleWeight( style );
            }

            return m_styleWeights[style.ID][style.ClassID].fxType;
        }*/

        private static void AddStyleWeight(Style style)
        {
            WeightInfo weight;

            //weight.classID = style.ClassID;
            //weight.ID = style.ID;
            weight.dmgWeight = 400 * style.GrowthRate;
            weight.fxType = eStyleFX.None;
            weight.fxWeight = 0.0;

            if (style.OpeningRequirementType == Style.eOpening.Offensive && style.OpeningRequirementValue != 0)
            {
                if (m_styleWeights.ContainsKey(style.ID) && m_styleWeights[style.ID].ContainsKey(style.ClassID))
                {
                    WeightInfo tmpweight = m_styleWeights[style.ID][style.ClassID];
                    tmpweight.dmgWeight += 100;
                    m_styleWeights[style.ID][style.ClassID] = tmpweight;
                }
            }

            if (style.Procs != null && style.Procs.Count > 0)
            {
                //Spell proc = SkillBase.GetSpellByID( style.Procs[0].SpellID );
                Spell proc = GetSpell(style.Procs[0].SpellID);

                if (proc != null)
                {
                    try
                    {
                        weight.fxType = (eStyleFX)(Enum.Parse(typeof(eStyleFX), proc.SpellType, true));
                    }
                    catch (Exception)
                    {
                        weight.fxType = eStyleFX.Other;
                    }

                    switch (weight.fxType)
                    {
                        // result: 0 to 2,000 (for 10s stun) - puts us at 1200 for a 6s stun, basically top priority
                        case eStyleFX.StyleStun:
                            weight.fxWeight = 0.2 * proc.Duration;
                            break;
                        case eStyleFX.DirectDamage:
                            weight.fxWeight = 2.5 * proc.Damage;
                            break;
                        case eStyleFX.Lifedrain:
                            weight.fxWeight = 5.0 * proc.Damage;
                            break;
                        case eStyleFX.HealOverTime:
                            weight.fxWeight = 500 + (proc.Value * (proc.Duration / 1000.0)); // [could be improved to consider tick frequency]
                            break;
                        case eStyleFX.StyleCombatSpeedDebuff:
                            weight.fxWeight = 80 * proc.Value;
                            break;
                        case eStyleFX.Heal:
                            weight.fxWeight = 6.0 * proc.Value;
                            break;
                        case eStyleFX.StrengthConstitutionDebuff:
                            weight.fxWeight = 8.0 * proc.Value;
                            break;
                        case eStyleFX.StrengthDebuff:
                            weight.fxWeight = 4.5 * proc.Value;
                            break;
                        case eStyleFX.StyleBleeding:
                            weight.fxWeight = 2.0 * proc.Damage * (proc.Duration / 1000.0); // [could be improved to consider tick frequency]
                            break;
                        // give a small bonus to other style effects - assume a style with some effect is better than an equivalent style with no effect
                        default:
                            weight.fxWeight = 50;
                            break;
                    }
                }
            }

            if (m_styleWeights.ContainsKey(style.ID) == false)
            {
                m_styleWeights.Add(style.ID, new Dictionary<int, WeightInfo>(1));
            }

            if (m_styleWeights[style.ID].ContainsKey(style.ClassID) == false)
            {
                m_styleWeights[style.ID].Add(style.ClassID, weight);
            }
            else
            {
                m_styleWeights[style.ID][style.ClassID] = weight;
            }
        }

        /// <summary>
        /// Some of the more common & important style effects to consider
        /// </summary>
        private enum eStyleFX : byte
        {
            None = 0,
            StyleStun = 1,
            StyleCombatSpeedDebuff,
            DirectDamage,
            Heal,
            HealOverTime,
            Lifedrain,
            StrengthConstitutionDebuff,
            StrengthDebuff,
            StyleBleeding,
            Other
        }

        private struct WeightInfo
        {
            //public int ID;
            //public int classID;
            public double dmgWeight;
            public double fxWeight;
            public eStyleFX fxType;
        }

        private struct SCStyle
        {
            public string Name;
            public ushort ID;
            public int ClassID;
            public double Weight;

            public SCStyle(Style style)
            {
                Name = style.Name;
                ID = style.ID;
                ClassID = style.ClassID;
                Weight = 0.0;
            }

            public double DamageWeight
            {
                get
                {
                    if (m_styleWeights.ContainsKey(ID) == false || m_styleWeights[ID].ContainsKey(ClassID) == false)
                    {
                        //AddStyleWeight( SkillBase.GetStyleByID( ID, ClassID ) );
                        AddStyleWeight(GetStyle(ID, ClassID));
                    }

                    return m_styleWeights[ID][ClassID].dmgWeight;
                }
            }

            public double EffectWeight
            {
                get
                {
                    if (m_styleWeights.ContainsKey(ID) == false || m_styleWeights[ID].ContainsKey(ClassID) == false)
                    {
                        //AddStyleWeight( SkillBase.GetStyleByID( ID, ClassID ) );
                        AddStyleWeight(GetStyle(ID, ClassID));
                    }

                    return m_styleWeights[ID][ClassID].fxWeight;
                }
            }

            public eStyleFX EffectType
            {
                get
                {
                    if (m_styleWeights.ContainsKey(ID) == false || m_styleWeights[ID].ContainsKey(ClassID) == false)
                    {
                        //AddStyleWeight( SkillBase.GetStyleByID( ID, ClassID ) );
                        AddStyleWeight(GetStyle(ID, ClassID));
                    }

                    return m_styleWeights[ID][ClassID].fxType;
                }
            }
        }

        private class SCStyleComparer : IComparer<SCStyle>
        {
            public int Compare(SCStyle x, SCStyle y)
            {
                return x.Weight.CompareTo(y.Weight);
            }
        }
    }
}

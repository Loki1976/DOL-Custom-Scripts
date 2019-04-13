using System;
using DOL.GS;
using DOL.GS.Spells;
using DOL.GS.PacketHandler;
using DOL.Language;
using DOL.GS.Effects;
using System.Collections;
using System.Reflection;
using DOL.Database;
using DOL.Events;
using DOL.GS.GameEvents;
using DOL.GS.Scripts;
using log4net;

/* Summon Buffbot Command - Created By Deathwish, with a BIG THANKS to geshi for his help!
 * Version 1.0 (13/07/2010) For the use of all Dol Members.
 * 
 * Please share any updates or changes.
 * 
 * This script will summon a Buffbot for the cost of 5bps, that lasts 30 seconds.
 * This script contains everything you need to run the script.
 * 
 * How to use: Place script into your scripts folder.
 * InGame Use: /bb to summon (5 bps is needed to summon or you cant summon the Buffbot)
 * 
 * To change Buffbots name guild etc see line 204.
 * (I am not the creator of the buffbot script i have added to this script,
 * its only there for people that dont have a working bb and to make life easier for people that cant use C#!)
 * 
 * Update V1.1 (27/07/10)
 * Now summon buffbot will load in rvr zones, also remove the loading up error
 * 
 * Updated V1.2 (02/08/10)
 * Added a timer for 30 sec so player cant abuse the script.
*/

namespace DOL.GS.Commands
{
    [CmdAttribute(
        "&bb",
        ePrivLevel.Player, // Set to player.
        "/bb - To Summon a Buffbot for the cost of 50bps")]

    public class summonbbCommandHandler : AbstractCommandHandler, ICommandHandler
    {
        #region Command Timer
        public const string Summon_Buff = "SummonBuff";
        public void OnCommand(GameClient client, string[] args)
        {
            GamePlayer player = client.Player as GamePlayer;
            long BuffTick = player.TempProperties.getProperty(Summon_Buff, 0L);
            long changeTime = player.CurrentRegion.Time - BuffTick;
            if (changeTime < 30000)
            {
                player.Out.SendMessage("You must wait " + ((30000 - changeTime) / 1000).ToString() + " more second to attempt to use this command!", eChatType.CT_System, eChatLoc.CL_ChatWindow);
                return;
            }
            player.TempProperties.setProperty(Summon_Buff, player.CurrentRegion.Time);

            #endregion Command timer
      
        #region Command spell Loader 
            if (client.Player.BountyPoints >= 100) // how many bps are need to summon the buffbot
            {
                
                SpellLine line = new SpellLine("BuffBotCast", "BuffBot Cast", "unknown", false);
                    ISpellHandler spellHandler = ScriptMgr.CreateSpellHandler(client.Player, BuffBotSpell, line);
                    if (spellHandler != null)
                        spellHandler.StartSpell(client.Player);
                    client.Player.RemoveBountyPoints(100); // removes the amount of bps from the player
                    client.Player.Out.SendMessage("You have summoned a Buffbot!", eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                    client.Player.SaveIntoDatabase(); // saves new amount of bps
                    client.Player.Out.SendUpdatePlayer(); // updates players bps 
            }
             #endregion command spell loader
            else client.Player.Out.SendMessage("You don't have enough Bounty Pounts to summon a Buffbot!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
        }



        [ScriptLoadedEvent]
        public static void OnScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            Spell load;
            load = BuffBotSpell;
        }

        #region Spell
        protected static Spell m_buffbotSpell;
        public static Spell BuffBotSpell
        {
            get
            {
                if (m_buffbotSpell == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.CastTime = 0;
                    spell.ClientEffect = 0;
                    spell.Duration = 15;
                    spell.Description = "Summons a Buffbot to your location for " + spell.Duration + " seconds.";
                    spell.Name = "BuffBot Spell";
                    spell.Type = "Summon A Buffbot";
                    spell.Range = 0;
                    spell.SpellID = 121232;
                    spell.Target = "Self";
                    spell.Value = BuffbotTemplate.TemplateId;
                    m_buffbotSpell = new Spell(spell, 1);
                    SkillBase.GetSpellList(GlobalSpellsLines.Item_Effects).Add(m_buffbotSpell);
                }
                return m_buffbotSpell;
            }
        }
        #endregion

        #region Npc
        protected static NpcTemplate m_buffbotTemplate;
        public static NpcTemplate BuffbotTemplate
        {
            get
            {
                if (m_buffbotTemplate == null)
                {
                    m_buffbotTemplate = new NpcTemplate();
                    m_buffbotTemplate.Flags += (byte)GameNPC.eFlags.GHOST + (byte)GameNPC.eFlags.PEACE;
                    m_buffbotTemplate.Name = "Buffbot";
                    m_buffbotTemplate.ClassType = "DOL.GS.Scripts.SummonedBuffbot";
                    m_buffbotTemplate.Model = "50";
                    m_buffbotTemplate.TemplateId = 93049;
                    NpcTemplateMgr.AddTemplate(m_buffbotTemplate);
                }
                return m_buffbotTemplate;
            }
        }
        #endregion

    }
}

#region Buffbot
namespace DOL.GS.Scripts
{ 

    public class SummonedBuffbot : GameNPC

  
    {
        public SummonedBuffbot()
            : base()
        {
        }

        public override int Concentration
        {
            get
            {
                return 100000;
            }
        }

        public override int Mana
        {
            get
            {
                return 100000;
            }
        }

        private static ArrayList m_baseSpells = null;
        public static ArrayList BaseBuffs
        {
            get
            {
                if (m_baseSpells == null)
                {
                    m_baseSpells = new ArrayList();
                    m_baseSpells.Add(BotStrBuff);
                    m_baseSpells.Add(BotConBuff);
                    m_baseSpells.Add(BotDexBuff);
                }
                return m_baseSpells;
            }
        }

        private static ArrayList m_specSpells = null;
        public static ArrayList SpecBuffs
        {
            get
            {
                if (m_specSpells == null)
                {
                    m_specSpells = new ArrayList();
                    m_specSpells.Add(BotStrConBuff);
                    m_specSpells.Add(BotDexQuiBuff);
                    m_specSpells.Add(BotAcuityBuff);
                    // m_specSpells.Add(BotSpecAFBuff);
                }
                return m_specSpells;
            }
        }

        private static ArrayList m_otherSpells = null;
        public static ArrayList OtherBuffs
        {
            get
            {
                if (m_otherSpells == null)
                {
                    m_otherSpells = new ArrayList();

                    m_otherSpells.Add(BotHealBuff);
                    m_otherSpells.Add(BotPoweregBuff);
                    m_otherSpells.Add(BotDmgaddBuff);
                    m_otherSpells.Add(BotHasteBuff);
                    // m_otherSpells.Add(BotHPRegenBuff);
                    m_otherSpells.Add(BotEndRegenBuff);
                }
                return m_otherSpells;
            }
        }

        private Queue m_buffs = new Queue();

        public override bool AddToWorld()
        {
            Model = 10;
            GuildName = "Buffbot";
            Level = 60;
            Realm = eRealm.None;
            return base.AddToWorld();
        }

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            if (player.InCombat)
            {
                player.Out.SendMessage("Buffbot says \"stop your combat if you want me to buff you!\"", eChatType.CT_Say, eChatLoc.CL_ChatWindow);
                return false;
            }
            if (GetDistanceTo(player) > WorldMgr.INTERACT_DISTANCE)
            {
                player.Out.SendMessage("Buffbot says \"Get over here if you want me to buff you!\"", eChatType.CT_Say, eChatLoc.CL_ChatWindow);
                return false;
            }
            if (player.ChampionLevel == 10)
            {
                SendReply(player, "Do you wish to cure [Disease] or [Poison] ? or how would you like me buff you with [CL Resists]?");
            }
            TurnTo(player, 3000);
            lock (m_buffs.SyncRoot)
            {
                foreach (Spell s in BaseBuffs)
                {
                    if (s.SpellType == "AcuityBuff" && player.CharacterClass.ClassType != eClassType.ListCaster)
                        continue;
                    Container con = new Container(s, BotBaseSpellLine, player);
                    m_buffs.Enqueue(con);
                }

                foreach (Spell s in SpecBuffs)
                {
                    Container con = new Container(s, BotSpecSpellLine, player);
                    m_buffs.Enqueue(con);
                }

                foreach (Spell s in OtherBuffs)
                {
                    if (s.SpellType == "PowerRegenBuff" && player.MaxMana == 0)
                        continue;
                    Container con = new Container(s, BotOtherSpellLine, player);
                    m_buffs.Enqueue(con);
                }
                //if the player has sickness it will be removed.
                GameSpellEffect effect = SpellHandler.FindEffectOnTarget(player, "PveResurrectionIllness");
                if (effect != null)
                {
                    effect.Cancel(false);
                    player.Out.SendMessage(GetName(0, false) + " cure your resurrection sickness.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
            }
            if (CurrentSpellHandler == null)
                CastBuffs();
            return true;
        }
        public override bool WhisperReceive(GameLiving source, string text)
        {
            GamePlayer player = source as GamePlayer;
            if (player != null)
            {
                //CastSpell(SkillBase.GetSpellByID(SPEEDOFTHEREALMID), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
                //SendReply(player, "Do you wish to cure [Disease] or [Poison] ? or how would you like me buff you with [CL Resists]?");
                if (player.ChampionLevel == 10)
                {
                    int DiseaseSpellID = 33026;
                    int PoisonSpellID = 33003;
                    int CLResist1 = 33029;
                    int CLResist2 = 33030;
                    int CLResist3 = 33031;
                    this.TargetObject = player;
                    switch (text)
                    {
                        case "Disease":
                            CastSpell((SkillBase.GetSpellByID(DiseaseSpellID)), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
                            break;
                        case "Poison":
                            CastSpell((SkillBase.GetSpellByID(PoisonSpellID)), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
                            break;
                        case "CL Resists":
                            Container con = new Container((SkillBase.GetSpellByID(CLResist1)), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells), player);
                            m_buffs.Enqueue(con);

                            con = new Container((SkillBase.GetSpellByID(CLResist2)), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells), player);
                            m_buffs.Enqueue(con);

                            con = new Container((SkillBase.GetSpellByID(CLResist3)), SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells), player);
                            m_buffs.Enqueue(con);
                            CastBuffs();
                            break;
                    }
                }
            }
            return true;
        }
        public void CastBuffs()
        {
            Spell BuffSpell = null;
            SpellLine BuffSpellLine = null;
            GameLiving target = null;
            while (m_buffs.Count > 0)
            {
                Container con = (Container)m_buffs.Dequeue();
                BuffSpell = con.Spell;
                target = con.Target;
                BuffSpellLine = con.SpellLine;

                ISpellHandler spellHandler = ScriptMgr.CreateSpellHandler(this, BuffSpell, BuffSpellLine);
                if (spellHandler != null)
                {
                    TargetObject = target;
                    TurnTo(target, 1000);
                    spellHandler.StartSpell(target);
                }
            }
        }

        #region SpellCasting


        private static SpellLine m_BotBaseSpellLine;
        private static SpellLine m_BotSpecSpellLine;
        private static SpellLine m_BotOtherSpellLine;
        /// <summary>
        /// Spell line used by bots
        /// </summary>
        public static SpellLine BotBaseSpellLine
        {
            get
            {
                if (m_BotBaseSpellLine == null)
                    m_BotBaseSpellLine = new SpellLine("BotBaseSpellLine", "BuffBot Spells", "unknown", true);

                return m_BotBaseSpellLine;
            }
        }

        public static SpellLine BotSpecSpellLine
        {
            get
            {
                if (m_BotSpecSpellLine == null)
                    m_BotSpecSpellLine = new SpellLine("BotSpecSpellLine", "BuffBot Spells", "unknown", false);

                return m_BotSpecSpellLine;
            }
        }

        public static SpellLine BotOtherSpellLine
        {
            get
            {
                if (m_BotOtherSpellLine == null)
                    m_BotOtherSpellLine = new SpellLine("BotOtherSpellLine", "BuffBot Spells", "unknown", true);

                return m_BotOtherSpellLine;
            }
        }

        private static Spell m_baseaf;
        private static Spell m_basestr;
        private static Spell m_basecon;
        private static Spell m_basedex;
        private static Spell m_strcon;
        private static Spell m_dexqui;
        private static Spell m_acuity;
        // private static Spell m_specaf;
        private static Spell m_powereg;
        private static Spell m_dmgadd;
        private static Spell m_haste;
        //  private static Spell m_hpRegen;
        private static Spell m_endRegen;
        private static Spell m_heal;

        #region Spells

        /// <summary>
        /// Bot Base AF buff
        /// </summary>
        public static Spell BotBaseAFBuff
        {
            get
            {
                if (m_baseaf == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = true;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1467;
                    spell.Icon = 1467;
                    spell.Duration = 65535;
                    spell.Value = 67;
                    spell.Name = "Armorfactor Buff";
                    spell.Description = "Adds to the recipient's Armor Factor (AF) resulting in better protection againts some forms of attack. It acts in addition to any armor the target is wearing.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100001;
                    spell.Target = "Realm";
                    spell.Type = "ArmorFactorBuff";
                    spell.EffectGroup = 1;
                    m_baseaf = new Spell(spell, 50);
                }
                return m_baseaf;
            }
        }

        /// <summary>
        /// Bot Str buff
        /// </summary>
        public static Spell BotStrBuff
        {
            get
            {
                if (m_basestr == null)
                {
                    DBSpell spell = new DBSpell();
                    ////spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1457;
                    spell.Icon = 1457;
                    spell.Duration = 65535;
                    spell.Value = 50;
                    spell.Name = "Strength Buff";
                    spell.Description = "Increases target's Strength.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100002;
                    spell.Target = "Realm";
                    spell.Type = "StrengthBuff";
                    spell.EffectGroup = 4;
                    m_basestr = new Spell(spell, 50);
                }
                return m_basestr;
            }
        }

        /// <summary>
        /// Bot Con buff
        /// </summary>
        public static Spell BotConBuff
        {
            get
            {
                if (m_basecon == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1486;
                    spell.Icon = 1486;
                    spell.Duration = 65535;
                    spell.Value = 44;
                    spell.Name = "Constitution Buff";
                    spell.Description = "Increases target's Constitution.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100003;
                    spell.Target = "Realm";
                    spell.Type = "ConstitutionBuff";
                    m_basecon = new Spell(spell, 50);
                }
                return m_basecon;
            }
        }

        /// <summary>
        /// Bot Dex buff
        /// </summary>
        public static Spell BotDexBuff
        {
            get
            {
                if (m_basedex == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1476;
                    spell.Icon = 1476;
                    spell.Duration = 65535;
                    spell.Value = 48;
                    spell.Name = "Dexterity Buff";
                    spell.Description = "Increases target's Dexterity.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100004;
                    spell.Target = "Realm";
                    spell.Type = "DexterityBuff";
                    m_basedex = new Spell(spell, 50);
                }
                return m_basedex;
            }
        }

        /// <summary>
        /// Bot Str/Con buff
        /// </summary>
        public static Spell BotStrConBuff
        {
            get
            {
                if (m_strcon == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1517;
                    spell.Icon = 1517;
                    spell.Duration = 65535;
                    spell.Value = 69;
                    spell.Name = "Strength/Constitution Buff";
                    spell.Description = "Increases Str/Con for a character";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100005;
                    spell.Target = "Realm";
                    spell.Type = "StrengthConstitutionBuff";
                    m_strcon = new Spell(spell, 50);
                }
                return m_strcon;
            }
        }

        /// <summary>
        /// Bot Dex/Qui buff
        /// </summary>
        public static Spell BotDexQuiBuff
        {
            get
            {
                if (m_dexqui == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1526;
                    spell.Icon = 1526;
                    spell.Duration = 65535;
                    spell.Value = 75;
                    spell.Name = "Dexterity/Quickness Buff";
                    spell.Description = "Decreases Dexterity and Quickness for a character.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100006;
                    spell.Target = "Realm";
                    spell.Type = "DexterityQuicknessBuff";
                    m_dexqui = new Spell(spell, 50);
                }
                return m_dexqui;
            }
        }

        /// <summary>
        /// Bot Acuity buff
        /// </summary>
        public static Spell BotAcuityBuff
        {
            get
            {
                if (m_acuity == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1538;
                    spell.Icon = 1538;
                    spell.Duration = 65535;
                    spell.Value = 52;
                    spell.Name = "Acuity Buff Buff";
                    spell.Description = "Increases Acuity (casting attribute) for a character.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100007;
                    spell.Target = "Realm";
                    spell.Type = "AcuityBuff";
                    m_acuity = new Spell(spell, 50);
                }
                return m_acuity;
            }
        }
        /*/// <summary>
        /// Bot Spec Af buff
        /// </summary>
        public static Spell BotSpecAFBuff
        {
            get
            {
                if (m_specaf == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1506;
                    spell.Icon = 1506;
                    spell.Duration = 65535;
                    spell.Value = 52;
                    spell.Name = "Spec AF Buff";
                    spell.Description = "Adds to the recipient's Armor Factor (AF), resulting in better protection against some forms of attack. It acts in addition to any armor the target is wearing.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100014;
                    spell.EffectGroup = 2;
                    spell.Target = "Realm";
                    spell.Type = "ArmorFactorBuff";
                    m_specaf = new Spell(spell, 50);
                }
                return m_specaf;
            }
        }*/
        /// <summary>
        /// Bot PowerReg buff
        /// </summary>
        public static Spell BotPoweregBuff
        {
            get
            {
                if (m_powereg == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 980;
                    spell.Icon = 980;
                    spell.Duration = 65535;
                    spell.Value = 2;
                    spell.Name = "Power Regeneration Buff";
                    spell.Description = "Target regenerates power regeneration during the duration of the spell";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100008;
                    spell.Target = "Realm";
                    spell.Type = "PowerRegenBuff";
                    m_powereg = new Spell(spell, 50);
                }
                return m_powereg;
            }
        }

        /// <summary>
        /// Bot DamageAdd buff
        /// </summary>
        public static Spell BotDmgaddBuff
        {
            get
            {
                if (m_dmgadd == null)
                {
                    DBSpell spell = new DBSpell();
                    ////spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 18;
                    spell.Icon = 18;
                    spell.Duration = 65535;
                    spell.Damage = 5.0;
                    spell.DamageType = 15;
                    spell.Name = "Damage Add Buff";
                    spell.Description = "Target's melee attacks do additional damage.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100009;
                    spell.Target = "Realm";
                    spell.Type = "DamageAdd";
                    m_dmgadd = new Spell(spell, 50);
                }
                return m_dmgadd;
            }
        }

        /// <summary>
        /// Bot DamageAdd buff
        /// </summary>
        public static Spell BotHasteBuff
        {
            get
            {
                if (m_haste == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 407;
                    spell.Icon = 407;
                    spell.Duration = 65535;
                    spell.Value = 13;
                    spell.Name = "Haste Buff";
                    spell.Description = "Increases the target's combat speed.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100010;
                    spell.Target = "Realm";
                    spell.Type = "CombatSpeedBuff";
                    m_haste = new Spell(spell, 50);
                }
                return m_haste;
            }
        }

        //////


        /* /// <summary>
         /// Bot HP Regen buff
         /// </summary>
        public static Spell BotHPRegenBuff
         {
                 get
                 {
                         if (m_hpRegen == null)
                         {
                                 DBSpell spell = new DBSpell();
                                 //spell.AutoSave = false;
                                 spell.CastTime = 0; spell.Radius = 200;
                                 spell.ClientEffect = 2430;
             spell.Icon = 2430;
             spell.Duration = 65535;
                                 spell.Value = 180;
                                 spell.Name = "Realm Speed";
                                 spell.Description = "Increases the target's movement speed.";
                                 spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                                 spell.SpellID = 24030;
                                 spell.Target = "Realm";
                                 spell.Type = "MovementSpeedBuff";
                                 m_hpRegen = new Spell(spell, 50);
                         }
                         return m_hpRegen;
                 }
         }*/

        /// <summary>
        /// Bot End Regen buff
        /// </summary>
        public static Spell BotEndRegenBuff
        {
            get
            {
                if (m_endRegen == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 3296;
                    spell.Icon = 3296;
                    spell.Duration = 65535;
                    spell.Value = 4;
                    spell.Name = "Endurance Regeneration Buff";
                    spell.Description = "Target regenerates endurance during the duration of the spell.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100012;
                    spell.Target = "Realm";
                    spell.Type = "EnduranceRegenBuff";
                    m_endRegen = new Spell(spell, 50);
                }
                return m_endRegen;
            }
        }

        /// <summary>
        /// Bot Heal buff
        /// </summary>
        public static Spell BotHealBuff
        {
            get
            {
                if (m_heal == null)
                {
                    DBSpell spell = new DBSpell();
                    //spell.AutoSave = false;
                    spell.CastTime = 0; spell.Radius = 200;
                    spell.ClientEffect = 1424;
                    spell.Value = 1;
                    spell.Name = "Heal";
                    spell.Description = "Heals the target.";
                    spell.Range = WorldMgr.VISIBILITY_DISTANCE;
                    spell.SpellID = 100013;
                    spell.Target = "Realm";
                    spell.Type = "Heal";
                    m_heal = new Spell(spell, 50);
                }
                return m_heal;
            }
        }

        #endregion Spells


        #endregion SpellCasting

        private void SendReply(GamePlayer target, string msg)
        {
            target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }

        public class Container
        {
            private Spell m_spell;
            public Spell Spell
            {
                get { return m_spell; }
            }

            private SpellLine m_spellLine;
            public SpellLine SpellLine
            {
                get { return m_spellLine; }
            }

            private GameLiving m_target;
            public GameLiving Target
            {
                get { return m_target; }
                set { m_target = value; }
            }
            public Container(Spell spell, SpellLine spellLine, GameLiving target)
            {
                m_spell = spell;
                m_spellLine = spellLine;
                m_target = target;
            }
        }
    }

}
#endregion Buffbot

#region Summon 

namespace DOL.GS.Spells
{
    [SpellHandlerAttribute("Summon A Buffbot")]
    public class SummonBuffbotSpellHandler : SpellHandler
    {
        public SummonBuffbotSpellHandler(GameLiving caster, Spell spell, SpellLine line)
            : base(caster, spell, line)
        { }

        protected GameNPC npc = null;

        public override void ApplyEffectOnTarget(GameLiving target, double effectiveness)
        {
            NpcTemplate template = NpcTemplateMgr.GetTemplate((int)m_spell.Value);

            base.ApplyEffectOnTarget(target, effectiveness);

            if (template.ClassType == "")
                npc = new GameNPC();
            else
            {
                try
                {
                    npc = new GameNPC();
                    npc = (GameNPC)Assembly.GetAssembly(typeof(GameServer)).CreateInstance(template.ClassType, false);

                }
                catch (Exception e)
                {
                }
                if (npc == null)
                {
                    try
                    {
                        npc = (GameNPC)Assembly.GetExecutingAssembly().CreateInstance(template.ClassType, false);
                    }
                    catch (Exception e)
                    {
                    }
                }
                if (npc == null)
                {
                    MessageToCaster("There was an error creating an instance of " + template.ClassType + "!", DOL.GS.PacketHandler.eChatType.CT_System);
                    return;
                }
                npc.LoadTemplate(template);
            }
            GameSpellEffect effect = CreateSpellEffect(npc, effectiveness);
            int x, y;
            m_caster.GetSpotFromHeading(64, out x, out y);
            npc.X = x;
            npc.Y = y;
            npc.Z = m_caster.Z;
            npc.CurrentRegion = m_caster.CurrentRegion;
            npc.Heading = (ushort)((m_caster.Heading + 2048) % 4096);
            npc.Realm = m_caster.Realm;
            npc.CurrentSpeed = 0;
            npc.Level = 1;
            npc.SetOwnBrain(new AI.Brain.BlankBrain());
            npc.AddToWorld();
        }

        public override int OnEffectExpires(GameSpellEffect effect, bool noMessages)
        {
            if (npc != null)
                npc.Delete();
            return base.OnEffectExpires(effect, noMessages);
        }

        public override bool IsOverwritable(GameSpellEffect compare)
        {
            return false;
        }
    }
}


#endregion
/*
 * Etaew: Originally based on the work I did for Fallen Realms and Dracis
 * 
 * 
 * 
 */

using System;
using System.Collections;
using DOL.Database;
using DOL.Events;
using DOL.GS;
using DOL.GS.PacketHandler;
using DOL.GS.Scripts;
using System.Threading;
using log4net;
using DOL.AI.Brain;

namespace DOL.GS.Scripts
{
    public class Summoner : GameNPC
    {
        public Summoner()
            : base()
        {
            SetOwnBrain(new SummonerBrain());
        }
        /// <summary>
        /// Defines a logger for this class.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Variables/Properties

        GameLiving m_summonerTarget;

        public int stage = 0;

        //Glares Target
        public GameLiving SummonerTarget
        {
            get
            {
                return m_summonerTarget;
            }
            set
            {
                m_summonerTarget = value;
            }
        }

        public override int MaxHealth
        {
            get
            {
                return base.MaxHealth * 5;
            }
        }

        public override double AttackDamage(InventoryItem weapon)
        {
            return base.AttackDamage(weapon) * 3.5;
        }

        /// <summary>
        /// Gets or sets the base maxspeed of this living
        /// </summary>
        public override int MaxSpeedBase
        {
            get
            {
                return 191 * 2;
            }
            set
            {
                m_maxSpeedBase = value;
            }
        }

        /// <summary>
        /// Melee Attack Range.
        /// </summary>
        public override int AttackRange
        {
            get
            {
                //Normal mob attacks have 200 ...
                return 400;
            }
            set { }
        }

        #endregion

        #region Combat

        public int DragonNukeandStun(RegionTimer timer)
        {
            //AOE Stun
            CastSpell(Summoner.Stun, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            //AOE Nuke
            CastSpell(Summoner.Nuke, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonStun(RegionTimer timer)
        {
            //AOE Stun
            CastSpell(Summoner.Stun, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonBlare(RegionTimer timer)
        {
            //AOE Stun
            CastSpell(Summoner.Blare, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonShield(RegionTimer timer)
        {
            CastSpell(Summoner.Shield, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonDot(RegionTimer timer)
        {
            CastSpell(Summoner.Dot, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonDotandStun(RegionTimer timer)
        {
            CastSpell(Summoner.Stun, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Dot, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonPb(RegionTimer timer)
        {
            CastSpell(Summoner.Stun, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Mez, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Pb, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Heal, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonMezHeal(RegionTimer timer)
        {
            CastSpell(Summoner.Mez, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            CastSpell(Summoner.Heal, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonMez(RegionTimer timer)
        {
            //AOE Stun
            CastSpell(Summoner.Mez, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public int DragonHeal(RegionTimer timer)
        {
            //AOE Stun
            CastSpell(Summoner.Heal, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }


        // What happens when the DragonNuke timer is filled
        public int DragonNuke(RegionTimer timer)
        {
            //AOE Nuke
            CastSpell(Summoner.Nuke, SkillBase.GetSpellLine(GlobalSpellsLines.Mob_Spells));
            return 0;
        }

        public void DragonGlare(object timer)
        {
            ActionDragonGlare(m_summonerTarget);
        }

        public override void Die(GameObject killer)
        {
            int count = 0;
            lock (this.XPGainers.SyncRoot)
            {
                foreach (System.Collections.DictionaryEntry de in this.XPGainers)
                {
                    GameObject obj = (GameObject)de.Key;
                    if (obj is GamePlayer)
                    {
                        GamePlayer player = obj as GamePlayer;
                        player.KillsEpicBoss++;
                        count++;
                    }
                }
            }

            base.Die(killer);
            // dragon died message
            SummonerBroadcast(Name + " is Vanquished");
        }

        public override void TakeDamage(GameObject source, eDamageType damageType, int damageAmount, int criticalAmount)
        {
            base.TakeDamage(source, damageType, damageAmount, criticalAmount);
            if (ObjectState != eObjectState.Active) return;
            GameLiving t = (GameLiving)source;
            int range = WorldMgr.GetDistance(this, t);
            if (range > 500)
            {
                m_summonerTarget = t;
                PickAction();
            }
        }

        void PickAction()
        {
            if (Util.Random(1) < 1)
            {
                //Glare
                Timer timer = new Timer(new TimerCallback(DragonGlare), null, 30, 0);
            }
            else
            {
                //Throw
                ActionDragonThrow(m_summonerTarget);
            }
        }

        void SummonerBroadcast(string message)
        {
            foreach (GamePlayer players in this.GetPlayersInRadius((ushort)(WorldMgr.VISIBILITY_DISTANCE + 1500)))
                players.Out.SendMessage(message, eChatType.CT_Broadcast, eChatLoc.CL_ChatWindow);
        }

        void ActionDragonGlare(GameLiving target)
        {
            // Let them know that they're about to die.
            SummonerBroadcast(Name + " seems to have a smurk on its face as it glares at " + target.Name);
            TargetObject = m_summonerTarget;
        }

        void ActionDragonThrow(GameLiving target)
        {
            SummonerBroadcast(Name + " throws " + m_summonerTarget.Name + " into the air!");
            m_summonerTarget.MoveTo(m_summonerTarget.CurrentRegionID,
                m_summonerTarget.X,
                m_summonerTarget.Y,
                m_summonerTarget.Z + 100,
                m_summonerTarget.Heading);
        }

        #endregion

        #region Spells
        protected static Spell m_glare;
        public static Spell Glare
        {
            get
            {
                if (m_glare == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 3;
                    spell.ClientEffect = 5510;
                    spell.Description = "Glare";
                    spell.Name = "Glare";
                    spell.Range = 2500;
                    spell.Damage = 500;
                    spell.DamageType = (int)eDamageType.Heat;
                    spell.SpellID = 6001;
                    spell.Target = "Enemy";
                    spell.Type = "DirectDamage";
                    m_glare = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_glare);
                }
                return m_glare;
            }
        }

        protected static Spell m_blare;
        public static Spell Blare
        {
            get
            {
                if (m_glare == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.ClientEffect = 14326;
                    spell.Description = "Glare";
                    spell.Name = "Glare";
                    spell.Range = 2500;
                    spell.Damage = 3000;
                    spell.DamageType = (int)eDamageType.Heat;
                    spell.SpellID = 6007;
                    spell.Target = "Enemy";
                    spell.Type = "DirectDamage";
                    m_blare = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_blare);
                }
                return m_blare;
            }
        }

        protected static Spell m_mez;
        public static Spell Mez
        {
            get
            {
                if (m_mez == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 5393;
                    spell.Duration = 60;
                    spell.Description = "Mesmerize";
                    spell.Name = "Mesmerize";
                    spell.Range = 3500;
                    spell.Radius = 150;
                    spell.SpellID = 6003;
                    spell.Target = "Enemy";
                    spell.Type = "Mesmerize";
                    m_mez = new Spell(spell, 70);
                    spell.Message1 = "You cannot move!";
                    spell.Message2 = "{0} cannot seem to move!";
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_mez);
                }
                return m_mez;
            }
        }

        protected static Spell m_heal;
        public static Spell Heal
        {
            get
            {
                if (m_heal == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 1;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 13019;
                    spell.Description = "Heal";
                    spell.Name = "Summoner heal";
                    spell.SpellID = 6004;
                    spell.Value = 1500;
                    spell.Target = "Self";
                    spell.Type = "Heal";
                    m_heal = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_heal);
                }
                return m_heal;
            }
        }

        protected static Spell m_nuke;
        public static Spell Nuke
        {
            get
            {
                if (m_nuke == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 4559;
                    spell.Description = "Nuke";
                    spell.Name = "Nuke";
                    spell.Range = 900;
                    spell.Radius = 100;
                    spell.Damage = 1000;
                    spell.DamageType = (int)eDamageType.Heat;
                    spell.SpellID = 6000;
                    spell.Target = "Enemy";
                    spell.Type = "DirectDamage";
                    m_nuke = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_nuke);
                }
                return m_nuke;
            }
        }

        protected static Spell m_pb;
        public static Spell Pb
        {
            get
            {
                if (m_pb == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 3;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 12143;
                    spell.Description = "Nuke";
                    spell.Name = "Nuke";
                    spell.Range = 3500;
                    spell.Radius = 100;
                    spell.Damage = 200;
                    spell.DamageType = (int)eDamageType.Matter;
                    spell.SpellID = 6008;
                    spell.Target = "Enemy";
                    spell.Type = "DirectDamage";
                    m_pb = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_pb);
                }
                return m_pb;
            }
        }

        protected static Spell m_dot;
        public static Spell Dot
        {
            get
            {
                if (m_dot == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 92;
                    spell.Duration = 60;
                    spell.Description = "dot";
                    spell.Name = "bane";
                    spell.Range = 3000;
                    spell.Radius = 100;
                    spell.Damage = 900;
                    spell.DamageType = (int)eDamageType.Matter;
                    spell.SpellID = 6005;
                    spell.Target = "Enemy";
                    spell.Type = "DamageOverTime";
                    m_dot = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_dot);
                }
                return m_dot;
            }
        }

        protected static Spell m_shield;
        public static Spell Shield
        {
            get
            {
                if (m_shield == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 13510;
                    spell.Duration = 60;
                    spell.Description = "Damage shield";
                    spell.Name = "Summoners bane";
                    spell.Damage = 400;
                    spell.DamageType = (int)eDamageType.Matter;
                    spell.SpellID = 6006;
                    spell.Target = "Self";
                    spell.Type = "DamageShield";
                    m_shield = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_shield);
                }
                return m_shield;
            }
        }

        protected static Spell m_stun;
        public static Spell Stun
        {
            get
            {
                if (m_stun == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.AutoSave = false;
                    spell.CastTime = 0;
                    spell.Uninterruptible = true;
                    spell.ClientEffect = 80;
                    spell.Duration = 9;
                    spell.Description = "Stun";
                    spell.Name = "Stun";
                    spell.Range = 900;
                    spell.Radius = 100;
                    spell.DamageType = 13;
                    spell.SpellID = 6002;
                    spell.Target = "Enemy";
                    spell.Type = "Stun";
                    spell.Message1 = "You cannot move!";
                    spell.Message2 = "{0} cannot seem to move!";
                    m_stun = new Spell(spell, 70);
                    SkillBase.GetSpellList(GlobalSpellsLines.Mob_Spells).Add(m_stun);
                }
                return m_stun;
            }
        }
        #endregion
    }
}

namespace DOL.AI.Brain
{
    public class SummonerBrain : StandardMobBrain
    {
        public SummonerBrain()
            : base()
        {
            AggroLevel = 100;
            AggroRange = 500;
            ThinkInterval = 2000;
        }
        public override void Think()
        {
            Summoner summoner = Body as Summoner;
            //If at full HP we reset stages
            if (summoner.HealthPercent == 100 && summoner.stage != 0)
                summoner.stage = 0;
            //Stage 1 < 98%
            else if (summoner.HealthPercent < 99 && summoner.HealthPercent > 97 && summoner.stage == 0)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNuke), 5000);
                    summoner.stage = 1;
                }
            }

            else if (summoner.HealthPercent < 96 && summoner.HealthPercent > 94 && summoner.stage == 1)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 2;
                }
            }

            else if (summoner.HealthPercent < 94 && summoner.HealthPercent > 92 && summoner.stage == 2)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonMez), 5000);
                    summoner.stage = 3;
                }
            }

            else if (summoner.HealthPercent < 92 && summoner.HealthPercent > 90 && summoner.stage == 3)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonBlare), 5000);
                    summoner.stage = 4;
                }
            }

            else if (summoner.HealthPercent < 90 && summoner.HealthPercent > 88 && summoner.stage == 4)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 5;
                }
            }

            else if (summoner.HealthPercent < 88 && summoner.HealthPercent > 86 && summoner.stage == 5)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDotandStun), 5000);
                    summoner.stage = 6;
                }
            }

            else if (summoner.HealthPercent < 86 && summoner.HealthPercent > 84 && summoner.stage == 6)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonShield), 5000);
                    summoner.stage = 7;
                }
            }

            else if (summoner.HealthPercent < 84 && summoner.HealthPercent > 82 && summoner.stage == 7)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDot), 5000);
                    summoner.stage = 8;
                }
            }

            else if (summoner.HealthPercent < 82 && summoner.HealthPercent > 80 && summoner.stage == 8)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 9;
                }
            }

            else if (summoner.HealthPercent < 80 && summoner.HealthPercent > 78 && summoner.stage == 9)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonBlare), 5000);
                    summoner.stage = 10;
                }
            }

            else if (summoner.HealthPercent < 78 && summoner.HealthPercent > 76 && summoner.stage == 10)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonMez), 5000);
                    summoner.stage = 11;
                }
            }

            else if (summoner.HealthPercent < 76 && summoner.HealthPercent > 74 && summoner.stage == 11)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNuke), 5000);
                    summoner.stage = 12;
                }
            }

            else if (summoner.HealthPercent < 74 && summoner.HealthPercent > 72 && summoner.stage == 12)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 13;
                }
            }

            else if (summoner.HealthPercent < 72 && summoner.HealthPercent > 70 && summoner.stage == 13)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonBlare), 5000);
                    summoner.stage = 14;
                }
            }

            else if (summoner.HealthPercent < 70 && summoner.HealthPercent > 68 && summoner.stage == 14)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonMez), 5000);
                    summoner.stage = 15;
                }
            }

            else if (summoner.HealthPercent < 68 && summoner.HealthPercent > 65 && summoner.stage == 15)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 16;
                }
            }

            else if (summoner.HealthPercent < 52 && summoner.HealthPercent > 50 && summoner.stage == 16)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 17;
                }
            }

            else if (summoner.HealthPercent < 50 && summoner.HealthPercent > 48 && summoner.stage == 17)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonStun), 5000);
                    summoner.stage = 18;
                }
            }

            else if (summoner.HealthPercent < 48 && summoner.HealthPercent > 46 && summoner.stage == 18)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 19;
                }
            }

            else if (summoner.HealthPercent < 46 && summoner.HealthPercent > 44 && summoner.stage == 19)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonShield), 5000);
                    summoner.stage = 20;
                }
            }

            else if (summoner.HealthPercent < 44 && summoner.HealthPercent > 42 && summoner.stage == 20)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDotandStun), 5000);
                    summoner.stage = 21;
                }
            }

            else if (summoner.HealthPercent < 42 && summoner.HealthPercent > 41 && summoner.stage == 21)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDot), 5000);
                    summoner.stage = 22;
                }
            }

            else if (summoner.HealthPercent < 41 && summoner.HealthPercent > 40 && summoner.stage == 22)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDotandStun), 5000);
                    summoner.stage = 23;
                }
            }

            else if (summoner.HealthPercent < 40 && summoner.HealthPercent > 38 && summoner.stage == 23)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDot), 5000);
                    summoner.stage = 24;
                }
            }

            else if (summoner.HealthPercent < 38 && summoner.HealthPercent > 36 && summoner.stage == 24)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonMez), 5000);
                    summoner.stage = 25;
                }
            }

            else if (summoner.HealthPercent < 36 && summoner.HealthPercent > 34 && summoner.stage == 25)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonStun), 5000);
                    summoner.stage = 26;
                }
            }

            else if (summoner.HealthPercent < 34 && summoner.HealthPercent > 32 && summoner.stage == 26)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNuke), 5000);
                    summoner.stage = 27;
                }
            }

            else if (summoner.HealthPercent < 32 && summoner.HealthPercent > 29 && summoner.stage == 27)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonBlare), 5000);
                    summoner.stage = 28;
                }
            }

            else if (summoner.HealthPercent < 25 && summoner.HealthPercent > 20 && summoner.stage == 28)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonNukeandStun), 5000);
                    summoner.stage = 29;
                }
            }

            else if (summoner.HealthPercent < 20 && summoner.HealthPercent > 15 && summoner.stage == 29)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonShield), 5000);
                    summoner.stage = 30;
                }
            }

            else if (summoner.HealthPercent < 15 && summoner.HealthPercent > 13 && summoner.stage == 30)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonDotandStun), 5000);
                    summoner.stage = 31;
                }
            }

            else if (summoner.HealthPercent < 12 && summoner.HealthPercent > 10 && summoner.stage == 31)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonBlare), 5000);
                    summoner.stage = 32;
                }
            }

            else if (summoner.HealthPercent < 9 && summoner.HealthPercent > 6 && summoner.stage == 32)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonShield), 5000);
                    summoner.stage = 33;
                }
            }

            else if (summoner.HealthPercent < 5 && summoner.stage == 33)
            {
                summoner.SummonerTarget = CalculateNextAttackTarget();
                if (summoner.SummonerTarget != null)
                {
                    new RegionTimer(summoner, new RegionTimerCallback(summoner.DragonPb), 5000);
                    summoner.stage = 34;
                }
            }
            base.Think();
        }
    }
}

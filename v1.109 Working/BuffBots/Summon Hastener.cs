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
        "&hastener",
        ePrivLevel.Player, // Set to player.
        "/hastener - To Summon a Hasteer for the cost of 50bps")]

    public class summonhastenerHandler : AbstractCommandHandler, ICommandHandler
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
            if (client.Player.BountyPoints >= 30) // how many bps are need to summon the buffbot
            {
                
                SpellLine line = new SpellLine("HastenerCast", "Hastener Cast", "unknown", false);
                    ISpellHandler spellHandler = ScriptMgr.CreateSpellHandler(client.Player, HastenerSpell, line);
                    if (spellHandler != null)
                        spellHandler.StartSpell(client.Player);
                    client.Player.RemoveBountyPoints(50); // removes the amount of bps from the player
                    client.Player.Out.SendMessage("You have summoned a Hastener!", eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                    client.Player.SaveIntoDatabase(); // saves new amount of bps
                    client.Player.Out.SendUpdatePlayer(); // updates players bp
               
            }
             #endregion command spell loader
            else client.Player.Out.SendMessage("You don't have enough Bounty Pounts to summon a Buffbot!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
        }



        [ScriptLoadedEvent]
        public static void OnScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            Spell load;
            load = HastenerSpell;
        }

        #region Spell
        protected static Spell m_hastenerSpell;
        public static Spell HastenerSpell
        {
            get
            {
                if (m_hastenerSpell == null)
                {
                    DBSpell spell = new DBSpell();
                    spell.CastTime = 0;
                    spell.ClientEffect = 0;
                    spell.Duration = 30;
                    spell.Description = "Summons a Hastener to your location for " + spell.Duration + " seconds.";
                    spell.Name = "Hastener Spell";
                    spell.Type = "Summon A Hastener";
                    spell.Range = 0;
                    spell.SpellID = 123535;
                    spell.Target = "Self";
                    spell.Value = HastenerTemplate.TemplateId;
                    m_hastenerSpell = new Spell(spell, 1);
                    SkillBase.GetSpellList(GlobalSpellsLines.Item_Effects).Add(m_hastenerSpell);
                }
                return m_hastenerSpell;
            }
        }
        #endregion

        #region Npc
        protected static NpcTemplate m_hastenerTemplate;
        public static NpcTemplate HastenerTemplate
        {
            get
            {
                if (m_hastenerTemplate == null)
                {
                    m_hastenerTemplate = new NpcTemplate();
                    m_hastenerTemplate.Flags += (byte)GameNPC.eFlags.GHOST;
                    m_hastenerTemplate.Name = "Hastener";
                    m_hastenerTemplate.ClassType = "DOL.GS.GameHastener";
                    m_hastenerTemplate.Model = "50";
                    m_hastenerTemplate.TemplateId = 11199;
                    NpcTemplateMgr.AddTemplate(m_hastenerTemplate);
                }
                return m_hastenerTemplate;
            }
        }
        #endregion

    }
}


#region Summon 

namespace DOL.GS.Spells
{
    [SpellHandlerAttribute("Summon A Hastener")]
    public class SummonAHastenerSpellHandler : SpellHandler
    {
        public SummonAHastenerSpellHandler(GameLiving caster, Spell spell, SpellLine line)
            : base(caster, spell, line)
        { }

        protected GameNPC npc = null;

        public override void ApplyEffectOnTarget(GameLiving target, double effectiveness)
        {
            NpcTemplate template = NpcTemplateMgr.GetTemplate((int)Spell.Value);

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
            Caster.GetSpotFromHeading(64, out x, out y);
            npc.X = x;
            npc.Y = y;
            npc.Z = Caster.Z;
            npc.CurrentRegion = Caster.CurrentRegion;
            npc.Heading = (ushort)((Caster.Heading + 2048) % 4096);
            npc.Realm = Caster.Realm;
            npc.CurrentSpeed = 0;
            npc.Level = 1;
            npc.SetOwnBrain(new AI.Brain.BlankBrain());
            npc.AddToWorld();
            effect.Start(npc);
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
using System;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using log4net;
using DOL;
using DOL.AI.Brain;
using DOL.GS.Scripts;
using DOL.GS.GameEvents;
using DOL.GS.Quests;
using DOL.GS.Spells;
using DOL.GS.Effects;
using DOL.Database;
using System.Reflection;

namespace DOL.GS.Scripts
{
    public class MagMellSetupTeleporter : GameNPC
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            Model = 2026;
            Name = "ZONE TELEPORTER";
            GuildName = "Zone Teleporter";
            Level = 50;
            Size = 60;
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

        private static ServerProperty curMap = GameServer.Database.SelectObject(typeof(ServerProperty), "`Key` = 'current_map'") as ServerProperty;

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            TurnTo(player.X, player.Y);
            player.Out.SendMessage("Hello " + player.Name + "!  I am the Arbiter and I can teleport you to the following locations\n\n[Main Setup]\n\n" +
            "[PvP Setup]\n\n[Gjalpinulva]\n\n[Master Level Trials]", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            return true;
        }
        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer t = (GamePlayer)source;
            TurnTo(t.X, t.Y);
            switch (str)
            {
                case "PvP Setup":
                    if (!t.InCombat)
                    {
                        //  if (curMap.Value == "Aegir's Landing PvP")
                        // {
                        //Say("I'm now teleporting you to the current PvP Setup area");
                        //t.MoveTo(151, 255443, 316099, 4048, 2194);
                        // }
                        /* else if (curMap.Value == "Knarr PvP")
                         {
                             Say("I'm now teleporting you to the current PvP Setup area");
                             t.MoveTo(151, 348551, 433572, 3712, 3338);
                         }
                         else if (curMap.Value == "Gothwaite PvP")
                         {
                             Say("I'm now teleporting you to the current PvP Setup area");
                             t.MoveTo(51, 526034, 505253, 3424, 1549);
                         }*/
                       // if (curMap.Value == "Mag Mell PvP")
                         {
                             Say("I'm now teleporting you to the current PvP Setup area");
                             t.MoveTo(200, 296554, 454088, 7139, 1101);
                         }
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Main Setup":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to the Main Setup area");
                        t.MoveTo(70, 569762, 538694, 6104, 3268);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Gjalpinulva":
                    if (!t.InCombat)

                    //if (t.Group.MemberCount >= 4) //You have enough
                    {
                        Say("I'm now teleporting you to Gjalpinulva's Lair");
                        t.MoveTo(100, 694102, 996417, 2861, 935);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                //else if (t.Group.MemberCount <= 3) //You dont have enough
                //t.Out.SendMessage("You need a group of at least 4 adventurers for this encounter!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                //break;
                case "Master Level Trials":
                    SendReply(t, "Which trial would you like to take?\n\n[Cetus]\n\n[Runihura]\n\n[Medussa]\n\n[Martikhoras]\n\n[Ammut]\n\n[Chimera]\n\n[Typhon]\n\n[Talos]\n\n[Phoenix]\n\n[Draco]");
                    break;

                case "Cetus":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Cetus");
                        t.MoveTo(78, 30258, 36507, 17005, 2305);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Runihura":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Runihura");
                        t.MoveTo(73, 335638, 464204, 10727, 8);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Medussa":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Medussa");
                        t.MoveTo(80, 68870, 21562, 18146, 2014);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Martikhoras":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Manticore");
                        t.MoveTo(88, 31982, 30892, 16300, 4091);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Ammut":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Ammut");
                        t.MoveTo(83, 44901, 36638, 15656, 3074);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Chimera":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Chimera");
                        t.MoveTo(73, 548432, 561198, 10672, 2445);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Typhon":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Typhon");
                        t.MoveTo(89, 45990, 22136, 14876, 3073);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Talos":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Talos");
                        t.MoveTo(73, 477955, 695019, 16156, 2576);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Phoenix":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to the Phoenix");
                        t.MoveTo(90, 34885, 37445, 19063, 2054);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Draco":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Draco");
                        t.MoveTo(91, 31886, 34425, 15763, 2046);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                default: break;
            }
            return true;
        }
        private void SendReply(GamePlayer target, string msg)
        {
            target.Client.Out.SendMessage(
                msg,
                eChatType.CT_Say, eChatLoc.CL_PopupWindow);
        }
        [ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            log.Info("\tTeleporter initialized: true");
        }
    }
}
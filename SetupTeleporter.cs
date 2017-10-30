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
    public class SetupTeleporter : GameNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            switch (Realm)
            {
                case eRealm.Albion:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2230); break;
                case eRealm.Midgard:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2232);
                    template.AddNPCEquipment(eInventorySlot.ArmsArmor, 2233);
                    template.AddNPCEquipment(eInventorySlot.LegsArmor, 2234);
                    template.AddNPCEquipment(eInventorySlot.HandsArmor, 2235);
                    template.AddNPCEquipment(eInventorySlot.FeetArmor, 2236);
                    break;
                case eRealm.Hibernia:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2231); ; break;
            }

            Inventory = template.CloseTemplate();
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

        private static ServerProperty curMap = GameServer.Database.SelectObject(typeof(ServerProperty), "`Key` = 'current_map'") as ServerProperty;

		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player)) return false;
			TurnTo(player.X,player.Y);
			player.Out.SendMessage("Hello "+player.Name+"! Would you like to return to the main [Setup]\n" +
            "enter the current [PvP] area, or challenge [Gjalpinulva] the Dragon?", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			return true;
		}
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer t = (GamePlayer) source;
			TurnTo(t.X,t.Y);
			switch(str)
			{
                case "PvP":
                    if (!t.InCombat)
                    {
                        if (curMap.Value == "Aegir's Landing PvP")
                        {
                            Say("I'm now teleporting you to the current PvP area");
                            t.MoveTo(151, 255443, 316099, 4048, 2194);
                        }
                        else if (curMap.Value == "Knarr PvP")
                        {
                            Say("I'm now teleporting you to the current PvP area");
                            t.MoveTo(151, 348551, 433572, 3712, 3338);
                        }
                        else if (curMap.Value == "Gothwaite PvP")
                        {
                            Say("I'm now teleporting you to the current PvP area");
                            t.MoveTo(51, 526034, 505253, 3424, 1549);
                        }
                        else if (curMap.Value == "Mag Mell PvP")
                        {
                            Say("I'm now teleporting you to the current PvP area");
                            t.MoveTo(200, 296554, 454088, 7139, 1101);
                        }
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Setup":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to Setup");
                        t.MoveTo(70, 569762, 538694, 6104, 3268);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "Gjalpinulva":

                    //if (t.Group.MemberCount >= 4) //You have enough
                    {
                        Say("I'm now teleporting you to the Dragon Gjalpinulva");
                        t.MoveTo(100, 694102, 996417, 2861, 935);
                        break;
                    }
                    //else if (t.Group.MemberCount <= 3) //You dont have enough
                    //t.Out.SendMessage("You need a group of at least 4 adventurers for this encounter!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    //break;

                default: break;
			}
			return true;
		}
		private void SendReply(GamePlayer target, string msg)
			{
				target.Client.Out.SendMessage(
					msg,
					eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			}
		[ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            log.Info("\tTeleporter initialized: true");
        }	
}
}
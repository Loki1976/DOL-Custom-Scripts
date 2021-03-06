using System;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using log4net;
using System.Reflection;

namespace DOL.GS.Scripts
{
    public class KnarrTeleporter : GameNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            Model = 2026;
            Name = "PvP TELEPORTER";
            GuildName = "PvP Teleporter";
            Level = 50;
            Size = 60;
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }
		public override bool Interact(GamePlayer player)
		{
            if (!base.Interact(player)) return false;
            TurnTo(player.X, player.Y);
            player.Out.SendMessage("Hello " + player.Name + "! Would you like to port to [PvP] or return to the [Main Setup]?", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
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
                case "Main Setup":
                    if (!t.InCombat)
                    {
                        Say("I'm now teleporting you to the Main Setup area");
                        t.MoveTo(70, 569762, 538694, 6104, 3268);
                    }
                    else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                    break;

                case "PvP":
                    if (!t.InCombat)
                    {
                        int RandPvP = Util.Random(1, 5);//Creates a random number between 1 and 5
                        if (RandPvP == 1)
                        {// send you to  the gloc below if number 1 comes up random
                            t.MoveTo(151, 307908, 433929, 3142, 1038);
                        }
                        else if (RandPvP == 2)
                        {
                            t.MoveTo(151, 303563, 434101, 3192, 1035);
                        }
                        else if (RandPvP == 3)
                        {
                            t.MoveTo(151, 301188, 435035, 3383, 21);
                        }
                        else if (RandPvP == 4)
                        {
                            t.MoveTo(151, 305734, 430108, 3267, 1055);
                        }
                        else if (RandPvP == 5)
                        {
                            t.MoveTo(151, 301478, 430009, 3315, 2971);
                        }
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
					eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			}
		[ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            log.Info("\tTeleporter initialized: true");
        }	
    }
	
}
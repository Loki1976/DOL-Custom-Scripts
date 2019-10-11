using System;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using log4net;
using System.Reflection;

namespace DOL.GS.Scripts
{
	public class DFTeleporter: GameNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            Name = "BP Farm";
			GuildName = "Teleporter";
            Model = 881;
            Size = 37;
            Level = 50;
            Flags |= eFlags.PEACE;
			
			return base.AddToWorld();
        }
		
		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player)) return false;
			//TurnTo(player.X,player.Y);
           player.Out.SendMessage("Hello " + player.Name + ", You can currently be translocated to your [BPFarm Zone].  Number of Players Currently In your BPFarm Zone = " + WorldMgr.GetClientsOfRegionCount(249) + " ", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
			return true;
		}
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer t = (GamePlayer) source;
			//TurnTo(t.X,t.Y);
			switch(str)
			{
 
                case "BPFarm Zone":
                    if (!t.InCombat)
                    {
                        int RandPvP = Util.Random(1, 3);//Creates a random number between 1 and 3
                        if (RandPvP == 1)
                        {// send you to  the gloc below if number 1 comes up random
                            t.MoveTo(249, 31670, 27908, 22893, 3058);
                        }
                        else if (RandPvP == 2)
                        {
                            t.MoveTo(249, 46385, 40298, 21357, 2033);
                        }
                        else if (RandPvP == 3)
                        {
                            t.MoveTo(249, 18093, 18681, 22893, 1024);
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
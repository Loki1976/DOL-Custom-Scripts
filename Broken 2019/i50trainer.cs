
using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;
using DOL.Database;
using log4net;


namespace DOL.GS.Trainer
{
    public class I50 : GameTrainer
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            if (log.IsInfoEnabled)
                log.Info("Master Trainer Initializing...");

        }


        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

                TurnTo(player, 250);
 
			bool bTest=false;
 			if (player.Name.Length >= 4)
            {
                if (player.Name.Substring(0, 4).ToLower() == "test")
                {
					bTest=true;
				}
			}
			
			return true;
			
			if (bTest == false) 
			{
				player.Out.SendMessage("Only test character can use instant level 50 !", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				player.Out.SendMessage("The name of test character should start with 'test'.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				player.Out.SendMessage("Make test character such as 'testaaa', 'testbbb' ...", eChatType.CT_System, eChatLoc.CL_SystemWindow);				
				return false;
			}

				
            if (player.Level < 50)
            {
                player.GainExperience(GameLiving.eXPSource.Other, 169999999950);
            }
			else
				player.Out.SendMessage("You are already level 50.", eChatType.CT_System, eChatLoc.CL_SystemWindow);

                player.Out.SendUpdatePlayer();
                player.Out.SendUpdatePoints();
                player.UpdatePlayerStatus();
                player.Health = player.MaxHealth;
                player.Endurance = player.MaxEndurance;
                player.Mana = player.MaxMana;

            return true;
			
        }


    }
}

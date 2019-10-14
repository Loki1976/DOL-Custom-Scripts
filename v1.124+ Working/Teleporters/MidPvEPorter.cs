using System;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using DOL.Events;
using DOL.Database;
using DOL.Database.Attributes;
using DOL.AI.Brain;
using DOL.GS.SkillHandler;
using DOL.GS;
using DOL.GS.Scripts;
using DOL.GS.PacketHandler;
using DOL.GS.Spells;
using DOL.GS.Effects;

using log4net;

namespace DOL.GS.Scripts
{
    public class MidPvEPorter : GameNPC
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            this.Level = 50;
            this.Name = "Zerta";
			Flags |= GameNPC.eFlags.PEACE;
            base.AddToWorld();
            return true;
        }
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            TurnTo(player.X, player.Y);
            player.Out.SendMessage("Hello " + player.Name + ", You can currently be translocated to the [Nisse's Lair], [Vendo Caverns], [Varulvhamn] or [Spindelhalla] .", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            player.Bind(true);

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
				
				case "Nisse's Lair":
				
				if (!t.InCombat)
				{	
                    SendReply(t, "I'm now translocating you to Nisse's Lair!");
                    t.MoveTo(129, 34660, 33197, 16464, 1104);
				}
					else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }

                    break;
				
				case "Vendo Caverns":
				
				if (!t.InCombat)
				{
				
					SendReply(t, "I'm now translocating you to Vendo Caverns!");
                    t.MoveTo(126, 32783, 33088, 16618, 2059);
					
				}
					else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }

                    break;
					
				case "Varulvhamn":
				
				if (!t.InCombat)
				{
				
                    SendReply(t, "I'm now translocating you to Varulvhamn!");
                    t.MoveTo(127, 35134, 30850, 14995, 1033);

}
				else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }

                    break;
					
				case "Spindelhalla":
				
				if (!t.InCombat)
				{
				
					SendReply(t, "I'm now translocating you to Spindelhalla!");
                    t.MoveTo(125, 32467, 31872, 16000, 3);
					
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
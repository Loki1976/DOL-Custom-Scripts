/* Powered by Krusck
* for Dawn of Light 
* Rewritten in data 05/06/09
* Work whit 1695 SVN
* */
using System;
using System.Collections.Generic;
using System.Text;
using DOL.GS;
using DOL.Database;
using DOL.GS.PacketHandler;

namespace DOL.GS.Commands
{
    [CmdAttribute(
            "&bomb",
            ePrivLevel.GM,
            "'/bomb kill/delete' - This Area are Deleted!")]
    public class AtomicaCommandHandler : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            if (args.Length < 2)
            {
                client.Out.SendMessage("Use: /bomb kill", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Out.SendMessage("Use: /bomb delete", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            if (args[1] == "kill")
            {
                foreach (GameNPC coso in client.Player.GetNPCsInRadius(2000)) //set your range 
                {
                    coso.Delete();
                }
                client.Out.SendMessage("This area has been Killed", DOL.GS.PacketHandler.eChatType.CT_Important, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            }
            if (args[1] == "delete")
            {
                foreach (GameNPC coso in client.Player.GetNPCsInRadius(2000)) //set your range 
                {
                    coso.Delete();
                }
                client.Out.SendMessage("This area has been Deleted", DOL.GS.PacketHandler.eChatType.CT_Important, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            }
        }
    }
}
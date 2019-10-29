/* Powered by Krusck
* for Server: Apocalipse PvP
* in data 05/06/09
* Supported by Dawn of Light
* Work Whit 1695 SVN 
*/


using System;
using System.Collections;
using DOL.GS;
using DOL.Database;
using DOL.GS.PacketHandler;

namespace DOL.GS.Commands
{
    [CmdAttribute("&move", //command to handle
         ePrivLevel.GM, //minimum privelege level
         "Teleports all players from a regionID to their Home Village", //command description
        //Usage
         "/move <regionID>")]


    public class Move : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            if (args.Length < 2)
            {
                client.Out.SendMessage("Use: /move <regionID>", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }

            ushort from_region = Convert.ToByte(args[1]);

            foreach (GameClient cl in WorldMgr.GetClientsOfRegion(from_region))
            {
                if (cl.Player.Realm == eRealm.Albion)
                {
                    cl.Player.MoveTo(1, 560421, 511840, 2344, 1);  //EDIT THIS line WHIT YOUR LOC want to be teleport
                    cl.Player.SaveIntoDatabase();
                    client.Out.SendMessage(cl.Player.Name + "", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (cl.Player.Realm == eRealm.Midgard)
                {
                    cl.Player.MoveTo(100, 804763, 723998, 4699, 1); //EDIT THIS LINE WHIT YOUR LOC want to be teleport
                    cl.Player.SaveIntoDatabase();
                    client.Out.SendMessage(cl.Player.Name + "", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (cl.Player.Realm == eRealm.Hibernia)
                {
                    cl.Player.MoveTo(200, 345684, 490996, 5200, 1); //EDIT THIS LINE WHIT YOUR LOC want to be teleport
                    cl.Player.SaveIntoDatabase();
                    client.Out.SendMessage(cl.Player.Name + "", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else
                {
                    client.Out.SendMessage(cl.Player.Name + "", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    return;
                }
            }
        }
    }
}
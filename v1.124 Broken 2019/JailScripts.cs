/* Powered By Krusck 
* for Apocalipse PvP 
* Supported by Dawn of Light
* in data 05/06/09
* work whit 1695 SVN
* */

using System;
using System.Collections;
using DOL.GS;
using DOL.Database;
using DOL.GS.PacketHandler;

namespace DOL.GS.Commands
{
    [CmdAttribute("&jail", // Command to handle
        ePrivLevel.GM, // Minimum privelege level
        "Teleports a player or yourself to the JAIL location", // Command description
        "/jail target", // Usages...
        "/jail player <PlayerName>",
        "/jail remove <PlayerName>")]

    public class OnJail : AbstractCommandHandler, ICommandHandler
    {
        private DataBase.Character GetPlayerFromDB(string Pname)
        {
            DataObject[] m_objs = GameServer.Database.SelectObjects(typeof(Character), "Name = '" + GameServer.Database.Escape(Pname) + "'");
            foreach (DataObject ob in m_objs) { return (Character)ob; }
            return null;
        }

        public void OnCommand(GameClient client, string[] args)
        {
            /*GameLocation jailLocation = PromiseUtils.GetJailLocation();
            if (jailLocation == null)
                return;*/
            if (args.Length < 2)
            {
                client.Out.SendMessage("Use: /jail target", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Out.SendMessage("Use: /jail player <PlayerName>", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Out.SendMessage("Use: /jail remove <PlayerName>", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            if (args[1] == "target")
            {
                if (client.Player.TargetObject is GamePlayer)
                {
                    GamePlayer target = (GamePlayer)client.Player.TargetObject;
                    target.MoveTo(249, 47382, 49715, 25001, 1); //jail room
                    target.Bind(true);
                    target.SaveIntoDatabase();
                    target.Out.SendMessage("Your character is jailed!", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    client.Out.SendMessage(target.Name + " jumped in the jail room", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    return;
                }
                else
                    return;
            }

            if (args[1] == "player")
            {
                if (args.Length < 2)
                {
                    client.Out.SendMessage("Invalid command!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    return;
                }
                string PlayerName = args[2];
                GameClient cl = WorldMgr.GetClientByPlayerName(PlayerName, false, false);

                if (cl != null) //client online
                {
                    if (cl.Player.Name == PlayerName)
                    {
                        cl.Player.MoveTo(249, 47382, 49715, 25001, 1);
                        cl.Player.Bind(true);
                        cl.Player.SaveIntoDatabase();
                        client.Out.SendMessage(cl.Player.Name + " jumped in the jail room", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        return;
                    }
                    else //sta giocando con un altro giocatore?
                    {
                        foreach (Character dbpg in cl.Account.Characters)
                        {
                            if (dbpg.Name == PlayerName)
                            {
                                dbpg.BindXpos = 47382;
                                dbpg.BindYpos = 49715;
                                dbpg.BindZpos = 25001;
                                dbpg.BindRegion = 249;
                                dbpg.BindHeading = 1;
                                dbpg.Xpos = 47382;
                                dbpg.Ypos = 49715;
                                dbpg.Zpos = 25001;
                                dbpg.Region = 249;
                                dbpg.Direction = 1;
                                GameServer.Database.SaveObject(dbpg);
                                client.Out.SendMessage("This player is ingame with another character: " + cl.Player.Name + "!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                return;
                            }
                        }
                    }
                }
                else //non è il game, controllo il database
                {
                    Character dbpg = GetPlayerFromDB(PlayerName);
                    if (dbpg != null)
                    {
                        dbpg.BindXpos = 47382;
                        dbpg.BindYpos = 49715;
                        dbpg.BindZpos = 25001;
                        dbpg.BindRegion = 249;
                        dbpg.BindHeading = 1;
                        dbpg.Xpos = 47382;
                        dbpg.Ypos = 49715;
                        dbpg.Zpos = 25001;
                        dbpg.Region = 249;
                        dbpg.Direction = 1;
                        GameServer.Database.SaveObject(dbpg);
                        client.Out.SendMessage("This character isn't ingame!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        return;
                    }
                    else
                    {
                        client.Out.SendMessage("This character doesn't exist on DataBase!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        return;
                    }
                }
            }

            if (args[1] == "remove")
            {
                if (args.Length < 2)
                {
                    client.Out.SendMessage("Invalid command!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    return;
                }
                string PlayerName = args[2];
                GameClient cl = WorldMgr.GetClientByPlayerName(PlayerName, false, false);

                if (cl != null) //client online
                {
                    if (cl.Player.Name == PlayerName)
                    {
                        if (cl.Player.Realm == eRealm.Albion)
                        {
                            cl.Player.MoveTo(1, 560421, 511840, 2344, 1);
                            cl.Player.Bind(true);
                            cl.Player.SaveIntoDatabase();
                            client.Out.SendMessage(cl.Player.Name + " jumped in Cotswold!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                        else if (cl.Player.Realm == eRealm.Midgard)
                        {
                            cl.Player.MoveTo(100, 804763, 723998, 4699, 1);
                            cl.Player.Bind(true);
                            cl.Player.SaveIntoDatabase();
                            client.Out.SendMessage(cl.Player.Name + " jumped in Mularn!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                        else if (cl.Player.Realm == eRealm.Hibernia)
                        {
                            cl.Player.MoveTo(200, 345684, 490996, 5200, 1);
                            cl.Player.Bind(true);
                            cl.Player.SaveIntoDatabase();
                            client.Out.SendMessage(cl.Player.Name + " jumped in MagMell!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                        else
                        {
                            client.Out.SendMessage(cl.Player.Name + " has not a realm!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                    }
                    else //sta giocando con un altro giocatore?
                    {
                        foreach (Character dbpg in cl.Account.Characters)
                        {
                            if (dbpg.Name == PlayerName)
                            {
                                if (dbpg.Realm == 1)
                                {
                                    dbpg.BindXpos = 560421;
                                    dbpg.BindYpos = 511840;
                                    dbpg.BindZpos = 2344;
                                    dbpg.BindRegion = 1;
                                    dbpg.BindHeading = 1;
                                    dbpg.Xpos = 560421;
                                    dbpg.Ypos = 511840;
                                    dbpg.Zpos = 2344;
                                    dbpg.Region = 1;
                                    dbpg.Direction = 1;
                                    GameServer.Database.SaveObject(dbpg);
                                    client.Out.SendMessage("This player is ingame with another character: " + cl.Player.Name + "!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    return;
                                }
                                if (dbpg.Realm == 2)
                                {
                                    dbpg.BindXpos = 804763;
                                    dbpg.BindYpos = 723998;
                                    dbpg.BindZpos = 4699;
                                    dbpg.BindRegion = 100;
                                    dbpg.BindHeading = 1;
                                    dbpg.Xpos = 804763;
                                    dbpg.Ypos = 723998;
                                    dbpg.Zpos = 4699;
                                    dbpg.Region = 100;
                                    dbpg.Direction = 1;
                                    GameServer.Database.SaveObject(dbpg);
                                    client.Out.SendMessage("This player is ingame with another character: " + cl.Player.Name + "!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    return;
                                }
                                if (dbpg.Realm == 3)
                                {
                                    dbpg.BindXpos = 345684;
                                    dbpg.BindYpos = 490996;
                                    dbpg.BindZpos = 5200;
                                    dbpg.BindRegion = 1;
                                    dbpg.BindHeading = 1;
                                    dbpg.Xpos = 345684;
                                    dbpg.Ypos = 490996;
                                    dbpg.Zpos = 5200;
                                    dbpg.Region = 1;
                                    dbpg.Direction = 1;
                                    GameServer.Database.SaveObject(dbpg);
                                    client.Out.SendMessage("This player is ingame with another character: " + cl.Player.Name + "!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                                    return;
                                }

                            }
                        }
                    }
                }
                else //non è il game, controllo il database
                {
                    Character dbpg = GetPlayerFromDB(PlayerName);
                    if (dbpg != null)
                    {
                        if (dbpg.Realm == 1)
                        {
                            dbpg.BindXpos = 560421;
                            dbpg.BindYpos = 511840;
                            dbpg.BindZpos = 2344;
                            dbpg.BindRegion = 1;
                            dbpg.BindHeading = 1;
                            dbpg.Xpos = 560421;
                            dbpg.Ypos = 511840;
                            dbpg.Zpos = 2344;
                            dbpg.Region = 1;
                            dbpg.Direction = 1;
                            GameServer.Database.SaveObject(dbpg);
                            client.Out.SendMessage("This character isn't online!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                        if (dbpg.Realm == 2)
                        {
                            dbpg.BindXpos = 804763;
                            dbpg.BindYpos = 723998;
                            dbpg.BindZpos = 4699;
                            dbpg.BindRegion = 100;
                            dbpg.BindHeading = 1;
                            dbpg.Xpos = 804763;
                            dbpg.Ypos = 723998;
                            dbpg.Zpos = 4699;
                            dbpg.Region = 100;
                            dbpg.Direction = 1;
                            GameServer.Database.SaveObject(dbpg);
                            client.Out.SendMessage("This character isn't online!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                        if (dbpg.Realm == 3)
                        {
                            dbpg.BindXpos = 345684;
                            dbpg.BindYpos = 490996;
                            dbpg.BindZpos = 5200;
                            dbpg.BindRegion = 1;
                            dbpg.BindHeading = 1;
                            dbpg.Xpos = 345684;
                            dbpg.Ypos = 490996;
                            dbpg.Zpos = 5200;
                            dbpg.Region = 1;
                            dbpg.Direction = 1;
                            GameServer.Database.SaveObject(dbpg);
                            client.Out.SendMessage("This character isn't online!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            client.Out.SendMessage(dbpg.Name + " jumped in Database", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            return;
                        }
                    }
                    else
                    {
                        client.Out.SendMessage("This character doesn't exist on DataBase!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        return;
                    }
                }
            }
        }
    }
}
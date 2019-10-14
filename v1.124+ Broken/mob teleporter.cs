/*
Author : Dams33
Date : May 2007
Description : Mob that allow to change item model by whisper
*/

using DOL;
using DOL.GS;
using DOL.Events;
using DOL.Database;
using System;
using DOL.GS.PacketHandler;
using DOL.AI.Brain;
using DOL.GS.Spells;
using System;
using System.Collections.Generic;


namespace DOL.GS.Scripts
{
    public class Teleporter : GameNPC
    {
        string msg = "Hello, where do you wish to port to? \n";
        public override bool Interact(GamePlayer player)
        {
            if (base.Interact(player))
            {
                ClearChat(player);
                if (player.KillsLegion == 100)
                {
                    if (player.RealmLevel > 90)
                    {
                        player.RespecRealm();
                        player.RealmPoints = 0;
                        player.RealmSpecialtyPoints = 0;
                        player.RealmLevel = 0;
                        player.GainRealmPoints(5974125, false);
                        /*
                        if (player.RealmSpecialtyPoints < 10)
                        {
                            player.RealmSpecialtyPoints = 90;
                        }
                        if (player.RealmLevel < 10)
                        {
                            player.RealmLevel = 90;
                        }
                         */
                        player.Out.SendMessage("Greetings, we have reset everyones realm rank to RR10 if you was above realm rank 10, this should not happen again as the RP rate has been lowered drastically", eChatType.CT_Spell, eChatLoc.CL_ChatWindow);
                        player.KillsLegion = 0;
                        player.SaveIntoDatabase();
                    }
                }

                List<GameNPC> npcs = WorldMgr.GetNPCsByGuild(this.GuildName, eRealm.None);
                foreach (GameNPC port in npcs)
                {
                    if (port.Name != this.Name)
                    {
                        msg += "[" + port.Name + "]\n";
                    }
                }
                SayTo(player, msg);
            }
            msg = "Hello, where do you wish to port to? \n";
            return true;

        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer player = (GamePlayer)source;
            TurnTo(player.X, player.Y);
            ClearChat(player);
            if (player.InCombat)
            {
                return false;
            }
            List<GameNPC> npcs = WorldMgr.GetNPCsByGuild(str, eRealm.None);
            if (npcs.Count != 0)
            {
                foreach (GameNPC portto in npcs)
                {
                    msg += "[" + portto.Name + "]\n";
                }
                SayTo(player, msg);
            }
            else
            {
                GameNPC[] npctoportto = WorldMgr.GetNPCsByName(str, eRealm.None);
                GameNPC portnow = null;
                foreach (GameNPC np in npctoportto)
                {
                    if (np.GuildName == this.GuildName)
                    {
                        portnow = np;
                    }
                }
                if (portnow != null)
                {
                    if (player.Group != null)
                    {
                        foreach (GamePlayer grupee in player.Group.GetPlayersInTheGroup())
                        {
                            grupee.Out.SendMessage(player.Name + " has ported to " + portnow.Name, eChatType.CT_Group, eChatLoc.CL_ChatWindow);
                        }
                    }
                    player.MoveTo(portnow.CurrentRegionID, portnow.X, portnow.Y, portnow.Z, portnow.Heading);
                    npcs = null;
                    return true;
                }
            }
            msg = "";


            return true;
        }



        public void SendReply(GamePlayer player, string msg)
        {
            player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }
        public void ClearChat(GamePlayer player)
        {
            GameObject obj = player.TargetObject;
            player.Out.SendChangeTarget(player);
            player.Out.SendMessage("", eChatType.CT_System, eChatLoc.CL_PopupWindow);
            player.Out.SendChangeTarget(obj);
        }



    }
}
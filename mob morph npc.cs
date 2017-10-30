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
    public class MobMorphNPC : GameNPC
    {
        string msg = "Hello, which morph do you wish to morph in to? \n";
        //List<GameNPC> cachedMorphList = new List<GameNPC>();
        Dictionary<int, List<string>> cachedMorphList = null;

        public override bool Interact(GamePlayer player)
        {
            if (base.Interact(player))
            {
                msg = "Hello, which morph do you wish to morph in to? \n";
                ClearChat(player);

                if (cachedMorphList == null)
                {
                    cachedMorphList = new Dictionary<int, List<string>>(); // RR required and list of mob names

                    List<GameNPC> morphs = WorldMgr.GetNPCsByGuild("Morphs", eRealm.None);
                    foreach (GameNPC morph in morphs)
                    {
                        List<string> names = new List<string>();
                        if (cachedMorphList.ContainsKey(morph.Level))
                        {
                            names = cachedMorphList[morph.Level];
                            cachedMorphList.Remove(morph.Level);
                        }
                        names.Add(morph.Name);

                       // int rr = ((int)morph.Level) + 10;
                        int rr = morph.Level;
                        cachedMorphList.Add(morph.Level, names);
                    }
                }
                foreach (KeyValuePair<int, List<string>> kvp in cachedMorphList)
                {
                    if (player.RealmLevel >= kvp.Key)
                    {
                        //130 = 130 / 10 = 13
                        //70 = 70 / 10 = 7..

                        //135 = 135 % 10 = 5.. 135 - 5 = 130.. / 10 = 13

                        //76 = 76 % 10  = 6... 76 - 66 = 70 / 10 = 7
                        string RR;
                        int partTwo = (kvp.Key % 10);

                        int partOne = (kvp.Key - partTwo) / 10;

                        RR = partOne.ToString() + "L" + partTwo.ToString();
                        msg += "[RR" +  RR + "]" + "\n";
                    }
                }
                SendReply(player, msg);
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
            msg = "Hello, which morph do you wish to morph in to? \n";
            if (str == "Reload")
            {
                cachedMorphList = new Dictionary<int, List<string>>(); // RR required and list of mob names

                List<GameNPC> morphs = WorldMgr.GetNPCsByGuild("Morphs", eRealm.None);
                foreach (GameNPC morph in morphs)
                {
                    List<string> names = new List<string>();
                    if (cachedMorphList.ContainsKey(morph.Level))
                    {
                        names = cachedMorphList[morph.Level];
                        cachedMorphList.Remove(morph.Level);
                    }
                    names.Add(morph.Name);

                    cachedMorphList.Add(morph.Level, names);
                }
            }
            if (str.Contains("RR"))
            {
                int rr = int.Parse(str.Replace("RR", "").Replace("L", ""));
                if (cachedMorphList.ContainsKey(rr))
                {
                    List<string> npcs = cachedMorphList[rr];
                    foreach (string mobName in npcs)
                    {
                        msg += "[" + mobName + "]\n";
                    }
                    SendReply(player, msg);
                }
            }
            else
            {
                GameNPC[] npcs = WorldMgr.GetNPCsByName(str, eRealm.None);
                if (npcs != null)
                {
                    if (player.RealmLevel >= npcs[0].Level)
                    {
                        player.Model = npcs[0].Model;
                        player.SaveIntoDatabase();
                    }
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
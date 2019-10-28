using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using DOL.AI;
using DOL.AI.Brain;
using DOL.Events;
using DOL.Database;
using DOL.GS.PacketHandler;

using log4net;
namespace DOL.GS.Scripts
{
    public class PvPHandler
    {
        private static string m_PvPZone = "Gothwaite";

        private static ushort lastRegionID = 69;
        private static Dictionary<string, string> m_IPsVotedAndVote = new Dictionary<string, string>();

        private static Dictionary<string, int> m_MapsAndVotes = new Dictionary<string, int>();

        public static void setPvPZone(string set)
        {
            GameNPC last = null;
            GameNPC[] search = WorldMgr.GetNPCsByName(m_PvPZone, eRealm.None);
            foreach (GameNPC searching in search)
            {
                if (searching.GuildName == "PvP Setup")
                {
                    last = searching;
                }
            }
            if (last != null)
            {
                lastRegionID = last.CurrentRegionID;
            }
            m_PvPZone = set;
            foreach (GameClient client in WorldMgr.GetAllPlayingClients())
            {
                bool ok = lastRegionID == 69 ? false : true;
                if (client.Player.CurrentRegionID == lastRegionID && ok)
                {
                    PortToPvPZone(client.Player);
                }
                client.Player.Out.SendMessage("The PvP Zone has changed to " + m_PvPZone + "!", eChatType.CT_Important, eChatLoc.CL_ChatWindow);
            }
        }
        public static string getPvPZone()
        {
            return m_PvPZone;
        }
        public static string ZoneWithHighestAmountOfVotes()
        {
            int highest = 0;
            string strHighest = "Gothwaite";
            foreach (KeyValuePair<string, int> kvp in m_MapsAndVotes)
            {
                if (kvp.Value > highest)
                {
                    highest = kvp.Value;
                    strHighest = kvp.Key;
                }
            }
            m_MapsAndVotes = new Dictionary<string, int>();
            m_IPsVotedAndVote = new Dictionary<string, string>();
            return strHighest;
        }
        public static int NumberOfVotes(string str)
        {
            int number = 0;
            if (m_MapsAndVotes.ContainsKey(str))
            {
                number = m_MapsAndVotes[str];
            }
            return number;
        }
        public static bool playerHasVoted(GamePlayer player)
        {
            if (m_IPsVotedAndVote.ContainsKey(player.Client.TcpEndpointAddress))
            {
                return true;
            }
            return false;
        }
        public static void AddVote(string Choice, GamePlayer Player)
        {
            if (m_IPsVotedAndVote.ContainsKey(Player.Client.TcpEndpointAddress))
            {
                //
                string oldVote = m_IPsVotedAndVote[Player.Client.TcpEndpointAddress];
               // int oldCount = m_MapsAndVotes[oldVote];
                int oldCount = 0;
                if (m_MapsAndVotes.ContainsKey(oldVote))
                {
                    oldCount = m_MapsAndVotes[oldVote] - 1;
                    m_MapsAndVotes.Remove(oldVote);
                }
                m_MapsAndVotes.Add(oldVote, oldCount);


                m_IPsVotedAndVote.Remove(Player.Client.TcpEndpointAddress);
                m_IPsVotedAndVote.Add(Player.Client.TcpEndpointAddress, Choice);

                //int newCount = m_MapsAndVotes[Choice];
                int newCount = 1;
                if (m_MapsAndVotes.ContainsKey(Choice))
                {
                    newCount = m_MapsAndVotes[Choice] + 1;
                    m_MapsAndVotes.Remove(Choice);
                }
                m_MapsAndVotes.Add(Choice, newCount);
            }
            else
            {
                m_IPsVotedAndVote.Add(Player.Client.TcpEndpointAddress, Choice);

                //int newCount = m_MapsAndVotes[Choice];
                int newCount = 1;
                if (m_MapsAndVotes.ContainsKey(Choice))
                {
                    newCount = m_MapsAndVotes[Choice] + 1;
                    m_MapsAndVotes.Remove(Choice);
                }
                m_MapsAndVotes.Add(Choice, newCount);
            }

            if (NumberOfVotes(Choice) > 29)
            {
                PvPHandler.setPvPZone(Choice);
            }
        }
        public static void PortToPvPZone(GamePlayer player)
        {
            GameNPC npc = currentPvPPort();
            if (npc == null)
            {
                player.Out.SendMessage("Error #1, please contact a GM", eChatType.CT_Important, eChatLoc.CL_ChatWindow);
                return;
            }
            player.MoveTo(npc.CurrentRegionID, npc.X, npc.Y, npc.Z, npc.Heading);
            player.Bind(true);
        }
        public static GameNPC currentPvPPort()
        { 
            GameNPC[] npcs = WorldMgr.GetNPCsByName(m_PvPZone, eRealm.None);
            if (npcs.Length > 0 && npcs != null)
            {
                GameNPC theNPC = npcs[0];
                if (theNPC != null)
                {
                    if (theNPC.GuildName == "PvP Setup")
                    {
                        return theNPC;
                    }
                }
            }
            return null;
        }
    }
    public class RedemptionTeleporter : GameNPC
    {
        public RedemptionTeleporter()
        {
        }
        public const string IS_VOTING = "IS_VOTING";
        public string GetAllPorts(bool addAmounts)
        {
            string str = "";
            List<GameNPC> pvpSetupMarkers = WorldMgr.GetNPCsByGuild("PvP Setup", eRealm.None);
            if (pvpSetupMarkers.Count > 0)
            {
                foreach (GameNPC npc in pvpSetupMarkers)
                {
                    if (addAmounts)
                    {
                        str += "(" + PvPHandler.NumberOfVotes(npc.Name) + " Votes)";
                    }
                    str += " [" + npc.Name + "]\n";
                }
            }
            return str;
        }
        public override bool Interact(GamePlayer player)
        {
            if (base.Interact(player))
            {
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

                SayTo(player, string.Format("Hello {0}, where do you wish to teleport?\n\n \n [PvP Zone] or [Setup] or See the PvP [Voting] options.", player.Name));
                List<GameNPC> pvpSetupMarkers = WorldMgr.GetNPCsByGuild("PvP Setup", eRealm.None);
                if (pvpSetupMarkers.Count > 0)
                {
                    string GMstr = "FORCE Change the PvP Zone ? ";
                    GMstr += GetAllPorts(false);
                    GMstr += "[Change] to whichever zone has the highest number of votes";
                    if (player.Client.Account.PrivLevel > 2)
                    {
                        SayTo(player, GMstr);
                    }
                }
                return true;
            }
            return false;
        }
        public bool CheckIfStrIsAMob(string text)
        {
            GameNPC[] npcs = WorldMgr.GetNPCsByName(text, eRealm.None);
            if (npcs.Length > 0 && npcs != null)
            {
                GameNPC theNPC = npcs[0];
                if (theNPC != null)
                {
                    if (theNPC.GuildName == "PvP Setup")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public override bool WhisperReceive(GameLiving source, string text)
        {
            if (base.WhisperReceive(source, text))
            {
                GamePlayer player = source as GamePlayer;
                if (player == null)
                    return false;

                if (text == "Change")
                {
                    PvPHandler.setPvPZone(PvPHandler.ZoneWithHighestAmountOfVotes());

                }
                if (text == "Voting")
                {
                    player.TempProperties.setProperty(IS_VOTING, true);
                    string str = "Which PvP Area do you wish to vote for? \n" + GetAllPorts(true) + " [Cancel] voting";
                    SendReply(player, str);
                }
                if (text == "Cancel" && player.TempProperties.getProperty<bool>(IS_VOTING) == true)
                {
                    player.TempProperties.removeProperty(IS_VOTING);
                    SendReply(player, "You have now cancelled your vote");
                }
                if (text == "Setup")
                {
                    player.MoveTo(70, 565006, 536702, 5983, 2561);
                    player.Bind(true);
                    return true; 
                }
                if (text == "PvP Zone")
                {
                    if (!PvPHandler.playerHasVoted(player))
                    {
                        player.TempProperties.setProperty(IS_VOTING, true);
                        string str = "Which PvP Area do you wish to vote for? \n" + GetAllPorts(true) + "";
                        SendReply(player, str);
                        return true;
                    }
                    else
                    {
                        PvPHandler.PortToPvPZone(player);
                        return true;
                    }
                }
                if (player.TempProperties.getProperty<bool>(IS_VOTING) == true)
                {
                    if (CheckIfStrIsAMob(text))
                    {
                        PvPHandler.AddVote(text, player);
                        player.TempProperties.removeProperty(IS_VOTING);
                        SendReply(player, "You have voted for " + text);
                    }
                }
                else if (player.Client.Account.PrivLevel > 2)
                {
                    if (CheckIfStrIsAMob(text))
                    {
                        PvPHandler.setPvPZone(text);
                    }
                }
                return true;
            }

            return false;
        }
    
        public void SendReply(GamePlayer player, string msg)
        {
            player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }
    }


}
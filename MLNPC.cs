/*
 * This NPC was originally written by crazys (kyle). 
 * Updated 10-23-08 by BluRaven
 * I have only changed (added) the eMLLine enum to get it to compile
 * as a standalone script with the current SVN of DOL.  Place both scripts into your scripts folder and restart your server.
 * to create in game type: /mob create DOL.GS.Scripts.MLNPC
 */

using System;
using System.Collections;
using System.Timers;
using log4net;
using DOL;
using DOL.GS;
using DOL.Database;
using DOL.Database.Attributes;
using DOL.GS.Scripts;
using DOL.Events;
using DOL.GS.GameEvents;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;

namespace DOL.GS
{


    /// <summary>
    /// Defines the Master Level lines
    /// </summary>
    public enum eMLLine : byte
    {
        /// <summary>
        /// No specific Master Level Line
        /// </summary>
        None = 0,
        /// <summary>
        /// Banelord Master Level Line
        /// </summary>
        Banelord = 1,
        /// <summary>
        /// Battlemaster Master Level Line
        /// </summary>
        BattleMaster = 2,
        /// <summary>
        /// Convoker Master Level Line
        /// </summary>
        Convoker = 3,
        /// <summary>
        /// Perfecter Master Level Line
        /// </summary>
        Perfecter = 4,
        /// <summary>
        /// Sojourner Master Level Line
        /// </summary>
        Sojourner = 5,
        /// <summary>
        /// Spymaster Master Level Line
        /// </summary>
        Spymaster = 6,
        /// <summary>
        /// Stormlord Master Level Line
        /// </summary>
        Stormlord = 7,
        /// <summary>
        /// Stormlord Master Level Line
        /// </summary>
        Warlord = 8,

    }
}



namespace DOL.GS.Scripts
{
    public class MLNPC : GameNPC
    {
        private static ItemTemplate item = null;
        private static ItemTemplate ml1token = null;
        private static ItemTemplate ml2token = null;
        private static ItemTemplate ml3token = null;
        private static ItemTemplate ml4token = null;
        private static ItemTemplate ml5token = null;
        private static ItemTemplate ml6token = null;
        private static ItemTemplate ml7token = null;
        private static ItemTemplate ml8token = null;
        private static ItemTemplate ml9token = null;
        private static ItemTemplate ml10token = null;

        string mlchoicea = "";
        string mlchoiceb = "";
        byte mlstatus = 0;

        private const int EFFECT_ID = 1610;
        private const int CAST_TIME = 20;
        private static ArrayList locs = new ArrayList();

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MLNPC()
            : base()
        {
            Flags |= GameNPC.eFlags.PEACE;
        }
        #region Add To World
        public override bool AddToWorld()
        {
            GuildName = "Master Levels";
            Level = 50;
            base.AddToWorld();
            return true;
        }
        #endregion Add To World
        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            ml1token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml1token");
            if (ml1token == null)
            {
                ml1token = new ItemTemplate();
                ml1token.Id_nb = "ml1token";
                ml1token.Name = "ML1 Credit";
                ml1token.Level = 50;
                ml1token.Item_Type = 40;
                ml1token.Model = 485;
                ml1token.IsTradable = true;
                ml1token.Object_Type = 0;
                ml1token.Quality = 100;
                ml1token.Weight = 1;
                ml1token.MaxCondition = 100;
                ml1token.MaxDurability = 100;
                ml1token.Condition = 100;
                ml1token.Durability = 100;
                ml1token.Price = 500;
                GameServer.Database.AddObject(ml1token);
            }

            ml2token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml2token");
            if (ml2token == null)
            {
                ml2token = new ItemTemplate();
                ml2token.Id_nb = "ml2token";
                ml2token.Name = "ML2 Credit";
                ml2token.Level = 50;
                ml2token.Item_Type = 40;
                ml2token.Model = 485;
                ml2token.IsTradable = true;
                ml2token.Object_Type = 0;
                ml2token.Quality = 100;
                ml2token.Weight = 1;
                ml2token.MaxCondition = 100;
                ml2token.MaxDurability = 100;
                ml2token.Condition = 100;
                ml2token.Durability = 100;
                ml2token.Price = 1000;
                GameServer.Database.AddObject(ml2token);
            }

            ml3token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml3token");
            if (ml3token == null)
            {
                ml3token = new ItemTemplate();
                ml3token.Id_nb = "ml3token";
                ml3token.Name = "ML3 Credit";
                ml3token.Level = 50;
                ml3token.Item_Type = 40;
                ml3token.Model = 485;
                ml3token.IsTradable = true;
                ml3token.Object_Type = 0;
                ml3token.Quality = 100;
                ml3token.Weight = 1;
                ml3token.MaxCondition = 100;
                ml3token.MaxDurability = 100;
                ml3token.Condition = 100;
                ml3token.Durability = 100;
                ml3token.Price = 1500;
                GameServer.Database.AddObject(ml3token);
            }

            ml4token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml4token");
            if (ml4token == null)
            {
                ml4token = new ItemTemplate();
                ml4token.Id_nb = "ml4token";
                ml4token.Name = "ML4 Credit";
                ml4token.Level = 50;
                ml4token.Item_Type = 40;
                ml4token.Model = 485;
                ml4token.IsTradable = true;
                ml4token.Object_Type = 0;
                ml4token.Quality = 100;
                ml4token.Weight = 1;
                ml4token.MaxCondition = 100;
                ml4token.MaxDurability = 100;
                ml4token.Condition = 100;
                ml4token.Durability = 100;
                ml4token.Price = 2000;
                GameServer.Database.AddObject(ml4token);
            }

            ml5token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml5token");
            if (ml5token == null)
            {
                ml5token = new ItemTemplate();
                ml5token.Id_nb = "ml5token";
                ml5token.Name = "ML5 Credit";
                ml5token.Level = 50;
                ml5token.Item_Type = 40;
                ml5token.Model = 485;
                ml5token.IsTradable = true;
                ml5token.Object_Type = 0;
                ml5token.Quality = 100;
                ml5token.Weight = 1;
                ml5token.MaxCondition = 100;
                ml5token.MaxDurability = 100;
                ml5token.Condition = 100;
                ml5token.Durability = 100;
                ml5token.Price = 2500;
                GameServer.Database.AddObject(ml5token);
            }

            ml6token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml6token");
            if (ml6token == null)
            {
                ml6token = new ItemTemplate();
                ml6token.Id_nb = "ml6token";
                ml6token.Name = "ML6 Credit";
                ml6token.Level = 50;
                ml6token.Item_Type = 40;
                ml6token.Model = 485;
                ml6token.IsTradable = true;
                ml6token.Object_Type = 0;
                ml6token.Quality = 100;
                ml6token.Weight = 1;
                ml6token.MaxCondition = 100;
                ml6token.MaxDurability = 100;
                ml6token.Condition = 100;
                ml6token.Durability = 100;
                ml6token.Price = 3000;
                GameServer.Database.AddObject(ml6token);
            }

            ml7token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml7token");
            if (ml7token == null)
            {
                ml7token = new ItemTemplate();
                ml7token.Id_nb = "ml7token";
                ml7token.Name = "ML7 Credit";
                ml7token.Level = 50;
                ml7token.Item_Type = 40;
                ml7token.Model = 485;
                ml7token.IsTradable = true;
                ml7token.Object_Type = 0;
                ml7token.Quality = 100;
                ml7token.Weight = 1;
                ml7token.MaxCondition = 100;
                ml7token.MaxDurability = 100;
                ml7token.Condition = 100;
                ml7token.Durability = 100;
                ml7token.Price = 3500;
                GameServer.Database.AddObject(ml7token);
            }

            ml8token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml8token");
            if (ml8token == null)
            {
                ml8token = new ItemTemplate();
                ml8token.Id_nb = "ml8token";
                ml8token.Name = "ML8 Credit";
                ml8token.Level = 50;
                ml8token.Item_Type = 40;
                ml8token.Model = 485;
                ml8token.IsTradable = true;
                ml8token.Object_Type = 0;
                ml8token.Quality = 100;
                ml8token.Weight = 1;
                ml8token.MaxCondition = 100;
                ml8token.MaxDurability = 100;
                ml8token.Condition = 100;
                ml8token.Durability = 100;
                ml8token.Price = 4000;
                GameServer.Database.AddObject(ml8token);
            }

            ml9token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml9token");
            if (ml9token == null)
            {
                ml9token = new ItemTemplate();
                ml9token.Id_nb = "ml9token";
                ml9token.Name = "ML9 Credit";
                ml9token.Level = 50;
                ml9token.Item_Type = 40;
                ml9token.Model = 485;
                ml9token.IsTradable = true;
                ml9token.Object_Type = 0;
                ml9token.Quality = 100;
                ml9token.Weight = 1;
                ml9token.MaxCondition = 100;
                ml9token.MaxDurability = 100;
                ml9token.Condition = 100;
                ml9token.Durability = 100;
                ml9token.Price = 4500;
                GameServer.Database.AddObject(ml9token);
            }

            ml10token = GameServer.Database.FindObjectByKey<ItemTemplate>("ml10token");
            if (ml10token == null)
            {
                ml10token = new ItemTemplate();
                ml10token.Id_nb = "ml10token";
                ml10token.Name = "ML10 Credit";
                ml10token.Level = 50;
                ml10token.Item_Type = 40;
                ml10token.Model = 485;
                ml10token.IsTradable = true;
                ml10token.Object_Type = 0;
                ml10token.Quality = 100;
                ml10token.Weight = 1;
                ml10token.MaxCondition = 100;
                ml10token.MaxDurability = 100;
                ml10token.Condition = 100;
                ml10token.Durability = 100;
                ml10token.Price = 5000;
                GameServer.Database.AddObject(ml10token);
            }
        }

        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer t = source as GamePlayer;
            if (WorldMgr.GetDistance(this, source) > WorldMgr.INTERACT_DISTANCE)
            {
                ((GamePlayer)source).Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return false;
            }
            if (t != null && item != null && item.Id_nb == "ml1token")
            {
                if (t.CharacterClass.ID == 1)//Paladin
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 2)//Armsman
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 3)//Scout
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Sojourner]";
                }
                else if (t.CharacterClass.ID == 4)//Minstrel
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Sojourner]";
                }
                else if (t.CharacterClass.ID == 5)//Theurgist
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 6)//Cleric
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 7)//Wizard
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 8)//Sorcerer
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 9)//Infiltrator
                {
                    mlchoicea = "[Spymaster]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 10)//Frair
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 11)//Mercenary
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 12)//Necromancer
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 13)//Cabalist
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 19)//Reaver
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 21)//Thane
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 22)//Warrior
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 23)//Shadowblade
                {
                    mlchoicea = "[Spymaster]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 24)//Skald
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Sojourner]";
                }
                else if (t.CharacterClass.ID == 25)//Hunter
                {
                    mlchoicea = "[Sojourner]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 26)//Healer
                {
                    mlchoicea = "[Sojourner]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 27)//Spiritmaster
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 28)//Shaman
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 29)//Runemaster
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 30)//Bonedancer
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 31)//Berserker
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 32)//Savage
                {
                    mlchoicea = "[Warlord]";
                    mlchoiceb = "[Battlemaster]";
                }
                else if (t.CharacterClass.ID == 33)//Heretic
                {
                    mlchoicea = "[Banelord]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 34)//Valkyrie
                {
                    mlchoicea = "[Stormlord]";
                    mlchoiceb = "[Warlord]";
                }
                else if (t.CharacterClass.ID == 39)//Bainshee
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 40)//Eldritch
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 41)//Enchanter
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 42)//Mentalist
                {
                    mlchoicea = "[Stormlord]";
                    mlchoiceb = "[Warlord]";
                }
                else if (t.CharacterClass.ID == 43)//Blademaster
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 44)//Hero
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Warlord]";
                }
                else if (t.CharacterClass.ID == 45)//Champion
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Banelord]";
                }
                else if (t.CharacterClass.ID == 46)//Warden
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 47)//Druid
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 48)//Bard
                {
                    mlchoicea = "[Sojourner]";
                    mlchoiceb = "[Perfecter]";
                }
                else if (t.CharacterClass.ID == 49)//Nightshade
                {
                    mlchoicea = "[Spymaster]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 50)//Ranger
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Sojourner]";
                }
                else if (t.CharacterClass.ID == 55)//Animist
                {
                    mlchoicea = "[Convoker]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 56)//Valewalker
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Stormlord]";
                }
                else if (t.CharacterClass.ID == 58)//Vampiir
                {
                    mlchoicea = "[Banelord]";
                    mlchoiceb = "[Warlord]";
                }
                else if (t.CharacterClass.ID == 59)//Warlock
                {
                    mlchoicea = "[Banelord]";
                    mlchoiceb = "[Convoker]";
                }
                else if (t.CharacterClass.ID == 60 || t.CharacterClass.ID == 61 || t.CharacterClass.ID == 62)//Mauler
                {
                    mlchoicea = "[Battlemaster]";
                    mlchoiceb = "[Warlord]";
                }
                if (t.GetSpellLine("ML1 Banelord") == null &&
                    t.GetSpellLine("ML1 Battlemaster") == null &&
                    t.GetSpellLine("ML1 Convoker") == null &&
                    t.GetSpellLine("ML1 Perfecter") == null &&
                    t.GetSpellLine("ML1 Sojourner") == null &&
                    t.GetSpellLine("ML1 Stormlord") == null &&
                    t.GetSpellLine("ML1 Spymaster") == null &&
                    t.GetSpellLine("ML1 Warlord") == null)
                {
                    t.TempProperties.setProperty("MLCHOICE", "path");
                    t.Out.SendMessage("I can grant you access to " + mlchoicea + " or " + mlchoiceb, eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    t.Inventory.RemoveItem(item);
                }
                else
                {
                    t.Out.SendMessage("You already have an ML path.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
                return true;
            }
            else if (t != null && item != null && item.Id_nb == "ml2token")
            {
                //if (t.MLLevel==1 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 1 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML1 Banelord") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Banelord"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Battlemaster") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Battlemaster"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Convoker") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Convoker"));
                        t.Out.SendMessage("You have gained Convoker ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Perfecter") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Perfecter"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Sojourner") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Sojourner"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Spymaster") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Spymaster"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Stormlord") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Stormlord"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML1 Warlord") != null)
                    {
                        t.MLLevel = 2; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML2 Warlord"));
                        t.Out.SendMessage("You have gained ML2.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml3token")
            {
                //if (t.MLLevel==2 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 2 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML2 Banelord") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Banelord"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Battlemaster") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Battlemaster"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Convoker") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Convoker"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Perfecter") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Perfecter"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Sojourner") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Sojourner"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Spymaster") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Spymaster"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Stormlord") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Stormlord"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML2 Warlord") != null)
                    {
                        t.MLLevel = 3; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML3 Warlord"));
                        t.Out.SendMessage("You have gained ML3.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml4token")
            {
                //if (t.MLLevel==3 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 3 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML3 Banelord") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Banelord"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Battlemaster") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Battlemaster"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Convoker") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Convoker"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Perfecter") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Perfecter"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Sojourner") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Sojourner"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Spymaster") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Spymaster"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Stormlord") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Stormlord"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML3 Warlord") != null)
                    {
                        t.MLLevel = 4; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML4 Warlord"));
                        t.Out.SendMessage("You have gained ML4.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml5token")
            {
                //if (t.MLLevel==4 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 4 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML4 Banelord") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Banelord"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Battlemaster") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Battlemaster"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Convoker") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Convoker"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Perfecter") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Perfecter"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Sojourner") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Sojourner"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Spymaster") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Spymaster"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Stormlord") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Stormlord"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML4 Warlord") != null)
                    {
                        t.MLLevel = 5; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML5 Warlord"));
                        t.Out.SendMessage("You have gained ML5.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml6token")
            {
                //if (t.MLLevel==5 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 5 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML5 Banelord") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Banelord"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Battlemaster") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Battlemaster"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Convoker") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Convoker"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Perfecter") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Perfecter"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Sojourner") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Sojourner"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Spymaster") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Spymaster"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Stormlord") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Stormlord"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML5 Warlord") != null)
                    {
                        t.MLLevel = 6; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML6 Warlord"));
                        t.Out.SendMessage("You have gained ML6.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml7token")
            {
                //if (t.MLLevel==6 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 6 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML6 Banelord") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Banelord"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Battlemaster") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Battlemaster"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Convoker") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Convoker"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Perfecter") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Perfecter"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Sojourner") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Sojourner"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Spymaster") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Spymaster"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Stormlord") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Stormlord"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML6 Warlord") != null)
                    {
                        t.MLLevel = 7; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML7 Warlord"));
                        t.Out.SendMessage("You have gained ML7.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml8token")
            {
                //if (t.MLLevel==7 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 7 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML7 Banelord") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Banelord"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Battlemaster") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Battlemaster"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Convoker") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Convoker"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Perfecter") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Perfecter"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Sojourner") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Sojourner"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Spymaster") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Spymaster"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Stormlord") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Stormlord"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML7 Warlord") != null)
                    {
                        t.MLLevel = 8; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML8 Warlord"));
                        t.Out.SendMessage("You have gained ML8.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml9token")
            {
                //if (t.MLLevel==8 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 8 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML8 Banelord") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Banelord"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Battlemaster") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Battlemaster"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Convoker") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Convoker"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Perfecter") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Perfecter"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Sojourner") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Sojourner"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Spymaster") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Spymaster"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Stormlord") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Stormlord"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML8 Warlord") != null)
                    {
                        t.MLLevel = 9; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML9 Warlord"));
                        t.Out.SendMessage("You have gained ML9.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }
            else if (t != null && item != null && item.Id_nb == "ml10token")
            {
                //if (t.MLLevel==9 && (t.MLGranted || t.Client.Account.PrivLevel > 1))
                if (t.MLLevel == 9 || t.Client.Account.PrivLevel > 1)
                {
                    if (t.GetSpellLine("ML9 Banelord") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Banelord"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Battlemaster") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Battlemaster"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Convoker") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Convoker"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Perfecter") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Perfecter"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Sojourner") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Sojourner"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Spymaster") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Spymaster"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Stormlord") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Stormlord"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                    else if (t.GetSpellLine("ML9 Warlord") != null)
                    {
                        t.MLLevel = 10; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML10 Warlord"));
                        t.Out.SendMessage("You have gained ML10.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.Inventory.RemoveItem(item); t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                        t.SaveIntoDatabase();
                    }
                }
                else
                {
                    t.Out.SendMessage("You must complete all previous MLs.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                }
            }

            return base.ReceiveItem(source, item);
        }

        protected static void GiveItem(GamePlayer player, ItemTemplate itemTemplate)
        {
            GiveItem(null, player, itemTemplate);
        }

        protected static void GiveItem(GameLiving source, GamePlayer player, ItemTemplate itemTemplate)
        {
            InventoryItem item = GameInventoryItem.Create<ItemTemplate>(itemTemplate);
            if (player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, item))
            {
                if (source == null)
                {
                    player.Out.SendMessage("You receive the " + itemTemplate.Name + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else
                {
                    player.Out.SendMessage("You receive " + itemTemplate.GetName(0, false) + " from " + source.GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
            }
            else
            {
                player.CreateItemOnTheGround(item);
                player.Out.SendMessage("Your Inventory is full. You couldn't recieve the " + itemTemplate.Name + ", so it's been placed on the ground. Pick it up as soon as possible or it will vanish in a few minutes.", eChatType.CT_Important, eChatLoc.CL_PopupWindow);
            }
        }
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            string msg = "";
            if (player.Level < 40)
                msg = "Please come back when you are over level 40.";
            else msg = "Hi I'm the master level trainer.  You turn in ML credit tokens to me and I grant you master levels.";

            player.Out.SendMessage(msg, eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            if (player.Client.Account.PrivLevel > 2)
            {
                player.Out.SendMessage("For testing purposes, I can give you a [ml1],[ml2],[ml3],[ml4],[ml5],[ml6],[ml7],[ml8],[ml9],[ml10],tokken and you can hand it back to me to get credit.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            }

            return true;
        }


        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer t = (GamePlayer)source;
            TurnTo(t.X, t.Y);

            if (t.Level != 50)
                t.Out.SendMessage("Please come back when you are level 50.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);

            switch (str)
            {
                case "aaaaaaaaaaaaaaaaaaaaaml1":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml1token);
                    break;
                case "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaml2":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml2token);
                    break;
                case "maaaaaaaaaaaaaaaaaaaaaaaaaal3":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml3token);
                    break;
                case "mlaaaaaaaaaaaaaaaa4":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml4token);
                    break;
                case "maaaaaaaaaaaaaaaaaaal5":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml5token);
                    break;
                case "maaaaaaaaaaaaal6":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml6token);
                    break;
                case "mlaaaaaaaaaaaaaa7":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml7token);
                    break;
                case "mlaaaaaaaaaaaaaaaaaaaaaaaa8":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml8token);
                    break;
                case "mlaaaaaaaaaaaaaaaaaaaa9":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml9token);
                    break;
                case "ml1aaaaaaaaaaaa0":
                    if (t.Client.Account.PrivLevel < 1)
                    {
                        break;
                    }
                    GiveItem(t, ml10token);
                    break;
                case "Banelord"://11,19,31,33,43,45,58,59,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                       (t.CharacterClass.ID == 11 ||
                        t.CharacterClass.ID == 19 || t.CharacterClass.ID == 31 ||
                        t.CharacterClass.ID == 33 || t.CharacterClass.ID == 43 ||
                        t.CharacterClass.ID == 45 || t.CharacterClass.ID == 58 ||
                        t.CharacterClass.ID == 59))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel = (byte)eMLLine.Banelord;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Banelord"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Battlemaster"://2,1,3,9,10,11,19,21,22,23,25,31,32,43,44,45,46,50,56,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 2 ||
                            t.CharacterClass.ID == 1 ||
                            t.CharacterClass.ID == 3 ||
                            t.CharacterClass.ID == 9 ||
                            t.CharacterClass.ID == 10 ||
                            t.CharacterClass.ID == 11 ||
                            t.CharacterClass.ID == 19 ||
                            t.CharacterClass.ID == 21 ||
                            t.CharacterClass.ID == 22 ||
                            t.CharacterClass.ID == 23 ||
                            t.CharacterClass.ID == 25 ||
                            t.CharacterClass.ID == 31 ||
                            t.CharacterClass.ID == 32 ||
                            t.CharacterClass.ID == 43 ||
                            t.CharacterClass.ID == 44 ||
                            t.CharacterClass.ID == 45 ||
                            t.CharacterClass.ID == 46 ||
                            t.CharacterClass.ID == 50 ||
                            t.CharacterClass.ID == 56 ||
                            t.CharacterClass.ID == 60 ||
                            t.CharacterClass.ID == 61 ||
                            t.CharacterClass.ID == 62))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.BattleMaster;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Battlemaster"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Convoker"://5,7,8,12,13,27,28,29,30,39,40,41,47,55,59
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 5 ||
                            t.CharacterClass.ID == 7 ||
                            t.CharacterClass.ID == 8 ||
                            t.CharacterClass.ID == 12 ||
                            t.CharacterClass.ID == 13 ||
                            t.CharacterClass.ID == 27 ||
                            t.CharacterClass.ID == 28 ||
                            t.CharacterClass.ID == 29 ||
                            t.CharacterClass.ID == 30 ||
                            t.CharacterClass.ID == 39 ||
                            t.CharacterClass.ID == 40 ||
                            t.CharacterClass.ID == 41 ||
                            t.CharacterClass.ID == 47 ||
                            t.CharacterClass.ID == 55 ||
                            t.CharacterClass.ID == 59))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Convoker;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Convoker"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Perfecter"://6,10,26,28,33,46,47,48,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 6 ||
                            t.CharacterClass.ID == 10 || t.CharacterClass.ID == 26 ||
                            t.CharacterClass.ID == 28 || t.CharacterClass.ID == 33 ||
                            t.CharacterClass.ID == 46 || t.CharacterClass.ID == 47 ||
                            t.CharacterClass.ID == 48))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Perfecter;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Perfecter"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Sojourner"://3,4,24,25,26,48,50,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 3 ||
                            t.CharacterClass.ID == 4 || t.CharacterClass.ID == 24 ||
                            t.CharacterClass.ID == 25 || t.CharacterClass.ID == 26 ||
                            t.CharacterClass.ID == 48 || t.CharacterClass.ID == 50))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Sojourner;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Sojourner"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Spymaster"://9,23,49,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 9 ||
                            t.CharacterClass.ID == 23 || t.CharacterClass.ID == 49))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Spymaster;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Spymaster"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Stormlord"://5,7,8,12,13,21,27,29,30,34,39,40,41,42,49,55,56,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 5 ||
                            t.CharacterClass.ID == 7 ||
                            t.CharacterClass.ID == 8 ||
                            t.CharacterClass.ID == 12 ||
                            t.CharacterClass.ID == 13 ||
                            t.CharacterClass.ID == 21 ||
                            t.CharacterClass.ID == 27 ||
                            t.CharacterClass.ID == 29 ||
                            t.CharacterClass.ID == 30 ||
                            t.CharacterClass.ID == 34 ||
                            t.CharacterClass.ID == 39 ||
                            t.CharacterClass.ID == 40 ||
                            t.CharacterClass.ID == 41 ||
                            t.CharacterClass.ID == 42 ||
                            t.CharacterClass.ID == 49 ||
                            t.CharacterClass.ID == 55 ||
                            t.CharacterClass.ID == 56))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Stormlord;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Stormlord"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                case "Warlord"://2,1,4,6,22,24,32,34,42,44,58,
                    if (t.TempProperties.getProperty<string>("MLCHOICE") != null &&
                           (t.CharacterClass.ID == 2 ||
                            t.CharacterClass.ID == 1 ||
                            t.CharacterClass.ID == 4 ||
                            t.CharacterClass.ID == 6 ||
                            t.CharacterClass.ID == 22 ||
                            t.CharacterClass.ID == 24 ||
                            t.CharacterClass.ID == 32 ||
                            t.CharacterClass.ID == 34 ||
                            t.CharacterClass.ID == 42 ||
                            t.CharacterClass.ID == 44 ||
                            t.CharacterClass.ID == 58 ||
                            t.CharacterClass.ID == 60 ||
                            t.CharacterClass.ID == 61 ||
                            t.CharacterClass.ID == 62))
                    {
                        t.TempProperties.removeProperty("MLCHOICE");
                        t.MLLevel= (byte)eMLLine.Warlord;
                        t.MLLevel = 1; t.MLGranted = false; t.MLExperience = 0;
                        t.AddSpellLine(SkillBase.GetSpellLine("ML1 Warlord"));
                        t.Out.SendMessage("You have gained ML1.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        t.UpdateSpellLineLevels(true); t.Out.SendUpdatePlayerSkills(); t.Out.SendUpdatePlayer(); t.UpdatePlayerStatus();
                    }
                    else
                    {
                        t.Out.SendMessage("Your class can not become this ML or you are not allowed.", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                    }
                    break;
                default: break;
            }

            return true;
        }
        public void SendReply(GamePlayer target, string msg)
        {
            target.Client.Out.SendMessage(
                msg,
                eChatType.CT_Say, eChatLoc.CL_PopupWindow);
        }
    }

}
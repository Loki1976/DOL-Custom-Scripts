/*
 * Originally written by ?
 * updated by BluRaven on 10-25-08.
 * update by Amuny on 26-05-09
 */

using System;
using System.Collections;
using DOL.GS.Effects;
using DOL.GS.PacketHandler;
using DOL.GS.Spells;
using DOL.Database;

namespace DOL.GS.Scripts
{
    public class ChampionNPC : GameNPC
    {
        public ChampionNPC()
            : base()
        {
            Name = "Champion Level";
            GuildName = "Amuny's PvP";

        }
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;
            TurnTo(player, 5000);

            //check if player needs to be promoted
            CheckPromoteChampion(player);

            if (!player.Champion && player.Level == 50)
            {
                player.Out.SendMessage("Would you like to embrace in the life of [champions]?.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
            }
            else if (player.Champion)
            {
                switch (player.Realm)
                {
                    case eRealm.Albion:
                        player.Out.SendMessage("Which line would you like to train?\n[Acolyte] or [Elementalist]?", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                        break;
                    case eRealm.Hibernia:
                        player.Out.SendMessage("Which line would you like to train?\n[Way of Nature] or [Way of Magic]?", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                        break;
                    case eRealm.Midgard:
                        player.Out.SendMessage("Which line would you like to train?\n[Seer] or [Mystic]?", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                        break;
                }
                player.Out.SendMessage("Choose your path. That's all I can offer to you for now.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                if (player.ChampionLevel < player.ChampionMaxLevel)
                {
                  //  player.Out.SendMessage("As a new Champion, I can now [grant] you the king's favor and let your learn new abilities.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                }
                if (player.Champion && player.ChampionLevel >= 5)
                {
                    player.Out.SendMessage("If you wish, I can also [respec] your champion abilities to let you choose other skills.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                }
            }
            else
            {
                player.Out.SendMessage("Come back when you are level 50.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
            }
            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {

            if (!base.WhisperReceive(source, str))
                return false;
            GamePlayer player = source as GamePlayer;
            if (player == null) return false;
            if (str == "dostuff")
            {
                DoSomeStuff(15);
                return true;
            }
            if (player.Level != 50)
            {
                player.Out.SendMessage("Your not strong enough to embrace the life of champions!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                return false;
            }
            if (str == "champions")
            {
                if (player.Champion)
                {
                    player.Out.SendMessage("You are already a champion!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return false;
                }
                player.Champion = true;
                player.SaveIntoDatabase();
                player.Out.SendMessage("You have just embraced on the life of the champions!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                Interact(player);
                return true;
            }
            if (str == "Agdranzt" && (player.Champion))
            {
                if (player.ChampionLevel == player.ChampionMaxLevel)
                {
                    player.Out.SendMessage("You are already at the maximum champion level!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return true;
                }

                do
                {
                    player.ChampionExperience = +player.ChampionExperienceForNextLevel;
                    CheckPromoteChampion(player);
                } while (player.ChampionLevel < player.ChampionMaxLevel);
                return true;
            }

            //level respec for players
            if (str == "respec")
            {
                if (player.Champion && player.ChampionLevel >= 5)
                {
                    player.RemoveSpellLine(GlobalSpellsLines.Champion_Spells + player.Name);
                    SkillBase.UnRegisterSpellLine(GlobalSpellsLines.Champion_Spells + player.Name);
                    player.ChampionSpells = "";
                    player.ChampionSpecialtyPoints = player.ChampionLevel;
                    player.UpdateSpellLineLevels(true);
                    player.RefreshSpecDependantSkills(true);
                    player.Out.SendUpdatePlayer();
                    player.UpdatePlayerStatus();
                    player.Out.SendUpdatePlayerSkills();
                    player.SaveIntoDatabase();
                    player.Out.SendMessage("You might have to reconnect to our world to make the changes take effect.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    //There is a bug here that old spells will come back once buying new ones untill relog of character.  you may want
                    //to force the player to relog after they respec champion spells.
                }
            }


            int ctype = 0;
            switch (player.Realm)
            {
                case eRealm.Albion:
                    {
                        switch (str)
                        {
                            case "Acolyte":
                                {
                                    ctype = 4;
                                }
                                break;
                            case "Elementalist":
                                {
                                    ctype = 5;
                                }
                                break;
                        }
                    }
                    break;
                case eRealm.Hibernia:
                    {
                        switch (str)
                        {
                            case "Way of Nature":
                                {
                                    ctype = 10;
                                }
                                break;
                            case "Way of Magic":
                                {
                                    ctype = 11;
                                }
                                break;
                        }
                    }
                    break;
                case eRealm.Midgard:
                    {
                        switch (str)
                        {
                            case "Seer":
                                {
                                    ctype = 8;
                                }
                                break;
                            case "Mystic":
                                {
                                    ctype = 9;
                                }
                                break;
                        }
                    }
                    break;
            }
            if (ctype != 0)
            {
                player.TempProperties.setProperty("championtraining", ctype);
                player.Out.SendChampionTrainerWindow(ctype);
            }

            return true;
        }

        public void DoSomeStuff(int times)
        {
            int idlinecount = 1;
            for (int i = 0; i < times; i++)
            {
                int indexcount = 1;
                int skillindexcount = 1;
                for (int o = 0; o < 5; o++)
                {
                    DBChampSpecs s1 = new DBChampSpecs();
                    s1.ObjectId = idlinecount.ToString() + "-" + skillindexcount.ToString() + "-" + indexcount.ToString();
                    s1.IdLine = idlinecount;
                    s1.SkillIndex = skillindexcount;
                    s1.Index = indexcount;
                    s1.Cost = 1;
                    s1.AllowAdd = true;
                    GameServer.Database.AddObject(s1);
                    indexcount++;
                }
                indexcount = 1;
                for (int o = 0; o < 5; o++)
                {
                    DBChampSpecs s1 = new DBChampSpecs();
                    s1.ObjectId = idlinecount.ToString() + "-" + (skillindexcount + 1).ToString() + "-" + indexcount.ToString();
                    s1.IdLine = idlinecount;
                    s1.SkillIndex = skillindexcount + 1;
                    s1.Index = indexcount;
                    s1.Cost = 1;
                    s1.AllowAdd = true;
                    GameServer.Database.AddObject(s1);
                    indexcount++;
                }
                indexcount = 1;
                for (int o = 0; o < 2; o++)
                {
                    DBChampSpecs s1 = new DBChampSpecs();
                    s1.ObjectId = idlinecount.ToString() + "-" + (skillindexcount + 2).ToString() + "-" + indexcount.ToString();
                    s1.IdLine = idlinecount;
                    s1.SkillIndex = skillindexcount + 2;
                    s1.Index = indexcount;
                    s1.Cost = 1;
                    s1.AllowAdd = true;
                    GameServer.Database.AddObject(s1);
                    indexcount++;
                }
                indexcount = 1;
                for (int o = 0; o < 3; o++)
                {
                    DBChampSpecs s1 = new DBChampSpecs();
                    s1.ObjectId = idlinecount.ToString() + "-" + (skillindexcount + 3).ToString() + "-" + indexcount.ToString();
                    s1.IdLine = idlinecount;
                    s1.SkillIndex = skillindexcount + 3;
                    s1.Index = indexcount;
                    s1.Cost = 1;
                    s1.AllowAdd = true;
                    GameServer.Database.AddObject(s1);
                    indexcount++;
                }
                indexcount = 1;
                for (int o = 0; o < 2; o++)
                {
                    DBChampSpecs s1 = new DBChampSpecs();
                    s1.ObjectId = idlinecount.ToString() + "-" + (skillindexcount + 4).ToString() + "-" + indexcount.ToString();
                    s1.IdLine = idlinecount;
                    s1.SkillIndex = skillindexcount + 4;
                    s1.Index = indexcount;
                    s1.Cost = 1;
                    s1.AllowAdd = true;
                    GameServer.Database.AddObject(s1);
                    indexcount++;
                }
                idlinecount++;

            }
        }


        protected void CheckPromoteChampion(GamePlayer player)
        {
            if (player.Champion)
            {
                bool cllevel = false;
                while (player.ChampionLevel < player.ChampionMaxLevel && player.ChampionExperience >= player.ChampionExperienceForNextLevel)
                {
                    player.ChampionLevelUp();
                    cllevel = true;
                }
                if (cllevel) //TODO: Out.Message (MLXP)
                    player.Out.SendMessage("You reached champion level " + player.ChampionLevel + "!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                //player.Out.SendMessage(LanguageMgr.GetTranslation(player.Client, "KingNPC.WhisperReceive.NewLevelMessage"), eChatType.CT_System, eChatLoc.CL_PopupWindow);
                return;
            }
        }

    }
}

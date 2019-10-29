
using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;
using DOL.Database;
using log4net;


namespace DOL.GS
{
    public class InteractiveTrainer : GameTrainer
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
            GameNPC npc = this;
            if (!base.Interact(player))
                return false;

            TurnTo(player, 50);

            if (player.Level < npc.Strength)
            {
                #region Advance Level

                    int curLevel = player.Level;
                    byte newLevel = Convert.ToByte(npc.Strength);
                    bool curSecondStage = player.IsLevelSecondStage;

                    if (newLevel > curLevel && curSecondStage)
                    {
                        player.GainExperience(GameLiving.eXPSource.Other, player.GetExperienceValueForLevel(++curLevel));
                    }
                    if (newLevel != curLevel || !curSecondStage)
                        player.Level = newLevel;
                    if (newLevel > 40)
                    {
                        if (curLevel < 40)
                            curLevel = 40;
                        for (int i = curLevel; i < newLevel; i++)
                        {

                        int specpoints = 0;
                        if (curSecondStage)
                                curSecondStage = false;
                            else
                            specpoints += player.CharacterClass.SpecPointsMultiplier * i / 20;
                    }
                    }
                

                #endregion

                player.GainRealmPoints(npc.MaxDistance);
                player.GainBountyPoints(npc.Dexterity);

                player.RefreshSpecDependantSkills(true);
                player.Out.SendUpdatePlayerSkills();
                player.Out.SendUpdatePlayer();
                player.Out.SendUpdatePoints();
                player.UpdatePlayerStatus();
                player.Health = player.MaxHealth;
                player.Endurance = player.MaxEndurance;
                player.Mana = player.MaxMana;

            }

            player.Out.SendTrainerWindow();
            if (player.Client.Account.PrivLevel > 1)
            {
                player.Out.SendMessage("I also happen to give out free [Respecs]." +
                    "\n Welcome " + player.Name + ", Would you like to change one my following feachers? \n" +
                "[Instant Level], [Instant Realm Points] or [Free Bounty Points]? \n\n", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                
            }
            player.Out.SendMessage("I also happen to give out free [Respecs].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            GameNPC npc = this;
            if (!base.WhisperReceive(source, str))
                return false;

            GamePlayer player = source as GamePlayer;

            if (player == null)
                return false;

            #region Instanst Levels
            if (str == "Instant Level")
            {
                player.Out.SendMessage("Please set what level you like new players to be.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                player.Out.SendMessage("[5][10][15][20][25][30][35][40][45][47][50]", eChatType.CT_System, eChatLoc.CL_PopupWindow);
              
            }

            if (str == "5") { player.Out.SendMessage("All new players will be given 5 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 5; }
            if (str == "10") { player.Out.SendMessage("All new players will be given 10 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 10; }
            if (str == "15") { player.Out.SendMessage("All new players will be given 15 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 15; }
            if (str == "20") { player.Out.SendMessage("All new players will be given 20 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 20; }
            if (str == "25") { player.Out.SendMessage("All new players will be given 25 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 25; }
            if (str == "30") { player.Out.SendMessage("All new players will be given 30 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 30; }
            if (str == "35") { player.Out.SendMessage("All new players will be given 35 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 35; }
            if (str == "40") { player.Out.SendMessage("All new players will be given 40 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 40; }
            if (str == "45") { player.Out.SendMessage("All new players will be given 45 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 45; }
            if (str == "47") { player.Out.SendMessage("All new players will be given 47 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 47; }
            if (str == "50") { player.Out.SendMessage("All new players will be given 50 free levels!", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Strength = 50; }
            #endregion Instanst Levels

            #region Free Bounty Points
            if (str == "Free Bounty Points")
            {
                player.Out.SendMessage("Please set the amount of Bounty Points you like new players to get.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                player.Out.SendMessage("[No BPs][200][400][600][800][1000][1200][1500][2000][4000][5000][7500][10000]", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                
            }

            if (str == "No BPs") { player.Out.SendMessage("All new players will not be give any free Bounty Points", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 0; }
            if (str == "200") { player.Out.SendMessage("All new players will be given  200 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 200; }
            if (str == "400") { player.Out.SendMessage("All new players will be given  400 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 400; }
            if (str == "600") { player.Out.SendMessage("All new players will be given  600 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 600; }
            if (str == "800") { player.Out.SendMessage("All new players will be given  800 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 800; }
            if (str == "1000") { player.Out.SendMessage("All new players will be given 1000 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 1000; }
            if (str == "1200") { player.Out.SendMessage("All new players will be given 1200 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 1200; }
            if (str == "1500") { player.Out.SendMessage("All new players will be given 1500 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 1500; }
            if (str == "2000") { player.Out.SendMessage("All new players will be given 2000 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 2000; }
            if (str == "4000") { player.Out.SendMessage("All new players will be given 4000 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 4000; }
            if (str == "5000") { player.Out.SendMessage("All new players will be given 5000 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 5000; }
            if (str == "7500") { player.Out.SendMessage("All new players will be given 7500 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 7500; }
            if (str == "10000") { player.Out.SendMessage("All new players will be given 10000 BPs", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Dexterity = 10000; }
            #endregion Free Bounty Points

            #region Instant Realm Points
            if (str == "Instant Realm Points")
            {
                player.Out.SendMessage("Please set the amount of realm points you like new players to get.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                player.Out.SendMessage("[No BRs][RR1][RR2][RR3][RR4][RR5][RR6][RR7][RR8][RR9][RR10][RR11][RR12]", eChatType.CT_System, eChatLoc.CL_PopupWindow);
               
            }

            if (str == "No RPs") { player.Out.SendMessage("All new players will not be give any free Realm Points", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.Constitution = 1; }
            if (str == "RR1") { player.Out.SendMessage("All new players will be given RR1", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 7125; }
            if (str == "RR2") { player.Out.SendMessage("All new players will be given RR2", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 61750; }
            if (str == "RR3") { player.Out.SendMessage("All new players will be given RR3", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 213875; }
            if (str == "RR4") { player.Out.SendMessage("All new players will be given RR4", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 513500; }
            if (str == "RR5") { player.Out.SendMessage("All new players will be given RR5", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 1010625; }
            if (str == "RR6") { player.Out.SendMessage("All new players will be given RR6", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 1755250; }
            if (str == "RR7") { player.Out.SendMessage("All new players will be given RR7", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 2797375; }
            if (str == "RR8") { player.Out.SendMessage("All new players will be given RR8", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 4187000; }
            if (str == "RR9") { player.Out.SendMessage("All new players will be given RR9", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 5974125; }
            if (str == "RR10") { player.Out.SendMessage("All new players will be given RR10", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 8208750; }
            if (str == "RR11") { player.Out.SendMessage("All new players will be given RR11", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 23308097; }
            if (str == "RR12") { player.Out.SendMessage("All new players will be given RR12", eChatType.CT_System, eChatLoc.CL_PopupWindow); npc.MaxDistance = 66181501; }
            #endregion Instant Realm Points

            npc.SaveIntoDatabase();
            return true;

            #region Respecs
            if (str == "Respecs")
            {
                player.Out.SendMessage("You currently have:\n" + player.RespecAmountAllSkill + " Full     skill respecs\n" + player.RespecAmountSingleSkill + " Single skill respecs\n" + player.RespecAmountRealmSkill + " Realm skill respecs\n" + player.RespecAmountChampionSkill + " Champion skill respecs", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                player.Out.SendMessage("Which would you like to buy:\n[Full], [Single], [Realm], [MasterLevel] or [Champion]?", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                return true;
            }

            if (str == "Full")
            {
                //first check if the player has too many
                if (player.RespecAmountAllSkill >= 5)
                {
                    player.Out.SendMessage("You already have " + player.RespecAmountAllSkill + " Full skill respecs, to use them simply target me and type /respec ALL", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return true;
                }
                //TODO next, check that the player can afford it

                //send dialog to player to confirm the purchase of the full skill respec
                player.Out.SendCustomDialog("Full Skill Respec price is: FREE Do you really want to buy one?", new CustomDialogResponse(RespecFullDialogResponse));
                return true;
            }

            if (str == "Single")
            {
                //first check if the player has too many
                if (player.RespecAmountSingleSkill >= 5)
                {
                    player.Out.SendMessage("You already have " + player.RespecAmountAllSkill + " Single skill Respecs, to use them simply target me and type /respec <line>", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return true;
                }

                //TODO next, check that the player can afford it
                //send dialog to player to confirm the purchase of the single skill respec
                player.Out.SendCustomDialog("Single Skill Respec price is: FREE Do you really want to buy one?", new CustomDialogResponse(RespecSingleDialogResponse));
                return true;
            }

            if (str == "Realm")
            {
                //first check if the player has too many
                if (player.RespecAmountRealmSkill >= 5)
                {
                    player.Out.SendMessage("You already have " + player.RespecAmountRealmSkill + " Realm skill respecs, to use them simply target me and type /respec Realm", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return true;
                }

                //TODO next, check that the player can afford it
                //send dialog to player to confirm the purchase of the full skill respec
                player.Out.SendCustomDialog("Realm Skill Respec price is: FREE Do you really want to buy one?", new CustomDialogResponse(RespecRealmDialogResponse));
                return true;
            }

            if (str == "ChampionLevel")
            {
                if (player.RespecAmountChampionSkill >= 5)
                {
                    player.Out.SendMessage("You already have " + player.RespecAmountChampionSkill + " Champion skill respecs, to use them please visit the Champion Level Master.", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                    return true;
                }

                //TODO next, check that the player can afford it
                //send dialog to player to confirm the purchase of the Champion skill respec token
                player.Out.SendCustomDialog("CL Skill Respec price is: FREE Do you really want to buy one?", new CustomDialogResponse(RespecChampionDialogResponse));
                return true;
            }
            #endregion Respec

            player.Out.SendTrainerWindow();
            return true;
        }

        #region TrainSpecLine
        public void TrainSpecLine(GamePlayer player, string line, int points)
        {
            if (player == null)
                return;

            if (!(player.TargetObject is GameTrainer))
            {
                player.Out.SendMessage("You must have your trainer targetted to be trained in a specialization line.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            if ((points <= 0) || (points >= 51))
            {
                player.Out.SendMessage("An Error occurred, there was an invalid amount to train: " + points + " points is not valid!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            int target = points;
            Specialization spec = player.GetSpecializationByName(line);
            if (spec == null)
            {
                player.Out.SendMessage("An Error occurred, there was an invalid line name: " + line + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            int current = spec.Level;
            if (current >= player.Level)
            {
                player.Out.SendMessage("You can't train in " + line + " again this level.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            if (points <= current)
            {
                player.Out.SendMessage("You have already trained this amount in " + line + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            target = target - current;
            ushort skillspecialtypoints = 0;
            int speclevel = 0;
            bool changed = false;
            for (int i = 0; i < target; i++)
            {
                if (spec.Level + speclevel >= player.Level)
                {
                    player.Out.SendMessage("You can't train in " + line + " again this level!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    break;
                }

                if ((player.SkillSpecialtyPoints + player.GetAutoTrainPoints(spec, 3)) - skillspecialtypoints >= (spec.Level + speclevel) + 1)
                {
                    changed = true;
                    skillspecialtypoints += (ushort)((spec.Level + speclevel) + 1);
                    if (spec.Level + speclevel < player.Level / 4 && player.GetAutoTrainPoints(spec, 4) != 0)
                        skillspecialtypoints -= (ushort)((spec.Level + speclevel) + 1);
                    speclevel++;
                }
                else
                {
                    player.Out.SendMessage("That specialization costs " + (spec.Level + 1) + " specialization points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    player.Out.SendMessage("You don't have that many specialization points left for this level.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    break;
                }
            }
            if (changed)
            {
                if (player.SkillSpecialtyPoints >= skillspecialtypoints)
                    spec.Level += speclevel;
                player.OnSkillTrained(spec);
                player.Out.SendUpdatePoints();
                player.Out.SendTrainerWindow();
                player.Out.SendMessage("You now have " + points + " points in the " + line + " line!", eChatType.CT_System, eChatLoc.CL_PopupWindow);
            }
            return;
        }
        #endregion TrainSpecLine
        #region RespecDialogResponse
        protected void RespecFullDialogResponse(GamePlayer player, byte response)
        {
            if (response != 0x01) return; //declined
            player.RespecAmountAllSkill++;
            player.Out.SendMessage("You just bought a Full skill respec!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
            player.Out.SendMessage("Target the trainer and type /respec ALL to use it!", eChatType.CT_System, eChatLoc.CL_SystemWindow);

        }
        protected void RespecSingleDialogResponse(GamePlayer player, byte response)
        {
            if (response != 0x01) return; //declined
            player.RespecAmountSingleSkill++;
            player.Out.SendMessage("You just bought a Single skill respec!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
            player.Out.SendMessage("Target the trainer and type /respec <line> to use it!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
        }
        protected void RespecRealmDialogResponse(GamePlayer player, byte response)
        {
            if (response != 0x01) return; //declined
            player.RespecAmountRealmSkill++;
            player.Out.SendMessage("You just bought a Realm skill respec!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
            player.Out.SendMessage("Target the trainer and type /respec Realm to use it!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
        }

        protected void RespecChampionDialogResponse(GamePlayer player, byte response)
        {
            if (response != 0x01) return; //declined
            player.RespecAmountChampionSkill++;
            player.Out.SendMessage("You just bought a Champion skill respec!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
            player.Out.SendMessage("Please visit the Champion Level master to use it!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
        }
        #endregion RespecDialogResponse
    }
}
/*
 * Auto-Insta 5 along with insta 50 on class selection.
 * Nothing Special But 3 npcs in one. =)
 * Written By: Rohadoc
 * Edited By: Kedrik
 * Edited again by BluRaven I added a gender check for the Valkyrie, it now correctly offers Valk
 * to only female's and to the right races. 
 * Also, I have it set to level the player to 5, pick the class, then level to 50.
 * also I added training options for alb, mid, and hib minotuar's.
 * Also fixed the specpoints, they were not being added properly.
 * It's originally written by Rohadoc.
 * Updated By: Etaew (11 January 2008)
 * Updated By BluRaven 10/20/08: Re-added Bainshee, Vampiir, Animist and Mauler.
 * Autotrain points wasn't working, moved it to after the player is made 50 to avoid autotraining lines that don't get
 * added to the player untill after (s)he levels passed 5.  Also updated scout & ranger autotrain line to 'Archery'.
 * Trainer will now give a set of free armor and a set of weapons according to the players available abilities.
 * Trainer will now sell Full, Single, and Realm respecs (no turn in item, just directly adds the respec to the player)
 * Added Templates for all classes for easy one click training for the players to common/popular spec templates for
 * their class. This is a feature I've seen on other shards that wasn't publicly available.  No one shared it with me,
 * I painstakenly re-created the code for them on my own.
 * 
 */
//Edited by FinalFury
//reworked by Shadexx
using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;
using DOL.Database;
using log4net;


namespace DOL.GS.Trainer
{
    public class Auto : GameTrainer
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Declarations
        private static ItemTemplate free_epic_cloak = null; //Free Epic Cloak

        private static ItemTemplate free_chain_vest = null; //Free Chain Vest
        private static ItemTemplate free_chain_arms = null; //Free Chain Arms
        private static ItemTemplate free_chain_legs = null; //Free Chain Legs
        private static ItemTemplate free_chain_helm = null; //Free Chain Helm
        private static ItemTemplate free_chain_gloves = null; //Free Chain Gloves
        private static ItemTemplate free_chain_boots = null; //Free Chain Boots

        private static ItemTemplate free_cloth_vest = null; //Free Cloth Vest
        private static ItemTemplate free_cloth_arms = null; //Free Cloth Arms
        private static ItemTemplate free_cloth_legs = null; //Free Cloth Legs
        private static ItemTemplate free_cloth_helm = null; //Free Cloth Helm
        private static ItemTemplate free_cloth_gloves = null; //Free Cloth Gloves
        private static ItemTemplate free_cloth_boots = null; //Free Cloth Boots

        private static ItemTemplate free_leather_vest = null; //Free Leather Vest
        private static ItemTemplate free_leather_arms = null; //Free Leather Arms
        private static ItemTemplate free_leather_legs = null; //Free Leather Legs
        private static ItemTemplate free_leather_helm = null; //Free Leather Helm
        private static ItemTemplate free_leather_gloves = null; //Free Leather Gloves
        private static ItemTemplate free_leather_boots = null; //Free Leather Boots

        private static ItemTemplate free_studded_vest = null; //Free Studded Vest
        private static ItemTemplate free_studded_arms = null; //Free Studded Arms
        private static ItemTemplate free_studded_legs = null; //Free Studded Legs
        private static ItemTemplate free_studded_helm = null; //Free Studded Helm
        private static ItemTemplate free_studded_gloves = null; //Free Studded Gloves
        private static ItemTemplate free_studded_boots = null; //Free Studded Boots

        private static ItemTemplate free_plate_vest = null; //Free Plate Vest
        private static ItemTemplate free_plate_arms = null; //Free Plate Arms
        private static ItemTemplate free_plate_legs = null; //Free Plate Legs
        private static ItemTemplate free_plate_helm = null; //Free Plate Helm
        private static ItemTemplate free_plate_gloves = null; //Free Plate Gloves
        private static ItemTemplate free_plate_boots = null; //Free Plate Boots

        private static ItemTemplate free_belt = null; //Free Belt
        private static ItemTemplate free_jewel = null; //Free Jewel
        private static ItemTemplate free_stats_ring = null; //Free Stats Ring
        private static ItemTemplate free_overcap_ring = null; //Free Overcap Ring
        private static ItemTemplate free_resists_bracer = null; //Free Resists Bracer
        private static ItemTemplate free_skill_bracer = null; //Free Skill Bracer
        private static ItemTemplate free_necklace = null; //Free Necklace

        //weapons
        private static ItemTemplate free_staff = null; //Free Caster Staff
        private static ItemTemplate free_slash = null; //Free Slash
        private static ItemTemplate free_thrust = null; //Free Thrust
        private static ItemTemplate free_crush = null; //Free Crush
        private static ItemTemplate free_axe = null; //Free Axe
        private static ItemTemplate free_long_bow = null; //Free Long Bow
        private static ItemTemplate free_composite_bow = null; //Free Composite Bow
        private static ItemTemplate free_recurved_bow = null; //Free Recurved Bow
        private static ItemTemplate free_short_bow = null; //Free Short Bow
        private static ItemTemplate free_crossbow = null; //Free Crossbow
        private static ItemTemplate free_twohanded_slash = null; //Free TwoHanded Slash
        private static ItemTemplate free_twohanded_crush = null; //Free TwoHanded Crush
        private static ItemTemplate free_twohanded_axe = null; //Free TwoHanded Axe
        private static ItemTemplate free_twohanded_sword = null; //Free TwoHanded Sword
        private static ItemTemplate free_twohanded_hammer = null; //Free TwoHanded Hammer
        private static ItemTemplate free_small_shield = null; //Free Small Shield
        private static ItemTemplate free_medium_shield = null; //Free Medium Shield
        private static ItemTemplate free_large_shield = null; //Free Large Shield
        private static ItemTemplate free_slash_spear = null; //Free Slash Spear
        private static ItemTemplate free_thrust_spear = null; //Free Thrust Spear
        private static ItemTemplate free_slash_flail = null; //Free Slash Flail
        private static ItemTemplate free_thrust_flail = null; //Free Thrust Flail
        private static ItemTemplate free_crush_flail = null; //Free Crush Flail
        private static ItemTemplate free_slash_claw = null; //Free Slash Claw
        private static ItemTemplate free_thrust_claw = null; //Free Thrust Claw
        private static ItemTemplate free_harp = null; //Free Harp
        private static ItemTemplate free_mauler_staff = null; //Free Mauler Staff
        private static ItemTemplate free_mauler_fist_wrap = null; //Free Mauler Fist Wrap
        private static ItemTemplate free_scythe = null; //Free Scythe
        private static ItemTemplate free_crush_polearm = null; //Free Crush Polearm
        private static ItemTemplate free_thrust_polearm = null; //Free Thrust Polearm
        private static ItemTemplate free_slash_polearm = null; //Free Slash Polearm

        private static ItemTemplate mlrespectoken = null;

        #endregion Declarations

        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            if (log.IsInfoEnabled)
                log.Info("Master Trainer Initializing...");
            #region ML Respec
            mlrespectoken = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("mlrespectoken");
            if (mlrespectoken == null)
            {
                mlrespectoken = new ItemTemplate();
                mlrespectoken.Id_nb = "mlrespectoken";
                mlrespectoken.Name = "ML Respec Token";
                mlrespectoken.Level = 50;
                mlrespectoken.Item_Type = 40;
                mlrespectoken.Model = 485;
                mlrespectoken.IsTradable = true;
                mlrespectoken.Object_Type = 0;
                mlrespectoken.Quality = 100;
                mlrespectoken.Weight = 1;
                mlrespectoken.MaxCondition = 100;
                mlrespectoken.MaxDurability = 100;
                mlrespectoken.Condition = 100;
                mlrespectoken.Durability = 100;
                GameServer.Database.AddObject(mlrespectoken);
            }
            #endregion ML Respec

        }


        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            //FinalFury: Reduced 5000 to 250, causes lag on some systems
            TurnTo(player, 250);

            if (player.Level < 50)
            {
                player.GainRealmPoints(0);

                #region Advance Level

                int curLevel = player.Level;
                byte newLevel = 50;
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

                player.RefreshSpecDependantSkills(true);
                player.Out.SendUpdatePlayerSkills();
                player.Out.SendUpdatePlayer();
                player.Out.SendUpdatePoints();
                player.UpdatePlayerStatus();
                player.Health = player.MaxHealth;
                player.Endurance = player.MaxEndurance;
                player.Mana = player.MaxMana;
            }

                player.Out.SendMessage("I also happen to give out free [Respecs].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);

            player.Out.SendTrainerWindow();
            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str))
                return false;

            GamePlayer player = source as GamePlayer;

            if (player == null)
                return false;



            //FinalFury: Removed ML Respec
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

        
        //FinalFury: Removed free gear
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
        //FinalFury: Removed ML text response
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

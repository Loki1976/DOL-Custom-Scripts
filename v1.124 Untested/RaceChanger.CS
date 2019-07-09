
using System;
using System.Collections.Generic;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.Effects;
using DOL.GS.Housing;
using DOL.GS.PacketHandler;
using System.Collections;
using DOL.Database;
using DOL.Language;

namespace DOL.GS.Scripts
{
    public class ZoraiNPC : GameNPC
    {

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;

            

            player.Client.Out.SendMessage(
                "Hello would you like to become part of the _____ [Race]? Or maybe you would like to know more about the [Chosen] Race?",
                eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            return true;
        }

        public override bool AddToWorld()
        {
            
            base.AddToWorld();
            return true;
        }


        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;

            if (!(source is GamePlayer)) return false;

            GamePlayer player = (GamePlayer)source;

            TurnTo(player.X, player.Y);

            switch (str)
            {



                    //This is set for Hib base classes, but you can easily change it.
                case "Race":
                    {
                        if (player.Level == 1 && player.DBCharacter.Gender == 1 /*Female*/ && (player.CharacterClass.ID == (int)eCharacterClass.Magician    
                            || player.CharacterClass.ID == (int)eCharacterClass.Guardian || player.CharacterClass.ID == (int)eCharacterClass.Stalker
                            || player.CharacterClass.ID == (int)eCharacterClass.Naturalist || player.CharacterClass.ID == (int)eCharacterClass.Forester))
                        {
                            player.Client.Out.SendMessage(
                    "Welcome to the [Chosen] way of life!",
                    eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                          

                            player.Race = (int)eRace.RaceName;
                           
                            player.Model = 890;
                            player.DBCharacter.CurrentModel = 890; //change model

                            //remove stats
                            player.ChangeBaseStat(eStat.STR, (short)(-player.Strength));
                            player.ChangeBaseStat(eStat.CON, (short)(-player.Constitution));
                            player.ChangeBaseStat(eStat.DEX, (short)(-player.Dexterity));
                            player.ChangeBaseStat(eStat.QUI, (short)(-player.Quickness));
                            player.ChangeBaseStat(eStat.INT, (short)(-player.Intelligence));
                            player.ChangeBaseStat(eStat.PIE, (short)(-player.Piety));
                            player.ChangeBaseStat(eStat.EMP, (short)(-player.Empathy));
                            player.ChangeBaseStat(eStat.CHR, (short)(-player.Charisma));

                            //add stats

                            player.ChangeBaseStat(eStat.STR, 60);
                            player.ChangeBaseStat(eStat.CON, 50);
                            player.ChangeBaseStat(eStat.DEX, 60);
                            player.ChangeBaseStat(eStat.QUI, 60);
                            player.ChangeBaseStat(eStat.INT, 70);
                            player.ChangeBaseStat(eStat.PIE, 55);
                            player.ChangeBaseStat(eStat.EMP, 45);
                            player.ChangeBaseStat(eStat.CHR, 45);

                            //some player updating
                            player.Out.SendUpdatePlayer();
                            player.Out.SendUpdatePoints();
                            player.Out.SendCharStatsUpdate();
                            player.UpdatePlayerStatus();
                            player.SaveIntoDatabase();

                            break;

                        }

                        else if (player.Level == 1 && player.DBCharacter.Gender == 0  /*Male*/ && (player.CharacterClass.ID == (int)eCharacterClass.Magician   
                            || player.CharacterClass.ID == (int)eCharacterClass.Guardian || player.CharacterClass.ID == (int)eCharacterClass.Stalker
                            || player.CharacterClass.ID == (int)eCharacterClass.Naturalist || player.CharacterClass.ID == (int)eCharacterClass.Forester))
                        {
                            player.Client.Out.SendMessage(
                    "Welcome to the [Chosen] way of life!",
                    eChatType.CT_Say, eChatLoc.CL_PopupWindow);

                            player.Race = (int)eRace.Zorai;
                            player.Model = 1211;
                            player.DBCharacter.CurrentModel = 1211;
                            player.ChangeBaseStat(eStat.STR, (short)(- player.Strength));
                            player.ChangeBaseStat(eStat.CON, (short)(-player.Constitution));
                            player.ChangeBaseStat(eStat.DEX, (short)(-player.Dexterity));
                            player.ChangeBaseStat(eStat.QUI, (short)(-player.Quickness));
                            player.ChangeBaseStat(eStat.INT, (short)(-player.Intelligence));
                            player.ChangeBaseStat(eStat.PIE, (short)(-player.Piety));
                            player.ChangeBaseStat(eStat.EMP, (short)(-player.Empathy));
                            player.ChangeBaseStat(eStat.CHR, (short)(-player.Charisma));

                            player.ChangeBaseStat(eStat.STR, 60);
                            player.ChangeBaseStat(eStat.CON, 50);
                            player.ChangeBaseStat(eStat.DEX, 60);
                            player.ChangeBaseStat(eStat.QUI, 60);
                            player.ChangeBaseStat(eStat.INT, 70);
                            player.ChangeBaseStat(eStat.PIE, 55);
                            player.ChangeBaseStat(eStat.EMP, 45);
                            player.ChangeBaseStat(eStat.CHR, 45);

                            player.Out.SendUpdatePlayer();
                            player.Out.SendUpdatePoints();
                            player.Out.SendCharStatsUpdate();
                            player.UpdatePlayerStatus();
                            player.SaveIntoDatabase();

                            break;

                        }

                        else
                        {
                            player.Client.Out.SendMessage(
                    "You must be a level 1 Magician, Guardian, Stalker, Forester, or Naturalist to become a [Chosen]!",
                    eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                            break;
                        }

                    }
                    break;

                case "Chosen":
                    {
                        player.Client.Out.SendMessage(
                "Put some info about the race here. If its custom or not, you could always list its stats/resists and possible classes",
                eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                        break;
                    }
                    break;


            }
            return true;
        }
     
    }
}
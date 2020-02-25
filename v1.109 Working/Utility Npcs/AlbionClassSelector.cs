/*Origanlly made by Tola
 * Remade by Wicurcyn
 * This script is made for Albs and Albs alone
 * It allows you to choose any alb class you desire
 * it will also give your character 50g for crafting
 * Characters must be level 5 to use this NPC
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DOL.Database;
using DOL.Database.Attributes;
using DOL.Events;
using DOL.GS;
using DOL.GS.Keeps;
using DOL.GS.Spells;
using DOL.GS.Effects;
using DOL.GS.PacketHandler;

using System.Reflection;
using log4net;


namespace DOL.GS.Scripts
{
    public class AlbionClassSelector : GameNPC    {
        
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private enum eCharacterClass : byte
        {
            //alb classes
            Armsman = 2,
            Cabalist = 13,
            Cleric = 6,
            Friar = 10,
            Heretic = 33,
            Infiltrator = 9,
            Mercenary = 11,
            Minstrel = 4,
            Necromancer = 12,
            Paladin = 1,
            Reaver = 19,
            Scout = 3,
            Sorcerer = 8,
            Theurgist = 5,
            Wizard = 7,
            MaulerAlb = 60,

        }

        public override eQuestIndicator GetQuestIndicator(GamePlayer player)
        {
            if (player.Level == 1) return eQuestIndicator.Available;
            return eQuestIndicator.None;
        }

        public override bool Interact(GamePlayer p)
        {
            GamePlayer player = p as GamePlayer;

            if (p == null)
                return false;


           if (player.Level != 1)
            {
                SayTo(player, "Hello!  You must be a new level one character to use my services, sorry.");
                return false;
            }
			
            if (base.Interact(player))
            {
                SayTo(player, "Hello! you may choose your class! And continue to be a great adventurer! Also since i take away all of your armor I will give you 50g to go craft with!");
                SayTo(player, "Beware, once a choice is made your new class will be set and you will be logged out of the game.  You can then return as your new class!");
				
                int column = 0;
                string classes = "";

                foreach (eCharacterClass cl in Enum.GetValues(typeof(eCharacterClass)))
                {
                    if (column++ > 4)
                    {
                        player.Out.SendMessage(classes, eChatType.CT_System, eChatLoc.CL_PopupWindow);
                        column = 0;
                        classes = "";
                    }

                    classes += " [" + Enum.GetName(typeof(eCharacterClass), cl) + "] ";
                }

                if (classes != "")
                    player.Out.SendMessage(classes, eChatType.CT_System, eChatLoc.CL_PopupWindow);

            }

            return false;
        }
        public override bool WhisperReceive(GameLiving source, string text)
        {
            GamePlayer player = source as GamePlayer;

            if (player == null || player.Level != 5)
                return false;

            if (base.WhisperReceive(source, text))
            {
                foreach (eCharacterClass cl in Enum.GetValues(typeof(eCharacterClass)))
                {
                    if (Enum.GetName(typeof(eCharacterClass), cl) == text)
                    {
                        if ((text == "Bainshee" || text == "Valkyrie"))//&amp;&amp; player.Gender != GS.eGender.Female)
                        {
                            //  SayTo(player, "You must be female to be a Valkyrie or Bainshee!");
                            //return false;
                        }

                        SetClass(player, (int)cl);
                        return true;
                    }
                }

            }

            return false;
        }
        
        public override bool AddToWorld()
        {
            Name = "Albion Class Selector";
            Level = 51;
            Flags = eFlags.PEACE;            
            Realm = eRealm.Albion;
            Model = 41;
            Size = 150;
            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            template.AddNPCEquipment(eInventorySlot.TorsoArmor, 4000, 66, 0, 5); //model, color, effect, extension
            template.AddNPCEquipment(eInventorySlot.ArmsArmor, 4002, 66, 0, 0);
            template.AddNPCEquipment(eInventorySlot.LegsArmor, 4001, 66, 0);
            template.AddNPCEquipment(eInventorySlot.HandsArmor, 4003, 66, 0, 5);
            template.AddNPCEquipment(eInventorySlot.HeadArmor, 4053, 66, 0, 0);
            template.AddNPCEquipment(eInventorySlot.Cloak, 3800, 0, 0, 0);
            template.AddNPCEquipment(eInventorySlot.FeetArmor, 4004, 66, 0, 5);
            Inventory = template.CloseTemplate();
            return base.AddToWorld();
        }


        private void SetClass(GamePlayer player, int newClassID)
        {
            //remove all their tricks and abilities!
            player.Reset();
            //player.RemoveAllSkills();
            player.RemoveAllSpecs();
            player.RemoveAllSpellLines();
            player.RemoveAllStyles();
            player.RespecChampionSkills();
            player.RemoveSpecialization("");
            player.RemoveAbility("");
            player.Inventory.ClearInventory();
            player.AddMoney(500000);
            
           

           

            //reset before, and after changing the class.          
            player.SetCharacterClass(newClassID);
            player.RespecAll(); 

            player.SaveIntoDatabase();

            player.Out.SendPlayerQuit(false);
            player.Quit(true);
        }       
    }
}




/*
 * public override bool WhisperReceive(GameLiving source, string text)
        {
            GamePlayer player = source as GamePlayer;

            if (player == null || player.Level != 5) //<--
                return false;

            if (base.WhisperReceive(source, text))
            {
                foreach (eCharacterClass cl in Enum.GetValues(typeof(eCharacterClass)))
                {
                    if (Enum.GetName(typeof(eCharacterClass), cl) == text)
                    {
                        if ((text == "Valkyrie" || text == "Bainshee"))  //(player.Gender != GS.eGender.Female);
                        {
                            //SayTo(player, "You must be female to be a Valkyrie or Bainshee!");
                            // return false;
                        }

                        SetClass(player, (int)cl);
                        return true;
                    }
*/
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

using DOL.Storm;

using System.Reflection;
using log4net;


namespace DOL.Storm
{
    public class ClassSelector : GameNPC
    {
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

            //mid classes
            Berserker = 31,
            Bonedancer = 30,
            Healer = 26,
            Hunter = 25,
            Runemaster = 29,
            Savage = 32,
            Shadowblade = 23,
            Shaman = 28,
            Skald = 24,
            Spiritmaster = 27,
            Thane = 21,
            Valkyrie = 34,
            Warlock = 59,
            Warrior = 22,
            MaulerMid = 61,

            //hib classes
            Animist = 55,
            Bainshee = 39,
            Bard = 48,
            Blademaster = 43,
            Champion = 45,
            Druid = 47,
            Eldritch = 40,
            Enchanter = 41,
            Hero = 44,
            Mentalist = 42,
            Nightshade = 49,
            Ranger = 50,
            Valewalker = 56,
            Vampiir = 58,
            Warden = 46,
            MaulerHib = 62,
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
                SayTo(player, "Hello! Since you're a level one D2 player I can set your class to anything you desire, just choose an entry from the list below.");
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

            if (player == null || player.Level != 1)
                return false;

            if (base.WhisperReceive(source, text))
            {
                foreach (eCharacterClass cl in Enum.GetValues(typeof(eCharacterClass)))
                {
                    if (Enum.GetName(typeof(eCharacterClass), cl) == text)
                    {
                        if ((text == "Valkyrie" || text == "Bainshee") && player.Gender != GS.eGender.Female)
                        {
                            SayTo(player, "You must be female to be a Valkyrie or Bainshee!");
                            return false;
                        }

                        SetClass(player, (int)cl);
                        return true;
                    }
                }

            }

            return false;
        }


        private void SetClass(GamePlayer player, int newClassID)
        {
            //remove all their tricks and abilities!
            //player.RemoveAllSkills();
            player.RemoveAllSpecs();
            player.RemoveAllSpellLines();
            player.RemoveAllStyles();
            player.RespecChampionSkills();

            //reset before, and after changing the class.
            player.Reset();
            player.SetCharacterClass(newClassID);
            player.Reset();

            player.SaveIntoDatabase();

            player.Out.SendPlayerQuit(false);
            player.Quit(true);
        }

    }
}
///////////////////////////////////////////////////
////////     PowerTrainer v1.0             ////////
////////     Copyright 2004 by Deagol      ////////
////////     Translated by Biceps          ////////
///////////////////////////////////////////////////

using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Collections;
using DOL.Database;
namespace DOL.GS.Scripts
{
	public class PowerTrainer: GameNPC
	{
		const int FREE_LEVEL = 50;

        public override bool AddToWorld()
        {
            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            switch (Realm)
            {
                case eRealm.Albion:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2230); break;
                case eRealm.Midgard:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2232);
                    template.AddNPCEquipment(eInventorySlot.ArmsArmor, 2233);
                    template.AddNPCEquipment(eInventorySlot.LegsArmor, 2234);
                    template.AddNPCEquipment(eInventorySlot.HandsArmor, 2235);
                    template.AddNPCEquipment(eInventorySlot.FeetArmor, 2236);
                    break;
                case eRealm.Hibernia:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2231); ; break;
            }

            Inventory = template.CloseTemplate();
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }
		/// <summary>
		/// For Recieving Respec Stones. 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public override bool ReceiveItem(GameLiving source, InventoryItem item)
		{
			if (source == null || item == null) return false;

			GamePlayer player = source as GamePlayer;
			if (player != null)
			{
				switch (item.Id_nb)
				{
					case "respec_single":
						{
							player.Inventory.RemoveCountFromStack(item, 1);
							player.RespecAmountSingleSkill++;
							player.Out.SendMessage("Thanks, I added a single spec respec to use /respec <linename>", eChatType.CT_System, eChatLoc.CL_PopupWindow);
							return true;
						}
					case "respec_full":
						{
							player.Inventory.RemoveCountFromStack(item, 1);
							player.RespecAmountAllSkill++;
							player.Out.SendMessage("A nice " + item.Name + "! I added a full respec to use /respec all", eChatType.CT_System, eChatLoc.CL_PopupWindow);
							return true;
						}
					case "respec_realm":
						{
							player.Inventory.RemoveCountFromStack(item, 1);
							player.RespecAmountRealmSkill++;
							player.Out.SendMessage("Thanks, I added a realm respec to use /respec realm", eChatType.CT_System, eChatLoc.CL_PopupWindow);
							return true;
						}
				}
			}


			return base.ReceiveItem(source, item);
		}

		public override bool Interact(GamePlayer player)
		{	
            if (!base.Interact(player)) return false;
            TurnTo(player, 30);
            if ((player.Level >= 1) && (player.Level < 5))
            {
                player.Level = 5;
                player.Health = player.MaxHealth;
                player.Endurance = player.MaxEndurance;
                player.Mana = player.MaxMana;
            }
			if (player.Level<5) player.Out.SendMessage("You are not ready to take on a profession yet. You have to reach the level 5 first.", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			else
			{
				if ((player.CharacterClass.BaseName==player.CharacterClass.Name)&&(player.Level==5))
				{
					player.Out.SendMessage("", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
					switch(player.CharacterClass.ID)
					{
						case 16:
							if ((player.RaceName=="Avalonian")||(player.RaceName=="AlbionMinotaur")||(player.RaceName=="Highlander")) player.Out.SendMessage("You can only become a [Cleric].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Briton") player.Out.SendMessage("You can become a [Cleric] or a [Friar].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Avalonian") || (player.RaceName == "Briton") || (player.RaceName == "Inconnu")) player.Out.SendMessage("You can become a [Heretic] or [Priest].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							break;
						case 20:
							player.Out.SendMessage("You can become a [Necromancer] if you want.", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
					break;
						case 15:
							player.Out.SendMessage("You can become a [Wizard] or a [Theurgist].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 14:
							if ((player.RaceName=="Briton")||(player.RaceName=="Saracen")) player.Out.SendMessage("You can become an [Armsman], a [Mercenary], [Paladin], or a [Reaver].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Avalonian")||(player.RaceName=="Highlander")) player.Out.SendMessage("You can become an [Armsman], [Mercenary], or a [Paladin].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Inconnu") player.Out.SendMessage("You can become an [Armsman], a [Mercenary] or a [Reaver].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Half Ogre")|| (player.RaceName == "AlbionMinotaur")) player.Out.SendMessage("You can become an [Armsman] or a [Mercenary].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							break;
						case 18:
							player.Out.SendMessage("You can become a [Cabalist] or a [Sorcerer].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 17:
							if ((player.RaceName=="Briton")||(player.RaceName=="Saracen")) player.Out.SendMessage("You can become an [Infiltrator], a [Minstrel] or a [Scout].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Highlander") || (player.RaceName=="AlbionMinotaur")) player.Out.SendMessage("You can become a [Minstrel] or a [Scout].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Inconnu") player.Out.SendMessage("You can become an [Infiltrator] or a [Scout].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 35:
							if ((player.RaceName=="Norseman")||(player.RaceName=="Troll")||(player.RaceName=="Dwarf")||(player.RaceName=="Frostalf")) player.Out.SendMessage("You can become a [Warrior], a [Thane], a [Berserker], a [Skald] or a [Savage].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Kobold") player.Out.SendMessage("You can become a [Warrior], a [Skald] or a [Savage].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Valkyn")||(player.RaceName == "MidgardMinotaur")) player.Out.SendMessage("You can become a [Warrior], a [Berserker] or a [Savage].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Frostalf") player.Out.SendMessage("You can only become a [Thane]", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Norseman") || (player.RaceName == "Frostalf") || (player.RaceName == "Dwarf")) player.Out.SendMessage("You can only become a [Valkyrie]", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							break;
						case 38:
							if ((player.RaceName=="Norseman")||(player.RaceName=="Frostalf")||(player.RaceName=="Kobold")||(player.RaceName=="Valkyn")) player.Out.SendMessage("You can become a [Hunter] or a [Shadowblade].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Dwarf") player.Out.SendMessage("You can only become a [Hunter].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 36:
                            if (player.RaceName == "Kobold") player.Out.SendMessage("You can become a [Runemaster], [Spiritmaster] or a [Warlock].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Norseman") || (player.RaceName == "Frostalf")) player.Out.SendMessage("You can become a [Runemaster], [Warlock] or a [Spiritmaster].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Troll")||(player.RaceName=="Valkyn")) player.Out.SendMessage("You can only become a [Bonedancer].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Dwarf") player.Out.SendMessage("You can only become a [Runemaster].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 37:
							if (player.RaceName=="Frostalf") player.Out.SendMessage("You can become a [Healer] or a [Shaman].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Norseman")||(player.RaceName=="Dwarf")) player.Out.SendMessage("You can only become a [Healer].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Kobold")||(player.RaceName=="Troll")||(player.RaceName == "MidgardMinotaur")) player.Out.SendMessage("You can only become a [Shaman].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
							case 52:
                            if ((player.RaceName == "Celt") || (player.RaceName == "Shar") || (player.RaceName == "Lurikeen")) player.Out.SendMessage("You can become a [Vampiir].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Celt")||(player.RaceName=="Shar")||(player.RaceName=="Lurikeen")) player.Out.SendMessage("You can become a [Blademaster], a [Hero] or a [Champion].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Elf") || (player.RaceName == "HiberniaMinotaur")) player.Out.SendMessage("You can become a [Blademaster] or a [Champion].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Firbolg") player.Out.SendMessage("You can become a [Blademaster] or a [Hero].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Lurikeen") player.Out.SendMessage("You can become a [Champion] or a [Hero].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Sylvan") player.Out.SendMessage("You can only become a [Hero].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 51:
                            if ((player.RaceName == "Celt") || (player.RaceName == "Shar") || (player.RaceName == "Lurikeen")) player.Out.SendMessage("You can become a [Vampiir].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Elf")||(player.RaceName=="Lurikeen")) player.Out.SendMessage("You can become an [Eldritch], an [Enchanter] or a [Mentalist].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                            if ((player.RaceName == "Celt") || (player.RaceName == "Firbolg") || (player.RaceName == "Sylvan")) player.Out.SendMessage("You can become a [Mentalist].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
							break;
						case 53:
							if ((player.RaceName=="Celt")||(player.RaceName=="Firbolg")) player.Out.SendMessage("You can become a [Bard], a [Druid] or a [Warden].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if ((player.RaceName=="Sylvan")|| (player.RaceName == "HiberniaMinotaur")) player.Out.SendMessage("You can become a [Druid] or a [Warden].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 54:
							if ((player.RaceName=="Elf")||(player.RaceName=="Lurikeen")||(player.RaceName=="Celt")) player.Out.SendMessage("You can become a [Nightshade] or a [Ranger].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							if (player.RaceName=="Shar") player.Out.SendMessage("You can only become a [Ranger].", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						case 57:
							player.Out.SendMessage("You can only become a [Valewalker]", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
							break;
						default: break;
					}
				}
				if ((player.CharacterClass.BaseName!=player.CharacterClass.Name)&&(player.Level==5)) player.Out.SendMessage("I am just here to help you to select your skills. I will not help you to gain experience.", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
				if ((player.CharacterClass.BaseName!=player.CharacterClass.Name)&&(player.Level>5)) player.Out.SendTrainerWindow();
			}
			return true;
		}
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer player = (GamePlayer) source;
			if(player.CharacterClass.Name==player.CharacterClass.BaseName)
			{
				switch(str)
				{
// __Albion________________________________________________________________________________________________________________
					case "Armsman":
                        if (((player.RaceName == "Inconnu") || (player.RaceName == "Half Ogre") || (player.RaceName == "AlbionMinotaur") || (player.RaceName == "Briton") || (player.RaceName == "Highlander") || (player.RaceName == "Saracen") || (player.RaceName == "Avalonian")) && (player.CharacterClass.ID == 14))
							PromotePlayer(player, (int)eCharacterClass.Armsman, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now an Armsman!");
						break;
					case "Cabalist":
						if (((player.RaceName=="Briton")||(player.RaceName=="Avalonian")||(player.RaceName=="Inconnu")||(player.RaceName=="Half Ogre")||(player.RaceName=="Saracen")) && (player.CharacterClass.ID==18))
							PromotePlayer(player, (int)eCharacterClass.Cabalist, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Cabalist!");
						break;
					case "Cleric":
                        if (((player.RaceName == "Briton") || (player.RaceName == "AlbionMinotaur") || (player.RaceName == "Avalonian") || (player.RaceName == "Highlander")) && (player.CharacterClass.ID == 16))
							PromotePlayer(player, (int)eCharacterClass.Cleric, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Cleric!");
						break;
					case "Friar":
						if (((player.RaceName=="Briton")||(player.RaceName=="Saracen")) && (player.CharacterClass.ID==16))
							PromotePlayer(player, (int)eCharacterClass.Friar, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Friar!");		
						break;
					case "Infiltrator":
						if (((player.RaceName=="Briton")||(player.RaceName=="Saracen")||(player.RaceName=="Inconnu")) && (player.CharacterClass.ID==17))
							PromotePlayer(player, (int)eCharacterClass.Infiltrator, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now an Infiltrator!");
						break;
					case "Mercenary":
                        if (((player.RaceName == "Briton") || (player.RaceName == "AlbionMinotaur") || (player.RaceName == "Half Ogre") || (player.RaceName == "Saracen") || (player.RaceName == "Highlander") || (player.RaceName == "Inconnu") || (player.RaceName == "Avalonian")) && (player.CharacterClass.ID == 14))
                            			PromotePlayer(player, (int)eCharacterClass.Mercenary, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Mercenary!");
						break;
					case "Minstrel":
                        if (((player.RaceName == "Briton") || (player.RaceName == "Saracen") || (player.RaceName == "AlbionMinotaur") || (player.RaceName == "Highlander")) && (player.CharacterClass.ID == 17))
							PromotePlayer(player, (int)eCharacterClass.Minstrel, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Minstrel!");
						break;
					case "Necromancer":
						PromotePlayer(player, (int)eCharacterClass.Necromancer, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Necromancer!");
						break;

					case "Paladin":
						if (((player.RaceName=="Briton")||(player.RaceName=="Saracen")||(player.RaceName=="Highlander")||(player.RaceName=="Avalonian")) && (player.CharacterClass.ID==14))
							PromotePlayer(player, (int)eCharacterClass.Paladin, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Paladin!");
						break;
					case "Reaver":
						if (((player.RaceName=="Briton")||(player.RaceName=="Saracen")||(player.RaceName=="Highlander")||(player.RaceName=="Inconnu")) && (player.CharacterClass.ID==14))
							PromotePlayer(player, (int)eCharacterClass.Reaver, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Reaver!");
						break;
					case "Scout":
						if (((player.RaceName=="Inconnu")||(player.RaceName=="Saracen")||(player.RaceName=="Highlander")||(player.RaceName=="Briton")) && (player.CharacterClass.ID==17))
							PromotePlayer(player, (int)eCharacterClass.Scout, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Scout!");
						break;
					case "Sorcerer":
						if (((player.RaceName=="Briton")||(player.RaceName=="Avalonian")||(player.RaceName=="Inconnu")||(player.RaceName=="Half Ogre")||(player.RaceName=="Saracen")) && (player.CharacterClass.ID==18))
							PromotePlayer(player, (int)eCharacterClass.Sorcerer, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Sorcerer!");
						break;
					case "Theurgist":
						if (((player.RaceName=="Avalonian")||(player.RaceName=="Briton")||(player.RaceName=="Half Ogre")) && (player.CharacterClass.ID==15))
							PromotePlayer(player, (int)eCharacterClass.Theurgist, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Therugist!");
						break;
					case "Wizard":
						if (((player.RaceName=="Briton")||(player.RaceName=="Avalonian")||(player.RaceName=="Half Ogre")) && (player.CharacterClass.ID==15))
							PromotePlayer(player, (int)eCharacterClass.Wizard, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Wizard!");
						break;
                    case "Heretic":
						if ((((player.RaceName == "Avalonian") || (player.RaceName == "Briton") || (player.RaceName == "Inconnu"))) && (player.CharacterClass.ID==16))
							PromotePlayer(player, (int)eCharacterClass.Heretic, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Heretic!");
						break;
                    case "Priest":
                        if ((((player.RaceName == "Avalonian") || (player.RaceName == "Briton") || (player.RaceName == "Inconnu"))) && (player.CharacterClass.ID == 16))
                            PromotePlayer(player, (int)eCharacterClass.Priest, "Arise, " + player.Name + ", and know that you are no longer a simple " + player.CharacterClass.BaseName + ". You are now a Priest!");
                        break;
// __Midgard________________________________________________________________________________________________________________

					case "Warrior":
                        if (((player.RaceName == "Kobold") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Dwarf") || (player.RaceName == "Valkyn") || (player.RaceName == "Norseman") || (player.RaceName == "Troll")) && (player.CharacterClass.ID == 35))
							PromotePlayer(player, (int)eCharacterClass.Warrior, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Warrior!");
						break;
					case "Thane":
						if (((player.RaceName=="Norseman")||(player.RaceName=="Troll")||(player.RaceName=="Frostalf")||(player.RaceName=="Dwarf")) && (player.CharacterClass.ID==35))
							PromotePlayer(player, (int)eCharacterClass.Thane, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Thane!");
						break;
					case "Berserker":
                        if (((player.RaceName == "Norseman") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Troll") || (player.RaceName == "Dwarf")) && (player.CharacterClass.ID == 35))
							PromotePlayer(player, (int)eCharacterClass.Berserker, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Berserker!");
						break;
					case "Skald":
                        if (((player.RaceName == "Kobold") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Norseman") || (player.RaceName == "Dwarf") || (player.RaceName == "Troll")) && (player.CharacterClass.ID == 35))
							PromotePlayer(player, (int)eCharacterClass.Skald, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Skald!");
						break;
					case "Savage":
                        if (((player.RaceName == "Norseman") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Kobold") || (player.RaceName == "Troll") || (player.RaceName == "Valkyn") || (player.RaceName == "Dwarf")) && (player.CharacterClass.ID == 35))
							PromotePlayer(player, (int)eCharacterClass.Savage, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Savage!");
						break;
					case "Hunter":
						if (((player.RaceName=="Norseman")||(player.RaceName=="Kobold")||(player.RaceName=="Valkyn")||(player.RaceName=="Dwarf")) && (player.CharacterClass.ID==38))
						        PromotePlayer(player, (int)eCharacterClass.Hunter, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Hunter!");
						break;
					case "Shadowblade":
						if (((player.RaceName=="Norseman")||(player.RaceName=="Kobold")||(player.RaceName=="Valkyn")||(player.RaceName=="Frostalf"))&& (player.CharacterClass.ID==38))
							PromotePlayer(player, (int)eCharacterClass.Shadowblade, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Shadowblade!");
						break;
					case "Runemaster":
						if (((player.RaceName=="Norseman")||(player.RaceName=="Kobold")||(player.RaceName=="Frostalf")||(player.RaceName=="Dwarf"))&& (player.CharacterClass.ID==36))
							PromotePlayer(player, (int)eCharacterClass.Runemaster, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Runemaster!");
						break;
					case "Spiritmaster":
						if (((player.RaceName=="Kobold")||(player.RaceName=="Norseman")||(player.RaceName=="Frostalf"))&& (player.CharacterClass.ID==36))
						        PromotePlayer(player, (int)eCharacterClass.Spiritmaster, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Spiritmaster!");
						break;
					case "Bonedancer":
						if (((player.RaceName=="Valkyn")||(player.RaceName=="Kobold")||(player.RaceName=="Troll")) && (player.CharacterClass.ID==36))
							PromotePlayer(player, (int)eCharacterClass.Bonedancer, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Bonedancer!");
                        break;
					case "Healer":
                        if (((player.RaceName == "Dwarf") || (player.RaceName == "Norseman") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Frostalf")) && (player.CharacterClass.ID == 37))
							PromotePlayer(player, (int)eCharacterClass.Healer, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Healer!");
						break;
					case "Shaman":
                        if (((player.RaceName == "Kobold") || (player.RaceName == "MidgardMinotaur") || (player.RaceName == "Troll") || (player.RaceName == "Frostalf")) && (player.CharacterClass.ID == 37))
							PromotePlayer(player, (int)eCharacterClass.Shaman, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Shaman!");
						break;

                    case "Warlock":
                        if (((player.RaceName == "Kobold") || (player.RaceName == "Norseman") || (player.RaceName == "Frostalf")) && (player.CharacterClass.ID == 36))
                            PromotePlayer(player, (int)eCharacterClass.Warlock, "Arise, " + player.Name + ", and know that you are no longer a simple " + player.CharacterClass.BaseName + ". You are now a Warlock!");
                        break;
                    case "Valkyrie":
                        if ((player.RaceName == "Norseman") || (player.RaceName == "Dwarf") && (player.CharacterClass.ID == 35))
                            PromotePlayer(player, (int)eCharacterClass.Valkyrie, "Arise, " + player.Name + ", you are now a Valkryie!");
                        break;

// __Hibernia______________________________________________________________________________________________________________
		
					case "Animist":
                        if (((player.RaceName == "Firbolg") || (player.RaceName == "Celt") || (player.RaceName == "Sylvan")) && (player.CharacterClass.ID == 57))
						PromotePlayer(player, (int)eCharacterClass.Animist, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now an Animist!");
						break;
					case "Bard":
						if (((player.RaceName=="Celt")||(player.RaceName=="Firbolg"))&& (player.CharacterClass.ID==53))
							PromotePlayer(player, (int)eCharacterClass.Bard, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Bard!");
						break;
					case "Blademaster":
                        if (((player.RaceName == "Firbolg") || (player.RaceName == "HiberniaMinotaur") || (player.RaceName == "Celt") || (player.RaceName == "Shar") || (player.RaceName == "Elf")) && (player.CharacterClass.ID == 52))
							PromotePlayer(player, (int)eCharacterClass.Blademaster, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Blademaster!");
						break;
					case "Champion":
						if (((player.RaceName=="Celt")|(player.RaceName=="Elf")||(player.RaceName=="Lurikeen")||(player.RaceName=="Shar")) && (player.CharacterClass.ID==52))
							PromotePlayer(player, (int)eCharacterClass.Champion, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Champion!");
						break;
					case "Druid":
                        if (((player.RaceName == "Celt") || (player.RaceName == "HiberniaMinotaur") || (player.RaceName == "Firbolg") || (player.RaceName == "Sylvan")) && (player.CharacterClass.ID == 53))
							PromotePlayer(player, (int)eCharacterClass.Druid, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Druid!");
						break;
					case "Eldritch":
						if (((player.RaceName=="Lurikeen")||(player.RaceName=="Elf")) && (player.CharacterClass.ID==51))
							PromotePlayer(player, (int)eCharacterClass.Eldritch, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now an Eldritch!");
						break;
					case "Enchanter":
						if (((player.RaceName=="Lurikeen")||(player.RaceName=="Elf")) && (player.CharacterClass.ID==51))
							PromotePlayer(player, (int)eCharacterClass.Enchanter, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now an Enchanter!");
						break;
                    case "Bainshee":
                        if (((player.RaceName == "Lurikeen") || (player.RaceName == "Elf")) && (player.CharacterClass.ID == 51))
                            PromotePlayer(player, (int)eCharacterClass.Bainshee, "Arise, " + player.Name + ", and know that you are now a bainshee!!");
                        break;
					case "Hero":
                        if (((player.RaceName == "Firbolg") || (player.RaceName == "HiberniaMinotaur") || (player.RaceName == "Lurikeen") || (player.RaceName == "Celt") || (player.RaceName == "Shar")) && (player.CharacterClass.ID == 52))
							PromotePlayer(player, (int)eCharacterClass.Hero, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Hero!");
						break;
					case "Mentalist":
						if (((player.RaceName=="Lurikeen")||(player.RaceName=="Celt")||(player.RaceName=="Elf")) && (player.CharacterClass.ID==51))
							PromotePlayer(player, (int)eCharacterClass.Mentalist, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Mentalist!");
						break;
					case "Nightshade":
						if (((player.RaceName=="Lurikeen")||(player.RaceName=="Elf")||(player.RaceName=="Celt"))  && (player.CharacterClass.ID==54))
							PromotePlayer(player, (int)eCharacterClass.Nightshade, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Nightshade!");
						break;
                    case "Vampiir":
                        if (((player.RaceName == "Lurikeen") || (player.RaceName == "Celt") || (player.RaceName == "Shar")) && ((player.CharacterClass.ID == 52)))
                            PromotePlayer(player, (int)eCharacterClass.Vampiir, "Arise " + player.Name + " and know that you are now a Vampiir!");
                        break;
					case "Ranger":
						if (((player.RaceName=="Lurikeen")||(player.RaceName=="Elf")||(player.RaceName=="Celt")||(player.RaceName=="Shar")) && (player.CharacterClass.ID==54))
							PromotePlayer(player, (int)eCharacterClass.Ranger, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Ranger!");
						break;
					case "Valewalker":
						if (((player.RaceName=="Firbolg")||(player.RaceName=="Celt")||(player.RaceName=="Sylvan"))&& (player.CharacterClass.ID==57))
							PromotePlayer(player, (int)eCharacterClass.Valewalker, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Valewalker!");
						break;
					case "Warden":
                        if (((player.RaceName == "Celt") || (player.RaceName == "HiberniaMinotaur") || (player.RaceName == "Firbolg")) && (player.CharacterClass.ID == 53))
							PromotePlayer(player, (int)eCharacterClass.Warden, "Arise, "+player.Name+", and know that you are no longer a simple "+player.CharacterClass.BaseName+". You are now a Warden!");
						break;
					default: break;
				}
			}
			return true;
		}
        public bool PromotePlayer(GamePlayer player, int classid, string messageToPlayer)
        {
            if (player == null) return false;


            if (player.SetCharacterClass(classid))
            {
                player.Out.SendMessage(this.Name + " says, \"" + messageToPlayer + "\"", eChatType.CT_System, eChatLoc.CL_PopupWindow);
                player.Out.SendMessage("You have been upgraded to the " + player.CharacterClass.Name + " class!", eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                player.CharacterClass.OnLevelUp(player);
                player.UpdateSpellLineLevels(true);
                //player.RefreshSpecDependendSkills();
                //player.Out.SendUpdatePlayerSpells();
                player.Out.SendUpdatePlayerSkills();
                player.Out.SendUpdatePlayer();
                player.UpdatePlayerStatus();
                switch (player.CharacterClass.Name)
                {
                    case "Armsman":
                        player.GetSpecializationByName("Slash", false).Level = 12;
                        player.GetSpecializationByName("Thrust", false).Level = 12;
                        break;
                    case "Mercenary":
                        player.GetSpecializationByName("Slash", false).Level = 12;
                        player.GetSpecializationByName("Thrust", false).Level = 12;
                        break;
                    case "Minstrel":
                        player.GetSpecializationByName("Instruments", false).Level = 12;
                        break;
                    case "Paladin":
                        player.GetSpecializationByName("Chants", false).Level = 12;
                        player.GetSpecializationByName("Slash", false).Level = 12;
                        break;
                    case "Reaver":
                        player.GetSpecializationByName("Flexible", false).Level = 12;
                        player.GetSpecializationByName("Slash", false).Level = 12;
                        break;
                    case "Scout":
                        player.GetSpecializationByName("Longbows", false).Level = 12;
                        break;
                    case "Infiltrator":
                        player.GetSpecializationByName("Stealth", false).Level = 12;
                        break;
                    case "Ranger":
                        player.GetSpecializationByName("Recurve Bow", false).Level = 12;
                        break;
                    case "Warrior":
                        player.GetSpecializationByName("Axe", false).Level = 12;
                        player.GetSpecializationByName("Sword", false).Level = 12;
                        player.GetSpecializationByName("Hammer", false).Level = 12;
                        break;
                }
                //Level 50
                if ((player.CharacterClass.Name == "Cleric") || (player.CharacterClass.Name == "Warlock") || (player.CharacterClass.Name == "Bainshee") || (player.CharacterClass.Name == "Priest") || (player.CharacterClass.Name == "Priestess") || (player.CharacterClass.Name == "Necromancer") || (player.CharacterClass.Name == "Theurgist") || (player.CharacterClass.Name == "Wizard") || (player.CharacterClass.Name == "Cabalist") || (player.CharacterClass.Name == "Sorcerer") || (player.CharacterClass.Name == "Sorceress") || (player.CharacterClass.Name == "Animist") || (player.CharacterClass.Name == "Eldritch") || (player.CharacterClass.Name == "Enchanter") || (player.CharacterClass.Name == "Mentalist") || (player.CharacterClass.Name == "Druid") || (player.CharacterClass.Name == "Bonedancer") || (player.CharacterClass.Name == "Runemaster") || (player.CharacterClass.Name == "Spiritmaster") || (player.CharacterClass.Name == "Healer") || (player.CharacterClass.Name == "Shaman")) player.SkillSpecialtyPoints += 220;
                if ((player.CharacterClass.Name == "Minstrel") || (player.CharacterClass.Name == "Vampiir") || (player.CharacterClass.Name == "Valewalker") || (player.CharacterClass.Name == "Bard") || (player.CharacterClass.Name == "Savage") || (player.CharacterClass.Name == "Skald")) player.SkillSpecialtyPoints += 330;
                if ((player.CharacterClass.Name == "Friar") || (player.CharacterClass.Name == "Warden")) player.SkillSpecialtyPoints += 396;
                if ((player.CharacterClass.Name == "Reaver") || (player.CharacterClass.Name == "Heretic") || (player.CharacterClass.Name == "Valkyrie") || (player.CharacterClass.Name == "Armsman") || (player.CharacterClass.Name == "Mercenary") || (player.CharacterClass.Name == "Paladin") || (player.CharacterClass.Name == "Scout") || (player.CharacterClass.Name == "Blademaster") || (player.CharacterClass.Name == "Champion") || (player.CharacterClass.Name == "Hero") || (player.CharacterClass.Name == "Ranger") || (player.CharacterClass.Name == "Hunter") || (player.CharacterClass.Name == "Berserker") || (player.CharacterClass.Name == "Thane") || (player.CharacterClass.Name == "Warrior")) player.SkillSpecialtyPoints += 445;
                if ((player.CharacterClass.Name == "Nightshade") || (player.CharacterClass.Name == "Shadowblade")) player.SkillSpecialtyPoints += 485;
                if (player.CharacterClass.Name == "Infiltrator") player.SkillSpecialtyPoints += 553;

                //Level 45
               // if ((player.CharacterClass.Name == "Cleric") || (player.CharacterClass.Name == "Warlock") || (player.CharacterClass.Name == "Bainshee") || (player.CharacterClass.Name == "Necromancer") || (player.CharacterClass.Name == "Theurgist") || (player.CharacterClass.Name == "Wizard") || (player.CharacterClass.Name == "Cabalist") || (player.CharacterClass.Name == "Sorcerer") || (player.CharacterClass.Name == "Sorceress") || (player.CharacterClass.Name == "Animist") || (player.CharacterClass.Name == "Eldritch") || (player.CharacterClass.Name == "Enchanter") || (player.CharacterClass.Name == "Mentalist") || (player.CharacterClass.Name == "Druid") || (player.CharacterClass.Name == "Bonedancer") || (player.CharacterClass.Name == "Runemaster") || (player.CharacterClass.Name == "Spiritmaster") || (player.CharacterClass.Name == "Healer") || (player.CharacterClass.Name == "Shaman")) player.SkillSpecialtyPoints += 104;
               // if ((player.CharacterClass.Name == "Minstrel") || (player.CharacterClass.Name == "Vampiir") || (player.CharacterClass.Name == "Valewalker") || (player.CharacterClass.Name == "Bard") || (player.CharacterClass.Name == "Savage") || (player.CharacterClass.Name == "Skald")) player.SkillSpecialtyPoints += 156;
               // if ((player.CharacterClass.Name == "Friar") || (player.CharacterClass.Name == "Warden")) player.SkillSpecialtyPoints += 186;
              //  if ((player.CharacterClass.Name == "Reaver") || (player.CharacterClass.Name == "Heretic") || (player.CharacterClass.Name == "Valkyrie") || (player.CharacterClass.Name == "Armsman") || (player.CharacterClass.Name == "Mercenary") || (player.CharacterClass.Name == "Paladin") || (player.CharacterClass.Name == "Scout") || (player.CharacterClass.Name == "Blademaster") || (player.CharacterClass.Name == "Champion") || (player.CharacterClass.Name == "Hero") || (player.CharacterClass.Name == "Ranger") || (player.CharacterClass.Name == "Hunter") || (player.CharacterClass.Name == "Berserker") || (player.CharacterClass.Name == "Thane") || (player.CharacterClass.Name == "Warrior")) player.SkillSpecialtyPoints += 210;
              //  if ((player.CharacterClass.Name == "Nightshade") || (player.CharacterClass.Name == "Shadowblade")) player.SkillSpecialtyPoints += 230;
               // if (player.CharacterClass.Name == "Infiltrator") player.SkillSpecialtyPoints += 261;

                player.Level = 50;
                player.Health = player.MaxHealth;
                player.Endurance = player.MaxEndurance;
                player.Mana = player.MaxMana;
                player.UpdatePlayerStatus();

                player.Out.SendMessage("You have been accepted by the " + player.CharacterClass.Profession + "!", eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                player.Out.SendTrainerWindow();
                return true;
            }
            return false;
        }


		private void SendReply(GamePlayer target, string msg)
		{
			target.Out.SendMessage(msg,eChatType.CT_Say,eChatLoc.CL_PopupWindow);
		}
	}
}
//Dawn of Light Version 1.7.48
//12/13/2004
//Written by Gavinius
//based on Nardin and Zjovaz previous script
//08/18/2005
//by sirru
//completely rewrote SetEffecplayer, no duel item things whatsoever left
//compatible with dol 1.7 and added some nice things like more 
//smalltalk, equip, emotes, changing of itemnames and spellcast at the end of chargeess
//plus i added changing of speed and color
//what could be done is trimming the prefixes from the name instead of looking at the db, but i dont know how to do that :)

using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.Database;
using System.Collections;
using DOL.GS.PacketHandler;

namespace DOL.GS.Scripts
{
	[NPCGuildScript("Secondary Charge NPC")]
    public class SecondaryChargeNPC : GameNPC
	{
        private string chargeNPC_ITEM_WEAK = "DOL.GS.Scripts.SecondaryChargeNPC_Item_Manipulation";//used to store the item in the player

		public override bool Interact(GamePlayer player)
		{
			if(base.Interact(player))
			{
				TurnTo(player,250);
				SendReply(player, 	"Greetings Adventurer!\n\n"+
				          			"I will add a special charge to your weapons or armor if you are truely worthy!...\n"+
				          			"It will cost you nothing however you must be of sufficient Realm Rank to be rewarded.\n\n"+
				          			"Simply give me the item and i will start my work.");
				return true;
			}
			return false;
		}

		public override bool ReceiveItem(GameLiving source, InventoryItem item)
		{
			GamePlayer t = source as GamePlayer;
			if(t == null || item == null) return false;
			if(WorldMgr.GetDistance(this,t) > WorldMgr.INTERACT_DISTANCE)
			{
				t.Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return false;
			}
            if (t.RealmPoints <= 513500)
			{
				t.Out.SendMessage("You must be of higher stature to be rewarded!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return false;
			}
            if ((item.Object_Type > 31 && item.Object_Type < 39) && (t.RealmPoints >= 513500))
            SendReply (t, "Alrighplayer, now select your Realm Rank, [5] [6] [7] [8] [9] [10] [11] [12] or [13]:");

			t.TempProperties.setProperty(chargeNPC_ITEM_WEAK, new WeakReference(item));
			return false;
		}
		
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;

			if(!(source is GamePlayer)) return false;
		
			GamePlayer player = (GamePlayer)source;

            TurnTo(player.X, player.Y);

			switch(str)
			{
                case "5":
                    SendReply(player, "At Rank 5 you may [Group Cure Disease], [Group Cure Nearsight] or [Bolster] nearby allies");
					break;
                case "6":
                    SendReply(player, "At Rank 6 you may summon a [Speed Warp] or a [Brittle Guard]");
                    break;
                case "7":
                    SendReply(player, "At Rank 7 you may [Snare] your enemy or create a trap to [Tangle] them");
                    break;
                case "8":
                    SendReply(player, "At Rank 8 your trap skills improve to damage your enemies [Health] or [Power]");
                    break;
                case "9":
                    SendReply(player, "At Rank 9 you may directly [Impede] enemies casting or [Demoralize] them");
                    break;
                case "10":
                    SendReply(player, "At Rank 10 you show unrivaled [Leadership] in battle or [Heal Allies] as a cost of your life");
                    break;
                case "11":
                    SendReply(player, "At Rank 11 you are able to [Fortify] yourself or cause your enemy great [Agony]");
                    break;
                case "12":
                    SendReply(player, "At Rank 12 you may [Spike] your melee damage or [Escape] the heat of battle");
                    break;
                case "13":
                    SendReply(player, "Finally, at Realm Rank 13 you have truely mastered the art of [War]");
                    break;

                case "Group Cure Disease":
                    if (player.RealmPoints >= 513500)
                    Setcharge(player, 7239);
					else SendReply(player, "You are not Realm Rank 5 or above, come back when you have reached this Realm Rank.");
					break;
                case "Group Cure Nearsight":
                    if (player.RealmPoints >= 513500)
					Setcharge(player, 7206);
                else SendReply(player, "You are not Realm Rank 5 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Bolster":
                    if (player.RealmPoints >= 513500)
					Setcharge(player, 7269);
                else SendReply(player, "You are not Realm Rank 5 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Speed Warp":
                    if (player.RealmPoints >= 1010625)
                        Setcharge(player, 7236);
                    else SendReply(player, "You are not Realm Rank 6 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Brittle Guard":
                    if (player.RealmPoints >= 1010625)
					Setcharge(player, 7317);
                else SendReply(player, "You are not Realm Rank 6 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Snare":
                    if (player.RealmPoints >= 1755250)
					Setcharge(player, 7276);
                else SendReply(player, "You are not Realm Rank 7 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Tangle":
                    if (player.RealmPoints >= 1755250)
                    Setcharge(player, 7219);
                else SendReply(player, "You are not Realm Rank 7 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Health":
                    if (player.RealmPoints >= 2797375)
					Setcharge(player, 7254);
                else SendReply(player, "You are not Realm Rank 8 or above, come back when you have reached this Realm Rank.");
                    break;
				case "Power":
                    if (player.RealmPoints >= 2797375)
					Setcharge(player, 7252);
                else SendReply(player, "You are not Realm Rank 8 or above, come back when you have reached this Realm Rank.");
                     break;
				case "Impede":
                    if (player.RealmPoints >= 4187000)
					Setcharge(player, 7261);
                else SendReply(player, "You are not Realm Rank 9 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Demoralize":
                    if (player.RealmPoints >= 4187000)
					Setcharge(player, 7296);
                else SendReply(player, "You are not Realm Rank 9 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Leadership":
                    if (player.RealmPoints >= 5974125)
					Setcharge(player, 7295);
                else SendReply(player, "You are not Realm Rank 10 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Heal Allies":
                    if (player.RealmPoints >= 5974125)
					Setcharge(player, 7289);
                else SendReply(player, "You are not Realm Rank 10 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Fortify":
                    if (player.RealmPoints >= 8208750)
					Setcharge(player, 7307);
                else SendReply(player, "You are not Realm Rank 11 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Agony":
                    if (player.RealmPoints >= 8208750)
					Setcharge(player, 7260);
                else SendReply(player, "You are not Realm Rank 11 or above, come back when you have reached this Realm Rank.");
                    break;
                case "Spike":
                    if (player.RealmPoints >= 23308097)
					Setcharge(player, 7306);
                else SendReply(player, "You are not Realm Rank 12 or above, come back when you have reached this Realm Rank.");
                    break;
				case "Escape":
                    if (player.RealmPoints >= 23308097)
					Setcharge(player, 7298);
                else SendReply(player, "You are not Realm Rank 12 or above, come back when you have reached this Realm Rank.");
                    break;
                case "War":
                    if (player.RealmPoints >= 66181501)
                        Setcharge(player, 7298);
                    else SendReply(player, "You are not Realm Rank 13 or above, come back when you have reached this Realm Rank.");
                    break;
			}

			return true;
		}

		public void SendReply(GamePlayer player, string msg)
		{
			player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
		}
		public void Setcharge(GamePlayer player, int charge)
		{
			WeakReference itemWeak = (WeakReference) player.TempProperties.getObjectProperty(chargeNPC_ITEM_WEAK, new WeakRef(null));
			
			player.TempProperties.removeProperty(chargeNPC_ITEM_WEAK);
			
			InventoryItem item = (InventoryItem) itemWeak.Target;

			item.SpellID1 = charge;
            item.Charges1 = 500;
            item.MaxCharges1 = 500;
			//player.RemoveBountyPoints(0);
			player.Out.SendInventoryItemsUpdate(new InventoryItem[] {item});
			SendReply(player, "My work upon "+item.Name+" is complete. Farewell adventurer.");
		}
	}
}



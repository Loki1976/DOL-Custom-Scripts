using System;
using System.Collections;
using System.Collections.Generic;

using DOL.Database;
using DOL.GS;
using DOL.GS.PacketHandler;
using DOL.Events;

namespace DOL.Storm.Market
{
    public class MarketNPC : GameNPC
    {
        public const string TEMP_BUY_CM_ITEM_KEY = "TempBuyCMItemKey";

        #region Market NPCs

        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            // create Market NPC's

            // Albion

            MarketNPC albMarketNPC = new MarketNPC();
            albMarketNPC.Name = "Leidolf Reynard";
            albMarketNPC.GuildName = "Realm Marketer";
            albMarketNPC.Realm = eRealm.Albion;
            albMarketNPC.Model = 79;

            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            template.AddNPCEquipment(eInventorySlot.TorsoArmor, 3058, 75);
            template.AddNPCEquipment(eInventorySlot.LegsArmor, 3060, 75);
            template.AddNPCEquipment(eInventorySlot.ArmsArmor, 3061, 75);
            template.AddNPCEquipment(eInventorySlot.Cloak, 669, 75);
            albMarketNPC.Inventory = template.CloseTemplate();
            albMarketNPC.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard);

            albMarketNPC.Size = 52;
            albMarketNPC.Level = 50;

            // In Camelot main square
            albMarketNPC.CurrentRegionID = 163;
            albMarketNPC.X = 578730;
            albMarketNPC.Y = 678067;
            albMarketNPC.Z = 8730;
            albMarketNPC.Heading = 1505;

            albMarketNPC.Flags ^= (uint)GameNPC.eFlags.PEACE;

            albMarketNPC.AddToWorld();


            // Midgard

            MarketNPC midMarketNPC = new MarketNPC();
            midMarketNPC.Name = "Leidolf Reynard";
            midMarketNPC.GuildName = "Realm Marketer";
            midMarketNPC.Realm = eRealm.Midgard;
            midMarketNPC.Model = 214;

            template = new GameNpcInventoryTemplate();
            template.AddNPCEquipment(eInventorySlot.TorsoArmor, 3058, 75);
            template.AddNPCEquipment(eInventorySlot.LegsArmor, 3060, 75);
            template.AddNPCEquipment(eInventorySlot.ArmsArmor, 3061, 75);
            template.AddNPCEquipment(eInventorySlot.Cloak, 669, 75);
            midMarketNPC.Inventory = template.CloseTemplate();
            midMarketNPC.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard);

            midMarketNPC.Size = 52;
            midMarketNPC.Level = 50;

            midMarketNPC.CurrentRegionID = 101;
            midMarketNPC.X = 29976;
            midMarketNPC.Y = 35041;
            midMarketNPC.Z = 8005;
            midMarketNPC.Heading = 177;

            midMarketNPC.Flags ^= (uint)GameNPC.eFlags.PEACE;

            midMarketNPC.AddToWorld();


            // Hibernia


            MarketNPC hibMarketNPC = new MarketNPC();
            hibMarketNPC.Name = "Cyric Dunn";
            hibMarketNPC.GuildName = "Realm Marketer";
            hibMarketNPC.Realm = eRealm.Hibernia;
            hibMarketNPC.Model = 363;

            template = new GameNpcInventoryTemplate();
            template.AddNPCEquipment(eInventorySlot.TorsoArmor, 3058, 75);
            template.AddNPCEquipment(eInventorySlot.LegsArmor, 3060, 75);
            template.AddNPCEquipment(eInventorySlot.ArmsArmor, 3061, 75);
            template.AddNPCEquipment(eInventorySlot.Cloak, 669, 75);
            hibMarketNPC.Inventory = template.CloseTemplate();
            hibMarketNPC.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard);

            hibMarketNPC.Size = 52;
            hibMarketNPC.Level = 50;

            hibMarketNPC.CurrentRegionID = 201;
            hibMarketNPC.X = 30484;
            hibMarketNPC.Y = 32212;
            hibMarketNPC.Z = 7922;
            hibMarketNPC.Heading = 3862;

            hibMarketNPC.Flags ^= (uint)GameNPC.eFlags.PEACE;

            hibMarketNPC.AddToWorld();

        }

        #endregion


        public const string TEMP_ITEM_PRICE_KEY = "TempItemPriceKey";

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            SayTo(player, "Hello " + player.Name + ", fine day to [Buy] or [Sell] some equipment, don't you think?");

            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str))
                return false;

            GamePlayer player = source as GamePlayer;
            if (player == null)
                return false;

            switch (str)
            {
                case "Sell":
                    {
                        DisplayOwnItems(player);
                        break;
                    }
                case "Buy":
                    {
                        player.Out.SendMarketExplorerWindow();
                        break;
                    }
            }

            return true;
        }

        public static void DisplayOwnItems(GamePlayer player)
        {
            ICollection items = player.Inventory.GetItemRange(eInventorySlot.Consignment_First, eInventorySlot.Consignment_Last);
            if (items.Count == 0)
            {
                player.Out.SendInventoryItemsUpdate(0x05, null);
            }
            else
            {
                player.Out.SendInventoryItemsUpdate(0x05, items);
            }
        }

        public override IList GetExamineMessages(GamePlayer player)
        {
            IList list = base.GetExamineMessages(player);
            list.Add("[Sells items to players]");
            return list;
        }

        public static void MoveItem(GamePlayer player, eInventorySlot fromSlot, eInventorySlot toSlot)
        {
            //backpack to CM
            if ((int)fromSlot >= (int)eInventorySlot.FirstBackpack && (int)fromSlot <= (int)eInventorySlot.LastBackpack && (int)toSlot >= (int)eInventorySlot.HousingInventory_First && (int)toSlot <= (int)eInventorySlot.HousingInventory_Last)
            {
                InventoryItem fromItem = player.Inventory.GetItem(fromSlot);
                player.TempProperties.setProperty(Storm.Market.MarketNPC.TEMP_ITEM_PRICE_KEY, fromItem);
                toSlot = (eInventorySlot)(toSlot - (int)eInventorySlot.HousingInventory_First + (int)eInventorySlot.Consignment_First);
                player.Inventory.MoveItem(fromSlot, toSlot, fromItem.Count);
                DisplayOwnItems(player);
            }
            else if ((int)fromSlot >= (int)eInventorySlot.HousingInventory_First && (int)fromSlot <= (int)eInventorySlot.HousingInventory_Last && (int)toSlot >= (int)eInventorySlot.FirstBackpack && (int)toSlot <= (int)eInventorySlot.LastBackpack)
            {

               
       


            }
        }

        public static void BuyItem(GamePlayer player, int slot)
        {
            //get temp list
            List<InventoryItem> items = player.TempProperties.getProperty<object>(DOL.GS.PacketHandler.Client.v168.PlayerMarketSearchRequestHandler.TEMP_SEARCH_KEY, null) as List<InventoryItem>;
            if (items == null)
            {
                GameServer.Instance.Logger.Error("temp items list is null in buy item, eek!");
                return;
            }

            //get item
            InventoryItem item = items[slot - 1000];
            if (item == null)
            {
                GameServer.Instance.Logger.Error("cannot find item from list, eek!");
            }
            player.Out.SendCustomDialog("Are you sure you want to buy " + item.Name + " for " + Money.GetString(item.SellPrice), ConfirmBuy);
            player.TempProperties.setProperty(TEMP_BUY_CM_ITEM_KEY, item);
        }

        public static void ConfirmBuy(GamePlayer player, byte response)
        {
            //get the item
            InventoryItem item = (InventoryItem)player.TempProperties.getProperty<object>(TEMP_BUY_CM_ITEM_KEY, null);
            player.TempProperties.removeProperty(TEMP_BUY_CM_ITEM_KEY);

            if (item == null)
                return;

            if (response != 0x01)
                return;

            if (player.RemoveMoney(item.SellPrice))
            {
                //give money to person who bought it
                //is player logged in
                Character character = GameServer.Database.SelectObject(typeof(Character), "`DOLCharacters_ID` = '" + item.OwnerID + "'") as Character;
                if (character == null)
                {
                    GameServer.Instance.Logger.Error("Cannot find character to pay for item " + item.ObjectId + " looking for character " + item.OwnerID);
                    return;
                }

                GameClient client = WorldMgr.GetClientByPlayerName(character.Name, true, false);
                if (client == null)
                {
                    long newMoney = character.Copper + (character.Silver * 100) + (character.Gold * 100 * 100) + (character.Platinum * 100 * 100 * 1000) + item.SellPrice;

                    character.Copper = Money.GetCopper(newMoney);
                    character.Silver = Money.GetSilver(newMoney);
                    character.Gold = Money.GetGold(newMoney);
                    character.Platinum = Money.GetPlatinum(newMoney);
                    character.Mithril = Money.GetMithril(newMoney);
                    GameServer.Database.DeleteObject(item);
                    GameServer.Database.SaveObject(character);
                }
                else
                {
                    client.Player.AddMoney(item.SellPrice, item.Name + " has been sold for {0}!");
                    client.Player.Inventory.RemoveItem(item);
                }
                player.Out.SendMessage("You have bought " + item.Name + " for " + Money.GetString(item.SellPrice), DOL.GS.PacketHandler.eChatType.CT_System, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                item.SellPrice = 0;
                player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, item);
            }
            else player.Out.SendMessage("You don't have enough money for that item!", DOL.GS.PacketHandler.eChatType.CT_System, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
        }
    }
}

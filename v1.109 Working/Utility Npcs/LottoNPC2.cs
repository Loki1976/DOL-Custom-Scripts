using System;
using System.Collections;
using System.Timers; 
using DOL;
using DOL.GS.PlayerClass;
using DOL.GS;
using DOL.Database; 
using DOL.GS.Scripts; 
using DOL.Events; 
using DOL.GS.GameEvents; 
using DOL.GS.PacketHandler; 
using DOL.GS.Quests;

namespace DOL.GS.Scripts
{
    public class LottoNPC : GameNPC
    {
        private const string REFUND_ITEM_WEAK = "refunded item";

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
            Flags |= GameNPC.eFlags.PEACE;
            return base.AddToWorld();
        }

        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer player = (GamePlayer)source;
            GamePlayer t = source as GamePlayer;
            if (GetDistanceTo(player) > WorldMgr.INTERACT_DISTANCE)
            {
                ((GamePlayer)source).Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return false;
            }

            if (t != null && item != null)
            {
                if (item.Id_nb == "prizetoken1")
                {
                    t.GainRealmPoints(12000);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations you won Realm Points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken2")
                {
                    t.GainBountyPoints(600); 
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations you won Bounty Points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken3")
                {
                    t.GainChampionExperience(10000000000);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations you won Champion Experience!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken4")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);
                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken5")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);
                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken6")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);
                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken7")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);
                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else if (item.Id_nb == "prizetoken8")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);
                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Congratulations!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                {
                    t.TempProperties.setProperty(REFUND_ITEM_WEAK, new WeakRef(item));
                }

            }
            return base.ReceiveItem(source, item);
        }

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            SayTo(player, "Hello " + player.Name + ", Hand your Prize Token to me and I shall Reward you!");

            return true;
        }

        private void SendReply(GamePlayer target, string msg)
        {
            target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);

        }

    }
}
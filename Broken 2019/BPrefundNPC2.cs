using System; 
using System.Collections; 
using System.Timers; 
using DOL; 
using DOL.GS;
using DOL.Database; 
using DOL.GS.Scripts; 
using DOL.Events; 
using DOL.GS.GameEvents; 
using DOL.GS.PacketHandler; 
using DOL.GS.Quests;

namespace DOL.GS.Scripts
{
    public class BPRefund : GameNPC
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
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer t = source as GamePlayer;
            if (WorldMgr.GetDistance(this, source) > WorldMgr.INTERACT_DISTANCE)
            {
                ((GamePlayer)source).Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return false;
            }

            long amount = 0;

            if (t != null && item != null)
            {
                if (item.Id_nb == "Small_BP_Stone")
                    amount = 1 * item.Count;
                else if (item.Id_nb == "Medium_BP_Stone")
                    amount = 2 * item.Count;
                else if (item.Id_nb == "Large_BP_Stone")
                    amount = 5 * item.Count;

                if (amount >= 0)
                {
                    if (item.ProcSpellID != 0) amount += 0;
                    if (item.ProcSpellID1 != 0) amount += 0;
                }
                if (amount == -1)
                {
                    if (item.ProcSpellID != 0) amount += 0;
                    if (item.ProcSpellID1 != 0) amount += 0;
                }

                if (amount >= 0)
                {
                    t.TempProperties.setProperty(REFUND_ITEM_WEAK, new WeakRef(item));
                    t.Client.Out.SendCustomDialog("You will get " + amount * 50 + " Bountpoints for this Item!", new CustomDialogResponse(RefundDialogResponse));
                }

            }
            return base.ReceiveItem(source, item);
        }

        protected void RefundDialogResponse(GamePlayer player, byte response)
        {
            WeakReference itemWeak =
                (WeakReference)player.TempProperties.getObjectProperty(
                REFUND_ITEM_WEAK,
                new WeakRef(null)
                );
            player.TempProperties.removeProperty(REFUND_ITEM_WEAK);

            InventoryItem item = (InventoryItem)itemWeak.Target;

            if (response != 0x01)
            {
                player.Out.SendMessage("You decline to have your " + item.Name + " refunded.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return;
            }
            long amount = 0;

            if (player != null && item != null)
            {
                if (item.Id_nb == "Small_BP_Stone")
                    amount = 1 * item.Count;
                else if (item.Id_nb == "Medium_BP_Stone")
                    amount = 2 * item.Count;
                else if (item.Id_nb == "Large_BP_Stone")
                    amount = 5 * item.Count;

                if (amount >= 0)
                {
                    if (item.ProcSpellID != 0) amount += 0;
                    if (item.ProcSpellID1 != 0) amount += 0;
                }
                if (amount == -1)
                {
                    if (item.ProcSpellID != 0) amount += 0;
                    if (item.ProcSpellID1 != 0) amount += 0;
                }
                if (amount >= 0)
                {
                    player.GainBountyPoints(amount);
                    player.Inventory.RemoveItem(item);
                }
            }

        }


        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            SayTo(player, "Hello " + player.Name + ", I can exchange your BP Stones for Bounty Points! Just hand them to me.");

            return true;
        }

        private void SendReply(GamePlayer target, string msg)
        {
            target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);

        }

    }
}
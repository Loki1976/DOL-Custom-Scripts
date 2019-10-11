using System;
using System.Collections;
using System.Timers;

using DOL;
using DOL.AI.Brain;
using DOL.GS;
using DOL.GS.Scripts;
using DOL.GS.GameEvents;
using DOL.GS.PacketHandler;
using DOL.GS.Quests;
using DOL.GS.Spells;
using DOL.GS.Effects;
using DOL.Database;
using DOL.Events;


namespace DOL.GS.Scripts
{

    public class MLremover : GameNPC
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

            if (t != null && item != null)
            {

                if (item.Id_nb == "mlrespectoken")
                {
                    t.Out.SendMessage("Excellent, you have Respec Token.\n" +
                    "Here, please take your Tokens!", eChatType.CT_Say, eChatLoc.CL_PopupWindow);


                    t.RemoveSpellLine("ML1 Battlemaster");
                    t.RemoveSpellLine("ML2 Battlemaster");
                    t.RemoveSpellLine("ML3 Battlemaster");
                    t.RemoveSpellLine("ML4 Battlemaster");
                    t.RemoveSpellLine("ML5 Battlemaster");
                    t.RemoveSpellLine("ML6 Battlemaster");
                    t.RemoveSpellLine("ML7 Battlemaster");
                    t.RemoveSpellLine("ML8 Battlemaster");
                    t.RemoveSpellLine("ML9 Battlemaster");
                    t.RemoveSpellLine("ML10 Battlemaster");

                    t.RemoveSpellLine("ML1 Banelord");
                    t.RemoveSpellLine("ML2 Banelord");
                    t.RemoveSpellLine("ML3 Banelord");
                    t.RemoveSpellLine("ML4 Banelord");
                    t.RemoveSpellLine("ML5 Banelord");
                    t.RemoveSpellLine("ML6 Banelord");
                    t.RemoveSpellLine("ML7 Banelord");
                    t.RemoveSpellLine("ML8 Banelord");
                    t.RemoveSpellLine("ML9 Banelord");
                    t.RemoveSpellLine("ML10 Banelord");

                    t.RemoveSpellLine("ML1 Convoker");
                    t.RemoveSpellLine("ML2 Convoker");
                    t.RemoveSpellLine("ML3 Convoker");
                    t.RemoveSpellLine("ML4 Convoker");
                    t.RemoveSpellLine("ML5 Convoker");
                    t.RemoveSpellLine("ML6 Convoker");
                    t.RemoveSpellLine("ML7 Convoker");
                    t.RemoveSpellLine("ML8 Convoker");
                    t.RemoveSpellLine("ML9 Convoker");
                    t.RemoveSpellLine("ML10 Convoker");

                    t.RemoveSpellLine("ML1 Perfecter");
                    t.RemoveSpellLine("ML2 Perfecter");
                    t.RemoveSpellLine("ML3 Perfecter");
                    t.RemoveSpellLine("ML4 Perfecter");
                    t.RemoveSpellLine("ML5 Perfecter");
                    t.RemoveSpellLine("ML6 Perfecter");
                    t.RemoveSpellLine("ML7 Perfecter");
                    t.RemoveSpellLine("ML8 Perfecter");
                    t.RemoveSpellLine("ML9 Perfecter");
                    t.RemoveSpellLine("ML10 Perfecter");

                    t.RemoveSpellLine("ML1 Sojourner");
                    t.RemoveSpellLine("ML2 Sojourner");
                    t.RemoveSpellLine("ML3 Sojourner");
                    t.RemoveSpellLine("ML4 Sojourner");
                    t.RemoveSpellLine("ML5 Sojourner");
                    t.RemoveSpellLine("ML6 Sojourner");
                    t.RemoveSpellLine("ML7 Sojourner");
                    t.RemoveSpellLine("ML8 Sojourner");
                    t.RemoveSpellLine("ML9 Sojourner");
                    t.RemoveSpellLine("ML10 Sojourner");

                    t.RemoveSpellLine("ML1 Stormlord");
                    t.RemoveSpellLine("ML2 Stormlord");
                    t.RemoveSpellLine("ML3 Stormlord");
                    t.RemoveSpellLine("ML4 Stormlord");
                    t.RemoveSpellLine("ML5 Stormlord");
                    t.RemoveSpellLine("ML6 Stormlord");
                    t.RemoveSpellLine("ML7 Stormlord");
                    t.RemoveSpellLine("ML8 Stormlord");
                    t.RemoveSpellLine("ML9 Stormlord");
                    t.RemoveSpellLine("ML10 Stormlord");

                    t.RemoveSpellLine("ML1 Spymaster");
                    t.RemoveSpellLine("ML2 Spymaster");
                    t.RemoveSpellLine("ML3 Spymaster");
                    t.RemoveSpellLine("ML4 Spymaster");
                    t.RemoveSpellLine("ML5 Spymaster");
                    t.RemoveSpellLine("ML6 Spymaster");
                    t.RemoveSpellLine("ML7 Spymaster");
                    t.RemoveSpellLine("ML8 Spymaster");
                    t.RemoveSpellLine("ML9 Spymaster");
                    t.RemoveSpellLine("ML10 Spymaster");

                    t.RemoveSpellLine("ML1 Warlord");
                    t.RemoveSpellLine("ML2 Warlord");
                    t.RemoveSpellLine("ML3 Warlord");
                    t.RemoveSpellLine("ML4 Warlord");
                    t.RemoveSpellLine("ML5 Warlord");
                    t.RemoveSpellLine("ML6 Warlord");
                    t.RemoveSpellLine("ML7 Warlord");
                    t.RemoveSpellLine("ML8 Warlord");
                    t.RemoveSpellLine("ML9 Warlord");
                    t.RemoveSpellLine("ML10 Warlord");

                    t.ReceiveItem(this, "ml1token");
                    t.ReceiveItem(this, "ml2token");
                    t.ReceiveItem(this, "ml3token");
                    t.ReceiveItem(this, "ml4token");
                    t.ReceiveItem(this, "ml5token");
                    t.ReceiveItem(this, "ml6token");
                    t.ReceiveItem(this, "ml7token");
                    t.ReceiveItem(this, "ml8token");
                    t.ReceiveItem(this, "ml9token");
                    t.ReceiveItem(this, "ml10token");
                    t.Inventory.RemoveItem(item); 
                    t.UpdateSpellLineLevels(true);
                    t.Out.SendUpdatePlayerSkills();
                    t.Out.SendUpdatePlayer();
                    t.UpdatePlayerStatus();
                    t.SaveIntoDatabase();

                    t.TempProperties.setProperty(REFUND_ITEM_WEAK, new WeakRef("mlrespectoken"));
                }

            }
            return base.ReceiveItem(source, item);
        }

            public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            SayTo(player, "Hello " + player.Name + ", Hand me an ML Respec Token and I shall remove your current ML choice. \n" +
                "I will also return your ML Tokens, you will need 10 empty inventory slots");

            return true;
        }

        private void SendReply(GamePlayer target, string msg)
        {
            target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }
    }
}
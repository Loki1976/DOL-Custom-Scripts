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

    public class DragonNPC : GameNPC
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
                #region Albion Dragonslayer Weapons

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Wizard"))
                {
                    t.Out.SendMessage("Excellent, you have slain the Dragon.\n" +
                    "Here, please take your reward!", eChatType.CT_Say, eChatLoc.CL_PopupWindow);

                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWizardStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Sorcerer" || t.CharacterClass.Name == "Sorceress"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSorcererStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Cabalist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerCabalistStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Theurgist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerTheurgistStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Necromancer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerNecromancerStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Armsman" || t.CharacterClass.Name == "Armswoman"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerEdge");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerMace");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerArchMace");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerFlamberge");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerHalberd");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerLance");
                    generic6.CopyFrom(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                    InventoryItem generic7 = new InventoryItem();
                    ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerMattock");
                    generic7.CopyFrom(tgeneric7);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                    InventoryItem generic8 = new InventoryItem();
                    ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDragonslayerPike");
                    generic8.CopyFrom(tgeneric8);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Cleric"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericDragonslayerMace");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if (t.CharacterClass.Name == "Friar")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarDragonslayerMace");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarDragonslayerQuarterStaff");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Heretic"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDragonslayerBarbedChain");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDragonslayerFlail");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDragonslayerMace");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Infiltrator"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDragonslayerEdge");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDragonslayerLaevusBlade");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDragonslayerLaevusEdge");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Mercenary"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerEdge");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerMace");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerLaevusBlade");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerLaevusEdge");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDragonslayerLaevusMace");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Minstrel"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelDragonslayerEdge");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHarp");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Paladin"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerEdge");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerMace");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerGreatEdge");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerGreatHammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDragonslayerGreatSword");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Reaver"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDragonslayerBarbedChain");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDragonslayerBlade");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDragonslayerEdge");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDragonslayerFlail");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDragonslayerMace");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Scout"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDragonslayerBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDragonslayerBow");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDragonslayerEdge");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
#endregion

                #region Midgard Dragonslayer Weapons

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bonedancer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBonedancerStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Runemaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerRunemasterStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Spiritmaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSpiritmasterStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warlock"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarlockStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warrior"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorAxe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorSword");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorTwohandedAxe");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorTwohandedHammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWarriorTwohandedSword");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Valkyrie"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerValkyrieSlashingSpear");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerValkyrieSword");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerValkyrieThrustingSpear");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerValkyrieTwohandedSword");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Healer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHealerTwohandedHammer");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHealerHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Shaman"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShamanHammer");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShamanTwohandedHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Hunter"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerCompoundBow");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHunterSlashingSpear");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHunterSpear");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHunterSword");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHunterTwohandedSword");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Savage"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageAxe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageSlashingGlaiverh");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageSlashingGlaivelh");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageSword");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageThrashingGlaiverh");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageThrashingGlaivelh");
                    generic6.CopyFrom(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                    InventoryItem generic7 = new InventoryItem();
                    ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageTwohandedAxe");
                    generic7.CopyFrom(tgeneric7);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                    InventoryItem generic8 = new InventoryItem();
                    ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageTwohandedHammer");
                    generic8.CopyFrom(tgeneric8);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                    InventoryItem generic9 = new InventoryItem();
                    ItemTemplate tgeneric9 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSavageTwohandedSword");
                    generic9.CopyFrom(tgeneric9);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic9);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Shadowblade"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShadowbladeAxe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShadowbladeHeavyAxe");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShadowbladeHeavyAxe2");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShadowbladeHeavySword");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerShadowbladeSword");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Berserker"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerAxerh");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerTwohandedAxe");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerAxelh");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerTwohandedSword");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerSword");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerTwohandedHammer");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBerserkerHammer");
                    generic6.CopyFrom(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Skald"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldAxe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldSword");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldTwohandedAxe");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldTwohandedHammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerSkaldTwohandedSword");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Thane"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneAxe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneSword");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneTwohandedAxe");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneTwohandedHammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerThaneTwohandedSword");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                #endregion Midgard Dragonslayer Weapons

                #region Hibernia Dragonslayer Weapons

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Animist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerAnimistStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bainshee"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBainsheeStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Eldritch"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerEldritchStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Enchanter") || (t.CharacterClass.Name == "Enchantress"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerEnchanterStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Mentalist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerMentalistStaff");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Valewalker"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerValewalkerScythe");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Vampiir"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirDragonslayerFuarSteel");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bard"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHarp");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBardBlade");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBardHammer");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Druid"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerDruidBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerDruidHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warden"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWardenBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerWardenHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Blademaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBlademasterBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBlademasterHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerBlademasterSteel");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterDragonslayerFuarBlade");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterDragonslayerFuarHammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterDragonslayerFuarSteel");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Champion"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerChampionBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerChampionHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerChampionSteel");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerChampionWarblade");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerChampionWarhammer");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Hero" || t.CharacterClass.Name == "Heroine"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroHammer");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroSpear");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroSteel");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroWarblade");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerHeroWarhammer");
                    generic5.CopyFrom(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Nightshade"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerNightshadeBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerNightshadeSteel");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeDragonslayerFuarBlade");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeDragonslayerFuarSteel");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Ranger"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerDragonslayerFuarBlade");
                    generic0.CopyFrom(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerDragonslayerFuarSteel");
                    generic1.CopyFrom(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerRangerBlade");
                    generic2.CopyFrom(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerRangerSteel");
                    generic3.CopyFrom(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DragonslayerRecurveBow");
                    generic4.CopyFrom(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);

                #endregion

                    t.TempProperties.setProperty(REFUND_ITEM_WEAK, new WeakRef("Gjalpinulva's_Remains"));
                }

            }
            return base.ReceiveItem(source, item);
        }

            public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            SayTo(player, "Hello " + player.Name + ", Hand me Gjalpinulva's remains to me and I shall reward you!");

            return true;
        }

        private void SendReply(GamePlayer target, string msg)
        {
            target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }
    }
}
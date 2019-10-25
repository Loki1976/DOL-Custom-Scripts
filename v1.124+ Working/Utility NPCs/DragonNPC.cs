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
            Flags = GameNPC.eFlags.PEACE; ;	// Peace flag.
            return base.AddToWorld();
        }

        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer t = source as GamePlayer;
            if (GetDistanceTo(t) > WorldMgr.INTERACT_DISTANCE)
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
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate>("DragonslayerWizardStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Sorcerer" || t.CharacterClass.Name == "Sorceress"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSorcererStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Cabalist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerCabalistStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Theurgist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerTheurgistStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Necromancer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerNecromancerStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Armsman" || t.CharacterClass.Name == "Armswoman"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerEdge");
                    generic1 = GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerMace");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerArchMace");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerFlamberge");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerHalberd");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerLance");
                    generic6= GameInventoryItem.Create<ItemTemplate>(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                    InventoryItem generic7 = new InventoryItem();
                    ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerMattock");
                    generic7= GameInventoryItem.Create<ItemTemplate>(tgeneric7);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                    InventoryItem generic8 = new InventoryItem();
                    ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ArmsmanDragonslayerPike");
                    generic8= GameInventoryItem.Create<ItemTemplate>(tgeneric8);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Cleric"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ClericDragonslayerMace");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if (t.CharacterClass.Name == "Friar")
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("FriarDragonslayerMace");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("FriarDragonslayerQuarterStaff");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Heretic"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("HereticDragonslayerBarbedChain");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("HereticDragonslayerFlail");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("HereticDragonslayerMace");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Infiltrator"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("InfiltratorDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("InfiltratorDragonslayerEdge");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("InfiltratorDragonslayerLaevusBlade");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("InfiltratorDragonslayerLaevusEdge");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Mercenary"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerEdge");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerMace");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerLaevusBlade");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerLaevusEdge");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MercenaryDragonslayerLaevusMace");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Minstrel"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MinstrelDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("MinstrelDragonslayerEdge");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHarp");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Paladin"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerEdge");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerMace");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerGreatEdge");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerGreatHammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("PaladinDragonslayerGreatSword");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Reaver"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ReaverDragonslayerBarbedChain");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ReaverDragonslayerBlade");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ReaverDragonslayerEdge");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ReaverDragonslayerFlail");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ReaverDragonslayerMace");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Scout"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ScoutDragonslayerBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ScoutDragonslayerBow");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("ScoutDragonslayerEdge");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
#endregion

                #region Midgard Dragonslayer Weapons

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bonedancer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBonedancerStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Runemaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerRunemasterStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Spiritmaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSpiritmasterStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warlock"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarlockStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warrior"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorAxe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorSword");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorTwohandedAxe");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorTwohandedHammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWarriorTwohandedSword");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Valkyrie"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerValkyrieSlashingSpear");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerValkyrieSword");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerValkyrieThrustingSpear");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerValkyrieTwohandedSword");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Healer"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHealerTwohandedHammer");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHealerHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Shaman"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShamanHammer");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShamanTwohandedHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Hunter"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerCompoundBow");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHunterSlashingSpear");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHunterSpear");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHunterSword");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHunterTwohandedSword");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Savage"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageAxe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageSlashingGlaiverh");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageSlashingGlaivelh");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageSword");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageThrashingGlaiverh");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageThrashingGlaivelh");
                    generic6= GameInventoryItem.Create<ItemTemplate>(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                    InventoryItem generic7 = new InventoryItem();
                    ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageTwohandedAxe");
                    generic7= GameInventoryItem.Create<ItemTemplate>(tgeneric7);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                    InventoryItem generic8 = new InventoryItem();
                    ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageTwohandedHammer");
                    generic8= GameInventoryItem.Create<ItemTemplate>(tgeneric8);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                    InventoryItem generic9 = new InventoryItem();
                    ItemTemplate tgeneric9 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSavageTwohandedSword");
                    generic9= GameInventoryItem.Create<ItemTemplate>(tgeneric9);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic9);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Shadowblade"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShadowbladeAxe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShadowbladeHeavyAxe");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShadowbladeHeavyAxe2");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShadowbladeHeavySword");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerShadowbladeSword");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Berserker"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerAxerh");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerTwohandedAxe");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerAxelh");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerTwohandedSword");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerSword");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerTwohandedHammer");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                    InventoryItem generic6 = new InventoryItem();
                    ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBerserkerHammer");
                    generic6= GameInventoryItem.Create<ItemTemplate>(tgeneric6);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Skald"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldAxe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldSword");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldTwohandedAxe");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldTwohandedHammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerSkaldTwohandedSword");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Thane"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneAxe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneSword");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneTwohandedAxe");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneTwohandedHammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerThaneTwohandedSword");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                #endregion Midgard Dragonslayer Weapons

                #region Hibernia Dragonslayer Weapons

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Animist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerAnimistStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bainshee"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBainsheeStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Eldritch"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerEldritchStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Enchanter") || (t.CharacterClass.Name == "Enchantress"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerEnchanterStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Mentalist"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerMentalistStaff");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Valewalker"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerValewalkerScythe");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Vampiir"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("VampiirDragonslayerFuarSteel");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Bard"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHarp");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBardBlade");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBardHammer");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Druid"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerDruidBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerDruidHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Warden"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWardenBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerWardenHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Blademaster"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBlademasterBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBlademasterHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerBlademasterSteel");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("BlademasterDragonslayerFuarBlade");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("BlademasterDragonslayerFuarHammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("BlademasterDragonslayerFuarSteel");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Champion"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerChampionBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerChampionHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerChampionSteel");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerChampionWarblade");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerChampionWarhammer");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Hero" || t.CharacterClass.Name == "Heroine"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroHammer");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroSpear");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroSteel");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroWarblade");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                    InventoryItem generic5 = new InventoryItem();
                    ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerHeroWarhammer");
                    generic5= GameInventoryItem.Create<ItemTemplate>(tgeneric5);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Nightshade"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerNightshadeBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerNightshadeSteel");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("NightshadeDragonslayerFuarBlade");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("NightshadeDragonslayerFuarSteel");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                    t.Inventory.RemoveItem(item); t.UpdatePlayerStatus();
                    t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }

                if ((item.Id_nb == "Gjalpinulva's_Remains") && (t.CharacterClass.Name == "Ranger"))
                {
                    InventoryItem generic0 = new InventoryItem();
                    ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("RangerDragonslayerFuarBlade");
                    generic0 = GameInventoryItem.Create<ItemTemplate>(tgeneric0);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                    InventoryItem generic1 = new InventoryItem();
                    ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("RangerDragonslayerFuarSteel");
                    generic1= GameInventoryItem.Create<ItemTemplate>(tgeneric1);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                    InventoryItem generic2 = new InventoryItem();
                    ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerRangerBlade");
                    generic2= GameInventoryItem.Create<ItemTemplate>(tgeneric2);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                    InventoryItem generic3 = new InventoryItem();
                    ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerRangerSteel");
                    generic3= GameInventoryItem.Create<ItemTemplate>(tgeneric3);

                    t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                    InventoryItem generic4 = new InventoryItem();
                    ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey<ItemTemplate> ("DragonslayerRecurveBow");
                    generic4= GameInventoryItem.Create<ItemTemplate>(tgeneric4);

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
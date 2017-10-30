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

    public class ChampionNPC : GameNPC
    {
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
        public override bool Interact(GamePlayer player)
        {

            TurnTo(player, 100);
            this.TargetObject = player;

            if (!base.Interact(player)) return false;
            if (player.ChampionLevel >= 5)
            {
                player.Out.SendMessage("Hello, I'am the Kings Armsmaster.\n" +
                  "I have [Weapons] if you are truely worthy!\n", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
                return true;
            }
            if (player.ChampionLevel <= 4)
                 player.Out.SendMessage("Hello, I'am the Kings Armsmaster.\n" +
                "Reach Champion level 5 and I shall grant you a reward, you are currently Champion Level" + player.ChampionLevel + " !", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
             return true;
        }

        public override bool WhisperReceive(GameLiving player, string str)
        {
            GamePlayer t = (GamePlayer)player;

            switch (str)
            {
                #region Albion Champion Weapons

                case "Weapons":
                    {
                        if (t.CharacterClass.Name == "Wizard" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "UpsilonWizardStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Sorcerer" || t.CharacterClass.Name == "Sorceress" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "UpsilonSorcererStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Cabalist" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "UpsilonCabalistStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Theurgist" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "UpsilonTheurgistStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        if (t.CharacterClass.Name == "Necromancer" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "UpsilonNecromancerStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        if (t.CharacterClass.Name == "Armsman" || t.CharacterClass.Name == "Armswoman" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDexteraEdge");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanDexteraMace");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoArchMace");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoFlamberge");
                            generic4.CopyFrom(tgeneric4);
                           
                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoHalberd");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                            InventoryItem generic6 = new InventoryItem();
                            ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoLance");
                            generic6.CopyFrom(tgeneric6);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                            InventoryItem generic7 = new InventoryItem();
                            ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoMattock");
                            generic7.CopyFrom(tgeneric7);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                            InventoryItem generic8 = new InventoryItem();
                            ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanSatagoPike");
                            generic8.CopyFrom(tgeneric8);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Cleric" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericDexteraMace");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Friar" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarDexteraMace");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarSatagoQuarterStaff");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Heretic" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDexteraBarbedChain");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDexteraFlail");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticDexteraMace");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Infiltrator" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorDexteraEdge");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorLaevusBlade");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorLaevusEdge");
                            generic3.CopyFrom(tgeneric3);
                            
                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Mercenary" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDexteraEdge");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryDexteraMace");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryLaevusBlade");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryLaevusEdge");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryLaevusMace");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Minstrel" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelDexteraEdge");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelDexteraHarp");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Paladin" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDexteraEdge");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinDexteraMace");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinSatagoGreatEdge");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinSatagoGreatHammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinSatagoGreatSword");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Reaver" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDexteraBarbedChain");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDexteraBlade");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDexteraEdge");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDexteraFlail");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverDexteraMace");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Scout" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDexteraBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDexteraBow");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutDexteraEdge");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                #endregion Albion Champion Weapons

                #region Midgard Champion Weapons

                        if (t.CharacterClass.Name == "Bonedancer" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnsuzBonedancerStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Runemaster" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnsuzRunemasterStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        if (t.CharacterClass.Name == "Spiritmaster" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnsuzSpiritmasterStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Warlock" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnsuzWarlockStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Warrior" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorAxe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorSword");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorTwohandedAxe");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorTwohandedHammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazWarriorTwohandedSword");
                            generic5.CopyFrom(tgeneric5);
                            
                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Valkyrie" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazValkyrieSlashingSpear");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazValkyrieSword");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazValkyrieThrustingSpear");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazValkyrieTwohandedSword");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Healer" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHealerTwohandedHammer");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHealerHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Shaman" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShamanHammer");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShamanTwohandedHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Hunter" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazCompoundBow");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHunterSlashingSpear");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHunterSpear");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHunterSword");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazHunterTwohandedSword");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Savage" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageAxe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageSlashingGlaiverh");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageSlashingGlaivelh");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageSword");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageThrashingGlaiverh");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                            InventoryItem generic6 = new InventoryItem();
                            ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageThrashingGlaivelh");
                            generic6.CopyFrom(tgeneric6);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                            InventoryItem generic7 = new InventoryItem();
                            ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageTwohandedAxe");
                            generic7.CopyFrom(tgeneric7);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);

                            InventoryItem generic8 = new InventoryItem();
                            ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageTwohandedHammer");
                            generic8.CopyFrom(tgeneric8);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);

                            InventoryItem generic9 = new InventoryItem();
                            ItemTemplate tgeneric9 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSavageTwohandedSword");
                            generic9.CopyFrom(tgeneric9);
                            
                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic9);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Shadowblade" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShadowbladeAxe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShadowbladeHeavyAxe");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShadowbladeHeavyAxe2");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShadowbladeHeavySword");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazShadowbladeSword");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Skald" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldAxe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldSword");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldTwohandedAxe");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldTwohandedHammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazSkaldTwohandedSword");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Thane" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneAxe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneSword");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneTwohandedAxe");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneTwohandedHammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThurisazThaneTwohandedSword");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        #endregion Midgard Champion Weapons

                #region Hibernia Champion Weapons

                        if (t.CharacterClass.Name == "Animist" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DraiochtAnimistStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Bainshee" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DraiochtBainsheeStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Eldritch" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DraiochtEldritchStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Enchanter" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DraiochtEnchanterStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Mentalist" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DraiochtMentalistStaff");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Valewalker" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharValewalkerScythe");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Vampiir" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirFuarSteel");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Bard" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardDocharHarp");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharBardBlade");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharBardHammer");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Druid" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharDruidBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharDruidHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Warden" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharWardenBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharWardenHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Blademaster" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharBlademasterBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharBlademasterHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharBlademasterSteel");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterFuarBlade");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterFuarHammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterFuarSteel");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Champion" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharChampionBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharChampionHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharChampionSteel");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharChampionWarblade");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharChampionWarhammer");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Hero" || t.CharacterClass.Name == "Heroine" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroHammer");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroSpear");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroSteel");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroWarblade");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                            InventoryItem generic5 = new InventoryItem();
                            ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharHeroWarhammer");
                            generic5.CopyFrom(tgeneric5);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Nightshade" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharNightshadeBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharNightshadeSteel");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeFuarBlade");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeFuarSteel");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }

                        if (t.CharacterClass.Name == "Ranger" && t.ChampionLevel >= 5)
                        {
                            InventoryItem generic0 = new InventoryItem();
                            ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerFuarBlade");
                            generic0.CopyFrom(tgeneric0);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                            InventoryItem generic1 = new InventoryItem();
                            ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerFuarSteel");
                            generic1.CopyFrom(tgeneric1);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                            InventoryItem generic2 = new InventoryItem();
                            ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharRangerBlade");
                            generic2.CopyFrom(tgeneric2);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                            InventoryItem generic3 = new InventoryItem();
                            ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharRangerSteel");
                            generic3.CopyFrom(tgeneric3);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                            InventoryItem generic4 = new InventoryItem();
                            ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DocharRecurveBow");
                            generic4.CopyFrom(tgeneric4);

                            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                            t.UpdatePlayerStatus();
                            t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                    }
                    break;
                        #endregion Hibernia Champion Weapons

                #region Champion Jewelry

                case "Jewelry":
                    {
                        InventoryItem generic0 = new InventoryItem();
                        ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionCloak");
                        generic0.CopyFrom(tgeneric0);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                        InventoryItem generic1 = new InventoryItem();
                        ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionNecklace");
                        generic1.CopyFrom(tgeneric1);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                        InventoryItem generic2 = new InventoryItem();
                        ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionBelt");
                        generic2.CopyFrom(tgeneric2);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                        InventoryItem generic3 = new InventoryItem();
                        ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionJewel");
                        generic3.CopyFrom(tgeneric3);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                        InventoryItem generic4 = new InventoryItem();
                        ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionRing");
                        generic4.CopyFrom(tgeneric4);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                        InventoryItem generic5 = new InventoryItem();
                        ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionBand");
                        generic5.CopyFrom(tgeneric5);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                        InventoryItem generic6 = new InventoryItem();
                        ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionBracer");
                        generic6.CopyFrom(tgeneric6);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                        InventoryItem generic7 = new InventoryItem();
                        ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionWristBand");
                        generic7.CopyFrom(tgeneric7);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                        t.UpdatePlayerStatus();
                        t.Out.SendMessage("Here you are!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    }
                    break;
            }
            return true;
        }
               private void SendReply(GamePlayer target, string msg)
    {
      target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
    }
  }
}
                #endregion Champion Jewelry  
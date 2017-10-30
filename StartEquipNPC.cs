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

  public class StartEquipNPC : GameNPC
  {
    const byte enable_Weapons = 1;
    const byte enable_Armors = 1;

    public override bool Interact(GamePlayer player)
    {   

      TurnTo(player, 100);
      this.TargetObject = player;

      if (!base.Interact(player)) return false;

      if (enable_Weapons == 1 && enable_Armors == 1)
        if (!player.InCombat)
        {
          player.Out.SendMessage("Hello, I'am the Kings Armory Master.\n" +
            "In order to help you in battle our king is providing free equipment from our \n" +
            "realm armory.  We have [Armor], [Weapons] and [Jewelry] in our storage.\n", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
          return true;
        }
        

        if (enable_Armors != 1)
        {
          player.Out.SendMessage("Hello,\nI'am the Armory Master of our Realm.\n" +
            "In order to help our recruits to join our battle our king started to give equipment from our realm armory to aid you in fight.\n" +
            "Do you need any [Armour] or a [Jewelry] ?.\n", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
          return true;
        }

        if (enable_Weapons != 1)
        {
          player.Out.SendMessage("Hello,\nI'am the Armory Master of our Realm.\n" +
            "In order to help our recruits to join our battle our king started to give equipment from our realm armory to aid you in fight.\n" +
            "Do you need any [Armor and Jewelry] or a [Cloak] ?.\n", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
          return true;
        }
        return false;
    }

    public override bool WhisperReceive(GameLiving player, string str)
    {
      GamePlayer t = (GamePlayer)player;

      switch (str)
      {
        case "Weapons":
          if (enable_Weapons == 1)
          if (t.Realm == eRealm.Albion)
          {
            if (!t.InCombat)
            {

              t.Out.SendMessage("What do you need ?\n" +
              "[2h Crushing Weapon]\n[2h Slashing Weapon]\n[2h Thrusting Weapon]\n[1h Slashing Weapon]\n" +
              "[1h Crushing Weapon]\n[1h Thrusting Weapon]\n[Offhand Sword]\n" +
              "[Offhand Crushing Weapon]\n[Offhand Dagger]\n[Friar Staff]\n" +
              "[Caster Staff]\n[Slash Whip]\n[Crush Whip]\n" +
              "[Small Shield]\n[Medium Shield]\n[Large Shield]\n" +
              "[Polearm Slash]\n[Polearm Thrust]\n[Polearm Crush]\n[Longbow]\n[Instrument]\n[Staff]", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            }
          }

          if (t.Realm == eRealm.Midgard)
          {
            if (!t.InCombat)
            {
              t.Out.SendMessage("What do you need ?\n" +
              "[1h Axe]\n[1h Sword]\n[1h Hammer]\n"+
              "[Left Axe]\n[2h Axe]\n[2h Sword]\n" +
              "[2h Hammer]\n[Spear]\n[Composite Bow]\n"+
              "[h2h Slash]\n[h2h Thrust]\n[Small Shield]\n[Medium Shield]\n[Large Shield]\n[Staff]", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            }
          }

          if (t.Realm == eRealm.Hibernia)
          {
            if (!t.InCombat)
            {
              t.Out.SendMessage("What do you need ?\n" +
              "[1h Blade]\n[1h Piercer]\n[1h Blunt]\n"+
              "[Offhand Blade]\n[Offhand Blunt]\n"+
              "[Offhand Piercer]\n[2h Blunt]\n[2h Blade]"+
              "\n[Scythe]\n[Celtic Spear]\n[Small Shield]\n[Medium Shield]\n[Large Shield]\n[Staff]" +
              "\n[Recurve Bow]\n[Harp]", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            }
          }
          break;
      #region Armour
      case "Armor":
          if (enable_Armors == 1)
          {
              if (t.CharacterClass.Name == "Wizard") 
            {
              InventoryItem generic0 = new InventoryItem();
              ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicVest");
              generic0.CopyFrom(tgeneric0);

              InventoryItem generic1 = new InventoryItem();
              ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicLegs");
              generic1.CopyFrom(tgeneric1);

              InventoryItem generic2 = new InventoryItem();
              ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicArms");
              generic2.CopyFrom(tgeneric2);

              InventoryItem generic3 = new InventoryItem();
              ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicGloves");
              generic3.CopyFrom(tgeneric3);

              InventoryItem generic4 = new InventoryItem();
              ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicBoots");
              generic4.CopyFrom(tgeneric4);

              InventoryItem generic5 = new InventoryItem();
              ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
              generic5.CopyFrom(tgeneric5);

              InventoryItem generic6 = new InventoryItem();
              ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
              generic6.CopyFrom(tgeneric6);              

              InventoryItem generic7 = new InventoryItem();
              ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WizardEpicHelm");
              generic7.CopyFrom(tgeneric7);


              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
              t.UpdatePlayerStatus();
              SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Priest" || t.CharacterClass.Name == "Priestess")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PriestEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Minstrel")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MinstrelEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Sorcerer" || t.CharacterClass.Name == "Sorceress")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SorcererEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Cleric")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ClericEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Paladin")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PaladinEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Mercenary")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MercenaryEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Reaver")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ReaverEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Cabalist")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "CabalistEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Necromancer")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NecromancerEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Infiltrator")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "InfiltratorEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Scout")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ScoutEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Armsman" || t.CharacterClass.Name == "Armswoman")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ArmsmanEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Theurgist")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "TheurgistEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Heretic")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HereticEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Friar")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeStaff");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FriarEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Spiritmaster")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SpiritmasterEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Runemaster")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RunemasterEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Bonedancer")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BonedancerEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Warlock")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarlockEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Hunter")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HunterEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Shadowblade")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShadowbladeEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Healer")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HealerEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Shaman")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ShamanEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Warrior")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WarriorEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Berserker")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BerserkerEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Thane")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ThaneEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Skald")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SkaldEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Savage")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "SavageEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Valkyrie")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValkyrieEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Champion")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Enchanter")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EnchanterEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Eldritch")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "EldritchEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Mentalist")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "MentalistEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Animist")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "AnimistEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

                if (t.CharacterClass.Name == "Champion")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ChampionEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Bard")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BardEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Nightshade")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "NightshadeEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Ranger")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "RangerEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Hero" || t.CharacterClass.Name == "Heroine")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "HeroEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Warden")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "WardenEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Blademaster")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BlademasterEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
            if (t.CharacterClass.Name == "Druid")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "DruidEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Valewalker")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "ValewalkerEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Bainshee")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
                generic6.CopyFrom(tgeneric6);

                InventoryItem generic7 = new InventoryItem();
                ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "BainsheeEpicHelm");
                generic7.CopyFrom(tgeneric7);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }

            if (t.CharacterClass.Name == "Vampiir")
            {
                InventoryItem generic0 = new InventoryItem();
                ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicVest");
                generic0.CopyFrom(tgeneric0);

                InventoryItem generic1 = new InventoryItem();
                ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicLegs");
                generic1.CopyFrom(tgeneric1);

                InventoryItem generic2 = new InventoryItem();
                ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicArms");
                generic2.CopyFrom(tgeneric2);

                InventoryItem generic3 = new InventoryItem();
                ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicGloves");
                generic3.CopyFrom(tgeneric3);

                InventoryItem generic4 = new InventoryItem();
                ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicBoots");
                generic4.CopyFrom(tgeneric4);

                InventoryItem generic5 = new InventoryItem();
                ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
                generic5.CopyFrom(tgeneric5);

                InventoryItem generic6 = new InventoryItem();
                ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "VampiirEpicHelm");
                generic6.CopyFrom(tgeneric6);


                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);
                t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);
                t.UpdatePlayerStatus();
                SendReply(t, "Here you are !");
            }
          } 
          break;


        case "Jewelry":
            if (t.Realm != eRealm.None)
          {
            InventoryItem generic45 = new InventoryItem();
            ItemTemplate tgeneric45 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeJewel1");
            generic45.CopyFrom(tgeneric45);

            InventoryItem generic46 = new InventoryItem();
            ItemTemplate tgeneric46 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeBracer1");
            generic46.CopyFrom(tgeneric46);

            InventoryItem generic47 = new InventoryItem();
            ItemTemplate tgeneric47 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeRing1");
            generic47.CopyFrom(tgeneric47);

            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic45);
            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic46);
            t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic47);
            t.UpdatePlayerStatus();
            SendReply(t, "Here you are !");
          }
          break;
      #endregion
      case "2h Crushing Weapon":
            if (t.Realm == eRealm.Albion)
            {
              InventoryItem generic8 = new InventoryItem();
              ItemTemplate tgeneric8 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbCrush2h");
              generic8.CopyFrom(tgeneric8);
              t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic8);
              t.UpdatePlayerStatus();              
            }
           break;

         case "2h Slashing Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic9 = new InventoryItem();
             ItemTemplate tgeneric9 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbSlash2h");
             generic9.CopyFrom(tgeneric9);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic9);
             t.UpdatePlayerStatus();
           }
           break;

         case "2h Thrusting Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic10 = new InventoryItem();
             ItemTemplate tgeneric10 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbThrust2H");
             generic10.CopyFrom(tgeneric10);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic10);
             t.UpdatePlayerStatus();
           }
           break;

         case "1h Slashing Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic11 = new InventoryItem();
             ItemTemplate tgeneric11 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbSlash");
             generic11.CopyFrom(tgeneric11);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic11);
             t.UpdatePlayerStatus();
           }
           break;

         case "1h Crushing Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic12 = new InventoryItem();
             ItemTemplate tgeneric12 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbCrush");
             generic12.CopyFrom(tgeneric12);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic12);
             t.UpdatePlayerStatus();
           }
           break;

         case "1h Thrusting Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic13 = new InventoryItem();
             ItemTemplate tgeneric13 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbThrust");
             generic13.CopyFrom(tgeneric13);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic13);
             t.UpdatePlayerStatus();
           }
           break;

         case "Offhand Sword":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic14 = new InventoryItem();
             ItemTemplate tgeneric14 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbSlash");
             generic14.CopyFrom(tgeneric14);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic14);
             t.UpdatePlayerStatus();
           }
           break;

         case "Offhand Crushing Weapon":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic15 = new InventoryItem();
             ItemTemplate tgeneric15 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbCrush");
             generic15.CopyFrom(tgeneric15);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic15);
             t.UpdatePlayerStatus();
           }
           break;

         case "Offhand Dagger":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic16 = new InventoryItem();
             ItemTemplate tgeneric16 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbThrust");
             generic16.CopyFrom(tgeneric16);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic16);
             t.UpdatePlayerStatus();
           }
           break;

         case "Friar Staff":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic17 = new InventoryItem();
             ItemTemplate tgeneric17 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeStaff");
             generic17.CopyFrom(tgeneric17);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic17);
             t.UpdatePlayerStatus();
           }
           break;

         case "Caster Staff":
             InventoryItem generic18 = new InventoryItem();
             ItemTemplate tgeneric18 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
             generic18.CopyFrom(tgeneric18);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic18);
             t.UpdatePlayerStatus();
           break;

         case "Slash Whip":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic19 = new InventoryItem();
             ItemTemplate tgeneric19 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbFlexSlash");
             generic19.CopyFrom(tgeneric19);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic19);
             t.UpdatePlayerStatus();
           }
           break;

         case "Crush Whip":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic20 = new InventoryItem();
             ItemTemplate tgeneric20 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbFlexCrush");
             generic20.CopyFrom(tgeneric20);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic20);
             t.UpdatePlayerStatus();
           }
           break;

         case "Bow":
             InventoryItem generic21 = new InventoryItem();
             ItemTemplate tgeneric21 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeBow");
             generic21.CopyFrom(tgeneric21);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic21);
             t.UpdatePlayerStatus();
           break;

         case "Small Shield":
             InventoryItem generic22 = new InventoryItem();
             ItemTemplate tgeneric22 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeSmallShield");
             generic22.CopyFrom(tgeneric22);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic22);
             t.UpdatePlayerStatus();
         break;

         case "Medium Shield":
           InventoryItem generic23 = new InventoryItem();
           ItemTemplate tgeneric23 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMediumShield");
           generic23.CopyFrom(tgeneric23);
           t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic23);
           t.UpdatePlayerStatus();
         break;

         case "Large Shield":
           InventoryItem generic24 = new InventoryItem();
           ItemTemplate tgeneric24 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeLargeShield");
           generic24.CopyFrom(tgeneric24);
           t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic24);
           t.UpdatePlayerStatus();
         break;

     case "Polearm Crush":
         InventoryItem generic25 = new InventoryItem();
         ItemTemplate tgeneric25 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbCrushPolearm");
         generic25.CopyFrom(tgeneric25);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic25);
         t.UpdatePlayerStatus();
         break;

         case "Polearm Slash":
           if (t.Realm == eRealm.Albion)
           {
             InventoryItem generic26 = new InventoryItem();
             ItemTemplate tgeneric26 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbSlashPolearm");
             generic26.CopyFrom(tgeneric26);
             t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic26);
             t.UpdatePlayerStatus();
           }
         break;

         case "Polearm Thrust":
         if (t.Realm == eRealm.Albion)
         {
           InventoryItem generic27 = new InventoryItem();
           ItemTemplate tgeneric27 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeAlbThrustPolearm");
           generic27.CopyFrom(tgeneric27);
           t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic27);
           t.UpdatePlayerStatus();
         }
         break;

         case "Instrument":
         if (t.Realm == eRealm.Albion)
         {
           InventoryItem generic28 = new InventoryItem();
           ItemTemplate tgeneric28 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "freeharp");
           generic28.CopyFrom(tgeneric28);
           t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic28);
           t.UpdatePlayerStatus();
         }
         break;

       case "Longbow":
         if (t.Realm == eRealm.Albion)
         {
           InventoryItem generic30 = new InventoryItem();
           ItemTemplate tgeneric30 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeBowa");
           generic30.CopyFrom(tgeneric30);
           t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic30);
           t.UpdatePlayerStatus();
         }
         break;       

       case "1h Axe":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic42 = new InventoryItem();
         ItemTemplate tgeneric42 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidAxe");
         generic42.CopyFrom(tgeneric42);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic42);
         t.UpdatePlayerStatus();
       }
       break;

     case "1h Sword":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic43 = new InventoryItem();
         ItemTemplate tgeneric43 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidSlash");
         generic43.CopyFrom(tgeneric43);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic43);
         t.UpdatePlayerStatus();
       }
       break;

     case "1h Hammer":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidCrush");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
       }
       break;

     case "Left Axe":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidAxe");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
       }
       break;

     case "2h Axe":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidAxe2H");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
       }
       break;

     case "2h Hammer":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidCrush2H");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
       }
       break;

     case "2h Sword":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidSlash2H");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
       }
       break;

     case "Spear":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic45 = new InventoryItem();
         ItemTemplate tgeneric45 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidSpearSlash");
         generic45.CopyFrom(tgeneric45);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic45);
         t.UpdatePlayerStatus();
       }
       break;

     case "Composite Bow":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic46 = new InventoryItem();
         ItemTemplate tgeneric46 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeBowm");
         generic46.CopyFrom(tgeneric46);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic46);
         t.UpdatePlayerStatus();
       }
       break;

     case "h2h Slash":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic47 = new InventoryItem();
         ItemTemplate tgeneric47 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidH2HSlash");
         generic47.CopyFrom(tgeneric47);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic47);
         t.UpdatePlayerStatus();
       }
       break;

     case "h2h Thrust":
       if (t.Realm == eRealm.Midgard)
       {
         InventoryItem generic48 = new InventoryItem();
         ItemTemplate tgeneric48 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeMidH2HThrust");
         generic48.CopyFrom(tgeneric48);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic48);
         t.UpdatePlayerStatus();
       }
       break;

     case "1h Blade":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic31 = new InventoryItem();
         ItemTemplate tgeneric31 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibSlash");
         generic31.CopyFrom(tgeneric31);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic31);
         t.UpdatePlayerStatus();
       }
       break;

     case "1h Piercer":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic32 = new InventoryItem();
         ItemTemplate tgeneric32 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibThrust");
         generic32.CopyFrom(tgeneric32);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic32);
         t.UpdatePlayerStatus();
       }
       break;

     case "1h Blunt":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic33 = new InventoryItem();
         ItemTemplate tgeneric33 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibCrush");
         generic33.CopyFrom(tgeneric33);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic33);
         t.UpdatePlayerStatus();
       }
       break;

     case "Offhand Blade":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic34 = new InventoryItem();
         ItemTemplate tgeneric34 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibSlash");
         generic34.CopyFrom(tgeneric34);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic34);
         t.UpdatePlayerStatus();
       }
       break;

     case "Offhand Blunt":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic35 = new InventoryItem();
         ItemTemplate tgeneric35 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibCrush");
         generic35.CopyFrom(tgeneric35);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic35);
         t.UpdatePlayerStatus();
       }
       break;

     case "Offhand Piercer":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic36 = new InventoryItem();
         ItemTemplate tgeneric36 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibThrust");
         generic36.CopyFrom(tgeneric36);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic36);
         t.UpdatePlayerStatus();
       }
       break;

     case "2h Blunt":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic37 = new InventoryItem();
         ItemTemplate tgeneric37 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibCrush2H");
         generic37.CopyFrom(tgeneric37);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic37);
         t.UpdatePlayerStatus();
       }
       break;

     case "2h Blade":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic38 = new InventoryItem();
         ItemTemplate tgeneric38 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibSlash2H");
         generic38.CopyFrom(tgeneric38);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic38);
         t.UpdatePlayerStatus();
       }
       break;

     case "Scythe":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic39 = new InventoryItem();
         ItemTemplate tgeneric39 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibScythe");
         generic39.CopyFrom(tgeneric39);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic39);
         t.UpdatePlayerStatus();
       }
       break;

     case "Celtic Spear":
       if (t.Realm == eRealm.Hibernia)
       {
         InventoryItem generic40 = new InventoryItem();
         ItemTemplate tgeneric40 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeHibSpearThrust");
         generic40.CopyFrom(tgeneric40);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic40);
         t.UpdatePlayerStatus();
       }
       break;

     case "Recurve Bow":
         InventoryItem generic41 = new InventoryItem();
         ItemTemplate tgeneric41 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeBowh");
         generic41.CopyFrom(tgeneric41);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic41);
         t.UpdatePlayerStatus();
       break;

     case "Harp":
       if (t.Realm != eRealm.None)
       {
         InventoryItem generic42 = new InventoryItem();
         ItemTemplate tgeneric42 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "freeharp");
         generic42.CopyFrom(tgeneric42);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic42);
         t.UpdatePlayerStatus();
       }
       break;

     case "Staff":
       if (t.Realm != eRealm.None)
       {
         InventoryItem generic43 = new InventoryItem();
         ItemTemplate tgeneric43 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "starter_staff_caster");
         generic43.CopyFrom(tgeneric43);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic43);
         t.UpdatePlayerStatus();
       }
       break;

     case "Cloak":
       if (t.Realm != eRealm.None)
       {
         InventoryItem generic44 = new InventoryItem();
         ItemTemplate tgeneric44 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "FreeCloak");
         generic44.CopyFrom(tgeneric44);
         t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic44);
         t.UpdatePlayerStatus();
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


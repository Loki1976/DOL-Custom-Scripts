/*
 *  Script by Taovil/ Dean
 * 
 */

using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using DOL.Database;
using log4net;


namespace DOL.GS.Scripts
{
    public class FreeStuff : GameNPC
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Declarations
        private static ItemTemplate free_epic_cloak = null; //Free Epic Cloak

        private static ItemTemplate free_chain_vest = null; //Free Chain Vest
        private static ItemTemplate free_chain_arms = null; //Free Chain Arms
        private static ItemTemplate free_chain_legs = null; //Free Chain Legs
        private static ItemTemplate free_chain_helm = null; //Free Chain Helm
        private static ItemTemplate free_chain_gloves = null; //Free Chain Gloves
        private static ItemTemplate free_chain_boots = null; //Free Chain Boots

        private static ItemTemplate free_cloth_vest = null; //Free Cloth Vest
        private static ItemTemplate free_cloth_arms = null; //Free Cloth Arms
        private static ItemTemplate free_cloth_legs = null; //Free Cloth Legs
        private static ItemTemplate free_cloth_helm = null; //Free Cloth Helm
        private static ItemTemplate free_cloth_gloves = null; //Free Cloth Gloves
        private static ItemTemplate free_cloth_boots = null; //Free Cloth Boots

        private static ItemTemplate free_leather_vest = null; //Free Leather Vest
        private static ItemTemplate free_leather_arms = null; //Free Leather Arms
        private static ItemTemplate free_leather_legs = null; //Free Leather Legs
        private static ItemTemplate free_leather_helm = null; //Free Leather Helm
        private static ItemTemplate free_leather_gloves = null; //Free Leather Gloves
        private static ItemTemplate free_leather_boots = null; //Free Leather Boots

        private static ItemTemplate free_studded_vest = null; //Free Studded Vest
        private static ItemTemplate free_studded_arms = null; //Free Studded Arms
        private static ItemTemplate free_studded_legs = null; //Free Studded Legs
        private static ItemTemplate free_studded_helm = null; //Free Studded Helm
        private static ItemTemplate free_studded_gloves = null; //Free Studded Gloves
        private static ItemTemplate free_studded_boots = null; //Free Studded Boots

        private static ItemTemplate free_plate_vest = null; //Free Plate Vest
        private static ItemTemplate free_plate_arms = null; //Free Plate Arms
        private static ItemTemplate free_plate_legs = null; //Free Plate Legs
        private static ItemTemplate free_plate_helm = null; //Free Plate Helm
        private static ItemTemplate free_plate_gloves = null; //Free Plate Gloves
        private static ItemTemplate free_plate_boots = null; //Free Plate Boots

        //weapons
        private static ItemTemplate free_staff = null; //Free Caster Staff
        private static ItemTemplate free_slash_alb = null; //Free Slash
        private static ItemTemplate free_slash_mid = null; //Free Slash
        private static ItemTemplate free_slash_hib = null; //Free Slash
        private static ItemTemplate free_thrust_alb = null; //Free Thrust
        private static ItemTemplate free_thrust_hib = null; //Free Thrust
        private static ItemTemplate free_crush_alb = null; //Free Crush
        private static ItemTemplate free_crush_mid = null; //Free Crush
        private static ItemTemplate free_crush_hib = null; //Free Crush
        private static ItemTemplate free_axe = null; //Free Axe
        private static ItemTemplate free_long_bow = null; //Free Long Bow
        private static ItemTemplate free_composite_bow = null; //Free Composite Bow
        private static ItemTemplate free_recurved_bow = null; //Free Recurved Bow
        private static ItemTemplate free_short_bow = null; //Free Short Bow
        private static ItemTemplate free_crossbow = null; //Free Crossbow
        private static ItemTemplate free_twohanded_slash_alb = null; //Free TwoHanded Slash
        private static ItemTemplate free_twohanded_slash_hib = null; //Free TwoHanded Slash
        private static ItemTemplate free_twohanded_crush_alb = null; //Free TwoHanded Crush
        private static ItemTemplate free_twohanded_crush_hib = null; //Free TwoHanded Crush
        private static ItemTemplate free_twohanded_axe = null; //Free TwoHanded Axe
        private static ItemTemplate free_twohanded_sword = null; //Free TwoHanded Sword
        private static ItemTemplate free_twohanded_hammer = null; //Free TwoHanded Hammer
        private static ItemTemplate free_small_shield = null; //Free Small Shield
        private static ItemTemplate free_medium_shield = null; //Free Medium Shield
        private static ItemTemplate free_large_shield = null; //Free Large Shield
        private static ItemTemplate free_slash_spear = null; //Free Slash Spear
        private static ItemTemplate free_thrust_spear = null; //Free Thrust Spear
        private static ItemTemplate free_slash_flail = null; //Free Slash Flail
        private static ItemTemplate free_thrust_flail = null; //Free Thrust Flail
        private static ItemTemplate free_crush_flail = null; //Free Crush Flail
        private static ItemTemplate free_slash_claw = null; //Free Slash Claw
        private static ItemTemplate free_thrust_claw = null; //Free Thrust Claw
        private static ItemTemplate free_harp = null; //Free Harp
        private static ItemTemplate free_scythe = null; //Free Scythe
        private static ItemTemplate free_crush_polearm = null; //Free Crush Polearm
        private static ItemTemplate free_thrust_polearm = null; //Free Thrust Polearm
        private static ItemTemplate free_slash_polearm = null; //Free Slash Polearm

	    private static ItemTemplate free_belt = null; //Free Belt
        private static ItemTemplate free_jewel = null; //Free Jewel
        private static ItemTemplate free_stats_ring = null; //Free Stats Ring
        private static ItemTemplate free_overcap_ring = null; //Free Overcap Ring
        private static ItemTemplate free_resists_bracer = null; //Free Resists Bracer
        private static ItemTemplate free_skill_bracer = null; //Free Skill Bracer
        private static ItemTemplate free_necklace = null; //Free Necklace
		
        private static ItemTemplate mlrespectoken = null;

        #endregion Declarations

		
		 public override bool AddToWorld()
        {
			
			Level = 55;
			Name = "Free Stuff";
            GuildName = "Armor and Weapons";
			Model = 1903;
            base.AddToWorld();
            this.Flags = eFlags.PEACE ;
            return true;
        }
        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            if (log.IsInfoEnabled)
                log.Info("Free Stuff NPC Initializing...");
           
           

            #region Armor
            ItemTemplate item = null;
            item = GameServer.Database.FindObjectByKey<ItemTemplate>("free_chain_vest");
            if (item == null)
            #region Free Chain Armor

            //Free Chain Vest
            item = new ItemTemplate();
            item.Id_nb = "free_chain_vest";
            item.Name = "Free Chain Vest";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 25;
            item.Weight = 50;
            item.Model = 776;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
		
            free_chain_vest = item;

            //Free Chain Arms
            item = new ItemTemplate();
            item.Id_nb = "free_chain_arms";
            item.Name = "Free Chain Arms";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 28;
            item.Weight = 25;
            item.Model = 778;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_chain_arms = item;


            //Free Chain Legs
            item = new ItemTemplate();
            item.Id_nb = "free_chain_legs";
            item.Name = "Free Chain Legs";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 27;
            item.Weight = 50;
            item.Model = 777;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_chain_legs = item;


            //Free Chain Boots
            item = new ItemTemplate();
            item.Id_nb = "free_chain_boots";
            item.Name = "Free Chain Boots";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 23;
            item.Weight = 30;
            item.Model = 780;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_chain_boots = item;


            //Free Chain Gloves
            item = new ItemTemplate();
            item.Id_nb = "free_chain_gloves";
            item.Name = "Free Chain Gloves";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 22;
            item.Weight = 32;
            item.Model = 779;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_chain_gloves = item;


            //Free Chain Helm
            item = new ItemTemplate();
            item.Id_nb = "free_chain_helm";
            item.Name = "Free Chain Helm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 27;
            item.Object_Type = 35;
            item.Item_Type = 21;
            item.Weight = 32;
            item.Model = 1291;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);

            free_chain_helm = item;


            #endregion Free Chain Armor
            #region Free Cloth Armor

            //Free Cloth Vest
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_vest";
            item.Name = "Free Cloth Vest";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 25;
            item.Weight = 20;
            item.Model = 2171;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_vest = item;

            //Free Cloth Arms
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_arms";
            item.Name = "Free Cloth Arms";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 28;
            item.Weight = 10;
            item.Model = 2161;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_arms = item;

            //Free Cloth Legs
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_legs";
            item.Name = "Free Cloth Legs";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 27;
            item.Weight = 14;
            item.Model = 2167;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_legs = item;


            //Free Cloth Boots
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_boots";
            item.Name = "Free Cloth Boots";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 23;
            item.Weight = 8;
            item.Model = 2166;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_boots = item;


            //Free Cloth Gloves
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_gloves";
            item.Name = "Free Cloth Gloves";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 22;
            item.Weight = 6;
            item.Model = 2181;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_gloves = item;


            //Free Cloth Helm
            item = new ItemTemplate();
            item.Id_nb = "free_cloth_helm";
            item.Name = "Free Cloth Helm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 51;
            item.SPD_ABS = 0;
            item.Object_Type = 32;
            item.Item_Type = 21;
            item.Weight = 8;
            item.Model = 1291;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_cloth_helm = item;

            #endregion Free Cloth Armor
            #region Free Leather Armor

            //Free Leather Vest
            item = new ItemTemplate();
            item.Id_nb = "free_leather_vest";
            item.Name = "Free Leather Vest";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 25;
            item.Weight = 30;
            item.Model = 1267;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_vest = item;

            //Free Leather Arms
            item = new ItemTemplate();
            item.Id_nb = "free_leather_arms";
            item.Name = "Free Leather Arms";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 28;
            item.Weight = 24;
            item.Model = 1269;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_arms = item;

            //Free Leather Legs
            item = new ItemTemplate();
            item.Id_nb = "free_leather_legs";
            item.Name = "Free Leather Legs";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 27;
            item.Weight = 30;
            item.Model = 1268;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_legs = item;


            //Free Leather Boots
            item = new ItemTemplate();
            item.Id_nb = "free_leather_boots";
            item.Name = "Free Leather Boots";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 23;
            item.Weight = 16;
            item.Model = 1270;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_boots = item;


            //Free Leather Gloves
            item = new ItemTemplate();
            item.Id_nb = "free_leather_gloves";
            item.Name = "Free Leather Gloves";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 22;
            item.Weight = 6;
            item.Model = 1271;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_gloves = item;


            //Free Leather Helm
            item = new ItemTemplate();
            item.Id_nb = "free_leather_helm";
            item.Name = "Free Leather Helm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 10;
            item.Object_Type = 33;
            item.Item_Type = 21;
            item.Weight = 8;
            item.Model = 1291;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;9;10;23;25;43;48;49;50;58;60;61;62";
			Load_Whria(ref item);
            free_leather_helm = item;

            #endregion Free Leather Armor
            #region Free Studded Armor

            //Free Studded Vest
            item = new ItemTemplate();
            item.Id_nb = "free_studded_vest";
            item.Name = "Free Studded Vest";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 25;
            item.Weight = 40;
            item.Model = 1192;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_vest = item;

            //Free Studded Arms
            item = new ItemTemplate();
            item.Id_nb = "free_studded_arms";
            item.Name = "Free Studded Arms";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 28;
            item.Weight = 36;
            item.Model = 1194;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_arms = item;

            //Free Studded Legs
            item = new ItemTemplate();
            item.Id_nb = "free_studded_legs";
            item.Name = "Free Studded Legs";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 27;
            item.Weight = 42;
            item.Model = 1193;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_legs = item;


            //Free Studded Boots
            item = new ItemTemplate();
            item.Id_nb = "free_studded_boots";
            item.Name = "Free Studded Boots";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 23;
            item.Weight = 10;
            item.Model = 1196;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_boots = item;


            //Free Studded Gloves
            item = new ItemTemplate();
            item.Id_nb = "free_studded_gloves";
            item.Name = "Free Studded Gloves";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 22;
            item.Weight = 10;
            item.Model = 1195;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_gloves = item;


            //Free Studded Helm
            item = new ItemTemplate();
            item.Id_nb = "free_studded_helm";
            item.Name = "Free Studded Helm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 19;
            item.Object_Type = 34;
            item.Item_Type = 21;
            item.Weight = 24;
            item.Model = 1291;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3;48;43;50;31;25;32";
			Load_Whria(ref item);
            free_studded_helm = item;

            #endregion Free Studded Armor
            #region Free Plate Armor

            //Free Plate Vest
            item = new ItemTemplate();
            item.Id_nb = "free_plate_vest";
            item.Name = "Free Plate Vest";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 25;
            item.Weight = 60;
            item.Model = 810;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_vest = item;

            //Free Plate Arms
            item = new ItemTemplate();
            item.Id_nb = "free_plate_arms";
            item.Name = "Free Plate Arms";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 28;
            item.Weight = 18;
            item.Model = 812;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_arms = item;

            //Free Plate Legs
            item = new ItemTemplate();
            item.Id_nb = "free_plate_legs";
            item.Name = "Free Plate Legs";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 27;
            item.Weight = 60;
            item.Model = 811;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_legs = item;


            //Free Plate Boots
            item = new ItemTemplate();
            item.Id_nb = "free_plate_boots";
            item.Name = "Free Plate Boots";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 23;
            item.Weight = 40;
            item.Model = 814;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_boots = item;


            //Free Plate Gloves
            item = new ItemTemplate();
            item.Id_nb = "free_plate_gloves";
            item.Name = "Free Plate Gloves";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 22;
            item.Weight = 12;
            item.Model = 813;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_gloves = item;


            //Free Plate Helm
            item = new ItemTemplate();
            item.Id_nb = "free_plate_helm";
            item.Name = "Free Plate Helm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 102;
            item.SPD_ABS = 34;
            item.Object_Type = 36;
            item.Item_Type = 21;
            item.Weight = 40;
            item.Model = 1291;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
			Load_Whria(ref item);
            free_plate_helm = item;

            #endregion Free Plate Armor

            #endregion Armor
            #region Weapons

            #region Free Staff (Caster)
            //free caster staff
            item = new ItemTemplate();
            item.Id_nb = "free_staff";
            item.Name = "Free Staff";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 44;
            item.Hand = 1;
            item.Type_Damage = 1;
            item.Object_Type = 8;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 442;
            item.Bonus = 35;
            item.Bonus1 = 50;
            item.Bonus1Type = 165;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_staff = item;
            #endregion Free Staff (Caster)
            #region Free Slash Alb (sword) Left hand
            //free slash (sword)
            item = new ItemTemplate();
            item.Id_nb = "free_slash_alb";
            item.Name = "Free Slash Alb";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 2;
            item.Object_Type = 3;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1017;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_alb = item;
            #endregion Free Slash Alb (sword) Left hand
            #region Free Slash Mid (sword) Left hand
            //free slash (sword)
            item = new ItemTemplate();
            item.Id_nb = "free_slash_mid";
            item.Name = "Free Slash Mid";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 2;
            item.Object_Type = 11;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1017;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_mid = item;
            #endregion Free Slash Mid (sword) Left hand
            #region Free Slash Hib (sword) Left hand
            //free slash (sword)
            item = new ItemTemplate();
            item.Id_nb = "free_slash_hib";
            item.Name = "Free Slash hib";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 2;
            item.Object_Type = 19;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1017;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_hib = item;
            #endregion Free Slash Hib (sword) Left hand
            #region Free Thrust Alb (Pierce) Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_alb";
            item.Name = "Free Thrust Alb";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 3;
            item.Object_Type = 4;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 2684;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_alb = item;
            #endregion Free Thrust Alb (Pierce) Left hand
            #region Free Thrust Hib (Pierce) Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_hib";
            item.Name = "Free Thrust Hib";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 3;
            item.Object_Type = 21;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 2684;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_hib = item;
            #endregion Free Thrust Hib (Pierce) Left hand
            #region Free Crush Alb (blunt) Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_crush_alb";
            item.Name = "Free Crush Alb";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 1;
            item.Object_Type = 2;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1009;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crush_alb = item;
            #endregion Free Crush Alb (blunt) Left hand
            #region Free Crush Mid (blunt) Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_crush_mid";
            item.Name = "Free Crush Mid";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 1;
            item.Object_Type = 12;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1009;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crush_mid = item;
            #endregion Free Crush Mid (blunt) Left hand
            #region Free Crush Hib (blunt) Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_crush_hib";
            item.Name = "Free Crush Hib";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 0;
            item.Type_Damage = 1;
            item.Object_Type = 20;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1009;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crush_hib = item;
            #endregion Free Crush Hib (blunt) Left hand
            #region Free Axe Left hand
            item = new ItemTemplate();
            item.Id_nb = "free_axe";
            item.Name = "Free Axe";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 35;
            item.Hand = 0;
            item.Type_Damage = 2;
            item.Object_Type = 17;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 1010;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_axe = item;
            #endregion Free Axe Left hand
            #region Free Long Bow (Scout)
            item = new ItemTemplate();
            item.Id_nb = "free_long_bow";
            item.Name = "Free Long Bow";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 54;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 9;
            item.Item_Type = 13;
            item.Weight = 40;
            item.Model = 849;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "3"; //scout
            free_long_bow = item;
            #endregion Free Long Bow (Scout)
            #region Free Composite Bow (Hunter)
            item = new ItemTemplate();
            item.Id_nb = "free_composite_bow";
            item.Name = "Free Composite Bow";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 44;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 15;
            item.Item_Type = 13;
            item.Weight = 40;
            item.Model = 564;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "25"; //hunter
            free_composite_bow = item;
            #endregion Free Composite Bow (Hunter)
            #region Free Recurved Bow (Ranger)
            item = new ItemTemplate();
            item.Id_nb = "free_recurved_bow";
            item.Name = "Free Recurved Bow";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 54;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 18;
            item.Item_Type = 13;
            item.Weight = 40;
            item.Model = 925;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            item.AllowedClasses = "50";//ranger
            free_recurved_bow = item;
            #endregion Free Recurved Bow (Ranger)
            #region Free Short Bow
            item = new ItemTemplate();
            item.Id_nb = "free_short_bow";
            item.Name = "Free Short Bow";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 5;
            item.Item_Type = 13;
            item.Weight = 40;
            item.Model = 922;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_short_bow = item;
            #endregion Free Short Bow
            #region Free Crossbow
            item = new ItemTemplate();
            item.Id_nb = "free_crossbow";
            item.Name = "Free Crossbow";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Type_Damage = 3;
            item.Object_Type = 10;
            item.Item_Type = 13;
            item.Weight = 40;
            item.Model = 226;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crossbow = item;
            #endregion Free Crossbow
            #region Free TwoHanded Slash Alb
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_slash_alb";
            item.Name = "Free TwoHanded Slash Alb";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 51;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 6;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 907;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_slash_alb = item;
            #endregion Free Twohanded Slash Alb
            #region Free TwoHanded Slash Hib
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_slash_hib";
            item.Name = "Free TwoHanded Slash Hib";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 51;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 22;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 907;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_slash_hib = item;
            #endregion Free Twohanded Slash Hib
            #region Free TwoHanded Crush Alb
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_crush_alb";
            item.Name = "Free TwoHanded Crush Alb";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 48;
            item.Hand = 1;
            item.Type_Damage = 1;
            item.Object_Type = 6;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 574;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_crush_alb = item;
            #endregion Free Twohanded Crush Alb
            #region Free TwoHanded Crush Hib
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_crush_hib";
            item.Name = "Free TwoHanded Crush Hib";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 48;
            item.Hand = 1;
            item.Type_Damage = 1;
            item.Object_Type = 22;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 574;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_crush_hib = item;
            #endregion Free Twohanded Crush Hib
            #region Free TwoHanded Axe
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_axe";
            item.Name = "Free TwoHanded Axe";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 51;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 13;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 577;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_axe = item;
            #endregion Free TwoHanded Axe
            #region Free TwoHanded Sword
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_sword";
            item.Name = "Free TwoHanded Sword";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 56;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 11;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 658;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_sword = item;
            #endregion Free TwoHanded Sword
            #region Free TwoHanded Hammer
            item = new ItemTemplate();
            item.Id_nb = "free_twohanded_hammer";
            item.Name = "Free TwoHanded Hammer";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 48;
            item.Hand = 1;
            item.Type_Damage = 1;
            item.Object_Type = 12;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 574;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_twohanded_hammer = item;
            #endregion Free TwoHanded Hammer
            #region Free Small Shield
            item = new ItemTemplate();
            item.Id_nb = "free_small_shield";
            item.Name = "Free Small Shield";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 28;
            item.Hand = 2;
            item.Type_Damage = 1;
            item.Object_Type = 42;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 2218;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_small_shield = item;
            #endregion Free Small Shield
            #region Free Medium Shield
            item = new ItemTemplate();
            item.Id_nb = "free_medium_shield";
            item.Name = "Free Medium Shield";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 32;
            item.Hand = 2;
            item.Type_Damage = 2;
            item.Object_Type = 42;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 2219;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_medium_shield = item;
            #endregion Free Medium Shield
            #region Free Large Shield
            item = new ItemTemplate();
            item.Id_nb = "free_large_shield";
            item.Name = "Free Large Shield";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Hand = 2;
            item.Type_Damage = 3;
            item.Object_Type = 42;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 2220;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_large_shield = item;
            #endregion Free Large Shield
            #region Free Slash Spear
            item = new ItemTemplate();
            item.Id_nb = "free_slash_spear";
            item.Name = "Free Slash Spear";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 14;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 657;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_spear = item;
            #endregion Free Slash Spear
            #region Free Thrust Spear
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_spear";
            item.Name = "Free Thrust Spear";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 14;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 657;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_spear = item;
            #endregion Free Thrust Spear
            #region Free Slash Flail
            item = new ItemTemplate();
            item.Id_nb = "free_slash_flail";
            item.Name = "Free Slash Flail";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 35;
            item.Type_Damage = 2;
            item.Object_Type = 24;
            item.Item_Type = 10;
            item.Weight = 35;
            item.Model = 861;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_flail = item;
            #endregion Free Slash Flail
            #region Free Thrust Flail
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_flail";
            item.Name = "Free Thrust Flail";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 35;
            item.Type_Damage = 3;
            item.Object_Type = 24;
            item.Item_Type = 10;
            item.Weight = 35;
            item.Model = 861;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_flail = item;
            #endregion Free Thrust Flail
            #region Free Crush Flail
            item = new ItemTemplate();
            item.Id_nb = "free_crush_flail";
            item.Name = "Free Crush Flail";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 35;
            item.Type_Damage = 1;
            item.Object_Type = 24;
            item.Item_Type = 10;
            item.Weight = 35;
            item.Model = 861;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crush_flail = item;
            #endregion Free Crush Flail
            #region Free Slash Claw
            item = new ItemTemplate();
            item.Id_nb = "free_slash_claw";
            item.Name = "Free Slash Claw";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 30;
            item.Hand = 2;
            item.Type_Damage = 2;
            item.Object_Type = 25;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 967;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_claw = item;
            #endregion Free Slash Claw
            #region Free Thrust Claw
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_claw";
            item.Name = "Free Thrust Claw";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 30;
            item.Hand = 2;
            item.Type_Damage = 3;
            item.Object_Type = 25;
            item.Item_Type = 11;
            item.Weight = 35;
            item.Model = 967;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_claw = item;
            #endregion Free Thrust Claw
            #region Free Harp
            item = new ItemTemplate();
            item.Id_nb = "free_harp";
            item.Name = "Free Harp";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 40;
            item.Object_Type = 45;
            item.Hand = 1;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 3985;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_harp = item;
            #endregion Free Harp
       

			
            #region Free Scythe
            item = new ItemTemplate();
            item.Id_nb = "free_scythe";
            item.Name = "Free Scythe";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 37;
            item.Hand = 0;
            item.Type_Damage = 2;
            item.Object_Type = 26;
            item.Item_Type = 10;
            item.Weight = 50;
            item.Model = 929;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_scythe = item;
            #endregion Free Scythe
            #region Free Crush Polearm
            item = new ItemTemplate();
            item.Id_nb = "free_crush_polearm";
            item.Name = "Free Crush Polearm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 56;
            item.Hand = 1;
            item.Type_Damage = 1;
            item.Object_Type = 7;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 2664;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_crush_polearm = item;
            #endregion Free Crush Polearm
            #region Free Thrust Polearm
            item = new ItemTemplate();
            item.Id_nb = "free_thrust_polearm";
            item.Name = "Free Thrust Polearm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 43;
            item.Hand = 1;
            item.Type_Damage = 3;
            item.Object_Type = 7;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 2665;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_thrust_polearm = item;
            #endregion Free Thrust Polearm
            #region Free Slash Polearm
            item = new ItemTemplate();
            item.Id_nb = "free_slash_polearm";
            item.Name = "Free Slash Polearm";
            item.Level = 50;
            item.Durability = 50000;
            item.MaxDurability = 50000;
            item.Condition = 50000;
            item.MaxCondition = 50000;
            item.Quality = 100;
            item.DPS_AF = 165;
            item.SPD_ABS = 48;
            item.Hand = 1;
            item.Type_Damage = 2;
            item.Object_Type = 7;
            item.Item_Type = 12;
            item.Weight = 50;
            item.Model = 2663;
            item.Bonus = 35;
            item.IsPickable = true;
            item.IsDropable = true;
            item.CanDropAsLoot = false;
            item.IsTradable = true;
            item.MaxCount = 1;
            item.PackSize = 1; item.AllowAdd = true;
            free_slash_polearm = item;
            #endregion Free Slash Polearm

            

        }
		private static void Load_Whria(ref ItemTemplate item)
		{
			if (item.Item_Type==25
			|| item.Item_Type==28
			|| item.Item_Type==27)
			{
			Random r= new Random();
			
			int base_stat=25;
			int rand_stat=2;

			item.Bonus1=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus2=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus3=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus4=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus5=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus6=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus7=base_stat+r.Next(-1*rand_stat,rand_stat);
			item.Bonus8=base_stat+r.Next(-1*rand_stat,rand_stat);
			
			item.Bonus1Type=1;
			item.Bonus2Type=2;
			item.Bonus3Type=3;
			item.Bonus4Type=4;
			item.Bonus5Type=5;
			item.Bonus6Type=6;
			item.Bonus7Type=7;
			item.Bonus8Type=8;
			
			}

			if (item.Item_Type==23
			|| item.Item_Type==22
			|| item.Item_Type==21)
			{
			Random r= new Random();

			int base_resist=4;
			int rand_resist=1;
			
			item.Bonus1=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus2=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus3=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus4=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus5=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus6=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus7=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus8=base_resist+r.Next(-1*rand_resist,rand_resist);
			item.Bonus9=base_resist+r.Next(-1*rand_resist,rand_resist);
			
			item.Bonus1Type=11;
			item.Bonus2Type=12;
			item.Bonus3Type=13;
			item.Bonus4Type=14;
			item.Bonus5Type=15;
			item.Bonus6Type=16;
			item.Bonus7Type=17;
			item.Bonus8Type=18;
			item.Bonus9Type=19;
			
			}

		}
		
        public void GiveFreeArmor(GamePlayer player)
        {
            if (player == null)
                return;


            //Find out the best armor type for this player and give him a full set of it.
            String armorType = GlobalConstants.ArmorLevelToName(player.BestArmorLevel, (eRealm)player.Realm);
            //change helm model to fit players head depending on their realm.
            int realmHelmModel = 0;
            if ((eRealm)player.Realm == eRealm.Hibernia) { realmHelmModel = 1292; }
            if ((eRealm)player.Realm == eRealm.Albion) { realmHelmModel = 1294; }
            if ((eRealm)player.Realm == eRealm.Midgard) { realmHelmModel = 1291; }

            switch (armorType)
            {
                case "scale": //scale gets chain armor instead of scale
                case "chain":
                    {
                        GiveItem(player, free_chain_vest);
                        GiveItem(player, free_chain_legs);
                        GiveItem(player, free_chain_boots);
                        GiveItem(player, free_chain_arms);
                        GiveItem(player, free_chain_gloves);
                        free_chain_helm.Model = realmHelmModel;
                        GiveItem(player, free_chain_helm);
                        break;
                    }
                case "cloth":
                    {
                        GiveItem(player, free_cloth_vest);
                        GiveItem(player, free_cloth_legs);
                        GiveItem(player, free_cloth_boots);
                        GiveItem(player, free_cloth_arms);
                        GiveItem(player, free_cloth_gloves);
                        free_cloth_helm.Model = realmHelmModel;
                        GiveItem(player, free_cloth_helm);
                        break;
                    }
                case "leather":
                    {
                        GiveItem(player, free_leather_vest);
                        GiveItem(player, free_leather_legs);
                        GiveItem(player, free_leather_boots);
                        GiveItem(player, free_leather_arms);
                        GiveItem(player, free_leather_gloves);
                        free_leather_helm.Model = realmHelmModel;
                        GiveItem(player, free_leather_helm);
                        break;
                    }
                case "reinforced": //reinforced gets studded armor instead of reinforced
                case "studded":
                    {
                        GiveItem(player, free_studded_vest);
                        GiveItem(player, free_studded_legs);
                        GiveItem(player, free_studded_boots);
                        GiveItem(player, free_studded_arms);
                        GiveItem(player, free_studded_gloves);
                        free_studded_helm.Model = realmHelmModel;
                        GiveItem(player, free_studded_helm);
                        break;
                    }
                case "plate":
                    {
                        GiveItem(player, free_plate_vest);
                        GiveItem(player, free_plate_legs);
                        GiveItem(player, free_plate_boots);
                        GiveItem(player, free_plate_arms);
                        GiveItem(player, free_plate_gloves);
                        free_plate_helm.Model = realmHelmModel;
                        GiveItem(player, free_plate_helm);
                        break;
                    }
            }
            //everyone gets the epic cloak, necklace, bracers, rings, and jewel
            return;
        }

        public void EquipFreeArmor(GamePlayer player)
        {
            if (player == null)
                return;


            //Find out the best armor type for this player and give him a full set of it.
            String armorType = GlobalConstants.ArmorLevelToName(player.BestArmorLevel, (eRealm)player.Realm);
            //change helm model to fit players head depending on their realm.
            int realmHelmModel = 0;
            if ((eRealm)player.Realm == eRealm.Hibernia) { realmHelmModel = 1292; }
            if ((eRealm)player.Realm == eRealm.Albion) { realmHelmModel = 1294; }
            if ((eRealm)player.Realm == eRealm.Midgard) { realmHelmModel = 1291; }

            //player.Inventory.GetItem
            switch (armorType)
            {
                case "scale": //scale gets chain armor instead of scale
                case "chain":
                    {
                        EquipGiveItem(player, free_chain_vest, eInventorySlot.TorsoArmor);
                        EquipGiveItem(player, free_chain_legs, eInventorySlot.LegsArmor);
                        EquipGiveItem(player, free_chain_boots, eInventorySlot.FeetArmor);
                        EquipGiveItem(player, free_chain_arms, eInventorySlot.ArmsArmor);
                        EquipGiveItem(player, free_chain_gloves, eInventorySlot.HandsArmor);
                        free_chain_helm.Model = realmHelmModel;
                        EquipGiveItem(player, free_chain_helm, eInventorySlot.HeadArmor);
                        break;
                    }
                case "cloth":
                    {
                        EquipGiveItem(player, free_cloth_vest, eInventorySlot.TorsoArmor);
                        EquipGiveItem(player, free_cloth_legs, eInventorySlot.LegsArmor);
                        EquipGiveItem(player, free_cloth_boots, eInventorySlot.FeetArmor);
                        EquipGiveItem(player, free_cloth_arms, eInventorySlot.ArmsArmor);
                        EquipGiveItem(player, free_cloth_gloves, eInventorySlot.HandsArmor);
                        free_cloth_helm.Model = realmHelmModel;
                        EquipGiveItem(player, free_cloth_helm, eInventorySlot.HeadArmor);
                        break;
                    }
                case "leather":
                    {
                        EquipGiveItem(player, free_leather_vest, eInventorySlot.TorsoArmor);
                        EquipGiveItem(player, free_leather_legs, eInventorySlot.LegsArmor);
                        EquipGiveItem(player, free_leather_boots, eInventorySlot.FeetArmor);
                        EquipGiveItem(player, free_leather_arms, eInventorySlot.ArmsArmor);
                        EquipGiveItem(player, free_leather_gloves, eInventorySlot.HandsArmor);
                        free_leather_helm.Model = realmHelmModel;
                        EquipGiveItem(player, free_leather_helm, eInventorySlot.HeadArmor);
                        break;
                    }
                case "reinforced": //reinforced gets studded armor instead of reinforced
                case "studded":
                    {
                        EquipGiveItem(player, free_studded_vest, eInventorySlot.TorsoArmor);
                        EquipGiveItem(player, free_studded_legs, eInventorySlot.LegsArmor);
                        EquipGiveItem(player, free_studded_boots, eInventorySlot.FeetArmor);
                        EquipGiveItem(player, free_studded_arms, eInventorySlot.ArmsArmor);
                        EquipGiveItem(player, free_studded_gloves, eInventorySlot.HandsArmor);
                        free_studded_helm.Model = realmHelmModel;
                        EquipGiveItem(player, free_studded_helm, eInventorySlot.HeadArmor);
                        break;
                    }
                case "plate":
                    {
                        EquipGiveItem(player, free_plate_vest, eInventorySlot.TorsoArmor);
                        EquipGiveItem(player, free_plate_legs, eInventorySlot.LegsArmor);
                        EquipGiveItem(player, free_plate_boots, eInventorySlot.FeetArmor);
                        EquipGiveItem(player, free_plate_arms, eInventorySlot.ArmsArmor);
                        EquipGiveItem(player, free_plate_gloves, eInventorySlot.HandsArmor);
                        free_plate_helm.Model = realmHelmModel;
                        EquipGiveItem(player, free_plate_helm, eInventorySlot.HeadArmor);
                        break;
                    }
            }

            return;
        }
	

		
		
        public void GiveFreeWeapons(GamePlayer player)
        {
            if (player == null)
                return;

            //Check if the player can use the weapon, if yes then give him one of that kind.
            if (player.HasAbilityToUseItem(free_staff)) { GiveItem(player, free_staff); }
            if (player.HasAbilityToUseItem(free_slash_alb)) { GiveItem(player, free_slash_alb); }
            if (player.HasAbilityToUseItem(free_slash_mid)) { GiveItem(player, free_slash_mid); }
            if (player.HasAbilityToUseItem(free_slash_hib)) { GiveItem(player, free_slash_hib); }
            if (player.HasAbilityToUseItem(free_thrust_alb)) { GiveItem(player, free_thrust_alb); }
            if (player.HasAbilityToUseItem(free_thrust_hib)) { GiveItem(player, free_thrust_hib); }
            if (player.HasAbilityToUseItem(free_crush_alb)) { GiveItem(player, free_crush_alb); }
            if (player.HasAbilityToUseItem(free_crush_mid)) { GiveItem(player, free_crush_mid); }
            if (player.HasAbilityToUseItem(free_crush_hib)) { GiveItem(player, free_crush_hib); }
            if (player.HasAbilityToUseItem(free_axe)) { GiveItem(player, free_axe); }
            if (player.HasAbilityToUseItem(free_long_bow)) { GiveItem(player, free_long_bow); }
            if (player.HasAbilityToUseItem(free_composite_bow)) { GiveItem(player, free_composite_bow); }
            if (player.HasAbilityToUseItem(free_recurved_bow)) { GiveItem(player, free_recurved_bow); }
            if (player.HasAbilityToUseItem(free_short_bow)) { GiveItem(player, free_short_bow); }
            if (player.HasAbilityToUseItem(free_crossbow)) { GiveItem(player, free_crossbow); }
            if (player.HasAbilityToUseItem(free_twohanded_slash_alb)) { GiveItem(player, free_twohanded_slash_alb); }
            if (player.HasAbilityToUseItem(free_twohanded_slash_hib)) { GiveItem(player, free_twohanded_slash_hib); }
            if (player.HasAbilityToUseItem(free_twohanded_crush_alb)) { GiveItem(player, free_twohanded_crush_alb); }
            if (player.HasAbilityToUseItem(free_twohanded_crush_hib)) { GiveItem(player, free_twohanded_crush_hib); }
            if (player.HasAbilityToUseItem(free_twohanded_axe)) { GiveItem(player, free_twohanded_axe); }
            if (player.HasAbilityToUseItem(free_twohanded_sword)) { GiveItem(player, free_twohanded_sword); }
            if (player.HasAbilityToUseItem(free_twohanded_hammer)) { GiveItem(player, free_twohanded_hammer); }
            if (player.HasAbilityToUseItem(free_large_shield)) { GiveItem(player, free_large_shield); }
            else if (player.HasAbilityToUseItem(free_medium_shield)) { GiveItem(player, free_medium_shield); }
            else if (player.HasAbilityToUseItem(free_small_shield)) { GiveItem(player, free_small_shield); }
            if (player.HasAbilityToUseItem(free_slash_spear)) { GiveItem(player, free_slash_spear); }
            if (player.HasAbilityToUseItem(free_thrust_spear)) { GiveItem(player, free_thrust_spear); }
            if (player.HasAbilityToUseItem(free_slash_flail)) { GiveItem(player, free_slash_flail); }
            if (player.HasAbilityToUseItem(free_thrust_flail)) { GiveItem(player, free_thrust_flail); }
            if (player.HasAbilityToUseItem(free_crush_flail)) { GiveItem(player, free_crush_flail); }
            if (player.HasAbilityToUseItem(free_slash_claw)) { GiveItem(player, free_slash_claw); }
            if (player.HasAbilityToUseItem(free_thrust_claw)) { GiveItem(player, free_thrust_claw); }
            if (player.HasAbilityToUseItem(free_harp)) { GiveItem(player, free_harp); }
            if (player.HasAbilityToUseItem(free_scythe)) { GiveItem(player, free_scythe); }
            if (player.HasAbilityToUseItem(free_crush_polearm)) { GiveItem(player, free_crush_polearm); }
            if (player.HasAbilityToUseItem(free_thrust_polearm)) { GiveItem(player, free_thrust_polearm); }
            if (player.HasAbilityToUseItem(free_slash_polearm)) { GiveItem(player, free_slash_polearm); }

        }
        #endregion Give Free Weapons

        #region GiveItem

        protected static void GiveItem(GamePlayer player, ItemTemplate itemTemplate)
        {
            GiveItem(null, player, itemTemplate);
        }

        protected static void GiveItem(GameLiving source, GamePlayer player, ItemTemplate itemTemplate)
        {
            ItemTemplate temp = GameServer.Database.FindObjectByKey<ItemTemplate>(itemTemplate.Id_nb);
            if (temp == null)
            {
                GameServer.Database.AddObject(itemTemplate);
            }
            InventoryItem item = GameInventoryItem.Create<ItemTemplate>(itemTemplate);
            item.AllowAdd = true;
            if (player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, item))
            {
                if (source == null)
                {
                    player.Out.SendMessage("You receive the " + itemTemplate.Name + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else
                {
                    player.Out.SendMessage("You receive " + itemTemplate.GetName(0, false) + " from " + source.GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
            }
            else
            {
                player.CreateItemOnTheGround(item);
                player.Out.SendMessage("Your Inventory is full. You couldn't recieve the " + itemTemplate.Name + ", so it's been placed on the ground. Pick it up as soon as possible or it will vanish in a few minutes.", eChatType.CT_Important, eChatLoc.CL_PopupWindow);
            }
        }
        #endregion GiveItem

        

        protected static void EquipGiveItem(GamePlayer player, ItemTemplate itemTemplate, eInventorySlot slot)
        {
            if (player.Inventory.GetItem(slot) != null)
            {
                GiveItem(player, player, itemTemplate);
                return;
            }
            EquipGiveItem(null, player, itemTemplate, slot);
        }

        protected static void EquipGiveItem(GameLiving source, GamePlayer player, ItemTemplate itemTemplate, eInventorySlot slot)
        {
            InventoryItem item = GameInventoryItem.Create<ItemTemplate>(itemTemplate);
            ItemTemplate temp = GameServer.Database.FindObjectByKey<ItemTemplate>(itemTemplate.Id_nb);
            if (temp == null)
            {
                GameServer.Database.AddObject(itemTemplate);
             
			}
           
            item.AllowAdd = true;
			if (player.Inventory.AddItem(slot, item))
            {
                if (source == null)
                {
                    player.Out.SendMessage("You receive the " + itemTemplate.Name + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
                else
                {
                    player.Out.SendMessage("You receive " + itemTemplate.GetName(0, false) + " from " + source.GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                }
            }
            else
            {
                player.CreateItemOnTheGround(item);
                player.Out.SendMessage("Your Inventory is full. You couldn't recieve the " + itemTemplate.Name + ", so it's been placed on the ground. Pick it up as soon as possible or it will vanish in a few minutes.", eChatType.CT_Important, eChatLoc.CL_PopupWindow);
            }
        }

       
        public void SendReply(GamePlayer player, string msg)
        {
            player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            //FinalFury: Reduced 5000 to 250, causes lag on some systems
           
//            player.Out.SendMessage("Hello "+player.Name+", I am able to give out free [Armor] and [Weapons].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            player.Out.SendMessage("Hello "+player.Name+", I am able to give out free [Weapons].", eChatType.CT_Say, eChatLoc.CL_PopupWindow);


            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str))
                return false;

            GamePlayer player = source as GamePlayer;

            if (player == null)
                return false;





           
 
            if (str == "Armor")
            {
                try
                {
                    EquipFreeArmor(player);
                }
                catch (Exception ex)
                {
                    player.Out.SendMessage(ex.Message, eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                    GiveFreeArmor(player);
                }
                return true;
            }
            if (str == "Weapons")
            {
                GiveFreeWeapons(player);
                return true;
            }
            return true;
        }


            

            

        }
        
        
    }
    

	

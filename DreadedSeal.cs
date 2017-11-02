/* DreadedSeal Script collection v1.0
 * 
 * This script populates everything needed into the database itself.
 * 
 * Server settings are added to control the level at which
 *  mobs start dropping seals, the base chance to drop seals,
 *  and the increase in drop chance per mob level.
 * 
 * REMEMBER TO ADD DOL.GS.Scripts.LootGeneratorDreadedSeal
 *  ENTRIES TO THE LOOTGENERATOR TABLE
 * 
 * Servers must be set to GameType PvE via DOLConfig for the database
 *  to be populated and for the code to work.
 * If you want a non PvE server to allow dreaded seals,
 *  search for "TEGS"
 *  and follow the instructions. 
 * 
 * The server loads the DB before populating it,
 *  so you'll have to run the server once to populate the DB,
 *  but won't see the collectors show up until you shut it
 *  down and run it again.
 * 
 * If you experience problems, run the following queries to clear out any
 *  old DB artifacts and run the server again.
 * DELETE FROM itemtemplate WHERE `Name` LIKE "% dreaded seal";
 * DELETE FROM mob WHERE guild="Dreaded seal collector";
 * DELETE FROM npcequipment WHERE `TemplateID` IN ('LadyNina','Fiana','Relena');
 * DELETE FROM `serverproperty` WHERE `Key` LIKE "%dreadedseal%";
 * 
 * 
 * Project inspired by code by RDSandersJR.
 * contributing code from BluRaven from his MLNPC.
 * Further additions By Bones.
 * Finalised by tegstewart
 *	who added populating seal collectors into DB, 
 *  wrote the LootGeneratorDreadedSeal class,
 * 	and added the PvE only restriction.
 *  
 * CHANGELOG
 * 11/2/2017 TEGS - Initial Version
*/

using System;
using System.Reflection;
using DOL.GS;
using DOL.AI.Brain;
using DOL.Database;
using DOL.Events;
using DOL.GS.PacketHandler;
using log4net;

namespace DOL.GS.Scripts
{
	/// <summary>
	/// LootGeneratorDreadedSeal
	/// At the moment this generator only adds DreadedSeal to the loot
	/// </summary>
	public class DreadedSealCollector : GameNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		protected int m_count;
		// count of items, for stack
		private long amount = 0;

		#region Add Seals/Collectors/EquipmentTemplates to Database
		// Add seals to Database if they don't exist
		[ScriptLoadedEvent]
		public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
		{
			// /* // TEGS: Remove the // at the beginning of this line to allow seals on all server types
			if (GameServer.Instance.Configuration.ServerType != eGameServerType.GST_PvE)
				return;
			// */ // TEGS: Remove the // at the beginning of this line to allow seals on all server types
        	
			#region Add Collectors to mob
			#region Lady Nina
			if (GameServer.Database.GetObjectCount<Mob>("Name='Lady Nina' AND ClassType='DOL.GS.Scripts.DreadedSealCollector'") == 0) {
				Mob mobLadyNina = new Mob();
	        		    	
				mobLadyNina.Name = "Lady Nina";
				mobLadyNina.ClassType = "DOL.GS.Scripts.DreadedSealCollector";
				mobLadyNina.Guild = "Dreaded Seal Collector";
				mobLadyNina.X = 33505;
				mobLadyNina.Y = 22668;
				mobLadyNina.Z = 8479;
				mobLadyNina.Heading = 1035;
				mobLadyNina.Region = 10;
				mobLadyNina.Model = 283;
				mobLadyNina.Size = 49;
				mobLadyNina.Level = 30;
				mobLadyNina.EquipmentTemplateID = "LadyNina";
				mobLadyNina.PackageID = "DreadedSealCollectors";
				mobLadyNina.Realm = 1;
	        		    	
				GameServer.Database.AddObject(mobLadyNina);
	        		    	
				if (log.IsWarnEnabled)
					log.Warn("Added mob " + mobLadyNina.Name);
			}
        			
			// Make sure the equipment template is in place
			if (GameServer.Database.GetObjectCount<NPCEquipment>("TemplateID='LadyNina'") == 0) {
				NPCEquipment gear = new NPCEquipment();
				gear.TemplateID = "LadyNina";
				gear.Slot = 25;
				gear.Model = 98;
				gear.Color = 40;
				GameServer.Database.AddObject(gear);
        				
				gear = new NPCEquipment();
				gear.TemplateID = "LadyNina";
				gear.Slot = 26;
				gear.Model = 96;
				gear.Color = 43;
				GameServer.Database.AddObject(gear);
        				
				if (log.IsWarnEnabled)
					log.Warn("Added NPCEquipment for " + gear.TemplateID);
			}
			#endregion
			#region Fiana
			if (GameServer.Database.GetObjectCount<Mob>("Name='Fiana' AND ClassType='DOL.GS.Scripts.DreadedSealCollector'") == 0) {
				Mob mobFiana = new Mob();

				mobFiana.Name = "Fiana";
				mobFiana.ClassType = "DOL.GS.Scripts.DreadedSealCollector";
				mobFiana.Guild = "Dreaded Seal Collector";
				mobFiana.X = 31613;
				mobFiana.Y = 33839;
				mobFiana.Z = 8030;
				mobFiana.Heading = 3231;
				mobFiana.Region = 101;
				mobFiana.Model = 162;
				mobFiana.Size = 48;
				mobFiana.Level = 30;
				mobFiana.EquipmentTemplateID = "Fiana";
				mobFiana.NPCTemplateID = 60160825;
				mobFiana.PackageID = "DreadedSealCollectors";
				mobFiana.Realm = 2;
	        		    
				GameServer.Database.AddObject(mobFiana);
	        		    	
				if (log.IsWarnEnabled)
					log.Warn("Added mob " + mobFiana.Name);
			}
        			
			// Make sure the equipment template is in place
			if (GameServer.Database.GetObjectCount<NPCEquipment>("TemplateID='Fiana'") == 0) {
				NPCEquipment gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 22;
				gear.Model = 137;
				gear.Color = 9;
				GameServer.Database.AddObject(gear);
        				
				gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 23;
				gear.Model = 138;
				gear.Color = 9;
				GameServer.Database.AddObject(gear);
        				
				gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 25;
				gear.Model = 134;
				gear.Color = 9;
				GameServer.Database.AddObject(gear);
        				        				
				gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 26;
				gear.Model = 96;
				gear.Color = 72;
				GameServer.Database.AddObject(gear);
        				        				
				gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 27;
				gear.Model = 152;
				gear.Color = 73;
				GameServer.Database.AddObject(gear);
        				        				
				gear = new NPCEquipment();
				gear.TemplateID = "Fiana";
				gear.Slot = 28;
				gear.Model = 141;
				gear.Color = 73;
				GameServer.Database.AddObject(gear);
        				
				if (log.IsWarnEnabled)
					log.Warn("Added NPCEquipment for " + gear.TemplateID);
			}
			#endregion
			#region Relena
			if (GameServer.Database.GetObjectCount<Mob>("Name='Relena' AND ClassType='DOL.GS.Scripts.DreadedSealCollector'") == 0) {
				Mob mobRelena = new Mob();
	        		    	
				mobRelena.Name = "Relena";
				mobRelena.ClassType = "DOL.GS.Scripts.DreadedSealCollector";
				mobRelena.Guild = "Dreaded Seal Collector";
				mobRelena.X = 32263;
				mobRelena.Y = 33049;
				mobRelena.Z = 7998;
				mobRelena.Heading = 2150;
				mobRelena.Region = 201;
				mobRelena.Model = 388;
				mobRelena.Size = 52;
				mobRelena.Level = 30;
				mobRelena.EquipmentTemplateID = "Relena";
				mobRelena.NPCTemplateID = 60165263;
				mobRelena.PackageID = "DreadedSealCollectors";
				mobRelena.Realm = 3;
	        		    	
				GameServer.Database.AddObject(mobRelena);
	        		    	
				if (log.IsWarnEnabled)
					log.Warn("Added mob " + mobRelena.Name);
			}
        			
			// Make sure the equipment template is in place
			if (GameServer.Database.GetObjectCount<NPCEquipment>("TemplateID='Relena'") == 0) {
				NPCEquipment gear = new NPCEquipment();
				gear.TemplateID = "Relena";
				gear.Slot = 23;
				gear.Model = 143;
				gear.Color = 43;
				GameServer.Database.AddObject(gear);
        				
				gear = new NPCEquipment();
				gear.TemplateID = "Relena";
				gear.Slot = 25;
				gear.Model = 58;
				gear.Color = 43;
				GameServer.Database.AddObject(gear);
        				
				gear = new NPCEquipment();
				gear.TemplateID = "Relena";
				gear.Slot = 26;
				gear.Model = 57;
				gear.Color = 0;
				GameServer.Database.AddObject(gear);
        				
				if (log.IsWarnEnabled)
					log.Warn("Added NPCEquipment for " + gear.TemplateID);
			}        			
			#endregion
			#endregion
			#region Add Seals to Item Templates
			ItemTemplate item;
			item = GameServer.Database.FindObjectByKey<ItemTemplate>("glowing_dreaded_seal");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "glowing_dreaded_seal";
				item.Name = "Glowing Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 483;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.IsDropable = true;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 10;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies -\n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog\n" +
				"Lady Nina in Camelot\n" +
				"and Fiana in Jordheim.";
				item.Price = 3000; // Realm Point Value
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			item = GameServer.Database.FindObjectByKey<ItemTemplate>("sanguine_dreaded_seal");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "sanguine_dreaded_seal";
				item.Name = "Sanguine Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 484;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 5;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies - \n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog\n" +
				"Lady Nina in Camelot\n" +
				"and Fiana in Jordheim.";
				item.Price = 3000;
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			item = GameServer.Database.FindObjectByKey<ItemTemplate>("lambent_dreaded_seal");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "lambent_dreaded_seal";
				item.Name = "Lambent Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 485;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 5;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies - \n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog\n" +
				"Lady Nina in Camelot\n" +
				"and Fiana in Jordheim.\n\n" +
				"This seal is worth 10 times the Glowing variety.";
				item.Price = 30000;
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			item = GameServer.Database.FindObjectByKey<ItemTemplate>("lambent_dreaded_seal2");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "lambent_dreaded_seal2";
				item.Name = "Lambent Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 485;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 5;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies - \n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog \n" +
				"Lady Nina in Camelot \n" +
				"and Fiana in Jordheim. \n\n" +
				"This seal is worth 10 times the Glowing variety.";
				item.Price = 30000;
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			item = GameServer.Database.FindObjectByKey<ItemTemplate>("fulgent_dreaded_seal");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "fulgent_dreaded_seal";
				item.Name = "Fulgent Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 486;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 1;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies - \n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog\n" +
				"Lady Nina in Camelot \n" +
				"and Fiana in Jordheim.\n\n" +
				"This seal is worth 50 times the Glowing variety.";
				item.Price = 150000;
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			item = GameServer.Database.FindObjectByKey<ItemTemplate>("effulgent_dreaded_seal");
			if (item == null) {
				item = new ItemTemplate();
				item.AllowAdd = true;
				item.AllowUpdate = true;
				item.Id_nb = "effulgent_dreaded_seal";
				item.Name = "Effulgent Dreaded Seal";
				item.Level = 30;
				item.Item_Type = 14;
				item.Model = 487;
				item.CanDropAsLoot = true;
				item.IsTradable = true;
				item.IsIndestructible = false;
				item.Object_Type = 0;
				item.Quality = 70;
				item.Weight = 0;
				item.MaxCondition = 100;
				item.MaxDurability = 100;
				item.Condition = 100;
				item.Durability = 100;
				item.MaxCount = 1;
				item.IsDropable = true;
				item.Description = "To show appreciation for service fighting these enemies - \n" +
				"the lords of the land will award Realm points and Realm abilities to those who defeat them.\n" +
				"The people who accept these seals are in the 3 major cities:\n" +
				"Relena in Tir Na Nog\n" +
				"Lady Nina in Camelot \n" +
				"and Fiana in Jordheim.\n\n" +
				"This seal is worth 250 times the Glowing variety.";
				item.Price = 750000;
				GameServer.Database.AddObject(item);

				if (log.IsWarnEnabled)
					log.Warn("Added " + item.Id_nb);
			}

			#endregion Add Seals to Item Templates
			#region Alb Crafting
			// add to Crafted Item Table
			DBCraftedItem seal;

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("4894");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal";
				seal.CraftedItemID = "4894"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("4895");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal2";
				seal.CraftedItemID = "4895"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("4896");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "fulgent_dreaded_seal";
				seal.CraftedItemID = "4896"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("4897");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "effulgent_dreaded_seal";
				seal.CraftedItemID = "4897"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}
			#endregion Alb Crafting
			#region Mid Crafting
			// add to Crafted Item Table
			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("11834");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal";
				seal.CraftedItemID = "11834"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("11835");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal2";
				seal.CraftedItemID = "11835"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("11836");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "fulgent_dreaded_seal";
				seal.CraftedItemID = "11836"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("11837");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "effulgent_dreaded_seal";
				seal.CraftedItemID = "11837"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}
			#endregion Mid Crafting
			#region Hib Crafting 
			// add to Crafted Item Table
			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("16564");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal";
				seal.CraftedItemID = "16564"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("16565");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "lambent_dreaded_seal2";
				seal.CraftedItemID = "16565"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("16566");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "fulgent_dreaded_seal";
				seal.CraftedItemID = "16566"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}

			seal = GameServer.Database.FindObjectByKey<DBCraftedItem>("16567");
			if (seal == null) {
				seal = new DBCraftedItem();
				seal.AllowAdd = true;
				seal.Id_nb = "effulgent_dreaded_seal";
				seal.CraftedItemID = "16567"; // Hib Crafting
				seal.CraftingLevel = 1;
				seal.CraftingSkillType = 15;
				seal.MakeTemplated = true;
				GameServer.Database.AddObject(seal);

				if (log.IsWarnEnabled)
					log.Warn("Added " + seal.Id_nb);
			}
			#endregion Hib Crafting
			#region All Realm Crafted X
			// add to Crafted X Item Table
			DBCraftedXItem sealx;
			sealx = GameServer.Database.FindObjectByKey<DBCraftedXItem>("lambent_dreaded_seal");
			if (sealx == null) {
				sealx = new DBCraftedXItem();
				sealx.AllowAdd = true;
				sealx.CraftedItemId_nb = "lambent_dreaded_seal";
				sealx.IngredientId_nb = "glowing_dreaded_seal";
				sealx.Count = 10;

				GameServer.Database.AddObject(sealx);

				if (log.IsWarnEnabled)
					log.Warn("Added " + sealx.CraftedItemId_nb);
			}

			sealx = GameServer.Database.FindObjectByKey<DBCraftedXItem>("lambent_dreaded_seal2");
			if (sealx == null) {
				sealx = new DBCraftedXItem();
				sealx.AllowAdd = true;
				sealx.CraftedItemId_nb = "lambent_dreaded_seal2";
				sealx.IngredientId_nb = "sanguine_dreaded_seal";
				sealx.Count = 10;

				GameServer.Database.AddObject(sealx);

				if (log.IsWarnEnabled)
					log.Warn("Added " + sealx.CraftedItemId_nb);
			}

			sealx = GameServer.Database.FindObjectByKey<DBCraftedXItem>("fulgent_dreaded_seal");
			if (sealx == null) {
				sealx = new DBCraftedXItem();
				sealx.AllowAdd = true;
				sealx.CraftedItemId_nb = "fulgent_dreaded_seal";
				sealx.IngredientId_nb = "lambent_dreaded_seal";
				sealx.Count = 5;

				GameServer.Database.AddObject(sealx);

				if (log.IsWarnEnabled)
					log.Warn("Added " + sealx.CraftedItemId_nb);
			}

			sealx = GameServer.Database.FindObjectByKey<DBCraftedXItem>("effulgent_dreaded_seal");
			if (sealx == null) {
				sealx = new DBCraftedXItem();
				sealx.AllowAdd = true;
				sealx.CraftedItemId_nb = "effulgent_dreaded_seal";
				sealx.IngredientId_nb = "fulgent_dreaded_seal";
				sealx.Count = 5;

				GameServer.Database.AddObject(sealx);

				if (log.IsWarnEnabled)
					log.Warn("Added " + sealx.CraftedItemId_nb);
			}
			#endregion All Realm Crafted X
		}
		#endregion Add Seals/Collectors/EquipmentTemplates to Database

		private void SendReply(GamePlayer target, string msg)
		{
			target.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_ChatWindow);
		}

		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player))
				return false;
            
			// /* // TEGS: Remove the // at the beginning of this line to allow seals on all server types
			if (GameServer.Instance.Configuration.ServerType == eGameServerType.GST_PvE)
				player.Out.SendMessage("Hand me any Dreaded Seal and I'll give you realm points!", 
					eChatType.CT_Say, eChatLoc.CL_ChatWindow);
			else
            // */ // TEGS: Remove the // at the beginning of this line to allow seals on all server types 
            	player.Out.SendMessage("I only collect Dreaded Seals on PvE servers.  Sell them to merchants instead.", 
					eChatType.CT_Say, eChatLoc.CL_ChatWindow);
            	
			return true;
		}

		public override bool ReceiveItem(GameLiving source, InventoryItem item)
		{
			GamePlayer player = source as GamePlayer;
			int Level = player.Level;
			long currentrps = player.RealmPoints;
			long maxrps = 66181501;

			// /* // TEGS: Remove the // at the beginning of this line to allow seals on all server types
			if (GameServer.Instance.Configuration.ServerType != eGameServerType.GST_PvE) {
				player.Out.SendMessage("I only collect Dreaded Seals on PvE servers.  Sell them to merchants instead.", 
					eChatType.CT_Say, eChatLoc.CL_ChatWindow);
				return false;
			}
			// */ // TEGS: Remove the // at the beginning of this line to allow seals on all server types
            
			if (GetDistanceTo(player) > WorldMgr.INTERACT_DISTANCE) {
				((GamePlayer)source).Out.SendMessage("You are too far away to give anything to me "
				+ player.Name + ". Come a little closer.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return false;
			}

			if (player != null && item != null && currentrps < maxrps && item.Id_nb == "glowing_dreaded_seal"
			             || item.Id_nb == "sanguine_dreaded_seal"
			             || item.Id_nb == "lambent_dreaded_seal"
			             || item.Id_nb == "lambent_dreaded_seal2"
			             || item.Id_nb == "fulgent_dreaded_seal"
			             || item.Id_nb == "effulgent_dreaded_seal") {
				m_count = item.Count;
				if (Level <= 20) {
					((GamePlayer)source).Out.SendMessage("You are too young yet to make use of these items "
					+ player.Name + ". Come back in " + (21 - Level) + " levels.", eChatType.CT_Say, eChatLoc.CL_ChatWindow);
					return false;
				} else if (Level > 20 & Level < 26) {
					amount += (item.Price / 150) * m_count; // At level 21 to 25 you get 20 Realm Points per set of seals.
					player.GainBountyPoints(1);             // Force a +1 BP gain
				} else if (Level > 25 & Level < 31) {
					amount += (item.Price / 100) * m_count; // At level 26 to 30 you get 30 Realm Points per set of seals.
					player.GainBountyPoints(2);             // Force a +2 BP gain
				} else if (Level > 30 & Level < 36) {
					amount += (item.Price / 60) * m_count; // At level 31 to 35 you get 50 Realm Points per set of seals.
					player.GainBountyPoints(3);            // Force a +3 BP gain
				} else if (Level > 35 & Level < 41)
					amount += (item.Price / 10) * m_count; // At level 36 to 40 you get 300 Realm Points per set of seals.
                    else if (Level > 40 & Level < 46)
					amount += (item.Price / 4) * m_count; // At level 41 to 45 you get 700 Realm Points per set of seals.
                    else if (Level > 45 & Level < 50)
					amount += (item.Price / 2) * m_count; // At level 46 to 49 you get 1500 Realm Points per set of seals.
                    else if (Level > 49)
					amount += item.Price * m_count; // At level 50 you get 3000 Realm Points per set of seals.
                
				if (amount + currentrps > maxrps)
					amount = maxrps - currentrps; // only give enough realm points to reach max
                
				player.GainRealmPoints(amount);
				if (Level > 35) {
					player.GainBountyPoints(amount / 55);
				} // Only BP+ those of 36+ to prevent double BP gains
				player.Inventory.RemoveItem(item);
				player.Out.SendUpdatePoints();
				amount = 0;
				m_count = 0;
				currentrps = 0;
				return base.ReceiveItem(source, item);
			}

			((GamePlayer)source).Out.SendMessage("I am not interested in that item, come back with something useful "
			+ player.Name + ".", eChatType.CT_Say, eChatLoc.CL_ChatWindow);
			return false;
		}
	}

	/// <summary>
	/// DreadedSealCollector
	/// Populates seals, seal collectors, and seal craft recipes into the DB
	/// Handles seal collector interactions
	/// Only adds DB content and allows seals to be turned in on PvE servers
	/// </summary>
	public class LootGeneratorDreadedSeal : LootGeneratorBase
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static ItemTemplate m_DreadedSeal = GameServer.Database.FindObjectByKey<ItemTemplate>("glowing_dreaded_seal");
		private static ServerProperty StartingLevel = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_starting_level");
		// Level to start dropping seals at
		private static ServerProperty DropChancePerLevel = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_drop_chance_per_level");
		// Increase in hundredths of a percent per level of mob
		private static ServerProperty BaseChance = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_base_chance");
		// Base chance to drop a seal
		
		[ScriptLoadedEvent]
		// Adds server properties if they don't already exist
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
		{
			ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        	
			// /* // TEGS: Remove the // at the beginning of this line to allow seals on all server types
			if (GameServer.Instance.Configuration.ServerType != eGameServerType.GST_PvE)
				return;
			// */ // TEGS: Remove the // at the beginning of this line to allow seals on all server types
        	
			#region Starting Level
			ServerProperty oStartingLevel = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_starting_level");
        	
			if (oStartingLevel == null) {
				oStartingLevel = new ServerProperty();
        		
				oStartingLevel.Category = "pve";
				oStartingLevel.Key = "lootgenerator_dreadedseal_starting_level";
				oStartingLevel.Description = "Mob level to start dropping Dreaded Glowing Seals";
				oStartingLevel.DefaultValue = "25";
				oStartingLevel.Value = "25";
        		
				GameServer.Database.AddObject(oStartingLevel);

				if (log.IsWarnEnabled)
					log.Warn("Added Serverproperty " + oStartingLevel.Key);
			}
			#endregion
			#region Chance per level
			ServerProperty oDropChancePerLevel = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_drop_chance_per_level");
        	
			if (oDropChancePerLevel == null) {
				oDropChancePerLevel = new ServerProperty();
        		
				oDropChancePerLevel.Category = "pve";
				oDropChancePerLevel.Key = "lootgenerator_dreadedseal_drop_chance_per_level";
				oDropChancePerLevel.Description = "Increase in Dreaded Glowing Seal drop chance per level, in hundredths of a percent.";
				oDropChancePerLevel.DefaultValue = "25";
				oDropChancePerLevel.Value = "25";
        		
				GameServer.Database.AddObject(oDropChancePerLevel);

				if (log.IsWarnEnabled)
					log.Warn("Added Serverproperty " + oDropChancePerLevel.Key);
			}
			#endregion
			#region Base chance
			ServerProperty oBaseChance = GameServer.Database.FindObjectByKey<ServerProperty>("lootgenerator_dreadedseal_base_chance");
        	
			if (oBaseChance == null) {
				oBaseChance = new ServerProperty();
        		
				oBaseChance.Category = "pve";
				oBaseChance.Key = "lootgenerator_dreadedseal_base_chance";
				oBaseChance.Description = "Base chance to drop a Dreaded Seal, in hundredths of a percent.";
				oBaseChance.DefaultValue = "25";
				oBaseChance.Value = "25";
        		
				GameServer.Database.AddObject(oBaseChance);

				if (log.IsWarnEnabled)
					log.Warn("Added Serverproperty " + oBaseChance.Key);
			}
			#endregion
		}
		
		/// <summary>
		/// Generate loot for given mob
		/// </summary>
		/// <param name="mob"></param>
		/// <param name="killer"></param>
		/// <returns></returns>
		public override LootList GenerateLoot(GameNPC mob, GameObject killer)
		{
			LootList loot = base.GenerateLoot(mob, killer);
			
			int iStartingLevel;
			int iPercentPerLevel;
			int iBaseChance;
			
			try {
				GamePlayer player = killer as GamePlayer;
				if (killer is GameNPC && ((GameNPC)killer).Brain is IControlledBrain)
					player = ((ControlledNpcBrain)((GameNPC)killer).Brain).GetPlayerOwner();
				if (player == null)
					return loot;

				if (int.TryParse(StartingLevel.Value, out iStartingLevel) == false) {
					if (log.IsErrorEnabled)
						log.Error("Serverproperty entry " + StartingLevel.Key + " is not an integer.  Loot generator for Dreaded Seals disabled.");
					
					return loot;
				}
				
				if (int.TryParse(DropChancePerLevel.Value, out iPercentPerLevel) == false) {
					if (log.IsErrorEnabled)
						log.Error("Serverproperty entry " + DropChancePerLevel.Key + " is not an integer.  Loot generator for Dreaded Seals disabled.");
					
					return loot;
				}
				
				if (int.TryParse(BaseChance.Value, out iBaseChance) == false) {
					if (log.IsErrorEnabled)
						log.Error("Serverproperty entry " + BaseChance.Key + " is not an integer.  Loot generator for Dreaded Seals disabled.");
					
					return loot;
				}

				
				if (mob.Level < iStartingLevel)
					return loot;
				
				int iPercentDrop = (mob.Level - iStartingLevel) * iPercentPerLevel + iBaseChance;
				
				Random rnd = new Random();  // Loot.AddChance() is only accurate to the nearest percent, while I want it to go to the nearest quarter percent
				
				int iRandom = rnd.Next(10000);
				
				if (iRandom < iPercentDrop) {
					ItemTemplate DreadedSeal = new ItemTemplate(m_DreadedSeal);					
					
					loot.AddFixed(DreadedSeal, 1);
				}
				
				
				if (log.IsDebugEnabled)
					log.Debug("LootGeneratorDreadedSeal Calculations: "
					+ "Mob level " + mob.Level.ToString()
					+ ", " + (iPercentDrop / 100).ToString()
					+ "% drop chance, rolled " + (iRandom / 100).ToString());
			} catch {
				return loot;
			}
			
			return loot;
		}
	}
}
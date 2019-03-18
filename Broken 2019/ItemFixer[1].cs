using System;
using System.Collections;
using System.Reflection;

using DOL.Database;
using DOL.Events;
using DOL.GS;
using DOL.GS.PacketHandler;

using log4net;

namespace DOL.GS.Scripts
{
	public class ItemFixer : GameNPC 
	{
		private static ItemFixer Albion_ItemNPC = null;
		private static ItemFixer Midgard_ItemNPC = null;
		private static ItemFixer Hibernia_ItemNPC = null;

		/// <summary>
		/// Defines a logger for this class.
		/// </summary>
		public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		[ScriptLoadedEvent]
		public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
		{
			GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
			template.AddNPCEquipment(eInventorySlot.TwoHandWeapon, 1658);
			template.AddNPCEquipment(eInventorySlot.Cloak, 1720);
			template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2245);
			template = template.CloseTemplate();

			Albion_ItemNPC = new ItemFixer();
			Albion_ItemNPC.Model = 280;
			Albion_ItemNPC.Name = "Equipment Transmogrifier";
			Albion_ItemNPC.GuildName = "Equipment Transmogrification Services";
			Albion_ItemNPC.Realm = eRealm.Albion;
			Albion_ItemNPC.CurrentRegionID = 10;
			Albion_ItemNPC.Size = 60;
			Albion_ItemNPC.Level = 50;
			Albion_ItemNPC.X = 33481;
			Albion_ItemNPC.Y = 22794;
			Albion_ItemNPC.Z = 8480;
			Albion_ItemNPC.Heading = 914;
			Albion_ItemNPC.Inventory = template;
			Albion_ItemNPC.AddToWorld();
			Albion_ItemNPC.SwitchWeapon(eActiveWeaponSlot.TwoHanded);

			Midgard_ItemNPC = new ItemFixer();
			Midgard_ItemNPC.Model = 160;
			Midgard_ItemNPC.Name = "Equipment Transmogrifier";
			Midgard_ItemNPC.GuildName = "Equipment Transmogrification Services";
			Midgard_ItemNPC.Realm = eRealm.Midgard;
			Midgard_ItemNPC.CurrentRegionID = 101;
			Midgard_ItemNPC.Size = 60;
			Midgard_ItemNPC.Level = 50;
			Midgard_ItemNPC.X = 31318;
			Midgard_ItemNPC.Y = 27531;
			Midgard_ItemNPC.Z = 8776;
			Midgard_ItemNPC.Heading = 3128;
			Midgard_ItemNPC.Inventory = template;
			Midgard_ItemNPC.AddToWorld();
			Midgard_ItemNPC.SwitchWeapon(eActiveWeaponSlot.TwoHanded);

			Hibernia_ItemNPC = new ItemFixer();
			Hibernia_ItemNPC.Model = 340;
			Hibernia_ItemNPC.Name = "Equipment Transmogrifier";
			Hibernia_ItemNPC.GuildName = "Equipment Transmogrification Services";
			Hibernia_ItemNPC.Realm = eRealm.Hibernia;
			Hibernia_ItemNPC.CurrentRegionID = 201;
			Hibernia_ItemNPC.Size = 60;
			Hibernia_ItemNPC.Level = 50;
			Hibernia_ItemNPC.X = 33061;
			Hibernia_ItemNPC.Y = 31676;
			Hibernia_ItemNPC.Z = 8000;
			Hibernia_ItemNPC.Heading = 4090;
			Hibernia_ItemNPC.Inventory = template;
			Hibernia_ItemNPC.AddToWorld();
			Hibernia_ItemNPC.SwitchWeapon(eActiveWeaponSlot.TwoHanded);

			if (log.IsInfoEnabled)
				log.Info("Item Fixers Loaded");
		}

		[ScriptUnloadedEvent]
		public static void OnScriptUnloaded(DOLEvent e, object sender, EventArgs args)
		{
			Albion_ItemNPC.Delete();
			Midgard_ItemNPC.Delete();
			Hibernia_ItemNPC.Delete();
		}

		public override IList GetExamineMessages(GamePlayer player)
		{
			IList list = new ArrayList();
			list.Add("You target [" + GetName(0, false) + "]");
			list.Add("You examine " + GetName(0, false) + "  " + GetPronoun(0, true) + " is " + GetAggroLevelString(player, false) + " and is a item fixer.");
			list.Add("[Give him an object to be fixed]");
			return list;
		}

		public override bool ReceiveItem(GameLiving source, InventoryItem item)
		{
			GamePlayer player = source as GamePlayer;
			if (player == null || item == null)
				return false;

			if (item.Count != 1)
			{
				player.Out.SendMessage(GetName(0, false) + " can't fix stacked objets.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return false;
			}

			string id = item.Id_nb;
			ItemTemplate template = (ItemTemplate) GameServer.Database.FindObjectByKey(typeof (ItemTemplate), GameServer.Database.Escape(id));
			if (template == null)
			{
				SayTo(player, "Unfortunately I can't find a template for " + id);
				return false;
			}
			InventoryItem newItem = new InventoryItem(template);
			newItem.Condition = item.Condition;
			newItem.Durability = item.Durability;
			player.Inventory.RemoveItem(item);
			player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, newItem);
			SayTo(player, "There you go, " + item.Name + " is updated to the latest version");
			return true;
		}
	}
}

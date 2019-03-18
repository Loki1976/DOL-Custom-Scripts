

using System;
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using System.Reflection;
using System.Collections;
using DOL.Database;
using log4net;

namespace DOL.GS.Scripts
{

    public class FreeWeapon : GameNPC
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static ItemTemplate EpicCloak = null;
        private static ItemTemplate ResistsBracer = null;

        private static ItemTemplate SkillBracer = null;
        private static ItemTemplate StatsRing = null;

        private static ItemTemplate RangerEpicBow = null;
        private static ItemTemplate ScoutEpicBow = null;
        private static ItemTemplate HunterEpicBow = null;
        private static ItemTemplate EpicShortBow = null;
        private static ItemTemplate EpicCrossBow = null;
        private static ItemTemplate EpicCasterStaff = null;
        private static ItemTemplate EpicFriarStaff = null;
        private static ItemTemplate EpicHarp = null;
        private static ItemTemplate EpicFist = null;
        private static ItemTemplate EpicMaulerStaff = null;
        private static ItemTemplate Midgard2HHammer = null;
        private static ItemTemplate Midgard2HSword = null;
        private static ItemTemplate Midgard2HAxe = null;
        private static ItemTemplate Midgard1HHammer = null;
        private static ItemTemplate Midgard1HSword = null;
        private static ItemTemplate Midgard1HAxe = null;
        private static ItemTemplate Midgard1HLeftAxe = null;
        private static ItemTemplate MidgardSlashClaw = null;
        private static ItemTemplate MidgardSlashLeftClaw = null;
        private static ItemTemplate MidgardThrustClaw = null;
        private static ItemTemplate MidgardThrustLeftClaw = null;
        private static ItemTemplate MidgardThrustSpear = null;
        private static ItemTemplate MidgardSlashSpear = null;
        private static ItemTemplate EpicSmallShield = null;
        private static ItemTemplate EpicMediumShield = null;
        private static ItemTemplate EpicLargeShield = null;
        private static ItemTemplate HiberniaTrustSpear = null;
        private static ItemTemplate HiberniaSlashSpear = null;
        private static ItemTemplate HiberniaSlashLargeWeapon = null;
        private static ItemTemplate HiberniaThrustLargeWeapon = null;
        private static ItemTemplate HiberniaCrushLargeWeapon = null;
        private static ItemTemplate HiberniaScythe = null;
        private static ItemTemplate HiberniaBlade = null;
        private static ItemTemplate HiberniaLeftBlade = null;
        private static ItemTemplate HiberniaBlunt = null;
        private static ItemTemplate HiberniaLeftBlunt = null;
        private static ItemTemplate HiberniaPierce = null;
        private static ItemTemplate HiberniaLeftPierce = null;
        private static ItemTemplate AlbionTwoHandedCrush = null;
        private static ItemTemplate AlbionTwoHandedThrust = null;
        private static ItemTemplate AlbionTwoHandedSlash = null;
        private static ItemTemplate AlbionCrushPole = null;
        private static ItemTemplate AlbionThrustPole = null;
        private static ItemTemplate AlbionSlashPole = null;
        private static ItemTemplate AlbionSlash = null;
        private static ItemTemplate AlbionLeftSlash = null;
        private static ItemTemplate AlbionCrush = null;
        private static ItemTemplate AlbionLeftCrush = null;
        private static ItemTemplate AlbionThrust = null;
        private static ItemTemplate AlbionLeftThrust = null;
        private static ItemTemplate AlbionCrushFlex = null;
        private static ItemTemplate AlbionSlashFlex = null;
        private static ItemTemplate AlbionThrustFlex = null;
		
		protected static void GiveItem(GamePlayer player, ItemTemplate itemTemplate) { GiveItem(null, player, itemTemplate); }
        protected static void GiveItem(GameLiving source, GamePlayer player, ItemTemplate itemTemplate)
        {
            itemTemplate.AllowAdd = true;
            if (GameServer.Database.SelectObject<ItemTemplate>("Id_nb = '" + itemTemplate.Id_nb + "'") == null)
            {
                GameServer.Database.AddObject(itemTemplate);
            }
		//ameInventoryItem.Create<ItemTemplate>(itemTemplate);
            InventoryItem item = GameInventoryItem.Create<ItemTemplate>(itemTemplate);
            if (player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, item))
            {
                if (source == null) { player.Out.SendMessage("You receive the " + itemTemplate.Name + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow); }
                else { player.Out.SendMessage("You receive " + itemTemplate.GetName(0, false) + " from " + source.GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow); }
            }
            else { player.CreateItemOnTheGround(item); player.Out.SendMessage("Your Inventory is full. You couldn't recieve the " + itemTemplate.Name + ",so it's been placed on the ground. Pick it up as soon as possible or it will vanish in a few minutes.", eChatType.CT_Important, eChatLoc.CL_PopupWindow); }
        }

        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            if (log.IsInfoEnabled) log.Info("Equipment NPC Loading ...");

            ItemTemplate item = null;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicCloack"; item.Name = "Epic Cloak"; item.Level = 50; item.Item_Type = 26; item.Model = 1720; item.Hand = 0; item.Type_Damage = 0; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 0; item.SPD_ABS = 0; item.Object_Type = 41; item.Quality = 100; item.Weight = 1; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicCloak = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "SkillBracer"; item.Name = "Skill Bracer"; item.Level = 50; item.Item_Type = 33; item.Model = 1885; item.Hand = 0; item.Type_Damage = 0; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 0; item.SPD_ABS = 0; item.Object_Type = 41; item.Quality = 100; item.Weight = 1; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000;
            item.Bonus1 = 11; item.Bonus1Type = 163; item.Bonus2 = 11; item.Bonus2Type = 164; item.Bonus3 = 11; item.Bonus3Type = 167; item.Bonus4 = 11; item.Bonus4Type = 168; item.Bonus5 = 11; item.Bonus5Type = 31; item.Bonus6 = 11; item.Bonus6Type = 49; item.Bonus7 = 11; item.Bonus7Type = 43; item.Bonus8 = 11; item.Bonus8Type = 40; SkillBracer = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "ResistsBracer"; item.Name = "Resists Bracer"; item.Level = 50; item.Item_Type = 33; item.Model = 1885; item.Hand = 0; item.Type_Damage = 0; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 0; item.SPD_ABS = 0; item.Object_Type = 41; item.Quality = 100; item.Weight = 1; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000;
            item.Bonus1 = 26; item.Bonus1Type = 11; item.Bonus2 = 26; item.Bonus2Type = 12; item.Bonus3 = 26; item.Bonus3Type = 13; item.Bonus4 = 26; item.Bonus4Type = 14; item.Bonus5 = 26; item.Bonus5Type = 15; item.Bonus6 = 26; item.Bonus6Type = 16; item.Bonus7 = 26; item.Bonus7Type = 17; item.Bonus8 = 26; item.Bonus8Type = 18; item.Bonus9 = 26; item.Bonus9Type = 19; ResistsBracer = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "StatsRing"; item.Name = "Stats Ring"; item.Level = 50; item.Item_Type = 35; item.Model = 1888; item.Hand = 0; item.Type_Damage = 0; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 0; item.SPD_ABS = 0; item.Object_Type = 41; item.Quality = 100; item.Weight = 1; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000;
            item.Bonus1 = 101; item.Bonus1Type = 1; item.Bonus2 = 101; item.Bonus2Type = 2; item.Bonus3 = 101; item.Bonus3Type = 3; item.Bonus4 = 101; item.Bonus4Type = 4; item.Bonus5 = 101; item.Bonus5Type = 5; item.Bonus6 = 101; item.Bonus6Type = 6; item.Bonus7 = 101; item.Bonus7Type = 7; item.Bonus8 = 101; item.Bonus8Type = 8; item.Bonus9 = 101; item.Bonus9Type = 9; item.Bonus10 = 200; item.Bonus10Type = 10; StatsRing = item;

            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "RangerEpicBow"; item.Name = "Bow of the Ranger"; item.Level = 50; item.Item_Type = (int)eInventorySlot.DistanceWeapon; item.Model = 471; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 54; item.Object_Type = 18; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; RangerEpicBow = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "ScoutEpicBow"; item.Name = "Bow of the Scout"; item.Level = 50; item.Item_Type = (int)eInventorySlot.DistanceWeapon; item.Model = 471; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 54; item.Object_Type = 9; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; ScoutEpicBow = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HunterEpicBow"; item.Name = "Bow of the Hunter"; item.Level = 50; item.Item_Type = (int)eInventorySlot.DistanceWeapon; item.Model = 564; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 44; item.Object_Type = 15; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HunterEpicBow = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicShortBow"; item.Name = "Epic Short Bow"; item.Level = 50; item.Item_Type = (int)eInventorySlot.DistanceWeapon; item.Model = 471; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 40; item.Object_Type = 5; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicShortBow = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicCrossBow"; item.Name = "Epic Cross Bow"; item.Level = 50; item.Item_Type = (int)eInventorySlot.DistanceWeapon; item.Model = 892; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 40; item.Object_Type = 10; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicCrossBow = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicCasterStaff"; item.Name = "Epic Staff of the Caster"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 883; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 44; item.Object_Type = 8; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; item.Bonus1 = 50; item.Bonus1Type = 165; EpicCasterStaff = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicFriarStaff"; item.Name = "Epic Friar Quarterstaff"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 883; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 30; item.Object_Type = 8; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicFriarStaff = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicMaulerStaff"; item.Name = "Epic Mauler Staff"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 883; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 30; item.Object_Type = 28; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicMaulerStaff = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicFist"; item.Name = "Epic Fist"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 3478; item.Hand = 2; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 33; item.Object_Type = 27; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicFist = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard2HHammer"; item.Name = "Two Handed Hammer of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 576; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 48; item.Object_Type = 12; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard2HHammer = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard2HSword"; item.Name = "Two Handed Sword of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 314; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 56; item.Object_Type = 11; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard2HSword = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard2HAxe"; item.Name = "Two Handed Axe of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 577; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 51; item.Object_Type = 13; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard2HAxe = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardThrustSpear"; item.Name = "Thrust Spear of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 332; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 40; item.Object_Type = 14; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardThrustSpear = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardSlashSpear"; item.Name = "Slash Spear of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 332; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 40; item.Object_Type = 14; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardSlashSpear = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard1HHammer"; item.Name = "One Handed Hammer of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 323; item.Hand = 0; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 36; item.Object_Type = 12; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard1HHammer = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard1HSword"; item.Name = "One Handed Sword of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 311; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 11; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard1HSword = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard1HAxe"; item.Name = "One Handed Axe of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 573; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 35; item.Object_Type = 13; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard1HAxe = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "Midgard1HLeftAxe"; item.Name = "One Handed Left Axe of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 577; item.Hand = 2; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 35; item.Object_Type = 17; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; Midgard1HLeftAxe = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardSlashClaw"; item.Name = "One Handed Slash Claw of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 965; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 30; item.Object_Type = 25; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardSlashClaw = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardSlashLeftClaw"; item.Name = "One Handed Left Slash Claw of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 965; item.Hand = 2; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 30; item.Object_Type = 25; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardSlashLeftClaw = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardThrustClaw"; item.Name = "One Handed Thrust Claw of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 965; item.Hand = 0; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 31; item.Object_Type = 25; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardThrustClaw = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "MidgardThrustLeftClaw"; item.Name = "One Handed Left Thrust Claw of Midgard"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 965; item.Hand = 2; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 31; item.Object_Type = 25; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; MidgardThrustLeftClaw = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionTwoHandedCrush"; item.Name = "Two Handed Crush of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2662; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 48; item.Object_Type = 06; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionTwoHandedCrush = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionTwoHandedThrust"; item.Name = "Two Handed Thruster of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2662; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 37; item.Object_Type = 06; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionTwoHandedThrust = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionTwoHandedSlash"; item.Name = "Two Handed Slasher of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2662; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 51; item.Object_Type = (int)eObjectType.TwoHandedWeapon; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionTwoHandedSlash = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionCrushPole"; item.Name = "Crush Pole of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2664; item.Hand = 1; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 56; item.Object_Type = 7; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionCrushPole = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionThrustPole"; item.Name = "Thrust Pole of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2664; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 43; item.Object_Type = 7; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionThrustPole = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionSlashPole"; item.Name = "Slash Pole of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2664; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 48; item.Object_Type = 07; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionSlashPole = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionSlash"; item.Name = "One Handed Slasher of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 311; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 03; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionSlash = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionLeftSlash"; item.Name = "One Left Handed Slasher of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 311; item.Hand = 2; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 03; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionLeftSlash = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionCrush"; item.Name = "One Handed Crusher of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 323; item.Hand = 0; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 02; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionCrush = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionLeftCrush"; item.Name = "Left Handed Crusher of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 323; item.Hand = 2; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 02; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionLeftCrush = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionThrust"; item.Name = "One Handed Thruster of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 2658; item.Hand = 0; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = (int)eObjectType.ThrustWeapon; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionThrust = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionLeftThrust"; item.Name = "Left Handed Thruster of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 2658; item.Hand = 2; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = (int)eObjectType.ThrustWeapon; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionLeftThrust = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionCrushFlex"; item.Name = "One Handed Crush Flex of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 859; item.Hand = 0; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 35; item.Object_Type = 24; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionCrushFlex = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionSlashFlex"; item.Name = "One Handed Slash Flex of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 859; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 35; item.Object_Type = 24; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionSlashFlex = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "AlbionThrustFlex"; item.Name = "One Handed Thrust Flex of Albion"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 859; item.Hand = 0; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 35; item.Object_Type = 24; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; AlbionThrustFlex = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaThrustSpear"; item.Name = "Thrust Spear of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 332; item.Hand = 1; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 51; item.Object_Type = 23; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaTrustSpear = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaSlashSpear"; item.Name = "Slash Spear of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 332; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 51; item.Object_Type = 23; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaSlashSpear = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaSlashLargeWeapon"; item.Name = "Slash Large Weapon of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2690; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.Realm = 3; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 45; item.Object_Type = 22; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaSlashLargeWeapon = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaCrushLargeWeapon"; item.Name = "Thrust Large Weapon of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2691; item.Hand = 1; item.Realm = 3; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 45; item.Object_Type = 22; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaCrushLargeWeapon = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaThrustLargeWeapon"; item.Name = "Thrust Large Weapon of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 469; item.Hand = 1; item.Realm = 3; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 45; item.Object_Type = 22; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaThrustLargeWeapon = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaScythe"; item.Name = "Slash Scythe of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 927; item.Hand = 1; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 37; item.Object_Type = 26; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaScythe = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HibernaBlade"; item.Name = "One Handed Blade of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 311; item.Hand = 0; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 19; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaBlade = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaLeftBlade"; item.Name = "Left Handed Blade of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 311; item.Hand = 2; item.Type_Damage = (int)eDamageType.Slash; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 19; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaLeftBlade = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaBlunt"; item.Name = "One Handed Blunt of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 323; item.Hand = 0; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 20; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaBlunt = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaLeftBlunt"; item.Name = "One Handed Left Blunt of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 323; item.Hand = 2; item.Type_Damage = (int)eDamageType.Crush; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 20; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaLeftBlunt = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaPierce"; item.Name = "One Handed Pierce of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.RightHandWeapon; item.Model = 2684; item.Hand = 0; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 34; item.Object_Type = 21; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaPierce = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "HiberniaLeftPierce"; item.Name = "One Left Handed Piercer of Hibernia"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 323; item.Hand = 2; item.Type_Damage = (int)eDamageType.Thrust; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 21; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; HiberniaLeftPierce = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicHarp"; item.Name = "Epic Harp"; item.Level = 50; item.Item_Type = (int)eInventorySlot.TwoHandWeapon; item.Model = 2116; item.Hand = 1; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 4; item.SPD_ABS = 40; item.Object_Type = (int)eObjectType.Instrument; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicHarp = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicSmallShield"; item.Name = "Cross Realm Small Shield"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 59; item.Hand = 2; item.Type_Damage = 1; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 28; item.Object_Type = 42; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicSmallShield = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicMediumShield"; item.Name = "Cross Realm Medium Shield"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 61; item.Hand = 2; item.Type_Damage = 2; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 32; item.Object_Type = 42; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicMediumShield = item;
            item = new ItemTemplate(); item.AllowAdd = true; item.Id_nb = "EpicLargeShield"; item.Name = "Cross Realm Large Shield"; item.Level = 50; item.Item_Type = (int)eInventorySlot.LeftHandWeapon; item.Model = 60; item.Hand = 2; item.Type_Damage = 3; item.IsDropable = true; item.IsPickable = true; item.DPS_AF = 165; item.SPD_ABS = 40; item.Object_Type = 42; item.Quality = 100; item.Weight = 22; item.Bonus = 35; item.MaxCondition = 50000; item.MaxDurability = 50000; item.Condition = 50000; item.Durability = 50000; EpicLargeShield = item;

            if (log.IsInfoEnabled) log.Info("Weapon NPC Loaded....");
        }
		public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            else SendReply(player, "Hello! " + player.Name + ", are you interested in some :[Weapons]?");
            return true;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer player = (GamePlayer)source;
            switch (str)
            {
                case "Weapons":
                    {


                        if (player.CharacterClass.ID == (byte)eCharacterClass.Wizard) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Minstrel) { SendReply(player, "Would you like a [Slash Weapon],[Thrust Weapon],[Harp]\n" + "or a [Small Shield]"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Sorcerer) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Cleric) { SendReply(player, "Would you like A [Crush Weapon] or a [Medium Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Paladin) { SendReply(player, "Would you like a [Two Handed Crush] a [Two Handed Slash] a [Two Handed Thrust]\n" + "or a [Crush Weapon] a [Slash Weapon] a [Thrust Weapon], a [Small Shield], [Medium Shield], [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Mercenary) { SendReply(player, "Would you like a [Crush Weapon],[Slash Weapon],[Thrust Weapon]\n" + "a [Offhand Thruster],[Offhand Slasher],[Offhand Crusher], a [Small Shield], [Medium Shield] or a [Short Bow]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Reaver) { SendReply(player, "Would you like a [Slash Weapon],[Crush Weapon],[Thrust Weapon]\n" + "or a [Thrust Flex],[Slash Flex],[Crush Flex], a [Small Shield], [Medium Shield], [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Cabalist) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Infiltrator) { SendReply(player, "Would you like a [Slash Weapon],[Thrust Weapon]\n" + "a [Offhand Thruster],[Offhand Slasher],or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Necromancer) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Scout) { SendReply(player, "Would you like a [Slash Weapon],[Thrust Weapon] a [Albion Bow] or a [Small Shield]"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Armsman) { SendReply(player, "Would you like a [Crush Weapon],[Slash Weapon],[Thrust Weapon]\n" + "a [Two Handed Crush] a [Two Handed Slash] a [Two Handed Thrust], a [Small Shield], [Medium Shield], [Large Shield] a [Crush Pole] a [Slash Pole] or a [Thrust Pole], [Cross Bow]"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Theurgist) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Friar) { SendReply(player, "Would you like a [Staff] a [Crush Weapon] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Heretic) { SendReply(player, "Would you like a [Slash Weapon],[Crush Weapon],[Thrust Weapon]\n" + "or a [Thrust Flex],[Slash Flex],[Crush Flex] or finally a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Hunter) { SendReply(player, "Would you like a [Midgard Bow] a [One Handed Sword] a [Two Handed Sword]] or a [Midgard Slash Spear]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Shadowblade) { SendReply(player, "Would you like a [Sword],[Axe],[Left Axe],[Two Handed Sword],[Two Handed Axe] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Spiritmaster) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Runemaster) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Bonedancer) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Healer) { SendReply(player, "Would you like a [Hammer],a [Two Handed Hammer] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Shaman) { SendReply(player, "Would you like a [Hammer],a [Two Handed Hammer] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Warrior) { SendReply(player, "Would you like a [Hammer],[Sword],[Axe] ,a [Two Handed Hammer],[Two Handed Sword],[Two Handed Axe], a [Small Shield], [Medium Shield] or a [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Berserker) { SendReply(player, "Would you like a [Hammer],[Sword],[Axe],[Left Axe] ,a [Two Handed Hammer],[Two Handed Sword],[Two Handed Axe] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Thane) { SendReply(player, "Would you like a [Hammer],[Sword],[Axe],a [Two Handed Hammer],[Two Handed Sword],[Two Handed Axe], a [Small Shield], [Medium Shield] or a [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Skald) { SendReply(player, "Would you like a [Hammer],[Sword],[Axe],a [Two Handed Hammer],[Two Handed Sword],[Two Handed Axe] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Savage) { SendReply(player, "Would you like a [Hammer],[Sword],[Axe],a [Two Handed Hammer],[Two Handed Sword],[Two Handed Axe] a [Small Shield]\n" + "Or a [Slash Claw],[Offhand Slash Claw],[Thrust Claw],[Offhand Thrust Claw] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Warlock) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Valkyrie) { SendReply(player, "Would you like a [Hammer], [Sword], [Midgard Thrust Spear], [Midgard Slash Spear], [Two Handed Sword], a [Small Shield], [Medium Shield] or a [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Bard) { SendReply(player, "Would you like a [Blunt Weapon] a [Blade Weapon] a [Harp] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Bainshee) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Druid) { SendReply(player, "Would you like a [Blunt Weapon], [Blade Weapon], or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Warden) { SendReply(player, "Would you like a [Blade Weapon], [Blunt Weapon], a [Small Shield], [Medium Shield] or a [Large Shield], a [Short Bow]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Blademaster) { SendReply(player, "Would you like a [Blade Weapon], [Blunt Weapon], [Pierce Weapon], [Offhand Blade], [Offhand Blunt], [Offhand Pierce], a [Small Shield] or a [Medium Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Hero) { SendReply(player, "Would you like a [Blade Weapon], [Blunt Weapon], [Pierce Weapon], a [Large Blunt], [Large Blade], [Large Pierce], a [Hibernian Blade Spear], [Hibernian Pierce Spear], a [Small Shield], [Medium Shield] or a [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Champion) { SendReply(player, "Would you like a [Blade Weapon], [Blunt Weapon], [Pierce Weapon], a [Large Blunt], [Large Blade], [Large Pierce], a [Small Shield], [Medium Shield] or a [Large Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Eldritch) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Enchanter) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Mentalist) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Nightshade) { SendReply(player, "Would you like a [Blade Weapon], [Offhand Blade], [Pierce Weapon], [Offhand Pierce] or a [Small Shield]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Ranger) { SendReply(player, "Would you like a [Blade Weapon], [Offhand Blade], [Pierce Weapon], [Offhand Pierce], a [Small Shield] or a [Hibernian Bow]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Animist) { SendReply(player, "Would you like the [Caster Staff]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Valewalker) { SendReply(player, "Would you like the [Hibernia Scythe]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.Vampiir) { SendReply(player, "Would you like a [Pierce Weapon]?"); }
                        if (player.CharacterClass.ID == (byte)eCharacterClass.MaulerMid || player.CharacterClass.ID == (byte)eCharacterClass.MaulerHib || player.CharacterClass.ID == (byte)eCharacterClass.MaulerAlb) { SendReply(player, "Would you like a [Mauler Staff] or some [Fists]?"); }
                        break;
                    }
                case "Hibernia Scythe": { GiveItem(player, HiberniaScythe); break; }
                case "Caster Staff": { GiveItem(player, EpicCasterStaff); break; }
                case "Blade Weapon": { GiveItem(player, HiberniaBlade); break; }
                case "Offhand Blade": { GiveItem(player, HiberniaLeftBlade); break; }
                case "Pierce Weapon": { GiveItem(player, HiberniaPierce); break; }
                case "Offhand Pierce": { GiveItem(player, HiberniaLeftPierce); break; }
                case "Hibernian Bow": { GiveItem(player, RangerEpicBow); break; }
                case "Blunt Weapon": { GiveItem(player, HiberniaBlunt); break; }
                case "Offhand Blunt": { GiveItem(player, HiberniaLeftBlunt); break; }
                case "Large Blunt": { GiveItem(player, HiberniaCrushLargeWeapon); break; }
                case "Large Blade": { GiveItem(player, HiberniaSlashLargeWeapon); break; }
                case "Large Pierce": { GiveItem(player, HiberniaThrustLargeWeapon); break; }
                case "Hibernian Pierce Spear": { GiveItem(player, HiberniaTrustSpear); break; }
                case "Hibernian Blade Spear": { GiveItem(player, HiberniaSlashSpear); break; }
                case "Hammer": { GiveItem(player, Midgard1HHammer); break; }
                case "Axe": { GiveItem(player, Midgard1HAxe); break; }
                case "Left Axe": { GiveItem(player, Midgard1HLeftAxe); break; }
                case "Sword": { GiveItem(player, Midgard1HSword); break; }
                case "Midgard Thrust Spear": { GiveItem(player, MidgardThrustSpear); break; }
                case "Two Handed Hammer": { GiveItem(player, Midgard2HHammer); break; }
                case "Two Handed Sword": { GiveItem(player, Midgard2HSword); break; }
                case "Two Handed Axe": { GiveItem(player, Midgard2HAxe); break; }
                case "Slash Claw": { GiveItem(player, MidgardSlashClaw); break; }
                case "Offhand Slash Claw": { GiveItem(player, MidgardSlashLeftClaw); break; }
                case "Thrust Claw": { GiveItem(player, MidgardThrustClaw); break; }
                case "Offhand Thrust Claw": { GiveItem(player, MidgardThrustLeftClaw); break; }
                case "Midgard Bow": { GiveItem(player, HunterEpicBow); break; }
                case "Midgard Slash Spear": { GiveItem(player, MidgardSlashSpear); break; }
                case "Staff": { GiveItem(player, EpicFriarStaff); break; }
                case "Mauler Staff": { GiveItem(player, EpicMaulerStaff); break; }
                case "Fists": { GiveItem(player, EpicFist); GiveItem(player, EpicFist); break; }
                case "Crush Weapon": { GiveItem(player, AlbionCrush); break; }
                case "Thrust Weapon": { GiveItem(player, AlbionThrust); break; }
                case "Slash Weapon": { GiveItem(player, AlbionSlash); break; }
                case "Offhand Thruster": { GiveItem(player, AlbionLeftThrust); break; }
                case "Offhand Slasher": { GiveItem(player, AlbionLeftSlash); break; }
                case "Offhand Crusher": { GiveItem(player, AlbionLeftCrush); break; }
                case "Thrust Flex": { GiveItem(player, AlbionThrustFlex); break; }
                case "Slash Flex": { GiveItem(player, AlbionSlashFlex); break; }
                case "Crush Flex": { GiveItem(player, AlbionCrushFlex); break; }
                case "Two Handed Crush": { GiveItem(player, AlbionTwoHandedCrush); break; }
                case "Two Handed Slash": { GiveItem(player, AlbionTwoHandedSlash); break; }
                case "Two Handed Thrust": { GiveItem(player, AlbionTwoHandedThrust); break; }
                case "Crush Pole": { GiveItem(player, AlbionCrushPole); break; }
                case "Slash Pole": { GiveItem(player, AlbionSlashPole); break; }
                case "Thrust Pole": { GiveItem(player, AlbionThrustPole); break; }
                case "Albion Bow": { GiveItem(player, ScoutEpicBow); break; }
                case "Small Shield": { GiveItem(player, EpicSmallShield); break; }
                case "Medium Shield": { GiveItem(player, EpicMediumShield); break; }
                case "Large Shield": { GiveItem(player, EpicLargeShield); break; }
                case "Harp": { GiveItem(player, EpicHarp); break; }
                case "Short Bow": { GiveItem(player, EpicShortBow); break; }
                case "Cross Bow": { GiveItem(player, EpicCrossBow); break; }
            }
            return true;
        }

        private void SendReply(GamePlayer target, string msg) { target.Out.SendMessage(msg, eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
    }

}
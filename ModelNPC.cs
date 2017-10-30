/*
 * By Uggae 11-05-06 
 * Very simple item model changing NPC that gives you the option to pick between 7 models for 
 * each weapon/armor, 9 models for robes and cloaks, 8 models for helms/circlets and an option 
 * to reset the item back to its original model. No longer will players be able to exploit 
 * LOS with boats or lag servers with multiple items! Please note item models are realm specific, 
 * and sorry in advance if I made any mistakes. It took me some time to add in all the models.
 * 
 * It also generates a random comment, insult, or compliment for the player. You can change 
 * the messages with anything you want or toggle this option off if you wish to do so.
 * 
 * To everyone who has worked on any daoc script and to those who have helped work on the DOL server: THANK YOU! =)
 */
using DOL;
using DOL.GS;
using DOL.Events;
using DOL.Database;
using System;
using DOL.GS.PacketHandler;

namespace DOL.GS.Scripts
{
	[NPCGuildScript("Model NPC")]
	public class ModelNPC : GameNPC
	{
        //No editing needed to change the way it asks for money, it omits the price if it's set to 0
		public long Price = Money.GetMoney(0,0,0,0,0);//change to (mith,plat,gold,sil,cop) to charge money 
        public string TempProperty = "ModelNPC";
        public bool chortle = true; //change to false if you don't want the NPC to make comments

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
			if(base.Interact(player))
			{
				TurnTo(player,500);
                WeakReference itemWeak = (WeakReference)player.TempProperties.getObjectProperty(TempProperty, new WeakRef(null));
                InventoryItem item = (InventoryItem)itemWeak.Target;

                string cost = "";
                if (Price > 0)
                    cost = " for " + Money.GetString(Price);

                if (item == null)
                {
                    SendReply(player, "Hello there! " +
                        "I can change the looks of your weapons and armour" + cost + "..." +
                        " Just hand me the item and I will begin my work.");
                }
                else
                {
                    ReceiveItem(player, item);
                }
				return true;
			}
            return false;
		}
		
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
			if(!(source is GamePlayer)) return false;
			GamePlayer player = (GamePlayer)source;
			TurnTo(player.X,player.Y);
            
            WeakReference itemWeak = (WeakReference)player.TempProperties.getObjectProperty(TempProperty, new WeakRef(null));
            InventoryItem item = (InventoryItem)itemWeak.Target;

            if (item == null)
            {
                SendReply(player, "I need an item to work on!");
                return false;
            }
            int model = item.Model;
			switch (str.ToLower())
            {
                #region reset to item model default
                case "reset":
                    {
                        ItemTemplate m_item = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), item.Id_nb);
                        if (m_item != null)
                        {
                            model = m_item.Model;
                        }
                        else
                        {
                            SendReply(player, "I am sorry, I can't seem to find that item!");
                            player.TempProperties.removeProperty(TempProperty);
                            return false;
                        }
                    }
                    break;
                #endregion reset
                #region artifact weapons
                #region battler
                case "battler":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Sword:
                                case eObjectType.Blades:
                                case eObjectType.Blunt:
                                case eObjectType.CrushingWeapon:
                                case eObjectType.Hammer:
                                    model = 2112;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Sword:
                                case eObjectType.LargeWeapons:
                                case eObjectType.Hammer:
                                case eObjectType.TwoHandedWeapon:
                                    model = 1670;
                                    break;
                                case eObjectType.PolearmWeapon:
                                    model = 3448;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion battler
                #region braggarts bow
                case "braggarts bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                        case eObjectType.RecurvedBow:
                        case eObjectType.CompositeBow:
                            model = 1667;
                            break;
                        default: return false;
                    }
                    break;
                #endregion braggarts bow
                #region bruiser
                case "bruiser":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blunt:
                                case eObjectType.CrushingWeapon:
                                case eObjectType.Hammer:
                                    model = 1671;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.LargeWeapons:
                                case eObjectType.TwoHandedWeapon:
                                case eObjectType.PolearmWeapon:
                                    model = 2113;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion battler
                #region crocodile tooth dagger
                case "crocodile tooth dagger":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.ThrustWeapon:
                                case eObjectType.SlashingWeapon:
                                case eObjectType.Blades:
                                case eObjectType.CrushingWeapon:
                                case eObjectType.Piercing:
                                case eObjectType.Blunt:
                                    model = 1669;
                                    break;
                                case eObjectType.LeftAxe:
                                case eObjectType.Axe:
                                    model = 3451;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion temporary
                #region fools bow
                case "fools bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                        case eObjectType.RecurvedBow:
                        case eObjectType.CompositeBow:
                            model = 1666;
                            break;
                        default: return false;
                    }
                    break;
                #endregion fools bow
                #region golden spear
                case "golden spear":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.SlashingWeapon:
                                case eObjectType.ThrustWeapon:
                                case eObjectType.Piercing:
                                    model = 1807;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.CelticSpear:
                                case eObjectType.Spear:
                                    model = 1662;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion golden spear
                #region malice axe
                case "malice axe":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                case eObjectType.SlashingWeapon:
                                    model = 2109;
                                    break;
                                case eObjectType.CrushingWeapon:
                                case eObjectType.Blunt:
                                    model = 3447;
                                    break;
                                case eObjectType.LeftAxe:
                                case eObjectType.Axe:
                                    model = 2109;
                                    break;
                                case eObjectType.Sword:
                                    model = 2109;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.LargeWeapons:
                                case eObjectType.TwoHandedWeapon:
                                case eObjectType.PolearmWeapon:
                                    switch ((eDamageType)item.Type_Damage)
                                    {
                                        case eDamageType.Crush:
                                            model = 3449;
                                            break;
                                        case eDamageType.Thrust://there is no thrust malice
                                        case eDamageType.Slash:
                                            model = 2110;
                                            break;
                                        default: return false;
                                    }
                                    break;
                                case eObjectType.Sword:
                                    model = 2110;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion malice axe
                #region scepter meritorious
                case "scepter meritorious":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CrushingWeapon:
                        case eObjectType.Hammer:
                        case eObjectType.Blunt:
                            model = 1672;
                            break;
                        default: return false;
                    }
                    break;
                #endregion scepter meritorious
                #region snake charmers
                case "snake charmers":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.HandToHand:
                                    model = 2197;
                                    break;
                                case eObjectType.Flexible:
                                    model = 2119;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Scythe:
                                    model = 2213;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion snake charmers
                #region spear of kings
                case "spear of kings":
                    switch (item.Item_Type)
                    {
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.CelticSpear:
                                case eObjectType.TwoHandedWeapon:
                                case eObjectType.Spear:
                                case eObjectType.PolearmWeapon:
                                    model = 1661;
                                    break;
                                case eObjectType.Scythe:
                                    model = 3450;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion spear of kings
                #region staff of god
                case "staff of god":
                    switch (item.Item_Type)
                    {
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Staff:
                                    model = 1660;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion staff of god
                #region tartaros gift
                case "tartaros gift":
                    switch (item.Item_Type)
                    {
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Staff:
                                    model = 1658;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion tartaros gift
                #region traitors dagger
                case "traitors dagger":
                    switch (item.Item_Type)
                    {
                        case Slot.RIGHTHAND:
                        case Slot.LEFTHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Sword:
                                case eObjectType.Blades:
                                case eObjectType.ThrustWeapon:
                                case eObjectType.Piercing:
                                    model = 1668;
                                    break;
                                default: return false;
                            }
                            break;
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Sword:
                                case eObjectType.LeftAxe:
                                case eObjectType.Axe:
                                    model = 3452;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion traitors dagger
                #region traldors oracle
                case "traldors oracle":
                    switch (item.Item_Type)
                    {
                        case Slot.TWOHAND:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Staff:
                                    model = 1659;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion traldors oracle
                #endregion artifact weapons
                #region artifact shields
                #region atens shield
                case "atens shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1:
                                case 2:
                                case 3: model = 1663;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion atens shield
                #region cyclops shield
                case "cyclops shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1:
                                case 2: 
                                case 3: model = 1664;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion cyclops shield
                #region khaos shield
                case "khaos shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1:
                                case 2:
                                case 3: model = 1665;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion khaos shield
                #endregion artifact shields
                #region artifact armor
                #region arms of the winds
                case "arms of the winds":
                    switch (item.Item_Type)
                    {
                        case Slot.ARMS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 2500;
                                    break;
                                case eObjectType.Leather:
                                    model = 2501;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2502;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 1733;
                                    break;
                                case eObjectType.Plate:
                                    model = 2503;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion arms of the winds
                #region alvarus leggings
                case "alvarus leggings":
                    switch (item.Item_Type)
                    {
                        case Slot.LEGS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 2504;
                                    break;
                                case eObjectType.Leather:
                                    model = 2507;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2508;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 1745;
                                    break;
                                case eObjectType.Plate:
                                    model = 2510;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion alvarus leggings
                #region crown of zahur
                case "crown of zahur":
                    switch (item.Item_Type)
                    {
                        case Slot.HELM:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Scale:
                                case eObjectType.Cloth:
                                case eObjectType.Leather:
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                case eObjectType.Chain:
                                case eObjectType.Plate:
                                    model = 1839;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion crown of zahur
                #region eirenes hauberk
                case "eirenes hauberk":
                    switch (item.Item_Type)
                    {
                        case Slot.TORSO:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 2227;
                                    break;
                                case eObjectType.Plate:
                                    model = 2226;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion eirenes hauberk
                #region enyalios boots
                case "enyalios boots":
                    switch (item.Item_Type)
                    {
                        case Slot.FEET:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2488;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 1728;
                                    break;
                                case eObjectType.Plate:
                                    model = 2487;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion enyalios boots
                #region flamedancers boots
                case "flamedancers boots":
                    switch (item.Item_Type)
                    {
                        case Slot.FEET:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 1730;
                                    break;
                                case eObjectType.Leather:
                                    model = 2485;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion flamedancers boots
                #region foppish sleeves
                case "foppish sleeves":
                    switch (item.Item_Type)
                    {
                        case Slot.ARMS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 1732;
                                    break;
                                case eObjectType.Leather:
                                    model = 2490;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2491;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion foppish sleeves
                #region golden scarab vest
                case "golden scarab vest":
                    switch (item.Item_Type)
                    {
                        case Slot.TORSO:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Leather:
                                    model = 2187;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2497;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion golden scarab vest
                #region guard of valor
                case "guard of valor":
                    switch (item.Item_Type)
                    {
                        case Slot.TORSO:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 2120;
                                    break;
                                case eObjectType.Leather:
                                    model = 2470;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2473;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 2477;
                                    break;
                                case eObjectType.Plate:
                                    model = 2479;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion guard of valor
                #region maddening scalars
                case "maddening scalars":
                    switch (item.Item_Type)
                    {
                        case Slot.HANDS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    model = 2492;
                                    break;
                                case eObjectType.Leather:
                                    model = 2493;
                                    break;
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 2494;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 2495;
                                    break;
                                case eObjectType.Plate:
                                    model = 1746;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion maddening scalars
                #region mariashas sharkskin gloves
                case "mariashas sharkskin gloves":
                    switch (item.Item_Type)
                    {
                        case Slot.HANDS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 1734;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion mariashas sharkskin gloves
                #region naliahs robe
                case "naliahs robe":
                    switch (item.Item_Type)
                    {
                        case Slot.TORSO:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2516;
                                            break;
                                        case 2: model = 2185;
                                            break;
                                        case 3: model = 2516;
                                            break;
                                    }
                                    break;
                                case eObjectType.Leather:
                                    model = 2515;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion naliahs robe
                #region wings dive
                case "wings dive":
                    switch (item.Item_Type)
                    {
                        case Slot.LEGS:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Reinforced:
                                case eObjectType.Studded:
                                    model = 1768;
                                    break;
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                    model = 2482;
                                    break;
                                case eObjectType.Plate:
                                    model = 2483;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion wings dive
                #region winged helm
                case "winged helm":
                    switch (item.Item_Type)
                    {
                        case Slot.HELM:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Scale:
                                case eObjectType.Chain:
                                case eObjectType.Plate:
                                    model = 2223;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion winged helm
                #endregion artifact armor
                #region basic armor: volcanus, oceanus, stygia, aerus, classic, tiara, robes, cloaks
                #region volcanus
                case "volcanus":
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type) //switch armor type
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2162;
                                            break;
                                        case 2:
                                            model = 2163;
                                            break;
                                        case 3:
                                            model = 2164;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2161;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 2167;
                                            break;
                                        case 3:
                                            model = 2168;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2249;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2165;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 2166;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2361;
                                            break;
                                        case 2:
                                            model = 2379;
                                            break;
                                        case 3:
                                            model = 2397;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2176;
                                            break;
                                        case 2:
                                            model = 2177;
                                            break;
                                        case 3:
                                            model = 2178;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2175;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 2182;
                                            break;
                                        case 3:
                                            model = 2183;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2181;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2179;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 2180;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2364;
                                            break;
                                        case 2:
                                            model = 2382;
                                            break;
                                        case 3:
                                            model = 2400;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1780;
                                            break;
                                        case 2:
                                            model = 1781;
                                            break;
                                        case 3:
                                            model = 1782;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1779;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 1786;
                                            break;
                                        case 3:
                                            model = 1787;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1785;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1783;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 1784;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2370;
                                            break;
                                        case 2:
                                            model = 2388;
                                            break;
                                        case 3:
                                            model = 2406;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1694;
                                            break;
                                        case 2:
                                            model = 1695;
                                            break;
                                        case 3:
                                            model = 1696;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1693;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 1700;
                                            break;
                                        case 3:
                                            model = 1701;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1699;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1697;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 1698;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2367;
                                            break;
                                        case 2:
                                            model = 2385;
                                            break;
                                        case 3:
                                            model = 2403;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1712;
                                            break;
                                        case 2:
                                            model = 1713;
                                            break;
                                        case 3:
                                            model = 1714;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1711;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 1718;
                                            break;
                                        case 3:
                                            model = 1719;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1717;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1715;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 1716;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2373;
                                            break;
                                        case 2:
                                            model = 2409;
                                            break;
                                        case 3:
                                            model = 2391;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1703;
                                            break;
                                        case 2:
                                            model = 1704;
                                            break;
                                        case 3:
                                            model = 1705;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1702;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2:
                                            model = 1709;
                                            break;
                                        case 3:
                                            model = 1710;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1708;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1706;
                                            break;
                                        case 2:
                                        case 3:
                                            model = 1707;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 2376;
                                            break;
                                        case 2:
                                            model = 2394;
                                            break;
                                        case 3:
                                            model = 2412;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    break; 
                #endregion volcanus
                #region oceanus
                case "oceanus":
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2172;
                                            break;
                                        case 2: model = 2173; 
                                            break;
                                        case 3: model = 2174;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1625;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1631;
                                            break;
                                        case 2: model = 1632; 
                                            break;
                                        case 3: model = 1631;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1620;
                                            break;
                                        case 2: model = 1624; 
                                            break;
                                        case 3: model = 1622;
                                            break;
                                    }
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1629;
                                            break;
                                        case 2:
                                        case 3: model = 1630;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2253;
                                            break;
                                        case 2: model = 2271; 
                                            break;
                                        case 3: model = 2289;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1848;
                                            break;
                                        case 2: model = 1849;
                                            break;
                                        case 3: model = 1850;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1639;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1854;
                                            break;
                                        case 2: model = 1854;
                                            break;
                                        case 3: model = 1855;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1645;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1851;
                                            break;
                                        case 2:
                                        case 3: model = 1852;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2256;
                                            break;
                                        case 2: model = 2274; 
                                            break;
                                        case 3: model = 2292;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1848;
                                            break;
                                        case 2: model = 1850; 
                                            break;
                                        case 3: model = 1849;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1847;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1854;
                                            break;
                                        case 2: model = 1854; 
                                            break;
                                        case 3: model = 1855;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1853;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1851;
                                            break;
                                        case 2:
                                        case 3: model = 1852;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2262;
                                            break;
                                        case 2: model = 2298; 
                                            break;
                                        case 3: model = 2280;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2101;
                                            break;
                                        case 2: model = 2102; 
                                            break;
                                        case 3: model = 2103; 
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2100;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2107;
                                            break;
                                        case 2: model = 2108; 
                                            break;
                                        case 3: model = 2107;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2106;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2104;
                                            break;
                                        case 2:
                                        case 3: model = 2105;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2259;
                                            break;
                                        case 2: model = 2277; 
                                            break;
                                        case 3: model = 2295;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1771;
                                            break;
                                        case 2: model = 1773; 
                                            break;
                                        case 3: model = 1772;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1770;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1777;
                                            break;
                                        case 2: model = 1777; 
                                            break;
                                        case 3: model = 1778;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1776;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1774;
                                            break;
                                        case 2: model = 1775; 
                                            break;
                                        case 3: model = 1775;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2265;
                                            break;
                                        case 2: model = 2283; 
                                            break;
                                        case 3: model = 2301;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2092;
                                            break;
                                        case 2: model = 2093; 
                                            break;
                                        case 3: model = 2094;
                                            break;
                                    } 
                                    break;
                                case Slot.ARMS:
                                    model = 2091;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2098;
                                            break;
                                        case 2: model = 2098; 
                                            break;
                                        case 3: model = 2099;
                                            break;
                                    } 
                                    break;
                                case Slot.HANDS:
                                    model = 2097;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2095;
                                            break;
                                        case 2:
                                        case 3: model = 2096;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2268;
                                            break;
                                        case 2: model = 2286; 
                                            break;
                                        case 3: model = 2304;
                                            break;
                                    }                                    
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    break;
                #endregion oceanus
                #region stygia
                case "stygia":
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2153;
                                            break;
                                        case 2: model = 2154;
                                            break;
                                        case 3: model = 2155;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2152;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 2158;
                                            break;
                                        case 3: model = 2159;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2248;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2156;
                                            break;
                                        case 2:
                                        case 3: model = 2157;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2307;
                                            break;
                                        case 2: model = 2325;
                                            break;
                                        case 3: model = 2343;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2135;
                                            break;
                                        case 2: model = 2136;
                                            break;
                                        case 3: model = 2137;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2134;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 2141;
                                            break;
                                        case 3: model = 2142;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2140;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2138;
                                            break;
                                        case 2:
                                        case 3: model = 2139;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2310;
                                            break;
                                        case 2: model = 2328;
                                            break;
                                        case 3: model = 2346;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1757;
                                            break;
                                        case 2: model = 1758;
                                            break;
                                        case 3: model = 1759;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1756;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1763;
                                            break;
                                        case 3: model = 1764;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1762;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                            model = 1760;
                                            break;
                                        case 2:
                                        case 3: model = 1761;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2316;
                                            break;
                                        case 2: model = 2334;
                                            break;
                                        case 3: model = 2352;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1809;
                                            break;
                                        case 2: model = 1810;
                                            break;
                                        case 3: model = 1811;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1808;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1815;
                                            break;
                                        case 3: model = 1816;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1814;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1812;
                                            break;
                                        case 2:
                                        case 3: model = 1813;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2313;
                                            break;
                                        case 2: model = 2331;
                                            break;
                                        case 3: model = 2349;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1789;
                                            break;
                                        case 2: model = 1790;
                                            break;
                                        case 3: model = 1791;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1788;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1795;
                                            break;
                                        case 3: model = 1796;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1794;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1792;
                                            break;
                                        case 2:
                                        case 3: model = 1793;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2319;
                                            break;
                                        case 2: model = 2355;
                                            break;
                                        case 3: model = 2337; 
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2124;
                                            break;
                                        case 2: model = 2126;
                                            break;
                                        case 3: model = 2125;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2123;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2130;
                                            break;
                                        case 2: model = 2131;
                                            break;
                                        case 3:
                                            model = 2130;                                            
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2129;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2127;
                                            break;
                                        case 2:
                                        case 3: model = 2128;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2322;
                                            break;
                                        case 2: model = 2358;
                                            break;
                                        case 3: model = 2340;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    break;
                #endregion stygia
                #region aerus
                case "aerus": 
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2238;
                                            break;
                                        case 2: model = 2239;
                                            break;
                                        case 3: model = 2240;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2237;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 2243;
                                            break;
                                        case 3: model = 2244;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2250;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2241;
                                            break;
                                        case 2:
                                        case 3: model = 2242;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2415;
                                            break;
                                        case 2: model = 2433;
                                            break;
                                        case 3: model = 2451;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2144;
                                            break;
                                        case 2: model = 2145;
                                            break;
                                        case 3: model = 2146;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 2143;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 2150;
                                            break;
                                        case 3: model = 2151;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 2149;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2147;
                                            break;
                                        case 2:
                                        case 3: model = 2148;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2418;
                                            break;
                                        case 2: model = 2436;
                                            break;
                                        case 3: model = 2454;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1798;
                                            break;
                                        case 2: model = 1799;
                                            break;
                                        case 3: model = 1800;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1797;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1804;
                                            break;
                                        case 3: model = 1805;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1803;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1801;
                                            break;
                                        case 2:
                                        case 3: model = 1802;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2424;
                                            break;
                                        case 2: model = 2442;
                                            break;
                                        case 3: model = 2460;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1736;
                                            break;
                                        case 2: model = 1737;
                                            break;
                                        case 3: model = 1738;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1735;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1742;
                                            break;
                                        case 3: model = 1743;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1741;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1739;
                                            break;
                                        case 2:
                                        case 3: model = 1740;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2421;
                                            break;
                                        case 2: model = 2439;
                                            break;
                                        case 3: model = 2457;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1748;
                                            break;
                                        case 2: model = 1749;
                                            break;
                                        case 3: model = 1750;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1747;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1754;
                                            break;
                                        case 3: model = 1755;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1753;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1751;
                                            break;
                                        case 2:
                                        case 3: model = 1752;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2427;
                                            break;
                                        case 2: model = 2445;
                                            break;
                                        case 3: model = 2463;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1685;
                                            break;
                                        case 2: model = 1686;
                                            break;
                                        case 3: model = 1687;
                                            break;
                                    }
                                    break;
                                case Slot.ARMS:
                                    model = 1684;
                                    break;
                                case Slot.LEGS:
                                    switch ((int)player.Realm)
                                    {
                                        case 1:
                                        case 2: model = 1691;
                                            break;
                                        case 3:
                                            model = 1692;
                                            break;
                                    }
                                    break;
                                case Slot.HANDS:
                                    model = 1690;
                                    break;
                                case Slot.FEET:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1688;
                                            break;
                                        case 2:
                                        case 3: model = 1689;
                                            break;
                                    }
                                    break;
                                case Slot.HELM:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 2430;
                                            break;
                                        case 2: model = 2448;
                                            break;
                                        case 3: model = 2466;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    break;
                #endregion aerus
                #region classic
                case "classic":
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 285;
                                    break;
                                case Slot.ARMS:
                                    model = 287;
                                    break;
                                case Slot.LEGS:
                                    model = 286;
                                    break;
                                case Slot.HANDS:
                                    model = 288;
                                    break;
                                case Slot.FEET:
                                    model = 289;
                                    break;
                                case Slot.HELM:
                                    model = 825;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 36;
                                    break;
                                case Slot.ARMS:
                                    model = 38;
                                    break;
                                case Slot.LEGS:
                                    model = 37;
                                    break;
                                case Slot.HANDS:
                                    model = 39;
                                    break;
                                case Slot.FEET:
                                    model = 40;
                                    break;
                                case Slot.HELM:
                                    model = 336;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 230;
                                    break;
                                case Slot.ARMS:
                                    model = 232;
                                    break;
                                case Slot.LEGS:
                                    model = 231;
                                    break;
                                case Slot.HANDS:
                                    model = 233;
                                    break;
                                case Slot.FEET:
                                    model = 234;
                                    break;
                                case Slot.HELM:
                                    model = 830;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 275;
                                    break;
                                case Slot.ARMS:
                                    model = 277;
                                    break;
                                case Slot.LEGS:
                                    model = 276;
                                    break;
                                case Slot.HANDS:
                                    model = 278;
                                    break;
                                case Slot.FEET:
                                    model = 279;
                                    break;
                                case Slot.HELM:
                                    model = 833;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 428;
                                    break;
                                case Slot.ARMS:
                                    model = 430;
                                    break;
                                case Slot.LEGS:
                                    model = 429;
                                    break;
                                case Slot.HANDS:
                                    model = 431;
                                    break;
                                case Slot.FEET:
                                    model = 432;
                                    break;
                                case Slot.HELM:
                                    model = 840;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    model = 206;
                                    break;
                                case Slot.ARMS:
                                    model = 208;
                                    break;
                                case Slot.LEGS:
                                    model = 47;
                                    break;
                                case Slot.HANDS:
                                    model = 89;
                                    break;
                                case Slot.FEET:
                                    model = 215;
                                    break;
                                case Slot.HELM:
                                    model = 64;
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    break;
                #endregion classic
                #region tiara
                case "circlet":
                    switch (item.Item_Type)
                    {
                        case Slot.HELM:
                            switch (item.Object_Type)
                            {
                                case (int)eObjectType.Cloth:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1294;
                                            break;
                                        case 2: model = 1296;
                                            break;
                                        case 3: model = 1298;
                                            break;
                                    }
                                    break;
                                case (int)eObjectType.Leather:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1295;
                                            break;
                                        case 2: model = 1297;
                                            break;
                                        case 3: model = 1299;
                                            break;
                                    }
                                    break;
                                case (int)eObjectType.Reinforced:
                                case (int)eObjectType.Studded:
                                case (int)eObjectType.Scale:
                                case (int)eObjectType.Chain:
                                case (int)eObjectType.Plate:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 1290;
                                            break;
                                        case 2: model = 1291;
                                            break;
                                        case 3: model = 1292;
                                            break;
                                    }
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion tiara
                #region robes
                case "robe 1":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1619;
                                    break;
                                case 2: model = 1621;
                                    break;
                                case 3: model = 1623;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "robe 2":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1006;
                                    break;
                                case 2: model = 2853;
                                    break;
                                case 3: model = 1007;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion robes
                #region cloaks
                case "guard cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    switch ((int)player.Realm)
                                    {
                                        case 1: model = 676;
                                            break;
                                        case 2: model = 677;
                                            break;
                                        case 3: model = 678;
                                            break;
                                        default: break;
                                    }
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "fine cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 443;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "fancy cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 559;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "regal cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 560;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "dressy cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 669;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "feather cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1720;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "magma cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1725;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "scaled cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1722;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "stygian cloak":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1724;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "cloudsong":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1727;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "harpy feather":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1721;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "healers embrace":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1723;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                case "shades of mist":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Magical:
                            switch (item.Item_Type)
                            {
                                case Slot.CLOAK:
                                    model = 1726;
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    break;
                #endregion
                #endregion basic armor: volcanus, oceanus, stygia, aerus, classic, tiara, robes, cloaks
                #region basic weapons
                //one hand
                #region blades
                case "scimitar":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Blades:
                            model = 8;
                            break;
                        default: return false;
                    }
                    break;
                case "duelists dagger earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Blades:
                            model = 1954;
                            break;
                        default: return false;
                    }
                    break;
                #endregion blades
                #region blunt
                case "celtic hammer air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Blunt:
                            model = 1985;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg hammer fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Blunt:
                            model = 1991;
                            break;
                        default: return false;
                    }
                    break;             
                #endregion blunt
                #region celtic spear
                case "celtic war spear":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CelticSpear:
                            model = 477;
                            break;
                        default: return false;
                    }
                    break;
                case "hibernian spear":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CelticSpear:
                            model = 556;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg spear air":
                switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CelticSpear:
                            model = 2005;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg spear fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CelticSpear:
                            model = 2008;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg spear water":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CelticSpear:
                            model = 2007;
                            break;
                        default: return false;
                    }
                    break;
                #endregion celtic spear
                #region crush
                case "coffin mace air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CrushingWeapon:
                            model = 1917;
                            break;
                        default: return false;
                    }
                    break;
                case "bishops mace fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CrushingWeapon:
                            model = 1915;
                            break;
                        default: return false;
                    }
                    break;
                #endregion crush
                #region flexible
                case "flex pick flail":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 863;
                            break;
                        default: return false;
                    }
                    break;
                case "flex spiked whip":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 865;
                            break;
                        default: return false;
                    }
                    break;
                case "flex whip dagger":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 868;
                            break;
                        default: return false;
                    }
                    break;
                case "scorpion flex mace":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 2132;
                            break;
                        default: return false;
                    }
                    break;
                case "flex spiked flail air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 1921;
                            break;
                        default: return false;
                    }
                    break;
                case "flex spiked whip air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Flexible:
                            model = 1925;
                            break;
                        default: return false;
                    }
                    break;
                #endregion flexible                
                #region hand to hand
                case "great bladed claw":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 967;
                            break;
                        default: return false;
                    }
                    break;
                case "great bladed fang":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 968;
                            break;
                        default: return false;
                    }
                    break;
                case "large bladed claw":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 973;
                            break;
                        default: return false;
                    }
                    break;
                case "large bladed greave":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 974;
                            break;
                        default: return false;
                    }
                    break;
                case "great bladed greave air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 2025;
                            break;
                        default: return false;
                    }
                    break;
                case "great bladed greave fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.HandToHand:
                            model = 2023;
                            break;
                        default: return false;
                    }
                    break;
                #endregion hand to hand
                #region large weapons
                case "elf great sword":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LargeWeapons:
                            model = 907;
                            break;
                        default: return false;
                    }
                    break;
                case "shod shilelagh":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LargeWeapons:
                            model = 912;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg hammer air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LargeWeapons:
                            model = 1977;
                            break;
                        default: return false;
                    }
                    break;
                case "troll splitter fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LargeWeapons:
                            model = 1983;
                            break;
                        default: return false;
                    }
                    break;
                #endregion large weapons
                #region fist wraps   TODO******
                /*case "fist wraps":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.FistWraps:
                            model = 1945;
                            break;
                        default: return false;
                    }
                    return false;
                    break;*/
                #endregion
                #region mauler staff   TODO******
                    /*case "mauler staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.MaulerStaff:
                            model = 0;
                            break;
                        default: return false;
                    }
                    return false;
                    break;*/
                #endregion mauler staff
                #region piercing, thrust and 1h blades
                case "briton guarded rapier":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.ThrustWeapon:
                        case eObjectType.Piercing:
                            model = 653;
                            break;
                        default: return false;
                    }
                    break;
                case "parrying rapier":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.ThrustWeapon:
                        case eObjectType.Piercing:
                            model = 888;
                            break;
                        default: return false;
                    }
                    break;
                case "duelists rapier air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.ThrustWeapon:
                        case eObjectType.Piercing:
                            model = 1957;
                            break;
                        default: return false;
                    }
                    break;
                case "duelists rapier fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.ThrustWeapon:
                        case eObjectType.Piercing:
                            model = 1959;
                            break;
                        case eObjectType.Blades:
                            model = 1959;
                            break;
                        default: return false;
                    }
                    break;
                #endregion piercing and thrust
                #region polearm
                case "saracen glaive air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.PolearmWeapon:
                            model = 1933;
                            break;
                        default: return false;
                    }
                    break;
                case "saracen glaive earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.PolearmWeapon:
                            model = 1934;
                            break;
                        default: return false;
                    }
                    break;
                case "saracen glaive fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.PolearmWeapon:
                            model = 1935;
                            break;
                        default: return false;
                    }
                    break;
                #endregion polearm
                #region scythe
                case "magma scythe":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Scythe:
                            model = 2213;
                            break;
                        default: return false;
                    }
                    break;
                case "greatwar scyth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Scythe:
                            model = 928;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg scythe air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Scythe:
                            model = 2001;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg scythe earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Scythe:
                            model = 2002;
                            break;
                        default: return false;
                    }
                    break;
                case "firbolg scythe fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Scythe:
                            model = 2003;
                            break;
                        default: return false;
                    }
                    break;
                #endregion scythe
                #region shields
                case "oceanus shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1: model = 2192;
                                    break;
                                case 2: model = 2193;
                                    break;
                                case 3: model = 2194;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "stygia shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1: model = 2200;
                                    break;
                                case 2: model = 2201;
                                    break;
                                case 3: model = 2202;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "aerus shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1: model = 2210;
                                    break;
                                case 2: model = 2211;
                                    break;
                                case 3: model = 2212;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "volcanus shield":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Shield:
                            switch (item.Type_Damage)
                            {
                                case 1: model = 2218;
                                    break;
                                case 2: model = 2219;
                                    break;
                                case 3: model = 2220;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion shields
                #region spear
                case "dwarven spear":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Spear:
                            model = 1029;
                            break;
                        default: return false;
                    }
                    break;
                case "troll spear":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Spear:
                            model = 1036;
                            break;
                        default: return false;
                    }
                    break;
                case "dwarven spear air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Spear:
                            model = 2045;
                            break;
                        default: return false;
                    }
                    break;
                case "dwarven spear earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Spear:
                            model = 2046;
                            break;
                        default: return false;
                    }
                    break;
                case "dwarven spear fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Spear:
                            model = 2047;
                            break;
                        default: return false;
                    }
                    break;
                #endregion spear
                #region slashing and 1 blades
                case "falchion air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.SlashingWeapon:
                            model = 1945;
                            break;
                        case eObjectType.Blades:
                            model = 1945;
                            break;
                        default: return false;
                    }
                    break;
                case "falchion earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.SlashingWeapon:
                            model = 1946;
                            break;
                        default: return false;
                    }
                    break;
                case "falchion fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.SlashingWeapon:
                            model = 1947;
                            break;
                        default: return false;
                    }
                    break;
                case "sabre axe":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.SlashingWeapon:
                            model = 873;
                            break;
                        default: return false;
                    }
                    break;
                #endregion slashing                
                #region staffs - caster
                #region albion
                case "bishops reach staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 881;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "spiked staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 882;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion albion                
                #region midgard
                case "shod staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 2: model = 565;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "norse wand":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 2: model = 1861;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion midgard
                #region hibernia
                case "celtic staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 468;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "elf staff":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 1185;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion hibernia
                #region all 3 realms
                case "staff air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1965;
                                    break;
                                case 2: model = 2065;
                                    break;
                                case 3: model = 2017;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "staff fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        //case eObjectType.MaulerStaff:
                        case eObjectType.Staff:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1967;
                                    break;
                                case 2: model = 2067;
                                    break;
                                case 3: model = 2019;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                    #endregion all 3 realms
                #endregion staffs - caster
                #region two handed
                case "two handed air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.TwoHandedWeapon:
                        switch ((eWeaponDamageType)item.Type_Damage)
                        {
                            case eWeaponDamageType.Crush: model = 1893;
                                break;
                            case eWeaponDamageType.Slash: model = 1901;
                                break;
                            case eWeaponDamageType.Thrust: model = 1897;
                                break;
                            default: return false;
                        }
                            break;
                        default: return false;
                    }
                    break;
                case "two handed earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.TwoHandedWeapon:
                        switch ((eWeaponDamageType)item.Type_Damage)
                        {
                            case eWeaponDamageType.Crush: model = 1894;
                                break;
                            case eWeaponDamageType.Slash: model = 1902;
                                break;
                            case eWeaponDamageType.Thrust: model = 1898;
                                break;
                            default: return false;
                        }
                            break;
                        default: return false;
                    }
                    break;
                case "two handed fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.TwoHandedWeapon:
                        switch ((eWeaponDamageType)item.Type_Damage)
                        {
                            case eWeaponDamageType.Crush: model = 1895;
                                break;
                            case eWeaponDamageType.Slash: model = 1903;
                                break;
                            case eWeaponDamageType.Thrust: model = 1899;
                                break;
                            default: return false;
                        }
                            break;
                        default: return false;
                    }
                    break;
                #endregion two handed                
                //1H and 2H
                #region axes, left axes, and 2h axes
                case "norse spiked axe":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LeftAxe:
                        case eObjectType.Axe:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 315;
                                    break;
                                case Slot.TWOHAND:
                                    model = 577;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "dwarven axe":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LeftAxe:
                        case eObjectType.Axe:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 1011;
                                    break;
                                case Slot.TWOHAND:
                                    model = 1027;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "troll axe":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LeftAxe:
                        case eObjectType.Axe:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 1025;
                                    break;
                                case Slot.TWOHAND:
                                    model = 955;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "dwarven axe fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.LeftAxe:
                        case eObjectType.Axe:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2031;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2051;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion axes, left axes, and 2h axes
                #region hammers and 2h hammers
                case "norse great hammer":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Hammer:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 324;
                                    break;
                                case Slot.TWOHAND:
                                    model = 576;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "troll hammer":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Hammer:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 950;
                                    break;
                                case Slot.TWOHAND:
                                    model = 956;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "hammer air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Hammer:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2041;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2053;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "hammer fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Hammer:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2043;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2055;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion hammers and 2h hammers                
                #region swords and 2h swords
                case "sword air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Sword:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2033;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2057;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "sword fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Sword:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2035;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2059;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "aerus sword":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Sword:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 2203;
                                    break;
                                case Slot.TWOHAND:
                                    model = 2204;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "norse dwarven sword":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Sword:
                            switch (item.Item_Type)
                            {
                                case Slot.RIGHTHAND:
                                case Slot.LEFTHAND:
                                    model = 655;
                                    break;
                                case Slot.TWOHAND:
                                    model = 658;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion sword
                #region bows
                #region albion bows
                case "brawlers bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                            model = 848;
                            break;
                        default: return false;
                    }
                    break;
                case "saracen long bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                            model = 849;
                            break;
                        default: return false;
                    }
                    break;
                case "skinners bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                            model = 850;
                            break;
                        default: return false;
                    }
                    break;
                case "thorn bow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                            model = 1909;
                            break;
                        default: return false;
                    }
                    break;
                case "thorn bow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Longbow:
                            model = 1911;
                            break;
                        default: return false;
                    }
                    break;
                #endregion albion bows
                #region midgard bows
                case "dwarven crushing bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CompositeBow:
                            model = 1037;
                            break;
                        default: return false;
                    }
                    break;
                case "kobold fang bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CompositeBow:
                            model = 1038;
                            break;
                        default: return false;
                    }
                    break;
                case "viking bearded bow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CompositeBow:
                            model = 2061;
                            break;
                        default: return false;
                    }
                    break;
                case "viking bearded bow earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CompositeBow:
                            model = 2062;
                            break;
                        default: return false;
                    }
                    break;
                case "viking bearded bow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.CompositeBow:
                            model = 2063;
                            break;
                        default: return false;
                    }
                    break;
                #endregion midgard bows
                #region hibernia bows
                case "recurve long bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.RecurvedBow:
                            model = 918;
                            break;
                        default: return false;
                    }
                    break;
                case "celtic great bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.RecurvedBow:
                            model = 919;
                            break;
                        default: return false;
                    }
                    break;
                case "lurikeen thorn bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.RecurvedBow:
                            model = 923;
                            break;
                        default: return false;
                    }
                    break;
                case "Elven long bow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.RecurvedBow:
                            model = 1993;
                            break;
                        default: return false;
                    }
                    break;
                case "Elven long bow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.RecurvedBow:
                            model = 1995;
                            break;
                        default: return false;
                    }
                    break;
                #endregion hibernia bows
                #region crossbows
                case "assault crossbow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 890;
                            break;
                        default: return false;
                    }
                    break;
                case "brawlers crossbow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 891;
                            break;
                        default: return false;
                    }
                    break;
                case "crossbow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 892;
                            break;
                        default: return false;
                    }
                    break;
                case "skinners crossbow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 893;
                            break;
                        default: return false;
                    }
                    break;
                case "steel crossbow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 894;
                            break;
                        default: return false;
                    }
                    break;
                case "skinners crossbow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 1961;
                            break;
                        default: return false;
                    }
                    break;
                case "skinners crossbow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Crossbow:
                            model = 1963;
                            break;
                        default: return false;
                    }
                    break;
                #endregion crossbows
                #region short bows
                #region albion short bows
                case "saracen short bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 850;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "saracen short bow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1905;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "saracen short bow earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1906;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "saracen short bow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1907;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "saracen short bow water":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 1: model = 1908;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion albion short bows
                #region hibernia short bows
                case "elven short bow":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 922;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "elven short bow air":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 1997;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "elven short bow earth":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 1998;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "elven short bow fire":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 1999;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                case "elven short bow water":
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Fired:
                            switch ((int)player.Realm)
                            {
                                case 3: model = 2000;
                                    break;
                                default: return false;
                            }
                            break;
                        default: return false;
                    }
                    break;
                #endregion hibernia short bows
                #endregion shortbows
                #endregion bows
                #endregion basic weapons
                default: return false;
			}            
            SetModel(player, model);
			return true;
		}
		
		public override bool ReceiveItem(GameLiving source, InventoryItem item)
		{
			GamePlayer player = source as GamePlayer;
			if(player == null || item == null) return false;
            bool output = false;
            #region messages
            if (WorldMgr.GetDistance(this,player) > WorldMgr.INTERACT_DISTANCE)
			{
				player.Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return false;
			}            
            if (player.TempProperties.getObjectProperty(TempProperty, null) != null)
            {
                WeakReference itemWeak = (WeakReference)player.TempProperties.getObjectProperty(TempProperty, new WeakRef(null));           
                item = (InventoryItem)itemWeak.Target;
                SendReply(player, "You already gave me an item! What was it again?");
                output = true;
            }
            
            string message = GenMessage() + "\n\n[Reset]\n";
            if (output || !chortle) message = "\n\n[Reset]\n";
            string items = "";
            switch ((eObjectType)item.Object_Type)
            {
                #region weapon types
                case eObjectType.Axe:
                    SendReply(player, "An Axe? " + message +
                        "[Crocodile Tooth Dagger]\n[Malice Axe]\n[Traitors Dagger]\n[Norse Spiked Axe]\n" + 
                        "[Dwarven Axe]\n[Troll Axe]\n[Dwarven Axe Fire]");
                    break;
                case eObjectType.Blades:
                    SendReply(player, "A Blades weapon? " + message +
                        "[Battler]\n[Crocodile Tooth Dagger]\n[Malice Axe]\n[Traitors Dagger]\n" +
                        "[Duelists Rapier Fire]\n[Duelists Dagger Earth]\b[Falchion Air]");
                    break;
                case eObjectType.Blunt:
                    SendReply(player, "A Blunt weapon? " + message +
                        "[Battler]\n[Bruiser]\n[Crocodile Tooth Dagger]\n[Malice Axe]\n[Scepter Meritorious]\n" +
                        "[Celtic Hammer Air]\n[Firbolg Hammer Fire]");
                    break;
                case eObjectType.CelticSpear:
                    SendReply(player, "A Celtic Spear.. " + message +
                        "[Golden Spear]\n[Spear of Kings]\n[Celtic War Spear]\n[Hibernian Spear]\n" +
                        "[Firbolg Spear Air]\n[Firbolg Spear Fire]\n[Firbolg Spear Water]");
                    break;
                case eObjectType.CrushingWeapon:
                    SendReply(player, "A Crush weapon? " + message +
                        "[Battler]\n[Bruiser]\n[Crocodile Tooth Dagger]\n[Malice Axe]\n[Scepter Meritorious]\n" +
                        "[Coffin Mace Air]\n[Bishops Mace Fire]");
                    break;
                case eObjectType.Flexible:
                    SendReply(player, "A Flex weapon? " + message +
                        "[Snake Charmers]\n[Flex Pick Flail]\n[Flex Spiked Whip]\n[Flex Whip Dagger]\n[Scorpion Flex Mace]\n" +
                        "[Flex Spiked Flail Air]\n[Flex Spiked Whip Air]");
                    break;
                case eObjectType.Hammer:
                    SendReply(player, "A Hammer? " + message +
                        "[Battler]\n[Bruiser]\n[Scepter Meritorious]\n[Norse Great Hammer]\n[Troll Hammer]" +
                        "\n[Hammer Air]\n[Hammer Fire]");
                    break;
                case eObjectType.HandToHand:
                    SendReply(player, "A Hand to Hand weapon.. " + message +
                        "[Snake Charmers]\n[Great Bladed Claw]\n[Great Bladed Fang]\n[Large Bladed Claw]\n" +
                        "[Large Bladed Greave]\n[Great Bladed Greave Air]\n[Great Bladed Greave Fire]");
                    break;
                case eObjectType.LargeWeapons:
                    SendReply(player, "A Large weapon indeed! " + message +
                        "[Battler]\n[Bruiser]\n[Malice Axe]\n[Elf Great Sword]\n[Shod Shilelagh]\n" +
                        "[Firbolg Hammer Air]\n[Troll Splitter Fire]");
                    break;
                case eObjectType.LeftAxe:
                    SendReply(player, "Another Left Axe? " + message +
                        "[Crocodile Tooth Dagger]\n[Malice Axe]\n[Traitors Dagger]\n[Norse Spiked Axe]\n" +
                        "[Dwarven Axe]\n[Troll Axe]\n[Dwarven Axe Fire]");
                    break;
                /*case eObjectType.FistWraps: //TODO
                    SendReply(player, "Fist Wraps! " + message +
                        "");
                    break;
                  case eObjectType.MaulerStaff:
                    string mstaffs = "";
                    switch ((int)player.Realm)
                    {
                        case 1: mstaffs = "\n";
                            break;
                        case 2: mstaffs = "\n";
                            break;
                        case 3: mstaffs = "\n";
                            break;
                    }
                    SendReply(player, "A Mauler Staff! " + message +
                        "[Staff of God]\n[Tartaros Gift]\n[Traldors Oracle]" + mstaffs);
                    break;*/
                case eObjectType.Piercing:
                    SendReply(player, "A Piercer!? " + message +
                        "[Crocodile Tooth Dagger]\n[Golden Spear]\n[Traitors Dagger][Briton Guarded Rapier]\n" + 
                        "[Parrying Rapier]\n[Duelists Rapier Air]\n[Duelists Rapier Fire]");
                    break;
                case eObjectType.ThrustWeapon:
                    SendReply(player, "A Thrust weapon? " + message +
                        "[Crocodile Tooth Dagger]\n[Golden Spear]\n[Traitors Dagger][Briton Guarded Rapier]\n" +
                        "[Parrying Rapier]\n[Duelists Rapier Air]\n[Duelists Rapier Fire]");
                    break;
                case eObjectType.PolearmWeapon:
                    SendReply(player, "A Polearm? " + message +
                        "[Battler]\n[Bruiser]\n[Malice]\n[Spear of Kings]\n[Saracen Glaive Air]\n" + 
                        "[Saracen Glaive Earth]\n[Saracen Glaive Fire]");
                    break;
                case eObjectType.Scythe:
                    SendReply(player, "A Scythe? " + message +
                        "[Snake Charmers]\n[Spear of Kings]\n[Magma Scythe]\n[Greatwar Scyth]\n" + 
                        "[Firbolg Scythe Air]\n[Firbolg Scythe Earth]\n[Firbolg Scythe Fire]");
                    break;
                case eObjectType.Spear:
                    SendReply(player, "A Spear? " + message +
                        "[Golden Spear]\n[Spear of Kings]\n[Dwarven Spear]\n[Troll Spear]\n" + 
                        "[Dwarven Spear Air]\n[Dwarven Spear Earth]\n[Dwarven Spear Fire]");
                    break;
                case eObjectType.SlashingWeapon:
                    SendReply(player, "A Slash weapon? " + message +
                        "[Crocodile Tooth Dagger]\n[Golden Spear]\n[Malice Axe]\n" +
                        "[Sabre Axe]\n[Falchion Air]\n[Falchion Earth]\n[Falchion Fire]");
                    break;
                case eObjectType.Staff:
                    string staffs = "";
                    switch ((int)player.Realm)
                    {
                        case 1: staffs = "\n[Bishops Reach Staff]\n[Spiked Staff]\n[Staff Air]\n[Staff Fire]";
                            break;
                        case 2: staffs = "\n[Shod Staff]\n[Norse Wand]\n[Staff Air]\n[Staff Fire]";
                            break;
                        case 3: staffs = "\n[Celtic staff]\n[Elf Staff]\n[Staff Air]\n[Staff Fire]";
                            break;
                    }
                    SendReply(player, "A Staff? " + message +
                        "[Staff of God]\n[Tartaros Gift]\n[Traldors Oracle]" + staffs);
                    break;
                case eObjectType.Sword:
                    SendReply(player, "A Sword? " + message +
                        "[Battler\n[Malice Axe]\n[Traitors Dagger]\n" +
                        "[Aerus Sword]\n[Norse Dwarven Sword]\n[Sword Air]\n[Sword Fire]");
                    break;
                case eObjectType.TwoHandedWeapon:
                    SendReply(player, "A Two Hander? " + message +
                        "[Battler]\n[Bruiser]\n[Malice Axe]\n[Spear of Kings]\n" +
                        "[Two Handed Air]\n[Two Handed Earth]\n[Two Handed Fire]");
                    break;
                //Shields
                case eObjectType.Shield:
                    SendReply(player, "A Shield? " + message +
                        "[Atens Shield]\n[Cyclops Shield]\n[Khaos Shield]\n[Oceanus Shield]\n" + 
                        "[Stygia Shield]\n[Aerus Sheild]\n[Volcanus Shield]");
                    break;
                //Bows
                case eObjectType.CompositeBow:
                    SendReply(player, "A Composite Bow? " + message +
                        "[Braggarts Bow]\n[Fools Bow]\n[Dwarven Crushing Bow]\n[Kobold Fang Bow]\n[Viking Beared Bow Air]\n" + 
                        "[Viking Beared Bow Earth]\n[Viking Beared Bow Fire]");
                    break;
                case eObjectType.Longbow:
                    SendReply(player, "Longbow? " + message +
                        "[Braggarts Bow]\n[Fools Bow]\n[Brawlers Bow]\n[Saracen Long Bow]\n[Skinners Bow]\n" + 
                        "[Thorn Bow Air]\n[Thorn Bow Fire]");
                    break;
                case eObjectType.RecurvedBow:
                    SendReply(player, "Another Recurve Bow? " + message +
                        "[Braggarts Bow]\n[Fools Bow]\n[Celtic Great Bow]\n[Lurikeen Thorn Bow]\n[Recurve Long Bow]\n" + 
                        "[Elven Long Bow Air]\n[Elven Long Bow Fire]");
                    break;
                case eObjectType.Fired:
                    string shortout = "";
                    switch ((int)player.Realm)
                    {
                        case 1: shortout = "\n\n[Saracen Short Bow]\n[Saracen Short Bow Air]\n[Saracen Short Bow Earth]\n" + 
                            "[Saracen Short Bow Fire]\n[Saracen Short Bow Water]";
                            break;
                        case 3: shortout = "\n\n[Elven Short Bow]\n[Elven Short Bow Air]\n[Elven Short Bow Earth]\n" + 
                            "[Elven Short Bow Fire]\n[Elven Short Bow Water]";
                            break;
                        default: return false;
                    }
                    SendReply(player, "Another Short Bow? " + message + shortout);
                    break;
                case eObjectType.Crossbow:
                    SendReply(player, "Cross Bow eh? " + message +
                        "[Assault Crossbow]\n[Crossbow]\n[Skinners Crossbow]\n[Steel Crossbow]\n[Skinners Crossbow Air]\n" + 
                        "[Skinners Crossbow Fire]");                    
                    break;
                #endregion weapons
                #region cloaks
                case eObjectType.Magical:
                    switch (item.Item_Type)
                    {
                        case Slot.CLOAK: 
                            SendReply(player, "A Cloak. " + message + items + "[Dressy Cloak]\n[Fine Cloak]\n[Fancy Cloak]\n" +
                                "[Cloudsong]\n[Harpy Feather]\n[Healers Embrace]\n[Shades of Mist]\n" +
                                "[Feather Cloak]\n[Guard Cloak]\n[Magma Cloak]\n[Regal Cloak]\n[Scaled Cloak]\n[Stygian Cloak]\n");
                            break;
                        default:
                            {
                                SendReply(player, "I dont recognize that item.");
                                return false;
                            }
                    }
                    break;
                #endregion cloaks
                #region armor types
                case eObjectType.Cloth:
                case eObjectType.Leather:
                case eObjectType.Studded:
                case eObjectType.Chain:
                case eObjectType.Plate:
                case eObjectType.Scale:
                case eObjectType.GenericArmor:                    
                    switch ((eObjectType)item.Object_Type)
                    {
                        #region cloth
                        case eObjectType.Cloth:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Guard of Valor]\n[Naliahs Robe]";
                                    break;
                                case Slot.ARMS:
                                    items = "[Arms of the Winds]\n[Foppish Sleeves]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]";
                                    break;
                                case Slot.FEET:
                                    items = "[Flamedancers Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Circlet]";
                                    break;
                            }
                            break;
                        #endregion Cloth
                        #region leather
                        case eObjectType.Leather:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Golden Scarab Vest]\n[Guard of Valor]\n[Naliahs Robe]";
                                    break;
                                case Slot.ARMS:
                                    items = "[Arms of the Winds]\n[Foppish Sleeves]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]";
                                    break;
                                case Slot.FEET:
                                    items = "[Flamedancers Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Circlet]";
                                    break;
                            }
                            break;
                        #endregion Leather
                        #region studded and reinforced
                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Golden Scarab Vest]\n[Guard of Valor]";
                                    break;
                                case Slot.ARMS:
                                    items = "[Arms of the Winds]\n[Foppish Sleeves]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]\n[Wings Dive]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]\n[Mariashas Sharkskin Gloves]";
                                    break;
                                case Slot.FEET:
                                    items = "[Enyalios Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Circlet]";
                                    break;
                            }
                            break;
                        #endregion Studded and Reinforced
                        #region chain
                        case eObjectType.Chain:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Eirenes Hauberk]\n[Guard of Valor]";
                                    break;
                                case Slot.ARMS:
                                    items = "[Arms of the Winds]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]\n[Wings Dive]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]";
                                    break;
                                case Slot.FEET:
                                    items = "[Enyalios Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Winged Helm]\n[Circlet]";
                                    break;
                            }
                            break;
                        #endregion Chain
                        #region scale
                        case eObjectType.Scale:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Eirenes Hauberk]\n[Guard of Valor]";
                                    break;
                                case Slot.ARMS:
                                    items = "[Arms of the Winds]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]\n[Wings Dive]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]";
                                    break;
                                case Slot.FEET:
                                    items = "[Enyalios Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Winged Helm]\n[Circlet]";
                                    break;
                            }
                            break;
                        #endregion Scale
                        #region plate
                        case eObjectType.Plate:
                            switch (item.Item_Type)
                            {
                                case Slot.TORSO:
                                    items = "[Eirenes Hauberk]\n[Guard of Valor]";
                                    break;
                                case Slot.ARMS: 
                                    items = "[Arms of the Winds]";
                                    break;
                                case Slot.LEGS:
                                    items = "[Alvarus Leggings]\n[Wings Dive]";
                                    break;
                                case Slot.HANDS:
                                    items = "[Maddening Scalars]";
                                    break;
                                case Slot.FEET:
                                    items = "[Enyalios Boots]";
                                    break;
                                case Slot.HELM:
                                    items = "[Crown of Zahur]\n[Winged Helm]\n[Circlet]";
                                    break;
                                default: return false;
                            }
                            break;
                        #endregion Plate
                        default: return false;
                    }
                    string itemtype = "";
                    switch (item.Item_Type)
                    {
                        case Slot.TORSO:
                            itemtype = "Torso? ";
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Cloth:
                                    items += "\n[Robe 1]\n[Robe 2]";
                                    break;
                            }
                            break;
                        case Slot.ARMS:
                            itemtype = "Arms? ";
                            break;
                        case Slot.LEGS:
                            itemtype = "Leggings? ";
                            break;
                        case Slot.HANDS:
                            itemtype = "Gloves? ";
                            break;
                        case Slot.FEET:
                            itemtype = "Boots? ";
                            break;
                        case Slot.HELM:
                            itemtype = "A Helm? ";
                            break;
                    }
                    SendReply(player, itemtype + message + "[Volcanus]\n[Oceanus]\n[Stygia]\n[Aerus]\n[Classic]\n" + items);
                    break;
                #endregion armor
                default:
                    {
                        SendReply(player, "I dont recognize that item.");
                        return false;
                    }
            }
            player.TempProperties.setProperty(TempProperty, new WeakReference(item));
			return true;
            #endregion messages
        }
		
		public void SendReply(GamePlayer player, string msg)
		{
			player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
		}

        public string GenMessage()
        {
            #region messages
            string temp = "";
            switch (Util.Random(22))
            {
                case 1: temp = "I can rework it so it looks like it came from";
                    break;
                case 2: temp = "A silly Elf came in here with one of those acting like he could use it.. go on, pick one";
                    break;
                case 3: temp = "I used to hunt trolls with one just like that. What'll it be?";
                    break;
                case 4: temp = "I dont know if its salvagable, give me an idea what you want it to look like";
                    break;
                case 5: temp = "I hope you have someone to protect you if you are going to use that";
                    break;
                case 6: temp = "My specialty. I can disquise it to look like it comes from";
                    break;
                case 7: temp = "You should be looking to train in a new profession from the likes of you";
                    break;
                case 8: temp = "Who are you trying to kid? Fine, where do you want it from?";
                    break;
                case 9: temp = "You can have the special discount. Quick pick one";
                    break;
                case 10: temp = "Alright, back to my work";
                    break;
                case 11: temp = "Be wary of assassins my friend";
                    break;
                case 12: temp = "Well hurry up, I dont have all day!";
                    break;
                case 13: temp = "Right. I can rework it so it looks like it came from";
                    break;
                case 14: temp = "Who are you trying to kid?";
                    break;
                case 15: temp = "Havn't seen one of those in a while. What do you want it to look like?";
                    break;
                case 16: temp = "I can rework that to look like";
                    break;
                case 17: temp = "Glad I am not on the other end of that thing. Alright, what will it be?";
                    break;
                case 18: temp = "One of my favorites! I can make it look like";
                    break;
                case 19: temp = "What'll it be?";
                    break;
                case 20: temp = "I hope you know how to use that. Pick your poison";
                    break;
                case 21: temp = "You got mighty purty lips"; //Complements of 
                    break;
                case 22: temp = "I dont have all day!";
                    break;
            }
            return temp;
            #endregion messages
        }

        public void SetModel(GamePlayer player, int number)
        {
            WeakReference itemWeak = (WeakReference)player.TempProperties.getObjectProperty(TempProperty, new WeakRef(null));
            player.TempProperties.removeProperty(TempProperty);
            InventoryItem item = (InventoryItem)itemWeak.Target;

            if (item == null || item.OwnerID != player.InternalID || item.OwnerID == null)
                return;
            if (Price > 0)
            {
                if (player.GetCurrentMoney() < Price)
                {
                    SendReply(player, "Please come back when you have " + Money.GetString(Price) + ".");
                    return;
                }
                else
                {
                    player.RemoveMoney(Price);
                    player.Out.SendMessage("You give " + Money.GetString(Price) + " to " + this.Name + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                    SendReply(player, "I thank you for your business.");
                }
            }
            else SendReply(player, "Enjoy the new look!");

            item.Model = number;
            item.CrafterName = this.Name;
            player.Out.SendInventoryItemsUpdate(new InventoryItem[] { item });
        }
	}
}
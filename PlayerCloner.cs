// Kakuri, April 2009

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;
using DOL.GS;
using DOL.GS.PacketHandler;
using DOL.Database;
using DOL.AI.Brain;
using DOL.GS.Commands;

namespace DOL.GS.Scripts
{
    public class PlayerCloner
    {
        public const string CLONE_CLASS_NAME = "PlayerClonerCloneClassName";

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const double MAX_BLOCKCHANCE = 35;
        private const double MAX_PARRYCHANCE = 33;
        private const double MAX_EVADECHANCE = 32;
        private const byte LEFTSWINGCHANCE = 75;

        private static short[] m_augStatBonus = { 0, 4, 12, 22, 34, 48 };
        private static short[] m_masteryDefenseBonus = { 0, 2, 5, 10, 16, 23 };

        private static readonly List<eInventorySlot> itemsToCopy = new List<eInventorySlot>(10);

        static PlayerCloner()
        {
            itemsToCopy.Add(eInventorySlot.RightHandWeapon);
            itemsToCopy.Add(eInventorySlot.LeftHandWeapon);
            itemsToCopy.Add(eInventorySlot.TwoHandWeapon);
            itemsToCopy.Add(eInventorySlot.HeadArmor);
            itemsToCopy.Add(eInventorySlot.HandsArmor);
            itemsToCopy.Add(eInventorySlot.FeetArmor);
            itemsToCopy.Add(eInventorySlot.TorsoArmor);
            itemsToCopy.Add(eInventorySlot.Cloak);
            itemsToCopy.Add(eInventorySlot.LegsArmor);
            itemsToCopy.Add(eInventorySlot.ArmsArmor);
        }

        /// <summary>
        /// Clones a player's appearance and melee abilities to a GameNPC
        /// </summary>
        /// <param name="playerName">Name of the character - case-sensitive</param>
        /// <returns>A clone of the player (null if the player is not found)</returns>
        public static GameNPC ClonePlayer(string playerName)
        {
            return ClonePlayer(playerName, false);
        }


        public static GameNPC ClonePlayer(string playerName, bool loadStyles)
        {
            GameNPC clone = new GameNPC();

            if (ClonePlayer(playerName, clone, loadStyles, 0))
            {
                return clone;
            }
            else
            {
                return null;
            }
        }


        public static bool ClonePlayer(string playerName, GameNPC clone)
        {
            return ClonePlayer(playerName, clone, false, 0);
        }


        /// <summary>
        /// Clones a player's appearance and melee abilities to a GameNPC
        /// </summary>
        /// <param name="playerName">Name of the character - case-sensitive</param>
        /// <param name="clone">The target GameNPC to clone the player to</param>
        /// <param name="loadStyles">True to load styles</param>
        /// <param name="cloneLevel">Level to set the clone to (affects stat bonuses); 0 to copy player's level</param>
        /// <returns>True if the player was found and cloned, false if not clone supplied or character not found</returns>
        // Players cloned by name, from the database, tend to be a bit tougher than players cloned from a GamePlayer
        public static bool ClonePlayer(string playerName, GameNPC clone, bool loadStyles, byte cloneLevel)
        {
            if (clone == null)
                return false;

            DOLCharacters dbchar = GameServer.Database.SelectObject<DOLCharacters>("Name = '" + GameServer.Database.Escape(playerName) + "'");

            if (dbchar == null)
                return false;

            //clone.Model = GetPlayerModel( (eRace)dbchar.Race, dbchar.Gender );
            SetNPCModel(clone, (ushort)dbchar.CurrentModel);
            clone.Level = cloneLevel > 0 ? cloneLevel : (byte)dbchar.Level;
            clone.Name = dbchar.Name + (dbchar.LastName.Length > 0 ? " " + dbchar.LastName : "");
            clone.TempProperties.setProperty(CLONE_CLASS_NAME, ((eCharacterClass)dbchar.Class).ToString());

            if (dbchar.GuildID.Length > 0)
            {
                Guild playerGuild = GuildMgr.GetGuildByGuildID(dbchar.GuildID);
                //DBGuild guild = GameServer.Database.SelectObject( typeof( DBGuild ), "GuildID = '" + dbchar.GuildID + "'" ) as DBGuild;

                if (playerGuild != null)
                {
                    clone.GuildName = playerGuild.Name;
                    //clone.GuildName = guild.GuildName;
                }
            }

            clone.IsCloakHoodUp = dbchar.IsCloakHoodUp;
            clone.IsCloakInvisible = dbchar.IsCloakInvisible;
            clone.IsHelmInvisible = dbchar.IsHelmInvisible;

            short baseBuffs = (short)(clone.Level * 1.25);
            short specBuffs = (short)(clone.Level * 1.25 * 1.5);
            short itemBonus = (short)((clone.Level * 1.5) + (clone.Level / 2.0 + 1.0));

            clone.Strength = (short)(dbchar.Strength + baseBuffs + specBuffs + itemBonus);
            clone.Constitution = (short)(dbchar.Constitution + baseBuffs + specBuffs + itemBonus);
            clone.Dexterity = (short)(dbchar.Dexterity + baseBuffs + specBuffs + itemBonus);
            // QUI affects mobs differently than players and goes nuts around 200+
            clone.Quickness = (short)(165);
            clone.MeleeDamageType = eDamageType.Slash;

            clone.MaxSpeedBase = ((short)dbchar.MaxSpeed);

            // parryLevel is capped at 51 and at 51 accounts for 77% of parry chance (remaining 23% is from MoParry)
            byte parryLevel = GetCharacterSpecLevel(dbchar, Specs.Parry);
            // any parry beyond 51 is applied to parryBonus; at 23 parryBonus gives a 5% bonus to ParryChance
            byte parryBonus = 0;
            // same as for parry
            byte shieldLevel = GetCharacterSpecLevel(dbchar, Specs.Shields);
            byte shieldBonus = 0;
            byte evadeLevel = GetCharacterAbilityLevel(dbchar, Abilities.Evade);
            byte moparryLevel = GetCharacterAbilityLevel(dbchar, "Mastery of Parrying");
            byte moblockLevel = GetCharacterAbilityLevel(dbchar, "Mastery of Blocking");
            byte mopainLevel = GetCharacterAbilityLevel(dbchar, "Mastery of Pain");
            byte augStrLevel = GetCharacterAbilityLevel(dbchar, "Augmented Strength");
            byte augConLevel = GetCharacterAbilityLevel(dbchar, "Augmented Constitution");
            byte augDexLevel = GetCharacterAbilityLevel(dbchar, "Augmented Dexterity");
            byte toughnessLevel = GetCharacterAbilityLevel(dbchar, "Toughness");
            byte pDefenseLevel = GetCharacterAbilityLevel(dbchar, "Physical Defense");

            if (parryLevel > 0)
            {
                // if we have artificially increased the clone's level, apply the same increase to the parry spec level
                if (clone.Level > dbchar.Level)
                    parryLevel = (byte)(parryLevel * ((double)cloneLevel / (double)dbchar.Level));

                // item bonus
                parryLevel += (byte)(clone.Level / 5);
                // RR bonus
                parryLevel += (byte)((dbchar.RealmLevel + 10) / 10);
            }

            if (parryLevel > (clone.Level + 1))
            {
                parryBonus = (byte)(parryLevel - clone.Level - 1);
                parryLevel = (byte)(clone.Level + 1);
            }

            // now we normalize parry as if the character is level 50 and has a corresponding increase in spec level
            // hmmmm... only makes sense if the clone is lvl 50, yes?
            //parryLevel = (byte)( parryLevel * ( 51.0 / ( dbchar.Level + 1 ) ) );

            if (shieldLevel > 0)
            {
                // if we have artificially increased the clone's level, apply the same increase to the parry spec level
                if (clone.Level > dbchar.Level)
                    shieldLevel = (byte)(shieldLevel * ((double)cloneLevel / (double)dbchar.Level));

                // item bonus
                shieldLevel += (byte)(dbchar.Level / 5);
                // RR bonus
                shieldLevel += (byte)((dbchar.RealmLevel + 10) / 10);
            }

            if (shieldLevel > (dbchar.Level + 1))
            {
                shieldBonus = (byte)(shieldLevel - dbchar.Level - 1);
                shieldLevel = (byte)(dbchar.Level + 1);
            }

            // now we normalize shield as if the character is level 50 and has a corresponding increase in spec level
            //shieldLevel = (byte)( shieldLevel * ( 51.0 / ( dbchar.Level + 1 ) ) );

            clone.ParryChance = (byte)(MAX_PARRYCHANCE * ((parryLevel * 0.0151) + (m_masteryDefenseBonus[moparryLevel] * 0.01)));

            if (parryBonus > 0)
                clone.ParryChance += (byte)(clone.ParryChance * (parryBonus / 23.0) * 0.05);

            clone.BlockChance = (byte)(MAX_BLOCKCHANCE * ((shieldLevel * 0.0151) + (m_masteryDefenseBonus[moblockLevel] * 0.01)));

            if (shieldBonus > 0)
                clone.BlockChance += (byte)(clone.BlockChance * (shieldBonus / 23.0) * 0.05);

            clone.EvadeChance = (byte)(MAX_EVADECHANCE * evadeLevel / 7.0);

            clone.Strength += (short)(m_augStatBonus[augStrLevel]);
            clone.Strength += (short)(mopainLevel * 10.0);
            clone.Strength += (short)((int)(dbchar.RealmLevel / 10.0) * 20.0);
            clone.Constitution += (short)(m_augStatBonus[augConLevel]);
            clone.Constitution += (short)(toughnessLevel * 4);
            clone.Constitution += (short)(pDefenseLevel * 5);
            clone.Constitution += (short)(dbchar.ChampionLevel * 5);
            clone.Dexterity += (short)(m_augStatBonus[augDexLevel]);

            #region Inventory
            GameNpcInventoryTemplate npcInventory = new GameNpcInventoryTemplate();
            IList<InventoryItem> charItems = GameServer.Database.SelectObjects<InventoryItem>("OwnerID = '" + dbchar.ObjectId + "'");

            foreach (InventoryItem item in charItems)
            {
                if (itemsToCopy.Contains((eInventorySlot)item.SlotPosition))
                {
                    if ((item.SlotPosition == (int)eInventorySlot.RightHandWeapon && dbchar.ActiveWeaponSlot == 0) ||
                         (item.SlotPosition == (int)eInventorySlot.TwoHandWeapon && dbchar.ActiveWeaponSlot == 1))
                    {
                        clone.MeleeDamageType = (eDamageType)item.Type_Damage;
                    }
                    else if (item.SlotPosition == (int)eInventorySlot.LeftHandWeapon)
                    {
                        if (item.Object_Type != (int)eObjectType.Shield)
                        {
                            clone.LeftHandSwingChance = LEFTSWINGCHANCE;
                        }
                    }

                    int itemModel = item.Model;

                    // keep clothing manager uses model IDs of 3800-3802 for guild cloaks... doesn't seem to work here :(
                    //if ( item.SlotPosition == (int)eInventorySlot.Cloak && item.Emblem > 0 )
                    //	itemModel = 3799 + dbchar.Realm;

                    npcInventory.AddNPCEquipment((eInventorySlot)item.SlotPosition, item.Model, item.Color, item.Effect, item.Extension);
                }
            }

            npcInventory.CloseTemplate();
            clone.Inventory = npcInventory;

            switch (dbchar.ActiveWeaponSlot & 0x0F)
            {
                case (int)GameLiving.eActiveWeaponSlot.TwoHanded: clone.SwitchWeapon(GameLiving.eActiveWeaponSlot.TwoHanded); break;
                case (int)GameLiving.eActiveWeaponSlot.Distance: clone.SwitchWeapon(GameLiving.eActiveWeaponSlot.Distance); break;
                default: clone.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard); break;
            }

            #endregion Inventory

            if (loadStyles)
            {
                clone.Styles = GetCharacterStyles(dbchar);
            }

            clone.SetOwnBrain(new MeleeStyler());
            //josh
            return true;
        }

        /// <summary>
        /// Clones a player's appearance to a GameNPC
        /// </summary>
        /// <param name="player">The player to clone</param>
        /// <returns>A clone of the player (null if the player is null)</returns>
        public static GameNPC ClonePlayer(GamePlayer player)
        {
            return ClonePlayer(player, false);
        }


        /// <summary>
        /// Clones a player's appearance and melee abilities to a GameNPC
        /// </summary>
        /// <param name="player">The player to clone</param>
        /// <param name="loadStyles">True to load styles</param>
        /// <returns>A clone of the player (null if the player is null)</returns>
        public static GameNPC ClonePlayer(GamePlayer player, bool loadStyles)
        {
            GameNPC clone = new GameNPC();
            ClonePlayer(player, clone, loadStyles);
            return clone;
        }


        /// <summary>
        /// Clones a player's appearance to a GameNPC
        /// </summary>
        /// <param name="player">The player to clone</param>
        /// <param name="clone">The target GameNPC to clone the player to</param>
        public static void ClonePlayer(GamePlayer player, GameNPC clone)
        {
            ClonePlayer(player, clone, false);
        }

        /// <summary>
        /// Clones a player's appearance and melee abilities to a GameNPC
        /// </summary>
        /// <param name="player">The player to clone</param>
        /// <param name="clone">The target GameNPC to clone the player to</param>
        /// <param name="loadStyles">True to load styles</param>
        // Players cloned by name, from the database, tend to be a bit tougher than players cloned from a GamePlayer
        public static void ClonePlayer(GamePlayer player, GameNPC clone, bool loadStyles)
        {
            if (player == null || clone == null)
                return;

            //clone.Model = GetPlayerModel( (eRace)player.Race, player.PlayerCharacter.Gender );
            SetNPCModel(clone, player.Model);
            clone.Level = player.Level;
            clone.Name = player.Name + (player.LastName.Length > 0 ? " " + player.LastName : "");
            clone.TempProperties.setProperty(CLONE_CLASS_NAME, player.CharacterClass.Name);
            clone.GuildName = player.GuildName;
            clone.IsCloakHoodUp = player.IsCloakHoodUp;
            clone.IsCloakInvisible = player.IsCloakInvisible;
            clone.IsHelmInvisible = player.IsHelmInvisible;

            clone.Strength = (short)player.GetModified(eProperty.Strength);
            clone.Constitution = (short)player.GetModified(eProperty.Constitution);
            clone.Dexterity = (short)player.GetModified(eProperty.Dexterity);
            // stats work differently for mobs, and qui goes crazy above about 150
            clone.Quickness = (short)(100 + player.Level);

            clone.MeleeDamageType = player.AttackWeapon == null ? eDamageType.Slash : (eDamageType)player.AttackWeapon.Type_Damage;
            clone.MaxSpeedBase = player.MaxSpeedBase;

            #region Inventory

            GameNpcInventoryTemplate npcInventory = new GameNpcInventoryTemplate();
            InventoryItem item;

            foreach (eInventorySlot slot in itemsToCopy)
            {
                item = player.Inventory.GetItem(slot);

                if (item != null)
                {
                    int itemModel = item.Model;

                    // keep clothing manager uses model IDs of 3800-3802 for guild cloaks... doesn't seem to work here :(
                    //if ( item.SlotPosition == (int)eInventorySlot.Cloak && item.Emblem > 0 )
                    //	itemModel = 3799 + (int)player.Realm;

                    npcInventory.AddNPCEquipment(slot, item.Model, item.Color, item.Effect, item.Extension);
                }
            }

            npcInventory.CloseTemplate();
            clone.Inventory = npcInventory;
            clone.UpdateNPCEquipmentAppearance();
            #endregion Inventory

            #region Behavior
            if (player.AttackWeapon != null)
            {
                if (player.AttackWeapon.Hand == 2)
                    clone.SwitchWeapon(GameLiving.eActiveWeaponSlot.TwoHanded);
                else
                    clone.SwitchWeapon(GameLiving.eActiveWeaponSlot.Standard);
            }

            if (player.CanUseLefthandedWeapon && player.Inventory.GetItem(eInventorySlot.LeftHandWeapon) != null && player.Inventory.GetItem(eInventorySlot.LeftHandWeapon).Object_Type != (int)eObjectType.Shield)
                clone.LeftHandSwingChance = LEFTSWINGCHANCE;

            // if a shield is equipped, copy the character's block chance
            // GamePlayer.GetBlockChance() returns a percentage from 1-99
            // GameNPC.BlockChance represents block chance percentage in the range 1-99
            if (player.Inventory.GetItem(eInventorySlot.LeftHandWeapon) != null && player.Inventory.GetItem(eInventorySlot.LeftHandWeapon).Object_Type == (int)eObjectType.Shield)
                clone.BlockChance = (byte)player.GetBlockChance();

            // GamePlayer.GetParryChance() returns a percentage from 1-99
            // GameNPC.ParryChance represents parry chance percentage in the range 1-99
            clone.ParryChance = (byte)player.GetParryChance();

            // GamePlayer.GetEvadeChance() returns a percentage from 1-99
            // GameNPC.EvadeChance represents evade chance percentage in the range 1-99
            clone.EvadeChance = (byte)player.GetEvadeChance();

            if (loadStyles)
            {
                clone.Styles = GetCharacterStyles(player.DBCharacter);
            }

            clone.SetOwnBrain(new MeleeStyler());
            #endregion Behavior

            //josh
        }


        /// <summary>
        /// Gets the level of the specified ability
        /// </summary>
        /// <param name="playerChar">Character to check</param>
        /// <param name="abilityName">Ability name (use Abilities enum)</param>
        /// <returns>The level of the ability (0 if not found; 1 if ability found, but no level)</returns>
        public static byte GetCharacterAbilityLevel(DOLCharacters playerChar, string abilityName)
        {
            string[] abilities = playerChar.SerializedAbilities.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] abilityInfo;
            byte abilityLevel = 0;

            foreach (string ability in abilities)
            {
                if (ability.StartsWith(abilityName))
                {
                    abilityInfo = ability.Split(new char[] { '|' });

                    if (abilityInfo.Length < 2)
                        abilityLevel = 1;
                    else
                        byte.TryParse(abilityInfo[1], out abilityLevel);
                }
            }

            return abilityLevel;
        }

        /// <summary>
        /// Gets the level of the specified spec
        /// </summary>
        /// <param name="playerChar">Character to check</param>
        /// <param name="specName">Spec name (use Specs enum)</param>
        /// <returns>The level of the spec (0 if not found; 1 if spec found, but no level)</returns>
        public static byte GetCharacterSpecLevel(DOLCharacters playerChar, string specName)
        {
            string[] playerspecs = playerChar.SerializedSpecs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] specInfo;
            byte specLevel = 0;

            foreach (string spec in playerspecs)
            {
                if (spec.StartsWith(specName))
                {
                    specInfo = spec.Split(new char[] { '|' });

                    if (specInfo.Length < 2)
                        specLevel = 1;
                    else
                        byte.TryParse(specInfo[1], out specLevel);
                }
            }

            return specLevel;
        }

        /*
        public static List<Spell> GetCharacterSpells(DOLCharacters playerChar)
        {
            string[] playerspecs = playerChar.SerializedSpecs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //ist<Styles.Style> charStyles = new List<DOL.GS.Styles.Style>(30);
            List<Spell> charStyles = new List<Spell>(150);
            List<Spell> specStyles;
            string specName;
            byte specLevel;

            foreach (string specInfo in playerspecs)
            {
                if (specInfo.Contains("|"))
                {
                    specName = specInfo.Split(new char[] { '|' })[0];
                    if (!byte.TryParse(specInfo.Split(new char[] { '|' })[1], out specLevel))
                        specLevel = 50;
                }
                else
                {
                    specName = specInfo;
                    specLevel = 50;
                }

                specStyles = SkillBase.GetSpellList(specName);


                for (int i = specStyles.Count - 1; i >= 0; i--)
                {
                    specStyles.RemoveAt(i);
                }

                charStyles.AddRange(specStyles);
            }

            return charStyles;
        }
         */
        /// <summary>
        /// Gets the list of styles
        /// </summary>
        /// <param name="playerChar">Character to check</param>
        /// <returns>List of styles</returns>
        public static List<Styles.Style> GetCharacterStyles(DOLCharacters playerChar)
        {
            string[] playerspecs = playerChar.SerializedSpecs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<Styles.Style> charStyles = new List<DOL.GS.Styles.Style>(30);
            List<Styles.Style> specStyles;
            string specName;
            byte specLevel;

            foreach (string specInfo in playerspecs)
            {
                if (specInfo.Contains("|"))
                {
                    specName = specInfo.Split(new char[] { '|' })[0];
                    if (!byte.TryParse(specInfo.Split(new char[] { '|' })[1], out specLevel))
                        specLevel = 50;
                }
                else
                {
                    specName = specInfo;
                    specLevel = 50;
                }

                specStyles = SkillBase.GetStyleList(specName, playerChar.Class);

              
                for (int i = specStyles.Count - 1; i >= 0; i--)
                {
                    if (specLevel < specStyles[i].SpecLevelRequirement)
                        specStyles.RemoveAt(i);
                }

                charStyles.AddRange(specStyles);
            }

            return charStyles;
        }


        private static void SetNPCModel(GameNPC npc, ushort playerModelID)
        {
            ushort size = (ushort)(playerModelID & 0x1800);
            ushort model = (ushort)(playerModelID & 0x7FF);

            switch (size)
            {
                case 0x800:
                    npc.Size = 47;
                    break;
                case 0x1800:
                    npc.Size = 53;
                    break;
                default:
                    npc.Size = 50;
                    break;
            }

            npc.Model = model;
        }

        // This isn't used - I created it before I figured out how to interpret a player's model ID
        // Haven't deleted it as it could be a useful list at some point
        public static ushort GetPlayerModel(eRace race, int gender)
        {
            //player.PlayerCharacter.Gender is 0 (male) or 1 (female)
            Dictionary<eRace, ushort[]> models = new Dictionary<eRace, ushort[]>(21);

            models.Add(eRace.Briton, new ushort[2] { 1960, 1961 });
            models.Add(eRace.Avalonian, new ushort[2] { 279, 499 });
            models.Add(eRace.Highlander, new ushort[2] { 1962, 1963 });
            models.Add(eRace.Saracen, new ushort[2] { 1964, 1965 });
            models.Add(eRace.Norseman, new ushort[2] { 1972, 1973 });
            models.Add(eRace.Troll, new ushort[2] { 1970, 1971 });
            models.Add(eRace.Dwarf, new ushort[2] { 1976, 1977 });
            models.Add(eRace.Kobold, new ushort[2] { 1974, 1975 });
            models.Add(eRace.Celt, new ushort[2] { 1984, 1985 });
            models.Add(eRace.Firbolg, new ushort[2] { 1982, 1983 });
            models.Add(eRace.Elf, new ushort[2] { 2022, 2023 });
            models.Add(eRace.Lurikeen, new ushort[2] { 1986, 1987 });
            models.Add(eRace.Inconnu, new ushort[2] { 1966, 1967 });
            models.Add(eRace.Valkyn, new ushort[2] { 1978, 1979 });
            models.Add(eRace.Sylvan, new ushort[2] { 1990, 1991 });
            models.Add(eRace.HalfOgre, new ushort[2] { 1968, 1969 });
            models.Add(eRace.Frostalf, new ushort[2] { 1980, 1981 });
            models.Add(eRace.Shar, new ushort[2] { 1992, 1993 });
            models.Add(eRace.AlbionMinotaur, new ushort[2] { 1398, 1398 });
            models.Add(eRace.MidgardMinotaur, new ushort[2] { 1409, 1409 });
            models.Add(eRace.HiberniaMinotaur, new ushort[2] { 1430, 1430 });

            return models[race][gender];
        }
    }


    [CmdAttribute(
        "&cloneplayer",
        ePrivLevel.GM,
        "Clone a player with live or calculated stats, optionally loading styles",
        "If a player is targeted, that player is cloned to a new mob using:",
        "/cloneplayer [live] [LoadStyles]",
        "If a mob is targeted, a player is cloned to the targeted mob using:",
        "/cloneplayer [live] Playername [LoadStyles]",
        "If the user has no target, a player is cloned to a new mob using:",
        "/cloneplayer [live] Playername [LoadStyles]")]
    public class ClonePlayerCommandHandler : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            if (client.Account.PrivLevel == (uint)ePrivLevel.Player)
                return;

            GamePlayer player = client.Player;

            if (player == null)
                return;

            bool loadStyles = args[args.Length - 1].ToLower().Equals("loadstyles");

            GameNPC clone = null;
            clone = player.TargetObject as GameNPC;
            bool cloneIsNewMob = clone == null;
            GamePlayer cloneSource = player.TargetObject as GamePlayer;
            string playerName = string.Empty;

            if (args.Length > 1 && args[1].Equals("live"))
            {
                if (cloneSource == null)
                {
                    if (args.Length < 3)
                    {
                        DisplaySyntax(client);
                        return;
                    }

                    playerName = args[2];
                    GameClient cloneClient = WorldMgr.GetClientByPlayerName(playerName, true, true);

                    if (cloneClient == null || cloneClient.Player == null)
                    {
                        DisplayMessage(client, "Unable to create live copy of:  " + playerName);
                        return;
                    }

                    cloneSource = cloneClient.Player;
                }

                if (clone == null)
                    clone = PlayerCloner.ClonePlayer(cloneSource, loadStyles);
                else
                    PlayerCloner.ClonePlayer(cloneSource, clone, loadStyles);
            }
            else
            {
                if (cloneSource == null && args.Length < 2)
                {
                    DisplaySyntax(client);
                    return;
                }

                if (cloneSource == null)
                    playerName = args[1];
                else
                    playerName = cloneSource.Name;

                if (clone == null)
                    clone = PlayerCloner.ClonePlayer(playerName, loadStyles);
                else
                    PlayerCloner.ClonePlayer(playerName, clone, loadStyles, 0);
            }

            if (clone == null)
            {
                DisplaySyntax(client);
            }
            else
            {
                if (loadStyles)
                    clone.SetOwnBrain(new MeleeStyler());

                if (cloneIsNewMob)
                {
                    clone.CurrentRegion = player.CurrentRegion;
                    clone.X = player.X;
                    clone.Y = player.Y;
                    clone.Z = player.Z;
                    clone.Heading = player.Heading;
                    clone.AddToWorld();
                }
            }
        }
    }
}
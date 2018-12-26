using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Describes all the properties of an item.
	/// </summary>
	public class Item {

		/// <summary>
		/// The category of the item.
		/// </summary>
		public enum ItemCategory {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
			CustomItem_Elemental_Brick_Explosive,
			CustomItem_Elemental_Roland_Incendiary,
			CustomItem_Reward_Shield,
			ElementalUpgrade_Brick,
			ItemGrade_Gear_ComDeck_Brick,
			ItemGrade_Gear_ComDeck_Lilith,
			ItemGrade_Gear_ComDeck_Mordecai,
			ItemGrade_Gear_ComDeck_Roland,
			ItemGrade_Gear_GrenadeMODs,
			ItemGrade_Gear_Shield,
			ItemGrade_HealthPacks_2,
			SDU_InventorySlots
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		}

		/// <summary>
		/// The type of the item.
		/// </summary>
		public enum ItemType {

		}

		/// <summary>
		/// The body of the item.
		/// </summary>
		public enum ItemBody {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
			None,
			CommandDeck_Brick_Bombardier,
			CommandDeck_Brick_Centurion,
			CommandDeck_Lilith_Mercenary,
			CommandDeck_Mordecai_Gunslinger,
			CommandDeck_Mordecai_Scavenger,
			CommandDeck_Roland_HeavyGunner,
			CommandDeck_Roland_Leader,
			CommandDeck_Roland_SupportGunner,
			ElementalUpgrade_RequiredShared,
			Shield1,
			Shield2,
			Shield3,
			Shield3b_Power,
			Shield4,
			Tunercuffs_1_Explosive,
			Tunercuffs_2_Incendiary,
			Tunercuffs_4_Shock,
			Tunercuffs_5_NoTech,
			Tunercuffs_6_ExplosiveRain,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		}

		/// <summary>
		/// The left property of the item.
		/// </summary>
		public enum ItemLeft {

		}

		/// <summary>
		/// The right property of the item.
		/// </summary>
		public enum ItemRight {

		}

		/// <summary>
		/// The material of the item.
		/// </summary>
		public enum ItemMaterial {

		}

		/// <summary>
		/// The manufacturer of the item.
		/// </summary>
		public enum ItemManufacturer {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
			Anshin,
			Atlas,
			Dahl,
			Eridian,
			Hyperion,
			Maliwan,
			Pangolin,
			SandSMunitions,
			Stock,
			Tediore,
			Torgue,
			Vladof
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		}

		/// <summary>
		/// The prefix of the item.
		/// </summary>
		public enum ItemPrefix {

		}

		/// <summary>
		/// The title of the item.
		/// </summary>
		public enum ItemTitle {

		}

		/// <summary>
		/// Constructs an item given a reader on the save file at the correct position.
		/// </summary>
		/// <param name="reader"></param>
		public Item(BinaryReader reader) {
			InternalItemCategory = reader.BL_ReadString();
			InternalItemType = reader.BL_ReadString();
			InternalItemBody = reader.BL_ReadString();
			InternalItemLeft = reader.BL_ReadString();
			InternalItemRight = reader.BL_ReadString();
			InternalItemMaterial = reader.BL_ReadString();
			InternalItemManufacturer = reader.BL_ReadString();
			InternalItemPrefix = reader.BL_ReadString();
			InternalItemTitle = reader.BL_ReadString();
			UnknownVariable1 = reader.ReadInt32();
			UnknownVariable2 = reader.ReadInt32();
			Equipped = reader.ReadInt32();
		}

		/// <summary>
		/// Writes this item to a writer in the save format.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.BL_WriteString(InternalItemCategory);
			writer.BL_WriteString(InternalItemType);
			writer.BL_WriteString(InternalItemBody);
			writer.BL_WriteString(InternalItemLeft);
			writer.BL_WriteString(InternalItemRight);
			writer.BL_WriteString(InternalItemMaterial);
			writer.BL_WriteString(InternalItemManufacturer);
			writer.BL_WriteString(InternalItemPrefix);
			writer.BL_WriteString(InternalItemTitle);
			writer.Write(UnknownVariable1);
			writer.Write(UnknownVariable2);
			writer.Write(Equipped);

		}

		/// <summary>
		/// The item's category.
		/// </summary>
		public ItemCategory Category;

		/// <summary>
		/// The internal string used by the save file for this item's category.
		/// </summary>
		public string InternalItemCategory {
			get { return CategoryInternalNames[Category]; }
			set { Category = CategoryInternalNames.First(x => x.Value == value).Key; }
		}

		/// <summary>
		/// The internal name of this item's type.
		/// </summary>
		public string InternalItemType;

		/// <summary>
		/// The item's body.
		/// </summary>
		public ItemBody Body;

		/// <summary>
		/// The internal name of this item's body.
		/// </summary>
		public string InternalItemBody {
			get { return BodyInternalNames[Body]; }
			set { Body = BodyInternalNames.First(x => x.Value == value).Key; }
		}

		/// <summary>
		/// The internal name of this item's left property.
		/// </summary>
		public string InternalItemLeft;

		/// <summary>
		/// The internal name of this item's right property.
		/// </summary>
		public string InternalItemRight;

		/// <summary>
		/// The internal name of this item's material.
		/// </summary>
		public string InternalItemMaterial;

		/// <summary>
		/// The item's manufacturer. Not all manufacturers make items, which is why this is separate
		/// from <see cref="Weapon.WeaponManufacturer"/>.
		/// </summary>
		public ItemManufacturer Manufacturer;

		/// <summary>
		/// The internal string used by the save file for this item's manufacturer.
		/// </summary>
		public string InternalItemManufacturer {
			get { return ManufacturerInternalNames[Manufacturer]; }
			set { Manufacturer = ManufacturerInternalNames.First(x => x.Value == value).Key; }
		}

		/// <summary>
		/// The internal name of this item's prefix.
		/// </summary>
		public string InternalItemPrefix;

		/// <summary>
		/// The internal name of this item's title.
		/// </summary>
		public string InternalItemTitle;

		/// <summary>
		/// And unknown variable.
		/// </summary>
		public Int32 UnknownVariable1;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable2;

		/// <summary>
		/// Stores whether this item is equipped or not. 0 means it is unequipped, 1 means it is.
		/// </summary>
		public Int32 Equipped;

		/// <summary>
		/// Maps categories to internal names.
		/// </summary>
		public static readonly Dictionary<ItemCategory, string> CategoryInternalNames = new Dictionary<ItemCategory, string> {
			{ ItemCategory.CustomItem_Elemental_Brick_Explosive, "gd_customitems.Items.CustomItem_Elemental_Brick_Explosive" },
			{ ItemCategory.CustomItem_Elemental_Roland_Incendiary, "gd_customitems.Items.CustomItem_Elemental_Roland_Incendiary" },
			{ ItemCategory.CustomItem_Reward_Shield, "gd_customitems.Items.CustomItem_reward_shield" },
			{ ItemCategory.ElementalUpgrade_Brick, "gd_itemgrades.ElementalUpgrades.ItemGrade_Elemental_Brick" },
			{ ItemCategory.ItemGrade_Gear_ComDeck_Brick, "gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Brick" },
			{ ItemCategory.ItemGrade_Gear_ComDeck_Lilith, "gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Lilith" },
			{ ItemCategory.ItemGrade_Gear_ComDeck_Mordecai, "gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Mordecai" },
			{ ItemCategory.ItemGrade_Gear_ComDeck_Roland, "gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Roland" },
			{ ItemCategory.ItemGrade_Gear_GrenadeMODs, "gd_itemgrades.Gear.ItemGrade_Gear_GrenadeMODs" },
			{ ItemCategory.ItemGrade_Gear_Shield, "gd_itemgrades.Gear.ItemGrade_Gear_Shield" },
			{ ItemCategory.ItemGrade_HealthPacks_2, "gd_itemgrades.Ammo.ItemGrade_HealthPacks_2" },
			{ ItemCategory.SDU_InventorySlots, "gd_itemgrades.StorageDeckUpgrades.ItemGrade_SDU_InventorySlots" },
		};

		/// <summary>
		/// Maps body types to internal names.
		/// </summary>
		public static readonly Dictionary<ItemBody, string> BodyInternalNames = new Dictionary<ItemBody, string> {
			{ ItemBody.None, "None" },
			{ ItemBody.CommandDeck_Brick_Bombardier, "gd_CommandDecks.Body_Brick.Brick_Bombardier" },
			{ ItemBody.CommandDeck_Brick_Centurion, "gd_CommandDecks.Body_Brick.Brick_Centurion" },
			{ ItemBody.CommandDeck_Lilith_Mercenary, "gd_CommandDecks.Body_Lilith.Lilith_Mercenary" },
			{ ItemBody.CommandDeck_Mordecai_Gunslinger, "gd_CommandDecks.Body_Mordecai.Mordecai_Gunslinger" },
			{ ItemBody.CommandDeck_Mordecai_Scavenger, "gd_CommandDecks.Body_Mordecai.Mordecai_Scavenger" },
			{ ItemBody.CommandDeck_Roland_HeavyGunner, "gd_CommandDecks.Body_Roland.Roland_HeavyGunner" },
			{ ItemBody.CommandDeck_Roland_Leader, "gd_CommandDecks.Body_Roland.Roland_Leader" },
			{ ItemBody.CommandDeck_Roland_SupportGunner, "gd_CommandDecks.Body_Roland.Roland_SupportGunner" },
			{ ItemBody.ElementalUpgrade_RequiredShared, "gd_ElementalUpgrade.Body.Body_RequiredShared" },
			{ ItemBody.Shield1, "gd_shields.Body.body1" },
			{ ItemBody.Shield2, "gd_shields.Body.body2" },
			{ ItemBody.Shield3, "gd_shields.Body.body3" },
			{ ItemBody.Shield3b_Power, "gd_shields.Body.body3b_power" },
			{ ItemBody.Shield4, "gd_shields.Body.body4" },
			{ ItemBody.Tunercuffs_1_Explosive, "gd_tunercuffs.Body.body1_explosive" },
			{ ItemBody.Tunercuffs_2_Incendiary, "gd_tunercuffs.Body.body2_incendiary" },
			{ ItemBody.Tunercuffs_4_Shock, "gd_tunercuffs.Body.body4_shock" },
			{ ItemBody.Tunercuffs_5_NoTech, "gd_tunercuffs.Body.body5_notech" },
			{ ItemBody.Tunercuffs_6_ExplosiveRain, "gd_tunercuffs.Body.body6_explosive_rain" }
		};

		/// <summary>
		/// Maps manufacturer types to internal names.
		/// </summary>
		public static readonly Dictionary<ItemManufacturer, string> ManufacturerInternalNames = new Dictionary<ItemManufacturer, string> {
			{ ItemManufacturer.Anshin, "gd_manufacturers.Manufacturers.Anshin" },
			{ ItemManufacturer.Atlas, "gd_manufacturers.Manufacturers.Atlas" },
			{ ItemManufacturer.Dahl, "gd_manufacturers.Manufacturers.Dahl" },
			{ ItemManufacturer.Eridian, "gd_manufacturers.Manufacturers.Eridian" },
			{ ItemManufacturer.Hyperion, "gd_manufacturers.Manufacturers.Hyperion" },
			{ ItemManufacturer.Maliwan, "gd_manufacturers.Manufacturers.Maliwan" },
			{ ItemManufacturer.Pangolin, "gd_manufacturers.Manufacturers.Pangolin" },
			{ ItemManufacturer.SandSMunitions, "gd_manufacturers.Manufacturers.SandSMunitions" },
			{ ItemManufacturer.Stock, "gd_manufacturers.Manufacturers.Stock" },
			{ ItemManufacturer.Tediore, "gd_manufacturers.Manufacturers.tediore" },
			{ ItemManufacturer.Torgue, "gd_manufacturers.Manufacturers.Torgue" },
			{ ItemManufacturer.Vladof, "gd_manufacturers.Manufacturers.Vladof" },
		};
	}
}

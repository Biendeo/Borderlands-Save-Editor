using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// The category of the item.
	/// </summary>
	public enum ItemCategory {

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
	/// Describes all the properties of an item.
	/// </summary>
	public class Item {
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
		/// The internal name of this item's category.
		/// </summary>
		public string InternalItemCategory;

		/// <summary>
		/// The internal name of this item's type.
		/// </summary>
		public string InternalItemType;

		/// <summary>
		/// The internal name of this item's body.
		/// </summary>
		public string InternalItemBody;

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
		/// The internal name of this item's manufacturer.
		/// </summary>
		public string InternalItemManufacturer;

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
		/// Stores a bunch of item internal names that I've seen.
		/// </summary>
		//? Change this to map strings to each enum.
		public static readonly List<string> ItemDefinitions = new List<string> {
			"gd_customitems.Items.CustomItem_StarterShield",
			"gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Roland",
			"gd_itemgrades.Gear.ItemGrade_Gear_GrenadeMODs",
			"gd_itemgrades.Gear.ItemGrade_Gear_Shield"
		};
	}
}

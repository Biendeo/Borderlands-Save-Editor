using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum ItemCategory {

	}

	public enum ItemType {

	}

	public enum ItemBody {

	}

	public enum ItemLeft {

	}

	public enum ItemRight {

	}

	public enum ItemMaterial {

	}

	public enum ItemManufacturer {

	}

	public enum ItemPrefix {

	}

	public enum ItemTitle {

	}

	public class Item {
		public string InternalItemCategory;
		public string InternalItemType;
		public string InternalItemBody;
		public string InternalItemLeft;
		public string InternalItemRight;
		public string InternalItemMaterial;
		public string InternalItemManufacturer;
		public string InternalItemPrefix;
		public string InternalItemTitle;
		public Int32 UnknownVariable1;
		public Int32 UnknownVariable2;
		public Int32 Equipped; // 0 means unequipped, 1 means equipped.

		//? Change this to map strings to each enum.
		public static readonly List<string> ItemDefinitions = new List<string> {
			"gd_customitems.Items.CustomItem_StarterShield",
			"gd_itemgrades.Gear.ItemGrade_Gear_ComDeck_Roland",
			"gd_itemgrades.Gear.ItemGrade_Gear_GrenadeMODs",
			"gd_itemgrades.Gear.ItemGrade_Gear_Shield"
		};
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum WeaponCategory {

	}

	public enum WeaponManufacturer {

	}

	public enum WeaponType {

	}

	public enum WeaponBody {

	}

	public enum WeaponGrip {

	}

	public enum WeaponMag {

	}

	public enum WeaponBarrel {

	}

	public enum WeaponSight {

	}

	public enum WeaponStock {

	}

	public enum WeaponAction {

	}

	public enum WeaponAccessory {

	}

	public enum WeaponMaterial {

	}

	public enum WeaponPrefix {

	}

	public enum WeaponTitle {

	}

	public class Weapon {
		public string InternalWeaponCategory;
		public string InternalWeaponManufacturer;
		public string InternalWeaponType;
		public string InternalWeaponBody;
		public string InternalWeaponGrip;
		public string InternalWeaponMag;
		public string InternalWeaponBarrel;
		public string InternalWeaponSight;
		public string InternalWeaponStock;
		public string InternalWeaponAction;
		public string InternalWeaponAccessory;
		public string InternalWeaponMaterial;
		public string InternalWeaponPrefix;
		public string InternalWeaponTitle;
		public Int32 UnknownVariable1;
		public Int32 UnknownVariable2; // 1 on my blue shotgun, 0 otherwise.
		public Int32 EquippedSlot; // 0 means unequipped, 1-4 means equipped in that slot.

		//? Change this to map strings to each enum.
		public static readonly List<string> WeaponDefinitions = new List<string> {
			"gd_customweapons.Custom_Starters.CustomWeap_repeater_starter_accurate",
			"gd_customweapons.Custom_Starters.CustomWeap_repeater_starter_strong",
			"gd_customweapons.Weapons.CustomWeap_MachinePistol_TheClipper",
			"gd_customweapons.Weapons.CustomWeap_Repeater_LadiesFinger",
			"gd_itemgrades.StarterGear.ItemGrade_StarterGear",
			"gd_itemgrades.Weapons.ItemGrade_Weapon_Launcher_Rocket",
			"gd_itemgrades.Weapons.ItemGrade_Weapon_RepeaterPistol",
			"gd_itemgrades.Weapons.ItemGrade_Weapon_RevolverPistol",
			"gd_itemgrades.Weapons.ItemGrade_Weapon_SniperRifle"
		};
	}
}

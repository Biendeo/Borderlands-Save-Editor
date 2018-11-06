using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// The category of the weapon.
	/// </summary>
	public enum WeaponCategory {

	}

	/// <summary>
	/// The manufacturer of the weapon.
	/// </summary>
	public enum WeaponManufacturer {

	}

	/// <summary>
	/// The type of the weapon.
	/// </summary>
	public enum WeaponType {

	}

	/// <summary>
	/// The body of the weapon.
	/// </summary>
	public enum WeaponBody {

	}

	/// <summary>
	/// The grip of the weapon.
	/// </summary>
	public enum WeaponGrip {

	}

	/// <summary>
	/// The magazine shape of the weapon.
	/// </summary>
	public enum WeaponMag {

	}

	/// <summary>
	/// The barrel of the weapon.
	/// </summary>
	public enum WeaponBarrel {

	}

	/// <summary>
	/// The sight of the weapon.
	/// </summary>
	public enum WeaponSight {

	}
	
	/// <summary>
	/// The stock of the weapon.
	/// </summary>
	public enum WeaponStock {

	}

	/// <summary>
	/// The action of the weapon.
	/// </summary>
	public enum WeaponAction {

	}

	/// <summary>
	/// The accessory of the weapon.
	/// </summary>
	public enum WeaponAccessory {

	}

	/// <summary>
	/// The material of the weapon.
	/// </summary>
	public enum WeaponMaterial {

	}

	/// <summary>
	/// The prefix of the weapon.
	/// </summary>
	public enum WeaponPrefix {

	}

	/// <summary>
	/// The title of the weapon.
	/// </summary>
	public enum WeaponTitle {

	}

	/// <summary>
	/// Stores all the properties of the weapon.
	/// </summary>
	public class Weapon {
		/// <summary>
		/// The internal string used by the save file for this weapon's category.
		/// </summary>
		public string InternalWeaponCategory;

		/// <summary>
		/// The internal string used by the save file for this weapon's manufacturer.
		/// </summary>
		public string InternalWeaponManufacturer;

		/// <summary>
		/// The internal string used by the save file for this weapon's type.
		/// </summary>
		public string InternalWeaponType;

		/// <summary>
		/// The internal string used by the save file for this weapon's body.
		/// </summary>
		public string InternalWeaponBody;

		/// <summary>
		/// The internal string used by the save file for this weapon's grip.
		/// </summary>
		public string InternalWeaponGrip;

		/// <summary>
		/// The internal string used by the save file for this weapon's magazine.
		/// </summary>
		public string InternalWeaponMag;

		/// <summary>
		/// The internal string used by the save file for this weapon's barrel.
		/// </summary>
		public string InternalWeaponBarrel;

		/// <summary>
		/// The internal string used by the save file for this weapon's sight.
		/// </summary>
		public string InternalWeaponSight;

		/// <summary>
		/// The internal string used by the save file for this weapon's stock.
		/// </summary>
		public string InternalWeaponStock;

		/// <summary>
		/// The internal string used by the save file for this weapon's action.
		/// </summary>
		public string InternalWeaponAction;

		/// <summary>
		/// The internal string used by the save file for this weapon's accessory.
		/// </summary>
		public string InternalWeaponAccessory;

		/// <summary>
		/// The internal string used by the save file for this weapon's material.
		/// </summary>
		public string InternalWeaponMaterial;

		/// <summary>
		/// The internal string used by the save file for this weapon's prefix.
		/// </summary>
		public string InternalWeaponPrefix;

		/// <summary>
		/// The internal string used by the save file for this weapon's title.
		/// </summary>
		public string InternalWeaponTitle;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable1;

		/// <summary>
		/// An unknown variable. I've noticed it's sometimes 1 like on one of my blue shotguns, but
		/// usually 0.
		/// </summary>
		public Int32 UnknownVariable2;

		/// <summary>
		/// The equipped slot of this weapon. If it is 0, it is unequipped. Otherwise, it is from 1
		/// to 4 depending on the slot.
		/// </summary>
		public Int32 EquippedSlot;

		/// <summary>
		/// A list of weapon definitions I've seen.
		/// </summary>
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

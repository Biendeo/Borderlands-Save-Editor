using System;
using System.Collections.Generic;
using System.IO;
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
		/// Constructs a weapon from a save file reader at the correct position.
		/// </summary>
		/// <param name="reader"></param>
		public Weapon(BinaryReader reader) {
			InternalWeaponCategory = reader.BL_ReadString();
			InternalWeaponManufacturer = reader.BL_ReadString();
			InternalWeaponType = reader.BL_ReadString();
			InternalWeaponBody = reader.BL_ReadString();
			InternalWeaponGrip = reader.BL_ReadString();
			InternalWeaponMag = reader.BL_ReadString();
			InternalWeaponBarrel = reader.BL_ReadString();
			InternalWeaponSight = reader.BL_ReadString();
			InternalWeaponStock = reader.BL_ReadString();
			InternalWeaponAction = reader.BL_ReadString();
			InternalWeaponAccessory = reader.BL_ReadString();
			InternalWeaponMaterial = reader.BL_ReadString();
			InternalWeaponPrefix = reader.BL_ReadString();
			InternalWeaponTitle = reader.BL_ReadString();
			UnknownVariable1 = reader.ReadInt32();
			LevelRank = reader.ReadInt32();
			EquippedSlot = reader.ReadInt32();
		}

		/// <summary>
		/// Writes this weapon to a writer in the Borderlands save format.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.BL_WriteString(InternalWeaponCategory);
			writer.BL_WriteString(InternalWeaponManufacturer);
			writer.BL_WriteString(InternalWeaponType);
			writer.BL_WriteString(InternalWeaponBody);
			writer.BL_WriteString(InternalWeaponGrip);
			writer.BL_WriteString(InternalWeaponMag);
			writer.BL_WriteString(InternalWeaponBarrel);
			writer.BL_WriteString(InternalWeaponSight);
			writer.BL_WriteString(InternalWeaponStock);
			writer.BL_WriteString(InternalWeaponAction);
			writer.BL_WriteString(InternalWeaponAccessory);
			writer.BL_WriteString(InternalWeaponMaterial);
			writer.BL_WriteString(InternalWeaponPrefix);
			writer.BL_WriteString(InternalWeaponTitle);
			writer.Write(UnknownVariable1);
			writer.Write(LevelRank);
			writer.Write(EquippedSlot);
		}

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
		/// This sets the level rank of the weapon. This rank is a value between 0 and 5 inclusive
		/// and affects the weapon's required level, damage output, and worth. It did not change
		/// the accuracy, fire rate, effects, or rarity of the weapon.
		/// 
		/// From what I've seen, rank 0 sits itself at a very low level, and they generally step up
		/// by about 8 or 9 levels at a time, until rank 5 where they sit very close to level 50.
		/// 
		/// I'm not too sure how to reach level 69, maybe there's another field?
		/// </summary>
		public Int32 LevelRank;

		/// <summary>
		/// The equipped slot of this weapon. If it is 0, it is unequipped. Otherwise, it is from 1
		/// to 4 depending on the slot. If the weapon exceeds the player's current level, it is
		/// removed from the equipped slot on load immediately.
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

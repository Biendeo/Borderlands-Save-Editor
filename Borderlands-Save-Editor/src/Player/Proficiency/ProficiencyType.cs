using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Proficiency {
	/// <summary>
	/// Defines the weapon type used by a proficiency.
	/// </summary>
	public enum ProficiencyType {
		/// <summary>
		/// The pistol proficiency.
		/// </summary>
		Pistol,
		/// <summary>
		/// The SMG proficiency.
		/// </summary>
		SMG,
		/// <summary>
		/// The shotgun proficiency.
		/// </summary>
		Shotgun,
		/// <summary>
		/// The combat rifle proficiency.
		/// </summary>
		CombatRifle,
		/// <summary>
		/// The sniper rifle proficiency.
		/// </summary>
		SniperRifle,
		/// <summary>
		/// The rocket launcher proficiency.
		/// </summary>
		RocketLauncher,
		/// <summary>
		/// The proficiency for Eridian weapons.
		/// </summary>
		Eridian
	}

	/// <summary>
	/// Extension methods related to proficiency types.
	/// </summary>
	public static class ProficiencyTypeExtensions {
		/// <summary>
		/// Returns the a readable name for this proficiency type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ToString(this ProficiencyType type) {
			return type.ProficiencyName();
		}

		/// <summary>
		/// Returns the <see cref="ProficiencyType"/> that uses the given internal name.
		/// </summary>
		/// <param name="internalName"></param>
		/// <returns></returns>
		public static ProficiencyType FromInternalName(string internalName) {
			return InternalNames.FirstOrDefault(x => x.Value == internalName).Key;
		}

		/// <summary>
		/// Returns the internal name for this proficiency type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string InternalName(this ProficiencyType type) {
			return InternalNames[type];
		}

		/// <summary>
		/// Returns a readable name for this proficiency type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ProficiencyName(this ProficiencyType type) {
			return ProficiencyNames[type];
		}

		/// <summary>
		/// Maps <see cref="ProficiencyType"/> to their internal names.
		/// </summary>
		public static readonly Dictionary<ProficiencyType, string> InternalNames = new Dictionary<ProficiencyType, string> {
			{ ProficiencyType.Pistol, "gd_skills_common.WeaponProficiencySkills.Skill_PistolProficiency" },
			{ ProficiencyType.SMG, "gd_skills_common.WeaponProficiencySkills.Skill_SMGProficiency" },
			{ ProficiencyType.Shotgun, "gd_skills_common.WeaponProficiencySkills.Skill_ShotgunProficiency" },
			{ ProficiencyType.CombatRifle, "gd_skills_common.WeaponProficiencySkills.Skill_CombatRifleProficiency" },
			{ ProficiencyType.SniperRifle, "gd_skills_common.WeaponProficiencySkills.Skill_SniperRifleProficiency" },
			{ ProficiencyType.RocketLauncher, "gd_skills_common.WeaponProficiencySkills.Skill_RocketLauncherProficiency" },
			{ ProficiencyType.Eridian, "gd_skills_common.WeaponProficiencySkills.Skill_EridanWeaponProficiency" } // Yes they spell it wrong.
		};

		/// <summary>
		/// Maps <see cref="ProficiencyType"/> to readable names.
		/// </summary>
		public static readonly Dictionary<ProficiencyType, string> ProficiencyNames = new Dictionary<ProficiencyType, string> {
			{ ProficiencyType.Pistol, "Pistol" },
			{ ProficiencyType.SMG, "SMG" },
			{ ProficiencyType.Shotgun, "Shotgun" },
			{ ProficiencyType.CombatRifle, "Combat Rifle" },
			{ ProficiencyType.SniperRifle, "Sniper Rifle" },
			{ ProficiencyType.RocketLauncher, "Rocket Launcher" },
			{ ProficiencyType.Eridian, "Eridian Weapon" }
		};
	}
}

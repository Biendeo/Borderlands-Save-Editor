using Borderlands_Save_Editor.Player.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Skill {
	/// <summary>
	/// Describes the specific skill type that a skill has.
	/// </summary>
	public enum SkillType {
		// I'm a bit lazy documenting these.
		//? There's many more skills than this.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		Soldier_Action_DeployScorpio,
		Hunter_Action_LaunchBloodwing,
		Hunter_Elemental_FireWing,
		Hunter_Elemental_ShockWing,
		Hunter_Elemental_AcidWing,
		Hunter_Elemental_ExplosiveWing,
		Hunter_Adrenaline,
		Hunter_Sniper_Focus,
		Hunter_Sniper_Caliber,
		Hunter_Sniper_Smirk,
		Hunter_Sniper_Killer,
		Hunter_Sniper_Loaded,
		Hunter_Sniper_CarrionCall,
		Hunter_Sniper_Trespass,
		Hunter_Rogue_SwiftStrike,
		Hunter_Rogue_Swipe,
		Hunter_Rogue_FastHands,
		Hunter_Rogue_OutForBlood,
		Hunter_Rogue_AerialImpact,
		Hunter_Rogue_Ransack,
		Hunter_Rogue_BirdOfPrey,
		Hunter_Gunslinger_Deadly,
		Hunter_Gunslinger_GunCrazy,
		Hunter_Gunslinger_LethalStrike,
		Hunter_Gunslinger_RiotousRemedy,
		Hunter_Gunslinger_Predator,
		Hunter_Gunslinger_HairTrigger,
		Hunter_Gunslinger_Relentless,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Extension methods involving skill types.
	/// </summary>
	public static class SkillTypeExtensions {
		/// <summary>
		/// Converts a skill type into a readable name.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ToString(this SkillType type) {
			return type.SkillName();
		}

		/// <summary>
		/// Converts a save format's internal name to a <see cref="SkillType"/>.
		/// </summary>
		/// <param name="internalName"></param>
		/// <returns></returns>
		public static SkillType FromInternalName(string internalName) {
			return InternalNames.FirstOrDefault(x => x.Value == internalName).Key;
		}

		/// <summary>
		/// Converts a skill type into the internal name used by save files.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string InternalName(this SkillType type) {
			return InternalNames[type];
		}

		/// <summary>
		/// Converts a skill type into the readable name of the skill.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string SkillName(this SkillType type) {
			return SkillNames[type];
		}

		/// <summary>
		/// Returns which character class this skill is associated with. This can be determined
		/// using the beginning of its internal name.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static CharacterClassType Class(this SkillType type) {
			if (type.InternalName().StartsWith("gd_skills2_Roland")) {
				return CharacterClassType.Soldier;
			} else if (type.InternalName().StartsWith("gd_skills2_Mordecai")) {
				return CharacterClassType.Hunter;
			} else if (type.InternalName().StartsWith("gd_skills2_Lilith")) {
				return CharacterClassType.Siren;
			} else if (type.InternalName().StartsWith("gd_skills2_Brick")) {
				return CharacterClassType.Berserker;
			} else {
				return CharacterClassType.Soldier;
			}
		}

		/// <summary>
		/// Maps skill types to internal names.
		/// </summary>
		public static readonly Dictionary<SkillType, string> InternalNames = new Dictionary<SkillType, string> {
			{ SkillType.Soldier_Action_DeployScorpio,       "gd_skills2_Roland.Action.A_DeployScorpio" },
			{ SkillType.Hunter_Action_LaunchBloodwing,      "gd_skills2_Mordecai.Action.A_LaunchBloodwing" },
			{ SkillType.Hunter_Elemental_FireWing,          "gd_skills2_Mordecai.ActionElemental.FireWing" },
			{ SkillType.Hunter_Elemental_ShockWing,         "gd_skills2_Mordecai.ActionElemental.ShockWing" },
			{ SkillType.Hunter_Elemental_AcidWing,          "gd_skills2_Mordecai.ActionElemental.AcidWing" },
			{ SkillType.Hunter_Elemental_ExplosiveWing,     "gd_skills2_Mordecai.ActionElemental.ExplosiveWing" },
			{ SkillType.Hunter_Adrenaline,                  "gd_skills2_Mordecai.Adrenaline.Adrenaline_Mordecai" },
			{ SkillType.Hunter_Sniper_Focus,                "gd_skills2_Mordecai.Sniper.Focus" },
			{ SkillType.Hunter_Sniper_Caliber,              "gd_skills2_Mordecai.Sniper.Caliber" },
			{ SkillType.Hunter_Sniper_Smirk,                "gd_skills2_Mordecai.Sniper.Smirk" },
			{ SkillType.Hunter_Sniper_Killer,               "gd_skills2_Mordecai.Sniper.Killer" },
			{ SkillType.Hunter_Sniper_Loaded,               "gd_skills2_Mordecai.Sniper.Loaded" },
			{ SkillType.Hunter_Sniper_CarrionCall,          "gd_skills2_Mordecai.Sniper.CarrionCall" },
			{ SkillType.Hunter_Sniper_Trespass,             "gd_skills2_Mordecai.Sniper.Trespass" },
			{ SkillType.Hunter_Rogue_SwiftStrike,           "gd_skills2_Mordecai.Rogue.SwiftStrike" },
			{ SkillType.Hunter_Rogue_Swipe,                 "gd_skills2_Mordecai.Rogue.Swipe" },
			{ SkillType.Hunter_Rogue_FastHands,             "gd_skills2_Mordecai.Rogue.FastHands" },
			{ SkillType.Hunter_Rogue_OutForBlood,           "gd_skills2_Mordecai.Rogue.OutForBlood" },
			{ SkillType.Hunter_Rogue_AerialImpact,          "gd_skills2_Mordecai.Rogue.AerialImpact" },
			{ SkillType.Hunter_Rogue_Ransack,               "gd_skills2_Mordecai.Rogue.Ransack" },
			{ SkillType.Hunter_Rogue_BirdOfPrey,            "gd_skills2_Mordecai.Rogue.BirdOfPrey" },
			{ SkillType.Hunter_Gunslinger_Deadly,           "gd_skills2_Mordecai.Gunslinger.Deadly" },
			{ SkillType.Hunter_Gunslinger_GunCrazy,         "gd_skills2_Mordecai.Gunslinger.GunCrazy" },
			{ SkillType.Hunter_Gunslinger_LethalStrike,     "gd_skills2_Mordecai.Gunslinger.LethalStrike" },
			{ SkillType.Hunter_Gunslinger_RiotousRemedy,    "gd_skills2_Mordecai.Gunslinger.RiotousRemedy" },
			{ SkillType.Hunter_Gunslinger_Predator,         "gd_skills2_Mordecai.Gunslinger.Predator" },
			{ SkillType.Hunter_Gunslinger_HairTrigger,      "gd_skills2_Mordecai.Gunslinger.HairTrigger" },
			{ SkillType.Hunter_Gunslinger_Relentless,       "gd_skills2_Mordecai.Gunslinger.Relentless" }
		};

		/// <summary>
		/// Maps skill types to readable names.
		/// </summary>
		public static readonly Dictionary<SkillType, string> SkillNames = new Dictionary<SkillType, string> {
			{ SkillType.Soldier_Action_DeployScorpio,       "Deploy Scorpio" },
			{ SkillType.Hunter_Action_LaunchBloodwing,      "Launch Bloodwing" },
			{ SkillType.Hunter_Elemental_FireWing,          "Fire Wing" },
			{ SkillType.Hunter_Elemental_ShockWing,         "Shock Wing" },
			{ SkillType.Hunter_Elemental_AcidWing,          "Acid Wing" },
			{ SkillType.Hunter_Elemental_ExplosiveWing,     "Explosive Wing" },
			{ SkillType.Hunter_Adrenaline,                  "Adrenaline" },
			{ SkillType.Hunter_Sniper_Focus,                "Focus" },
			{ SkillType.Hunter_Sniper_Caliber,              "Caliber" },
			{ SkillType.Hunter_Sniper_Smirk,                "Smirk" },
			{ SkillType.Hunter_Sniper_Killer,               "Killer" },
			{ SkillType.Hunter_Sniper_Loaded,               "Loaded" },
			{ SkillType.Hunter_Sniper_CarrionCall,          "Carrion Call" },
			{ SkillType.Hunter_Sniper_Trespass,             "Trespass" },
			{ SkillType.Hunter_Rogue_SwiftStrike,           "Swift Strike" },
			{ SkillType.Hunter_Rogue_Swipe,                 "Swipe" },
			{ SkillType.Hunter_Rogue_FastHands,             "Fast Hands" },
			{ SkillType.Hunter_Rogue_OutForBlood,           "Out For Blood" },
			{ SkillType.Hunter_Rogue_AerialImpact,          "Aerial Impact" },
			{ SkillType.Hunter_Rogue_Ransack,               "Ransack" },
			{ SkillType.Hunter_Rogue_BirdOfPrey,            "Bird Of Prey" },
			{ SkillType.Hunter_Gunslinger_Deadly,           "Deadly" },
			{ SkillType.Hunter_Gunslinger_GunCrazy,         "Gun Crazy" },
			{ SkillType.Hunter_Gunslinger_LethalStrike,     "Lethal Strike" },
			{ SkillType.Hunter_Gunslinger_RiotousRemedy,    "Riotous Remedy" },
			{ SkillType.Hunter_Gunslinger_Predator,         "Predator" },
			{ SkillType.Hunter_Gunslinger_HairTrigger,      "Hair Trigger" },
			{ SkillType.Hunter_Gunslinger_Relentless,       "Relentless" }
		};
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	//? There's many more skills than this.
	public enum SkillType {
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
	}

	public class Skill {
		public string InternalName;
		public SkillType Type {
			get { return SkillDefinitions[InternalName]; }
			set { InternalName = SkillDefinitions.FirstOrDefault(x => x.Value == value).Key; }
		}
		public UInt32 Level; // This shouldn't go beyond 5.
		public Int32 Unused_Points; // Since skills and proficiencies are the same, this is a redundant field that is just 0.
		public Int32 EquippedElemental; // Seems to be -1 on all skills, and 1 on the elemental that is equipped.


		public static readonly Dictionary<string, SkillType> SkillDefinitions = new Dictionary<string, SkillType> {
			{ "gd_skills2_Roland.Action.A_DeployScorpio", SkillType.Soldier_Action_DeployScorpio },
			{ "gd_skills2_Mordecai.Action.A_LaunchBloodwing", SkillType.Hunter_Action_LaunchBloodwing },
			{ "gd_skills2_Mordecai.ActionElemental.FireWing", SkillType.Hunter_Elemental_FireWing },
			{ "gd_skills2_Mordecai.ActionElemental.ShockWing", SkillType.Hunter_Elemental_ShockWing },
			{ "gd_skills2_Mordecai.ActionElemental.AcidWing", SkillType.Hunter_Elemental_AcidWing },
			{ "gd_skills2_Mordecai.ActionElemental.ExplosiveWing", SkillType.Hunter_Elemental_ExplosiveWing },
			{ "gd_skills2_Mordecai.Adrenaline.Adrenaline_Mordecai", SkillType.Hunter_Adrenaline },
			{ "gd_skills2_Mordecai.Sniper.Focus", SkillType.Hunter_Sniper_Focus },
			{ "gd_skills2_Mordecai.Sniper.Caliber", SkillType.Hunter_Sniper_Caliber },
			{ "gd_skills2_Mordecai.Sniper.Smirk", SkillType.Hunter_Sniper_Smirk },
			{ "gd_skills2_Mordecai.Sniper.Killer", SkillType.Hunter_Sniper_Killer },
			{ "gd_skills2_Mordecai.Sniper.Loaded", SkillType.Hunter_Sniper_Loaded },
			{ "gd_skills2_Mordecai.Sniper.CarrionCall", SkillType.Hunter_Sniper_CarrionCall },
			{ "gd_skills2_Mordecai.Sniper.Trespass", SkillType.Hunter_Sniper_Trespass },
			{ "gd_skills2_Mordecai.Rogue.SwiftStrike", SkillType.Hunter_Rogue_SwiftStrike },
			{ "gd_skills2_Mordecai.Rogue.Swipe", SkillType.Hunter_Rogue_Swipe },
			{ "gd_skills2_Mordecai.Rogue.FastHands", SkillType.Hunter_Rogue_FastHands },
			{ "gd_skills2_Mordecai.Rogue.OutForBlood", SkillType.Hunter_Rogue_OutForBlood },
			{ "gd_skills2_Mordecai.Rogue.AerialImpact", SkillType.Hunter_Rogue_AerialImpact },
			{ "gd_skills2_Mordecai.Rogue.Ransack", SkillType.Hunter_Rogue_Ransack },
			{ "gd_skills2_Mordecai.Rogue.BirdOfPrey", SkillType.Hunter_Rogue_BirdOfPrey },
			{ "gd_skills2_Mordecai.Gunslinger.Deadly", SkillType.Hunter_Gunslinger_Deadly },
			{ "gd_skills2_Mordecai.Gunslinger.GunCrazy", SkillType.Hunter_Gunslinger_GunCrazy },
			{ "gd_skills2_Mordecai.Gunslinger.LethalStrike", SkillType.Hunter_Gunslinger_LethalStrike },
			{ "gd_skills2_Mordecai.Gunslinger.RiotousRemedy", SkillType.Hunter_Gunslinger_RiotousRemedy },
			{ "gd_skills2_Mordecai.Gunslinger.Predator", SkillType.Hunter_Gunslinger_Predator },
			{ "gd_skills2_Mordecai.Gunslinger.HairTrigger", SkillType.Hunter_Gunslinger_HairTrigger },
			{ "gd_skills2_Mordecai.Gunslinger.Relentless", SkillType.Hunter_Gunslinger_Relentless },
		};
	}
}

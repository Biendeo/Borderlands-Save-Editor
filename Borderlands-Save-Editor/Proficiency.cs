using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum ProficiencyType {
		Pistol,
		SMG,
		Shotgun,
		CombatRifle,
		SniperRifle,
		RocketLauncher,
		Eridian
	}

	public class Proficiency {
		public string InternalName;
		public ProficiencyType Type {
			get { return ProficiencyDefinitions[InternalName]; }
			set { InternalName = ProficiencyDefinitions.FirstOrDefault(x => x.Value == value).Key; }
		}
		public Int32 Level;
		public Int32 Points;
		public Int32 Unusued_EquippedElemental; // Always -1, seems to be a carryover from Skills.

		public static readonly Dictionary<string, ProficiencyType> ProficiencyDefinitions = new Dictionary<string, ProficiencyType> {
			{ "gd_skills_common.WeaponProficiencySkills.Skill_PistolProficiency", ProficiencyType.Pistol },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_SMGProficiency", ProficiencyType.SMG },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_ShotgunProficiency", ProficiencyType.Shotgun },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_CombatRifleProficiency", ProficiencyType.CombatRifle },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_SniperRifleProficiency", ProficiencyType.SniperRifle },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_RocketLauncherProficiency", ProficiencyType.RocketLauncher },
			{ "gd_skills_common.WeaponProficiencySkills.Skill_EridanWeaponProficiency", ProficiencyType.Eridian }
		};

		public static readonly Int32[] PointsToLevel = { 0, 2400, 7200, 14400, 24000, 36000, 50400, 67200, 86400, 108000, 132000, 158400, 187200, 218400, 252000, 288000, 326400, 367200, 410400, 456000, 504000, 554400, 607200, 662400, 720000, 780000, 842400, 907200, 974400, 1044000, 1116000, 1190400, 1267200, 1346400, 1428000, 1512000, 1598400, 1687200, 1778400, 1872000, 1968000, 2066400, 2167200, 2270400, 2376000, 2484000, 2594400, 2707200, 2822400, 2940000, 3060000 };
	}
}

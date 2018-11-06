using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Skill {
	/// <summary>
	/// A container that holds all the skills (not including proficiencies) for a character.
	/// </summary>
	public class CharacterSkills {
		/// <summary>
		/// Constructs a container for a character's skills, defaulting every skill to deactivated.
		/// </summary>
		public CharacterSkills() {
			skills = new Dictionary<SkillType, Skill>();
			foreach (SkillType type in Enum.GetValues(typeof(SkillType))) {
				Skill newSkill = new Skill {
					Type = type
				};
				skills.Add(type, newSkill);
			}
		}

		/// <summary>
		/// Reads a single skill from a reader of the Borderlands save format.
		/// </summary>
		/// <param name="reader"></param>
		public void ReadSkill(BinaryReader reader) {
			Skill skill = GetSkill(SkillTypeExtensions.FromInternalName(reader.BL_ReadString()));
			skill.Activated = true;
			skill.Level = reader.ReadUInt32();
			skill.Unused_Points = reader.ReadInt32();
			skill.EquippedElemental = reader.ReadInt32();
		}

		/// <summary>
		/// Writes all activated skills to the given writer.
		/// </summary>
		/// <param name="writer"></param>
		public void WriteAllSkills(BinaryWriter writer) {
			foreach (var skillPair in skills) {
				if (skillPair.Value.Activated) {
					skillPair.Value.Write(writer);
				}
			}
		}

		/// <summary>
		/// Gets a skill of a given type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Skill GetSkill(SkillType type) {
			return skills[type];
		}

		/// <summary>
		/// Returns how many skills are currently active on this save file.
		/// </summary>
		public int ActiveCount => skills.Count(kv => kv.Value.Activated);

		private Dictionary<SkillType, Skill> skills;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Skill {
	/// <summary>
	/// Contains details about a character's properties of a skill.
	/// </summary>
	public class Skill {
		/// <summary>
		/// Constructs a skill with default properties.
		/// </summary>
		public Skill() {
			Type = SkillType.Soldier_Action_DeployScorpio;
			Level = 0;
			Unused_Points = 0;
			EquippedElemental = 0;
			Activated = false;
		}

		/// <summary>
		/// Reads in a skill from a reader on a save file.
		/// </summary>
		/// <param name="reader"></param>
		public Skill(BinaryReader reader) {
			InternalName = reader.BL_ReadString();
			Level = reader.ReadUInt32();
			Unused_Points = reader.ReadInt32();
			EquippedElemental = reader.ReadInt32();
			Activated = true;
		}

		/// <summary>
		/// Writes this skill to a writer for the save file.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.BL_WriteString(InternalName);
			writer.Write(Level);
			writer.Write(Unused_Points);
			writer.Write(EquippedElemental);
		}

		/// <summary>
		/// The internal name for this skill used for the save file. It is preferable to modify
		/// <see cref="Type"/> instead.
		/// </summary>
		public string InternalName {
			get { return Type.InternalName(); }
			set { Type = SkillTypeExtensions.FromInternalName(value); }
		}

		/// <summary>
		/// The type of this skill.
		/// </summary>
		public SkillType Type { get; set; }
		
		/// <summary>
		/// The level this skill has been upgraded to. This must always be between 0 and 5
		/// inclusive, since skills cannot go beyond this level.
		/// </summary>
		public UInt32 Level { get; set; }

		/// <summary>
		/// Skills and proficiencies share the same fields. This field is used by proficiencies to
		/// store the number of points associated with it. For skills, it is simply always 0.
		/// </summary>
		public Int32 Unused_Points { get; set; }

		/// <summary>
		/// Some skills apply an elemental effect to the primary skill of a character. For those
		/// skills, this is either 0 or 1 whether it is the currently equipped elemental. For
		/// every other skill, it is set to -1.
		/// </summary>
		public Int32 EquippedElemental { get; set; }

		/// <summary>
		/// Sets whether the skill is active or not in the save file. If it is false, it will not
		/// be written to the file and will assumed to be level 0 in the game.
		/// </summary>
		public bool Activated { get; set; }
	}
}

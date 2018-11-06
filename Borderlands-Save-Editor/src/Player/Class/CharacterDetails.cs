using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Class {
	/// <summary>
	/// Contains properties about the character themselves.
	/// </summary>
	public class CharacterDetails {
		/// <summary>
		/// Constructs a details object containing properties that the player would have at the
		/// start.
		/// </summary>
		public CharacterDetails() {
			ClassType = CharacterClassType.Soldier;
			Level = 1;
			Experience = 0;
			SkillPoints = 0;
			UnknownVariable = 0;
			Money = 0;
			Interal_PlaythroughTwoUnlocked = 0;
		}

		/// <summary>
		/// Constructs the character's details given a reader at the correct position.
		/// </summary>
		/// <param name="reader"></param>
		public CharacterDetails(BinaryReader reader) {
			InternalName = reader.BL_ReadString();
			Level = reader.ReadInt32();
			Experience = reader.ReadInt32();
			SkillPoints = reader.ReadInt32();
			UnknownVariable = reader.ReadInt32();
			Money = reader.ReadUInt32();
			Interal_PlaythroughTwoUnlocked = reader.ReadInt32();
		}

		/// <summary>
		/// Writes the character's details with the given writer.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.BL_WriteString(InternalName);
			writer.Write(Level);
			writer.Write(Experience);
			writer.Write(SkillPoints);
			writer.Write(UnknownVariable);
			writer.Write(Money);
			writer.Write(Interal_PlaythroughTwoUnlocked);
		}

		/// <summary>
		/// The internal name for the character's <see cref="CharacterClassType"/>. It is preferable
		/// to use <see cref="ClassType"/> instead.
		/// </summary>
		public string InternalName {
			get { return ClassType.InternalName(); }
			set { ClassType = CharacterClassTypeExtensions.FromInternalName(value); }
		}

		/// <summary>
		/// The class type used by this character.
		/// </summary>
		public CharacterClassType ClassType { get; set; }

		/// <summary>
		/// The character's level.
		/// </summary>
		public Int32 Level { get; set; }

		/// <summary>
		/// The character's total experience points.
		/// </summary>
		public Int32 Experience { get; set; }

		/// <summary>
		/// The character's available skill points left to spend.
		/// </summary>
		public Int32 SkillPoints { get; set; }

		/// <summary>
		/// An unknown variable; not too sure what this is used for.
		/// </summary>
		public Int32 UnknownVariable { get; set; }

		/// <summary>
		/// The amount of money this player is carrying.
		/// </summary>
		public UInt32 Money { get; set; }

		/// <summary>
		/// Whether the player is able to access playthrough two.
		/// </summary>
		public bool PlaythroughTwoUnlocked { get; set; }

		/// <summary>
		/// The internal field used to determine whether the player can access playthrough two or
		/// not. This should only be 1 or 0. It is preferable to use
		/// <see cref="PlaythroughTwoUnlocked"/>.
		/// </summary>
		public Int32 Interal_PlaythroughTwoUnlocked {
			get { return PlaythroughTwoUnlocked ? 1 : 0; }
			set { PlaythroughTwoUnlocked = value == 1 ? true : false; }
		}
	}
}

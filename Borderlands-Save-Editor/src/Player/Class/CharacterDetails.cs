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
			TotalExperience = 0;
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
			TotalExperience = reader.ReadInt32();
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
			writer.Write(TotalExperience);
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
		/// Returns the total number of experience points to reach a certain level. It does not take
		/// into account the current number of experience points.
		/// Return 0 if the level is less than 0, or the amount for the maximum level if too high.
		/// 
		/// TODO: I think experience keeps going when you hit level 69.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public static Int32 TotalExperienceToLevel(Int32 level) {
			if (level <= 0) {
				return 0;
			} else if (level > MaximumLevel) {
				return TotalExperienceToLevels.Last();
			} else {
				return TotalExperienceToLevels[level];
			}
		}

		/// <summary>
		/// Returns the number of experience points required to achieve a certain level. It is
		/// simply the difference of the total amount required for the two levels.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public static Int32 ExperienceToLevel(Int32 level) {
			return TotalExperienceToLevel(level) - TotalExperienceToLevel(level - 1);
		}

		/// <summary>
		/// The character's level.
		/// </summary>
		public Int32 Level { get; set; }

		/// <summary>
		/// The character's current experience for their current level.
		/// </summary>
		public Int32 LevelExperience { get; set; }

		/// <summary>
		/// The character's total experience points.
		/// </summary>
		public Int32 TotalExperience {
			get { return LevelExperience + TotalExperienceToLevel(Level); }
			set {
				Level = LevelFromTotalExperience(value);
				LevelExperience = value - TotalExperienceToLevel(Level);
			}
		}

		/// <summary>
		/// The amount of experience required to progress this current level.
		/// </summary>
		public Int32 ExperienceForNextLevel { get { return ExperienceToLevel(Level + 1); } }

		/// <summary>
		/// The total amount of experience required to progress this current level.
		/// </summary>
		public Int32 TotalExperienceForNextLevel { get { return TotalExperienceToLevel(Level + 1); } }

		/// <summary>
		/// The total number of experience points required for the maximum level.
		/// </summary>
		public static Int32 TotalExperienceForMaximumLevel { get { return TotalExperienceToLevel(MaximumLevel); } }

		/// <summary>
		/// Returns the total amount of experience acquired given a current level and the number of
		/// experience points in that level.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Int32 TotalExperienceFromLevelExperience(UInt32 level, Int32 points) {
			if (level > MaximumLevel) {
				level = MaximumLevel;
			}
			return TotalExperienceToLevels[level] + points;
		}

		/// <summary>
		/// Returns the current level given a total number of experience. This is clamped between 0
		/// and 69 inclusive.
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Int32 LevelFromTotalExperience(Int32 points) {
			Int32 currentLevel = 0;
			while (currentLevel < MaximumLevel && TotalExperienceToLevels[currentLevel + 1] <= points) {
				++currentLevel;
			}
			return currentLevel;
		}

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

		/// <summary>
		/// Notes the amount oof experience to reach the given level of the index. Because the
		/// player starts at level 1 with 0XP, index 1 is also 0.
		/// 
		/// TODO: Could this be its own class similar to <see cref="Proficiency"/>?
		/// </summary>
		public static readonly Int32[] TotalExperienceToLevels = { 0, 0, 358, 1241, 2850, 5376, 8997, 13886, 20208, 28126, 37798, 49377, 63016, 78861, 97061, 117757, 141092, 167206, 196238, 228322, 263595, 302190, 344238, 389873, 439222, 492414, 549578, 610840, 676325, 746158, 820463, 899363, 982980, 1071435, 1164850, 1263343, 1367034, 1476041, 1590483, 1710476, 1836137, 1967582, 2104926, 2248285, 2397772, 2553501, 2715586, 2884139, 3059273, 3241098, 3429728, 3625271, 3827840, 4037543, 4254491, 4478792, 4710556, 4949890, 5196902, 5451701, 5714393, 5985086, 6263885, 6550897, 6846227, 7149982, 7462266, 7783184, 8112840, 8451340, 8798786, 9155282, 9520932 };

		/// <summary>
		/// The maximum achievable level for the character.
		/// </summary>
		public const Int32 MaximumLevel = 69;
	}
}

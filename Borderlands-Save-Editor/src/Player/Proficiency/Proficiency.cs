using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Proficiency {
	/// <summary>
	/// Handles all the properties of a weapon type's proficiency counter.
	/// </summary>
	public class Proficiency {
		/// <summary>
		/// Constructs a default proficiency, assuming the pistol at level 0 with no points.
		/// </summary>
		public Proficiency() {
			Type = ProficiencyType.Pistol;
			Level = 0;
			Points = 0;
			Unused_EquippedElemental = -1;
			Activated = false;
		}

		/// <summary>
		/// Reads in a proficiency given a reader at the start of the block.
		/// </summary>
		/// <param name="reader"></param>
		public Proficiency(BinaryReader reader) {
			InternalName = reader.BL_ReadString();
			Level = reader.ReadUInt32();
			Points = reader.ReadInt32();
			Unused_EquippedElemental = reader.ReadInt32();
			Activated = true;
		}

		/// <summary>
		/// Writes a proficiency to a stream in the same format as a Borderlands 1 save file.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.BL_WriteString(InternalName);
			writer.Write(Level);
			writer.Write(Points);
			writer.Write(Unused_EquippedElemental);
		}

		/// <summary>
		/// Returns the total number of experience points to reach a certain level. It does not take
		/// into account the current number of experience points.
		/// Returns 0 if the level is less than 0, or the amount for the maximum level if too high.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public static Int32 TotalPointsToLevel(UInt32 level) {
			if (level <= 0) {
				return 0;
			} else if (level > MaximumLevel) {
				return TotalExperiencePointsToLevel.Last();
			} else {
				return TotalExperiencePointsToLevel[level];
			}
		}

		/// <summary>
		/// Returns the number of experience points required to achieve a certain level. It is
		/// simply the difference of the total amount required for the two levels.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public static Int32 PointsToLevel(UInt32 level) {
			return TotalPointsToLevel(level) - TotalPointsToLevel(level - 1);
		}

		/// <summary>
		/// The name used in the save file associated with this proficiency.
		/// It is preferable to use <see cref="Type"/> instead of this to reduce errors.
		/// </summary>
		public string InternalName {
			get { return Type.InternalName(); }
			set { Type = ProficiencyTypeExtensions.FromInternalName(value); }
		}

		/// <summary>
		/// The weapon type associated with this proficiency.
		/// </summary>
		public ProficiencyType Type { get; set; }

		/// <summary>
		/// The current level of this weapon proficiency. Starts at 0 until it reaches 50.
		/// </summary>
		public UInt32 Level { get; set; }

		/// <summary>
		/// The current number of points associated with this proficiency for its current level.
		/// This resets to 0 at the beginning of every level.
		/// </summary>
		public Int32 Points { get; set; }

		/// <summary>
		/// The total number of points associated with this proficiency. This does not reset on a
		/// level up.
		/// </summary>
		public Int32 TotalPoints {
			get { return Points + TotalPointsToLevel(Level); }
			set {
				Level = LevelFromTotalPoints(value);
				Points = value - TotalExperiencePointsToLevel[Level];
			}
		}

		/// <summary>
		/// The number of points required for the current level of this proficiency. Shortcuts the
		/// argument for <see cref="PointsToLevel(uint)"/>.
		/// </summary>
		public Int32 PointsForNextLevel { get { return PointsToLevel(Level + 1); } }

		/// <summary>
		/// The total number of points required for the current level of this proficiency. Shortcuts
		/// the argument for <see cref="TotalPointsToLevel(uint)"/>.
		/// </summary>
		public Int32 TotalPointsForNextLevel { get { return TotalPointsToLevel(Level + 1); } }
		
		/// <summary>
		/// The total number of experience points required for the maximum level of this
		/// proficiency.
		/// </summary>
		public static Int32 TotalPointsForMaximumLevel { get { return TotalPointsToLevel(MaximumLevel); } }

		/// <summary>
		/// Returns the total amount of points acquired given a current level and the number of
		/// points in that level.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Int32 TotalPointsFromLevelPoints(UInt32 level, Int32 points) {
			if (level > MaximumLevel) {
				level = MaximumLevel;
			}
			return TotalExperiencePointsToLevel[level] + points;
		}

		/// <summary>
		/// Returns the current level given a total number of points. This is clamped between 0 and
		/// 50 inclusive.
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static UInt32 LevelFromTotalPoints(Int32 points) {
			UInt32 currentLevel = 0u;
			while (currentLevel < MaximumLevel && TotalExperiencePointsToLevel[currentLevel + 1] <= points) {
				++currentLevel;
			}
			return currentLevel;
		}

		/// <summary>
		/// Proficiencies have the same internal structure as skills. This slot was used to detect
		/// the equipped elemental effect of the player class' skill. For proficiencies, it is
		/// always set to -1.
		/// </summary>
		public Int32 Unused_EquippedElemental { get; set; } // Always -1, seems to be a carryover from Skills.

		/// <summary>
		/// Determines whether this proficiency appears in the save file or not. In-game, all
		/// proficiencies with zero points are by default not activated. Unactivated proficiencies
		/// will not be written to save files!
		/// </summary>
		public bool Activated { get; set; } // This is just to note whether this is active in the save file.

		/// <summary>
		/// A list of the total number of points required to reach each level.
		/// </summary>
		public static readonly Int32[] TotalExperiencePointsToLevel = { 0, 2400, 7200, 14400, 24000, 36000, 50400, 67200, 86400, 108000, 132000, 158400, 187200, 218400, 252000, 288000, 326400, 367200, 410400, 456000, 504000, 554400, 607200, 662400, 720000, 780000, 842400, 907200, 974400, 1044000, 1116000, 1190400, 1267200, 1346400, 1428000, 1512000, 1598400, 1687200, 1778400, 1872000, 1968000, 2066400, 2167200, 2270400, 2376000, 2484000, 2594400, 2707200, 2822400, 2940000, 3060000 };

		/// <summary>
		/// The maximum achievable level for a proficiency.
		/// </summary>
		public const UInt32 MaximumLevel = 50;
	}
}

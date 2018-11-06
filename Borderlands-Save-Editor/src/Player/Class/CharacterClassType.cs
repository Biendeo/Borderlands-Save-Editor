using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Class {
	/// <summary>
	/// Describes the character class for a player.
	/// </summary>
	public enum CharacterClassType {
		/// <summary>
		/// Roland, the Soldier class.
		/// </summary>
		Soldier,
		/// <summary>
		/// Mordecai, the Hunter class.
		/// </summary>
		Hunter,
		/// <summary>
		/// Lilith, the Siren class.
		/// </summary>
		Siren,
		/// <summary>
		/// Brick the Berserker class.
		/// </summary>
		Berserker
	}

	/// <summary>
	/// Extension methods for the character class.
	/// </summary>
	public static class CharacterClassTypeExtensions {
		/// <summary>
		/// Returns a readable string representing the class type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ToString(this CharacterClassType type) {
			return type.ClassName();
		}

		/// <summary>
		/// Returns a <see cref="CharacterClassType"/> from an internal name used by a save.
		/// </summary>
		/// <param name="internalName"></param>
		/// <returns></returns>
		public static CharacterClassType FromInternalName(string internalName) {
			return InternalNames.FirstOrDefault(x => x.Value == internalName).Key;
		}

		/// <summary>
		/// Returns the internal name of this character class.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string InternalName(this CharacterClassType type) {
			return InternalNames[type];
		}

		/// <summary>
		/// Returns a readable name for this class type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ClassName(this CharacterClassType type) {
			return ClassNames[type];
		}

		/// <summary>
		/// Returns the character's name for this class type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string CharacterName(this CharacterClassType type) {
			return CharacterNames[type];
		}

		/// <summary>
		/// Maps <see cref="CharacterClassType"/> to internal name.
		/// </summary>
		public static readonly Dictionary<CharacterClassType, string> InternalNames = new Dictionary<CharacterClassType, string> {
			{ CharacterClassType.Soldier, "gd_Roland.Character.CharacterClass_Roland" },
			{ CharacterClassType.Hunter, "gd_mordecai.Character.CharacterClass_Mordecai" },
			{ CharacterClassType.Siren, "gd_lilith.Character.CharacterClass_Lilith" },
			{ CharacterClassType.Berserker, "gd_brick.Character.CharacterClass_Brick" }
		};

		/// <summary>
		/// Maps <see cref="CharacterClassType"/> to their class name.
		/// </summary>
		public static readonly Dictionary<CharacterClassType, string> ClassNames = new Dictionary<CharacterClassType, string> {
			{ CharacterClassType.Soldier, "Soldier" },
			{ CharacterClassType.Hunter, "Hunter" },
			{ CharacterClassType.Siren, "Siren" },
			{ CharacterClassType.Berserker, "Berserker" }
		};

		/// <summary>
		/// Maps <see cref="CharacterClassType"/> to their character name.
		/// </summary>
		public static readonly Dictionary<CharacterClassType, string> CharacterNames = new Dictionary<CharacterClassType, string> {
			{ CharacterClassType.Soldier, "Roland" },
			{ CharacterClassType.Hunter, "Mordecai" },
			{ CharacterClassType.Siren, "Lilith" },
			{ CharacterClassType.Berserker, "Brick" }
		};
	}
}

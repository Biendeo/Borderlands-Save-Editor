using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum CharacterClassType {
		Soldier,
		Hunter,
		Siren,
		Berserker
	}

	public class CharacterClass {
		public CharacterClass(string internalName) {
			InternalName = internalName;
		}

		public string InternalName;
		public CharacterClassType Type {
			get { return ClassDefintions[InternalName]; }
			set { InternalName = ClassDefintions.FirstOrDefault(x => x.Value == value).Key; }
		}

		public static readonly Dictionary<string, CharacterClassType> ClassDefintions = new Dictionary<string, CharacterClassType> {
			{ "gd_Roland.Character.CharacterClass_Roland", CharacterClassType.Soldier },
			{ "gd_mordecai.Character.CharacterClass_Mordecai", CharacterClassType.Hunter },
			{ "gd_lilith.Character.CharacterClass_Lilith", CharacterClassType.Siren },
			{ "gd_brick.Character.CharacterClass_Brick", CharacterClassType.Berserker }
		};
	}
}

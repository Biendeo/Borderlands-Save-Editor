﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Player.Proficiency {
	/// <summary>
	/// A container that holds all the proficiencies used by a character.
	/// </summary>
	public class CharacterProficiencies {
		/// <summary>
		/// Constructs a new container that defaults all the proficiencies to 0 points.
		/// </summary>
		public CharacterProficiencies() {
			proficiencies = new Dictionary<ProficiencyType, Proficiency>();
			foreach (ProficiencyType type in Enum.GetValues(typeof(ProficiencyType))) {
				Proficiency newProficiency = new Proficiency() {
					Type = type
				};
				proficiencies.Add(type, newProficiency);
			}
		}

		/// <summary>
		/// Reads in a single proficiency from a reader.
		/// </summary>
		/// <param name="reader"></param>
		public void ReadProficiency(BinaryReader reader) {
			Proficiency proficiency = GetProficiency(ProficiencyTypeExtensions.FromInternalName(reader.BL_ReadString()));
			proficiency.Activated = true;
			proficiency.Level = reader.ReadUInt32();
			proficiency.Points = reader.ReadInt32();
			proficiency.Unused_EquippedElemental = reader.ReadInt32();
		}

		/// <summary>
		/// Writes all the proficiencies stored to the given writer.
		/// </summary>
		/// <param name="writer"></param>
		public void WriteAllProficiencies(BinaryWriter writer) {
			foreach (var proficiencyPair in proficiencies) {
				if (proficiencyPair.Value.Activated) {
					proficiencyPair.Value.Write(writer);
				}
			}
		}

		/// <summary>
		/// Returns a specific proficiency given its type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Proficiency GetProficiency(ProficiencyType type) {
			return proficiencies[type];
		}

		/// <summary>
		/// Returns the pistol proficiency.
		/// </summary>
		public Proficiency Pistol { get { return GetProficiency(ProficiencyType.Pistol); } }

		/// <summary>
		/// Returns the SMG proficiency.
		/// </summary>
		public Proficiency SMG { get { return GetProficiency(ProficiencyType.SMG); } }

		/// <summary>
		/// Returns the shotgun proficiency.
		/// </summary>
		public Proficiency Shotgun { get { return GetProficiency(ProficiencyType.Shotgun); } }

		/// <summary>
		/// Returns the combat rifle proficiency.
		/// </summary>
		public Proficiency CombatRifle { get { return GetProficiency(ProficiencyType.CombatRifle); } }

		/// <summary>
		/// Returns the sniper rifle proficiency.
		/// </summary>
		public Proficiency SniperRifle { get { return GetProficiency(ProficiencyType.SniperRifle); } }

		/// <summary>
		/// Returns the rocket launcher proficiency.
		/// </summary>
		public Proficiency RocketLauncher { get { return GetProficiency(ProficiencyType.RocketLauncher); } }

		/// <summary>
		/// Returns the Eridian weapon proficiency.
		/// </summary>
		public Proficiency Eridian { get { return GetProficiency(ProficiencyType.Eridian); } }

		/// <summary>
		/// Returns how many proficiencies are active.
		/// </summary>
		public int ActiveCount => proficiencies.Count(kv => kv.Value.Activated);

		private Dictionary<ProficiencyType, Proficiency> proficiencies;
	}
}

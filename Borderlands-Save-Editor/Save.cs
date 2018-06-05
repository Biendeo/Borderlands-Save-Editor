using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public class Save {
		public Save() {
			Class = new CharacterClass("?");
			Proficiencies = new Dictionary<ProficiencyType, Proficiency>();
			foreach (var pair in Proficiency.ProficiencyDefinitions) {
				Proficiencies.Add(pair.Value, new Proficiency());
			}
			/*
			Skills = new Dictionary<SkillType, Skill>();
			foreach (var pair in Skill.SkillDefintions) {
				Skills.Add(pair.Value, new Skill());
			}
			*/
			Skills = new List<Skill>();
			Ammo = new Dictionary<AmmoType, AmmoPool>();
			foreach (var pair in AmmoPool.AmmoDefinitions) {
				Ammo.Add(pair.Value, new AmmoPool());
			}
			Items = new List<Item>();
			Weapons = new List<Weapon>();
			StatsBuffer = new byte[0];
			Stats = new Dictionary<StatID, Stat>();
			StatTable = new StatsTable();
			LocationsVisited = new List<string>();
			CurrentLocation = "?";
			Playthroughs = new List<Playthrough>();
			UnknownVariable14 = new List<Int32>();
			UnknownVariable15 = new List<Int32>();
			EchoPlaythroughs = new List<EchoPlaythrough>();
			UnknownVariable16 = new byte[0];
		}

		public static Save Read(string filePath) {
			Save save = new Save();

			byte[] file = File.ReadAllBytes(filePath);
			MemoryStream reader = new MemoryStream(file);

			// The file starts with a 15 byte header. Let's match this.
			//? This may be a version number too, but honestly everyone should be on the newest
			//? version by now.
			byte[] detectedHeader = ReadByteArray(reader, Header.Length);
			if (detectedHeader.Intersect(Header).Count() != detectedHeader.Union(Header).Count()) {
				Console.Error.WriteLine("This is not a valid save file!");
				return save;
			}

			// The file starts with the character class internal name.
			save.Class.InternalName = ReadString(reader);

			// Next is some stats.
			save.Level = ReadInt32(reader);
			save.Experience = ReadInt32(reader);
			save.SkillPoints = ReadInt32(reader);
			save.UnknownVariable1 = ReadInt32(reader);
			save.Money = ReadUInt32(reader);
			save.PlaythroughTwoUnlocked = ReadInt32(reader);

			// Now we've got skills and proficiencies to count. Borderlands made them the same
			// thing, but I'll make them separate.
			Int32 skillCount = ReadInt32(reader);
			for (Int32 x = 0; x < skillCount; ++x) {
				string skillName = ReadString(reader);
				// Check whether the skill is a proficiency.
				if (skillName.IndexOf("gd_skills_") == 0) {
					// In this case, it's a proficiency.
					ProficiencyType type = Proficiency.ProficiencyDefinitions[skillName];
					save.Proficiencies[type].InternalName = skillName;
					save.Proficiencies[type].Level = ReadInt32(reader);
					save.Proficiencies[type].Points = ReadInt32(reader);
					save.Proficiencies[type].Unusued_EquippedElemental = ReadInt32(reader);
				} else {
					Skill skill = new Skill();
					save.Skills.Add(skill);
					skill.InternalName = skillName;
					skill.Level = ReadUInt32(reader);
					skill.Unused_Points = ReadInt32(reader);
					skill.EquippedElemental = ReadInt32(reader);
				}
			}

			// Then there's stuff that no one knows.
			save.UnknownVariable2 = ReadInt32(reader);
			save.UnknownVariable3 = ReadInt32(reader);
			save.UnknownVariable4 = ReadInt32(reader);
			save.UnknownVariable5 = ReadInt32(reader);

			// Now we've got the ammo types.
			Int32 ammoCount = ReadInt32(reader);
			for (Int32 x = 0; x < ammoCount; ++x) {
				string ammoName = ReadString(reader);
				AmmoType type = AmmoPool.AmmoDefinitions[ammoName];
				save.Ammo[type].InternalName = ammoName;

				//? A sanity check to see if the pool name matches?
				if (save.Ammo[type].PoolInternalName != ReadString(reader)) {
					// Panic.
					Console.Error.WriteLine($"Tried to match expected ammo type {save.Ammo[type].PoolInternalName} but got something else!");
				}

				save.Ammo[type].Ammo = ReadFloat(reader);
				save.Ammo[type].UpgradeLevel = ReadInt32(reader);
			}

			// Now the items.
			Int32 itemCount = ReadInt32(reader);
			for (Int32 x = 0; x < itemCount; ++x) {
				Item item = new Item();
				save.Items.Add(item);
				item.InternalItemCategory = ReadString(reader);
				item.InternalItemType = ReadString(reader);
				item.InternalItemBody = ReadString(reader);
				item.InternalItemLeft = ReadString(reader);
				item.InternalItemRight = ReadString(reader);
				item.InternalItemMaterial = ReadString(reader);
				item.InternalItemManufacturer = ReadString(reader);
				item.InternalItemPrefix = ReadString(reader);
				item.InternalItemTitle = ReadString(reader);
				item.UnknownVariable1 = ReadInt32(reader);
				item.UnknownVariable2 = ReadInt32(reader);
				item.Equipped = ReadInt32(reader);
			}

			// Some slot counters.
			save.BackpackSlots = ReadInt32(reader);
			save.WeaponSlots = ReadInt32(reader);

			// Now the weapons.
			Int32 weaponCount = ReadInt32(reader);
			for (Int32 x = 0; x < weaponCount; ++x) {
				Weapon weapon = new Weapon();
				save.Weapons.Add(weapon);
				weapon.InternalWeaponCategory = ReadString(reader);
				weapon.InternalWeaponManufacturer = ReadString(reader);
				weapon.InternalWeaponType = ReadString(reader);
				weapon.InternalWeaponBody = ReadString(reader);
				weapon.InternalWeaponGrip = ReadString(reader);
				weapon.InternalWeaponMag = ReadString(reader);
				weapon.InternalWeaponBarrel = ReadString(reader);
				weapon.InternalWeaponSight = ReadString(reader);
				weapon.InternalWeaponStock = ReadString(reader);
				weapon.InternalWeaponAction = ReadString(reader);
				weapon.InternalWeaponAccessory = ReadString(reader);
				weapon.InternalWeaponMaterial = ReadString(reader);
				weapon.InternalWeaponPrefix = ReadString(reader);
				weapon.InternalWeaponTitle = ReadString(reader);
				weapon.UnknownVariable1 = ReadInt32(reader);
				weapon.UnknownVariable2 = ReadInt32(reader);
				weapon.EquippedSlot = ReadInt32(reader);
			}

			// Some stats?
			Int32 statLength = ReadInt32(reader);
			save.StatsBuffer = ReadByteArray(reader, statLength);
			save.StatTable.UnknownVariable1 = BitConverter.ToInt32(save.StatsBuffer, 0);
			save.StatTable.TotalBytesSize = BitConverter.ToInt32(save.StatsBuffer, 4);
			save.StatTable.TotalEntries = BitConverter.ToInt16(save.StatsBuffer, 8);

			for (Int16 x = 0; x < save.StatTable.TotalEntries; ++x) {
				Stat stat = new Stat();
				save.StatTable.Stats.Add(stat);
				stat.ID = save.StatsBuffer[10 + 7 * x];
				stat.UnknownVariable1 = save.StatsBuffer[11 + 7 * x];
				stat.UnknownVariable2 = save.StatsBuffer[12 + 7 * x];
				stat.Value = BitConverter.ToUInt32(save.StatsBuffer, 13 + 7 * x);
				//? Once I know the values, there shoudln't be a try/catch here.
				//? This slows down the program a lot while there's exceptions.
				if (Stat.StatDefinitions.ContainsKey(stat.ID)) {
					save.Stats.Add(Stat.StatDefinitions[stat.ID], stat);
				}
			}

			// Visited locations.

			Int32 locationsLength = ReadInt32(reader);
			for (Int32 x = 0; x < locationsLength; ++x) {
				save.LocationsVisited.Add(ReadString(reader));
			}

			save.CurrentLocation = ReadString(reader);

			// Some unknown variables.
			// I dunno what this is.
			save.UnknownVariable6 = ReadInt32(reader);
			save.UnknownVariable7 = ReadInt32(reader);
			save.UnknownVariable8 = ReadString(reader);
			save.UnknownVariable9 = ReadInt32(reader);
			save.UnknownVariable10 = ReadInt32(reader);
			save.UnknownVariable11 = ReadInt32(reader);
			save.UnknownVariable12 = ReadInt32(reader);
			ReadUInt32(reader);

			Int32 playthroughCount = ReadInt32(reader);
			for (Int32 x = 0; x < playthroughCount; ++x) {
				Playthrough playthrough = new Playthrough();
				save.Playthroughs.Add(playthrough);
				playthrough.Number = ReadInt32(reader);
				playthrough.ActiveMissionName = ReadString(reader);
				Int32 missionCount = ReadInt32(reader);
				for (Int32 y = 0; y < missionCount; ++y) {
					Mission mission = new Mission();
					playthrough.Missions.Add(mission);
					mission.InternalName = ReadString(reader);
					mission.MissionStatusFlag = ReadInt32(reader);
					mission.UnknownVariable2 = ReadInt32(reader);
					mission.UnknownVariable3 = ReadInt32(reader);
					Int32 detailsCount = ReadInt32(reader);
					for (Int32 z = 0; z < detailsCount; ++z) {
						Mission.MissionDetails detail = new Mission.MissionDetails();
						mission.Details.Add(detail);
						detail.UnknownString = ReadString(reader);
						detail.UnknownVariable = ReadInt32(reader);
					}
				}
			}

			save.PlayTimeSeconds = ReadInt32(reader);
			save.SaveTimeString = ReadString(reader);
			save.Name = ReadString(reader);
			save.Color1 = ReadUInt32(reader);
			save.Color2 = ReadUInt32(reader);
			save.Color3 = ReadUInt32(reader);

			save.UnknownVariable13 = ReadInt32(reader);
			Int32 unknown14Count = ReadInt32(reader);
			for (Int32 x = 0; x < unknown14Count; ++x) {
				save.UnknownVariable14.Add(ReadInt32(reader));
			}
			Int32 unknown15Count = ReadInt32(reader);
			for (Int32 x = 0; x < unknown15Count; ++x) {
				save.UnknownVariable15.Add(ReadInt32(reader));
			}

			Int32 echoPlaythroughCount = ReadInt32(reader);
			for (Int32 x = 0; x < echoPlaythroughCount; ++x) {
				EchoPlaythrough echoPlaythrough = new EchoPlaythrough();
				save.EchoPlaythroughs.Add(echoPlaythrough);
				echoPlaythrough.Playthrough = ReadInt32(reader);
				Int32 echoCount = ReadInt32(reader);
				for (Int32 y = 0; y < echoCount; ++y) {
					Echo echo = new Echo();
					echoPlaythrough.Echoes.Add(echo);
					echo.InternalName = ReadString(reader);
					echo.UnknownVariable1 = ReadInt32(reader);
					echo.UnknownVariable2 = ReadInt32(reader);
				}
			}

			//? This seems to be really short, except on my finished save file. Is this banked
			//? data?
			Int32 unknown16Length = ReadInt32(reader);
			save.UnknownVariable16 = ReadByteArray(reader, unknown16Length);

			return save;
		}

		public void Write(string filePath) {
			// The output file is indicated by the save number.
			byte[] testArray = new byte[50000];
			BinaryWriter writer = new BinaryWriter(File.OpenWrite(filePath));
			MemoryStream stream = new MemoryStream(testArray);
			//BinaryWriter writer = new BinaryWriter(stream);

			// Firstly, write the header.
			WriteByteArray(writer, Header);

			// Start with the character class's internal name.
			WriteString(writer, Class.InternalName);

			// Then some stats.
			WriteInt32(writer, Level);
			WriteInt32(writer, Experience);
			WriteInt32(writer, SkillPoints);
			WriteInt32(writer, UnknownVariable1);
			WriteUInt32(writer, Money);
			WriteInt32(writer, PlaythroughTwoUnlocked);

			// Now the skills.
			WriteInt32(writer, Proficiencies.Count + Skills.Count);
			foreach (var proficiency in Proficiencies) {
				WriteString(writer, proficiency.Value.InternalName);
				WriteInt32(writer, proficiency.Value.Level);
				WriteInt32(writer, proficiency.Value.Points);
				WriteInt32(writer, proficiency.Value.Unusued_EquippedElemental);
			}
			foreach (var skill in Skills) {
				WriteString(writer, skill.InternalName);
				WriteUInt32(writer, skill.Level);
				WriteInt32(writer, skill.Unused_Points);
				WriteInt32(writer, skill.EquippedElemental);
			}

			// Some unknown stuff.
			WriteInt32(writer, UnknownVariable2);
			WriteInt32(writer, UnknownVariable3);
			WriteInt32(writer, UnknownVariable4);
			WriteInt32(writer, UnknownVariable5);

			// Ammo.
			WriteInt32(writer, Ammo.Count);
			foreach (var ammo in Ammo) {
				WriteString(writer, ammo.Value.InternalName);
				WriteString(writer, ammo.Value.PoolInternalName);
				WriteFloat(writer, ammo.Value.Ammo);
				WriteInt32(writer, ammo.Value.UpgradeLevel);
			}

			// Items.
			WriteInt32(writer, Items.Count);
			foreach (var item in Items) {
				WriteString(writer, item.InternalItemCategory);
				WriteString(writer, item.InternalItemType);
				WriteString(writer, item.InternalItemBody);
				WriteString(writer, item.InternalItemLeft);
				WriteString(writer, item.InternalItemRight);
				WriteString(writer, item.InternalItemMaterial);
				WriteString(writer, item.InternalItemManufacturer);
				WriteString(writer, item.InternalItemPrefix);
				WriteString(writer, item.InternalItemTitle);
				WriteInt32(writer, item.UnknownVariable1);
				WriteInt32(writer, item.UnknownVariable2);
				WriteInt32(writer, item.Equipped);
			}

			WriteInt32(writer, BackpackSlots);
			WriteInt32(writer, WeaponSlots);

			// Weapons.
			WriteInt32(writer, Weapons.Count);
			foreach (var weapon in Weapons) {
				WriteString(writer, weapon.InternalWeaponCategory);
				WriteString(writer, weapon.InternalWeaponManufacturer);
				WriteString(writer, weapon.InternalWeaponType);
				WriteString(writer, weapon.InternalWeaponBody);
				WriteString(writer, weapon.InternalWeaponGrip);
				WriteString(writer, weapon.InternalWeaponMag);
				WriteString(writer, weapon.InternalWeaponBarrel);
				WriteString(writer, weapon.InternalWeaponSight);
				WriteString(writer, weapon.InternalWeaponStock);
				WriteString(writer, weapon.InternalWeaponAction);
				WriteString(writer, weapon.InternalWeaponAccessory);
				WriteString(writer, weapon.InternalWeaponMaterial);
				WriteString(writer, weapon.InternalWeaponPrefix);
				WriteString(writer, weapon.InternalWeaponTitle);
				WriteInt32(writer, weapon.UnknownVariable1);
				WriteInt32(writer, weapon.UnknownVariable2);
				WriteInt32(writer, weapon.EquippedSlot);
			}

			// Stats.
			WriteInt32(writer, StatsBuffer.Length);
			WriteInt32(writer, StatTable.UnknownVariable1);
			WriteInt32(writer, StatTable.TotalBytesSize);
			writer.Write(StatTable.TotalEntries);
			foreach (var stat in StatTable.Stats) {
				writer.Write(stat.ID);
				writer.Write(stat.UnknownVariable1);
				writer.Write(stat.UnknownVariable2);
				WriteUInt32(writer, stat.Value);
			}

			// Visited locations.
			WriteInt32(writer, LocationsVisited.Count);
			foreach (var location in LocationsVisited) {
				WriteString(writer, location);
			}

			WriteString(writer, CurrentLocation);

			WriteInt32(writer, UnknownVariable6);
			WriteInt32(writer, UnknownVariable7);
			WriteString(writer, UnknownVariable8);
			WriteInt32(writer, UnknownVariable9);
			WriteInt32(writer, UnknownVariable10);
			WriteInt32(writer, UnknownVariable11);
			WriteInt32(writer, UnknownVariable12);
			WriteUInt32(writer, 0);

			// Playthroughs.
			WriteInt32(writer, Playthroughs.Count);
			foreach (var playthrough in Playthroughs) {
				WriteInt32(writer, playthrough.Number);
				WriteString(writer, playthrough.ActiveMissionName);
				WriteInt32(writer, playthrough.Missions.Count);
				foreach (var mission in playthrough.Missions) {
					WriteString(writer, mission.InternalName);
					WriteInt32(writer, mission.MissionStatusFlag);
					WriteInt32(writer, mission.UnknownVariable2);
					WriteInt32(writer, mission.UnknownVariable3);
					WriteInt32(writer, mission.Details.Count);
					foreach (var detail in mission.Details) {
						WriteString(writer, detail.UnknownString);
						WriteInt32(writer, detail.UnknownVariable);
					}
				}
			}

			WriteInt32(writer, PlayTimeSeconds);
			WriteString(writer, SaveTimeString);
			WriteString(writer, Name);
			WriteUInt32(writer, Color1);
			WriteUInt32(writer, Color2);
			WriteUInt32(writer, Color3);

			WriteInt32(writer, UnknownVariable13);
			WriteInt32(writer, UnknownVariable14.Count);
			foreach (var x in UnknownVariable14) {
				WriteInt32(writer, x);
			}
			WriteInt32(writer, UnknownVariable15.Count);
			foreach (var x in UnknownVariable15) {
				WriteInt32(writer, x);
			}

			// Echo
			WriteInt32(writer, EchoPlaythroughs.Count);
			foreach (var echoPlaythrough in EchoPlaythroughs) {
				WriteInt32(writer, echoPlaythrough.Playthrough);
				WriteInt32(writer, echoPlaythrough.Echoes.Count);
				foreach (var echo in echoPlaythrough.Echoes) {
					WriteString(writer, echo.InternalName);
					WriteInt32(writer, echo.UnknownVariable1);
					WriteInt32(writer, echo.UnknownVariable2);
				}
			}

			WriteInt32(writer, UnknownVariable16.Length);
			writer.Write(UnknownVariable16);

			writer.Close();
		}

		public CharacterClass Class;
		public Int32 Level;
		public Int32 Experience;
		public Int32 SkillPoints;
		public Int32 UnknownVariable1;
		public UInt32 Money;
		public Int32 PlaythroughTwoUnlocked; // Please only make this one or zero.
		public Dictionary<ProficiencyType, Proficiency> Proficiencies;
		public List<Skill> Skills;
		//public Dictionary<SkillType, Skill> Skills;
		public Int32 UnknownVariable2;
		public Int32 UnknownVariable3;
		public Int32 UnknownVariable4;
		public Int32 UnknownVariable5;
		public Dictionary<AmmoType, AmmoPool> Ammo;
		public List<Item> Items;
		public Int32 BackpackSlots;
		public Int32 WeaponSlots;
		public List<Weapon> Weapons;
		public byte[] StatsBuffer;
		public Dictionary<StatID, Stat> Stats;
		public StatsTable StatTable;
		public List<string> LocationsVisited;
		public string CurrentLocation;
		public Int32 UnknownVariable6;
		public Int32 UnknownVariable7;
		public string UnknownVariable8;
		public Int32 UnknownVariable9;
		public Int32 UnknownVariable10;
		public Int32 UnknownVariable11;
		public Int32 UnknownVariable12;
		public List<Playthrough> Playthroughs;
		public Int32 PlayTimeSeconds;
		public TimeSpan PlayTime { get { return TimeSpan.FromSeconds(PlayTimeSeconds); } }
		public string SaveTimeString;
		public DateTime SaveTime { get { return new DateTime(int.Parse(SaveTimeString.Substring(0, 4)), int.Parse(SaveTimeString.Substring(4, 2)), int.Parse(SaveTimeString.Substring(6, 2)), int.Parse(SaveTimeString.Substring(8, 2)), int.Parse(SaveTimeString.Substring(10, 2)), int.Parse(SaveTimeString.Substring(12, 2))); } }
		public string Name;
		public UInt32 Color1;
		public UInt32 Color2;
		public UInt32 Color3;
		public Int32 UnknownVariable13;
		public List<Int32> UnknownVariable14;
		public List<Int32> UnknownVariable15;
		public List<EchoPlaythrough> EchoPlaythroughs;
		public byte[] UnknownVariable16;



		private static int SearchBytes(byte[] haystack, byte[] needle) {
			int len = needle.Length;
			int limit = haystack.Length - len;
			for (int i = 0; i <= limit; i++) {
				int k = 0;
				for (; k < len; k++) {
					if (needle[k] != haystack[i + k]) break;
				}
				if (k == len) return i;
			}
			return -1;
		}

		public static T[] SubArray<T>(T[] data, int index, int length) {
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public static byte[] ReadByteArray(MemoryStream stream, int length) {
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);
			return buffer;
		}

		public static string ReadString(MemoryStream stream) {
			Int32 length = ReadInt32(stream);
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);
			// The trimming is because all of these strings are 0 terminated. Weird that they also
			// include the length.
			return Encoding.ASCII.GetString(buffer).TrimEnd('\0');
		}

		public static Int32 ReadInt32(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToInt32(buffer, 0);
		}

		public static UInt32 ReadUInt32(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToUInt32(buffer, 0);
		}

		public static float ReadFloat(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToSingle(buffer, 0);
		}

		public static void WriteByteArray(BinaryWriter writer, byte[] arr) {
			writer.Write(arr);
		}

		public static void WriteString(BinaryWriter writer, string str) {
			WriteInt32(writer, str.Length + 1);
			writer.Write(Encoding.ASCII.GetBytes(str));
			//writer.Write(str);
			// The terminating 0 was removed when reading, so we write it.
			writer.Write((byte)'\0');
		}

		public static void WriteInt32(BinaryWriter writer, Int32 i) {
			writer.Write(i);
		}

		public static void WriteUInt32(BinaryWriter writer, UInt32 i) {
			writer.Write(i);
		}

		public static void WriteFloat(BinaryWriter writer, float f) {
			writer.Write(f);
		}

		public static readonly byte[] Header = { 0x57, 0x53, 0x47, 0x02, 0x00, 0x00, 0x00, 0x50, 0x4C, 0x59, 0x52, 0x23, 0x00, 0x00, 0x00 };
		public static readonly Int32[] ExperienceToLevels = { 0, 0, 358, 1241, 2850, 5376, 8997, 13886, 20208, 28126, 37798, 49377, 63016, 78861, 97061, 117757, 141092, 167206, 196238, 228322, 263595, 302190, 344238, 389873, 439222, 492414, 549578, 610840, 676325, 746158, 820463, 899363, 982980, 1071435, 1164850, 1263343, 1367034, 1476041, 1590483, 1710476, 1836137, 1967582, 2104926, 2248285, 2397772, 2553501, 2715586, 2884139, 3059273, 3241098, 3429728, 3625271, 3827840, 4037543, 4254491, 4478792, 4710556, 4949890, 5196902, 5451701, 5714393, 5985086, 6263885, 6550897, 6846227, 7149982, 7462266, 7783184, 8112840, 8451340, 8798786, 9155282, 9520932 };
	}


}

using Borderlands_Save_Editor.Player.Class;
using Borderlands_Save_Editor.Player.Proficiency;
using Borderlands_Save_Editor.Player.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Save {
	/// <summary>
	/// Describes all the properties used in a save file.
	/// </summary>
	public class Save {
		/// <summary>
		/// Constructs a save with some default parameters.
		/// </summary>
		public Save() {
			Details = new CharacterDetails();
			Proficiencies = new CharacterProficiencies();
			Skills = new CharacterSkills();
			Ammo = new Dictionary<AmmoType, AmmoPool>();
			foreach (var pair in AmmoPool.AmmoDefinitions) {
				Ammo.Add(pair.Value, new AmmoPool());
			}
			Items = new List<Item>();
			Weapons = new List<Weapon>();
			StatTable = new StatsTable();
			LocationsVisited = new List<string>();
			CurrentLocation = "?";
			Playthroughs = new List<Playthrough>();
			UnknownVariable15 = new List<Int32>();
			UnknownVariable16 = new List<Int32>();
			EchoPlaythroughs = new List<EchoPlaythrough>();
			UnknownVariable17 = new byte[0];
		}

		/// <summary>
		/// Reads in a save from a file path.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static Save Read(string filePath) {
			Save save = new Save();

			byte[] file = File.ReadAllBytes(filePath);
			MemoryStream stream = new MemoryStream(file);
			BinaryReader reader = new BinaryReader(stream);

			// The file starts with a 15 byte header. Let's match this.
			//? This may be a version number too, but honestly everyone should be on the newest
			//? version by now.
			byte[] detectedHeader = reader.ReadBytes(Header.Length);
			if (detectedHeader.Intersect(Header).Count() != detectedHeader.Union(Header).Count()) {
				Console.Error.WriteLine("This is not a valid save file!");
				return save;
			}

			// The file starts with the character class internal name.
			save.Details = new CharacterDetails(reader);

			// Now we've got skills and proficiencies to count. Borderlands made them the same
			// thing, but I'll make them separate.
			Int32 skillCount = reader.ReadInt32();
			for (Int32 x = 0; x < skillCount; ++x) {
				string skillName = reader.BL_ReadString();
				reader.BaseStream.Seek(-skillName.Length - 5, SeekOrigin.Current);
				// Check whether the skill is a proficiency.
				if (skillName.IndexOf("gd_skills_") == 0) {
					// In this case, it's a proficiency.
					save.Proficiencies.ReadProficiency(reader);
				} else {
					save.Skills.ReadSkill(reader);
				}
			}

			// Then there's stuff that no one knows.
			save.UnknownVariable2 = reader.ReadInt32();
			save.UnknownVariable3 = reader.ReadInt32();
			save.UnknownVariable4 = reader.ReadInt32();
			save.UnknownVariable5 = reader.ReadInt32();

			// Now we've got the ammo types.
			Int32 ammoCount = reader.ReadInt32();
			for (Int32 x = 0; x < ammoCount; ++x) {
				string ammoName = reader.BL_ReadString();
				AmmoType type = AmmoPool.AmmoDefinitions[ammoName];
				save.Ammo[type].InternalName = ammoName;

				//? A sanity check to see if the pool name matches?
				if (save.Ammo[type].PoolInternalName != reader.BL_ReadString()) {
					// Panic.
					Console.Error.WriteLine($"Tried to match expected ammo type {save.Ammo[type].PoolInternalName} but got something else!");
				}

				save.Ammo[type].Ammo = reader.ReadSingle();
				save.Ammo[type].UpgradeLevel = reader.ReadInt32();
			}

			// Now the items.
			Int32 itemCount = reader.ReadInt32();
			for (Int32 x = 0; x < itemCount; ++x) {
				save.Items.Add(new Item(reader));
			}

			// Some slot counters.
			save.BackpackSlots = reader.ReadInt32();
			save.WeaponSlots = reader.ReadInt32();

			// Now the weapons.
			Int32 weaponCount = reader.ReadInt32();
			for (Int32 x = 0; x < weaponCount; ++x) {
				save.Weapons.Add(new Weapon(reader));
			}

			// Some stats?
			save.StatTable.ReadStats(reader);

			// Visited locations.

			Int32 locationsLength = reader.ReadInt32();
			for (Int32 x = 0; x < locationsLength; ++x) {
				save.LocationsVisited.Add(reader.BL_ReadString());
			}

			save.CurrentLocation = reader.BL_ReadString();

			// Some unknown variables.
			// I dunno what this is.
			save.UnknownVariable6 = reader.ReadInt32();
			save.UnknownVariable7 = reader.ReadInt32();
			save.UnknownVariable8 = reader.BL_ReadString();
			save.UnknownVariable9 = reader.ReadInt32();
			save.UnknownVariable10 = reader.ReadInt32();
			save.SaveNumber = reader.ReadInt32();
			save.UnknownVariable12 = reader.ReadInt32();
			save.UnknownVariable13 = reader.ReadInt32();

			Int32 playthroughCount = reader.ReadInt32();
			for (Int32 x = 0; x < playthroughCount; ++x) {
				Playthrough playthrough = new Playthrough();
				save.Playthroughs.Add(playthrough);
				playthrough.Number = reader.ReadInt32();
				playthrough.ActiveMissionName = reader.BL_ReadString();
				Int32 missionCount = reader.ReadInt32();
				for (Int32 y = 0; y < missionCount; ++y) {
					Mission mission = new Mission();
					playthrough.Missions.Add(mission);
					mission.InternalName = reader.BL_ReadString();
					mission.MissionStatusFlag = reader.ReadInt32();
					mission.UnknownVariable2 = reader.ReadInt32();
					mission.UnknownVariable3 = reader.ReadInt32();
					Int32 detailsCount = reader.ReadInt32();
					for (Int32 z = 0; z < detailsCount; ++z) {
						Mission.MissionDetails detail = new Mission.MissionDetails();
						mission.Details.Add(detail);
						detail.UnknownString = reader.BL_ReadString();
						detail.UnknownVariable = reader.ReadInt32();
					}
				}
			}

			save.PlayTimeSeconds = reader.ReadInt32();
			save.SaveTimeString = reader.BL_ReadString();
			save.Name = reader.BL_ReadString();
			save.Color1 = reader.ReadUInt32();
			save.Color2 = reader.ReadUInt32();
			save.Color3 = reader.ReadUInt32();

			save.UnknownVariable14 = reader.ReadInt32();
			Int32 unknown14Count = reader.ReadInt32();
			for (Int32 x = 0; x < unknown14Count; ++x) {
				save.UnknownVariable15.Add(reader.ReadInt32());
			}
			Int32 unknown15Count = reader.ReadInt32();
			for (Int32 x = 0; x < unknown15Count; ++x) {
				save.UnknownVariable16.Add(reader.ReadInt32());
			}

			Int32 echoPlaythroughCount = reader.ReadInt32();
			for (Int32 x = 0; x < echoPlaythroughCount; ++x) {
				EchoPlaythrough echoPlaythrough = new EchoPlaythrough();
				save.EchoPlaythroughs.Add(echoPlaythrough);
				echoPlaythrough.Playthrough = reader.ReadInt32();
				Int32 echoCount = reader.ReadInt32();
				for (Int32 y = 0; y < echoCount; ++y) {
					Echo echo = new Echo();
					echoPlaythrough.Echoes.Add(echo);
					echo.InternalName = reader.BL_ReadString();
					echo.UnknownVariable1 = reader.ReadInt32();
					echo.UnknownVariable2 = reader.ReadInt32();
				}
			}

			//? This seems to be really short, except on my finished save file. Is this banked
			//? data?
			// From the looks of it, maybe? This seems to go right to the end of the file, so
			// there's nothing else to note there.
			// Starts with 0x34, 0x12, 0x21, 0x43. Does it mean anything? It's 1126240820 in dec.
			// Next four bytes are either 9, or 6472 in the save that actually uses it. Hm...
			// Next four are always 0x01, 0xC, 0x00, 0x00. This seems to be 3073 in dec.

			// The first weapon seems to start text 35 bytes in, so maybe it's not all ints. There
			// doesn't seem to be any readable characters though. In particular, at [31], that's
			// the length of the first item string. I'm not too sure where it's indicating 

			Int32 unknown17Length = reader.ReadInt32();
			save.UnknownVariable17 = reader.ReadBytes(unknown17Length);

			reader.Close();
			
			MemoryStream test = new MemoryStream(save.UnknownVariable17);
			BinaryReader testReader = new BinaryReader(test);
			Int32 u1 = testReader.ReadInt32();
			Int32 u2 = testReader.ReadInt32();
			Int32 u3 = testReader.ReadInt32();
			Int32 u4 = testReader.ReadInt32();
			Int32 u5 = testReader.ReadInt32();
			Int32 u6 = testReader.ReadInt32();
			Int32 u7 = testReader.ReadInt32();
			Int16 u8 = testReader.ReadInt16();
			byte u9 = testReader.ReadByte();

			if (save.SaveNumber == 10) {
				// This is kinda weird. Normally the properties for a weapon or item are stored as
				// a bunch of strings one after the other
				for (int j = 0; j < 8; ++j) {
					string s1 = testReader.BL_ReadString();
					string s2 = testReader.BL_ReadString();
					string s3 = testReader.BL_ReadString();
					int s4 = testReader.ReadInt32(); // 0x20?
					int s5 = testReader.ReadInt32(); // 0s?
					int s6 = testReader.ReadInt32(); // 0s?
					byte s7 = testReader.ReadByte(); // 0?
					string s8 = testReader.BL_ReadString();
					string s9 = testReader.BL_ReadString();
					string s10 = testReader.BL_ReadString();
					int s11 = testReader.ReadInt32(); // 0x20?
					int s12 = testReader.ReadInt32(); // 0s?
					int s13 = testReader.ReadInt32(); // 0s?
					byte s14 = testReader.ReadByte(); // 0?
					if (s9 == "Weapons") {
						for (int i = 2; i < 14; ++i) {
							string z1 = testReader.BL_ReadString();
							string z2 = testReader.BL_ReadString();
							string z3 = testReader.BL_ReadString();
							if (i == 2) {
								int bonusInt = testReader.ReadInt32();
								Console.WriteLine($"{i}-{j} has a bonus integer {bonusInt}");
							}
							int z4 = testReader.ReadInt32(); // 0x20?
							int z5 = testReader.ReadInt32(); // 0s?
							int z6 = testReader.ReadInt32(); // 0s?
							byte z7 = testReader.ReadByte(); // 0?
							Console.WriteLine($"Tag {i} weapon {j}: {z1} {z2} {z3} {z4} {z5} {z6} {z7}");
						}
						testReader.ReadBytes(15);
					} else if (s9 == "Gear") {
						for (int i = 2; i < 9; ++i) {
							string z1 = testReader.BL_ReadString();
							string z2 = testReader.BL_ReadString();
							string z3 = testReader.BL_ReadString();
							if (i == 8) {
								int bonusInt1 = testReader.ReadInt32();
								int bonusInt2 = testReader.ReadInt32();
								int bonusInt3 = testReader.ReadInt32();
								Console.WriteLine($"{i}-{j} has bonus integers {bonusInt1} {bonusInt2} {bonusInt3}");
							}
							int z4 = testReader.ReadInt32(); // 0x20?
							int z5 = testReader.ReadInt32(); // 0s?
							int z6 = testReader.ReadInt32(); // 0s?
							byte z7 = testReader.ReadByte(); // 0?
							Console.WriteLine($"Tag {i} weapon {j}: {z1} {z2} {z3} {z4} {z5} {z6} {z7}");
							if (i == 2) {
								int bonusInt = testReader.ReadInt32();
								Console.WriteLine($"{i}-{j} has a bonus integer {bonusInt}");
							}
						}
					}
				}
			}

			Console.WriteLine($"{save.SaveNumber} is {u1} {u2} {u3} {u4} {u5} {u6} {u7} {u8} {u9}");

			Int32 u10 = testReader.ReadInt32();
			Int32 u11 = testReader.ReadInt32();
			Int32 u12 = testReader.ReadInt32();
			Int32 u13 = testReader.ReadInt32();
			Int32 u14 = testReader.ReadInt32();
			Int32 u15 = testReader.ReadInt32();
			Int32 u16 = testReader.ReadInt32();
			Int32 u17 = testReader.ReadInt32();
			Int32 u18 = testReader.ReadInt32();
			Int32 u19 = testReader.ReadInt32();
			Int32 u20 = testReader.ReadInt32();
			Int32 u21 = testReader.ReadInt32();
			Int32 u22 = testReader.ReadInt32();
			Int32 u23 = testReader.ReadInt32();
			Int32 u24 = testReader.ReadInt32();

			Console.WriteLine($"{save.SaveNumber} is {u10} {u11} {u12} {u13} {u14} {u15} {u16} {u17} {u18} {u19} {u20} {u21} {u22} {u23} {u24}");
			return save;
		}

		/// <summary>
		/// Writes a save to a folder. The save name is generated based on the save number stored by
		/// this save.
		/// </summary>
		/// <param name="saveDirectory"></param>
		public void Write(string saveDirectory) {
			// The output file is indicated by the save number.
			BinaryWriter writer = new BinaryWriter(File.OpenWrite(Path.Combine(saveDirectory, SaveFileName)));
			//BinaryWriter writer = new BinaryWriter(stream);

			// Firstly, write the header.
			writer.Write(Header);

			Details.Write(writer);

			// Now the skills.
			writer.Write(Proficiencies.ActiveCount + Skills.ActiveCount);
			Proficiencies.WriteAllProficiencies(writer);
			Skills.WriteAllSkills(writer);

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
			WriteInt32(writer, 10 + StatTable.Stats.Count * 7);
			WriteInt32(writer, StatTable.UnknownVariable1);
			WriteInt32(writer, StatTable.TotalBytesSize);
			writer.Write(StatTable.TotalEntries);
			foreach (var stat in StatTable.Stats.Values) {
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
			WriteInt32(writer, SaveNumber);
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

			WriteInt32(writer, UnknownVariable14);
			WriteInt32(writer, UnknownVariable15.Count);
			foreach (var x in UnknownVariable15) {
				WriteInt32(writer, x);
			}
			WriteInt32(writer, UnknownVariable16.Count);
			foreach (var x in UnknownVariable16) {
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

			WriteInt32(writer, UnknownVariable17.Length);
			writer.Write(UnknownVariable17);

			writer.Close();
		}

		[Obsolete("Use the getter object SaveFileName instead.")]
		/// <summary>
		/// Returns the name of a save file that would be stored with the current save's number.
		/// This is simply in the form of save%04x.sav, where %04x is the save number as a four
		/// hex-degit string.
		/// 
		/// This is necessary as, while the game can read any save file inside the save folder, it
		/// only writes to this specific path.
		/// </summary>
		/// <returns></returns>
		public string SaveNameOld() {
			return "save" + SaveNumber.ToString("X4") + ".sav";
		}

		/// <summary>
		/// The name of the file that stores this save. This file name relies on the number of the
		/// save stored. While any named save can be read, the save will only be written to this
		/// specific path.
		/// </summary>
		public string SaveFileName { get { return "save" + SaveNumber.ToString("X4") + ".sav"; } }

		/// <summary>
		/// Stores the details of this character.
		/// </summary>
		public CharacterDetails Details { get; set; }

		/// <summary>
		/// Stores this character's weapon proficiencies.
		/// </summary>
		public CharacterProficiencies Proficiencies { get; set; }

		/// <summary>
		/// Stores this character's skills.
		/// </summary>
		public CharacterSkills Skills { get; set; }

		/// <summary>
		/// Some unknown variable.
		/// </summary>
		public Int32 UnknownVariable2;

		/// <summary>
		/// Some unknown variable.
		/// </summary>
		public Int32 UnknownVariable3;

		/// <summary>
		/// Some unknown variable.
		/// </summary>
		public Int32 UnknownVariable4;

		/// <summary>
		/// Some unknown variable.
		/// </summary>
		public Int32 UnknownVariable5;

		/// <summary>
		/// Stores a character's ammo pools for each ammo type.
		/// </summary>
		public Dictionary<AmmoType, AmmoPool> Ammo;

		/// <summary>
		/// A list of all the items this character has. These are not including weapons.
		/// </summary>
		public List<Item> Items;

		/// <summary>
		/// The amount of slots that the player can store in their backpack.
		/// 
		/// Normal playthroughs only allow you to reach 60? but this value an easily be modified
		/// without any negative side effects.
		/// </summary>
		public Int32 BackpackSlots;

		/// <summary>
		/// The amount of weapon slots available to the player.
		/// 
		/// TODO: Experiment with increasing this and settiing weapons to fill those slots. Does it
		/// work?
		/// </summary>
		public Int32 WeaponSlots;

		/// <summary>
		/// A list of this character's weapons.
		/// </summary>
		public List<Weapon> Weapons;

		/// <summary>
		/// All of the stats that are tracked by this character.
		/// </summary>
		public StatsTable StatTable;

		/// <summary>
		/// A list of all locations visited.
		/// 
		/// TODO: This could be an enum later on.
		/// </summary>
		public List<string> LocationsVisited;

		/// <summary>
		/// The current location that the player was last saved at.
		/// 
		/// This seems to only affect the load menu and not any actual position in the saves.
		/// </summary>
		public string CurrentLocation;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable6;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable7;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public string UnknownVariable8;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable9;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable10;

		/// <summary>
		/// Indicates what number save this is. This is important as it is used by the game to
		/// determine the save file path.
		/// </summary>
		public Int32 SaveNumber;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable12;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable13;

		/// <summary>
		/// A list of all the playthroughs this character has done (should only be two at most).
		/// </summary>
		public List<Playthrough> Playthroughs;

		/// <summary>
		/// Counts the play time in seconds.
		/// </summary>
		public Int32 PlayTimeSeconds;

		/// <summary>
		/// A way to represent the play time as a <see cref="TimeSpan"/> object for useful time
		/// calculations.
		/// </summary>
		public TimeSpan PlayTime { get { return TimeSpan.FromSeconds(PlayTimeSeconds); } }

		/// <summary>
		/// Represents the last save time as a string. This string is used internally by the save
		/// format. It is simply yyyymmddhhmmss in format.
		/// 
		/// TODO: Migrate towards storing the date as a <see cref="DateTime"/> and use a get/set on
		/// this field.
		/// </summary>
		public string SaveTimeString;
		public DateTime SaveTime { get { return new DateTime(int.Parse(SaveTimeString.Substring(0, 4)), int.Parse(SaveTimeString.Substring(4, 2)), int.Parse(SaveTimeString.Substring(6, 2)), int.Parse(SaveTimeString.Substring(8, 2)), int.Parse(SaveTimeString.Substring(10, 2)), int.Parse(SaveTimeString.Substring(12, 2))); } }

		/// <summary>
		/// The name of the player's character.
		/// </summary>
		public string Name;

		/// <summary>
		/// The first color decided by the player.
		/// 
		/// TODO: Figure out how this format works. It's not RGBA, but it does occupy 4 bytes.
		/// </summary>
		public UInt32 Color1;

		/// <summary>
		/// The second color of the player.
		/// </summary>
		public UInt32 Color2;

		/// <summary>
		/// The third color of the player.
		/// </summary>
		public UInt32 Color3;

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable14;

		/// <summary>
		/// An unknown variable; appears to be a list of integers.
		/// </summary>
		public List<Int32> UnknownVariable15;

		/// <summary>
		/// And unknown variable; appears to be a list of integers.
		/// </summary>
		public List<Int32> UnknownVariable16;

		/// <summary>
		/// A list of all the echo devices acquired in each playthrough.
		/// </summary>
		public List<EchoPlaythrough> EchoPlaythroughs;

		/// <summary>
		/// An unknown variable; this appears to just be an array of bytes.
		/// 
		/// It seems to relate to the bank used in the DLC, but its format is not known.
		/// </summary>
		public byte[] UnknownVariable17;

		/// <summary>
		/// Returns a subarray of a given array, given the starting index and th length of the subarray.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static T[] SubArray<T>(T[] data, int index, int length) {
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		/// <summary>
		/// Reads a byte array from the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		[Obsolete("Just read the value")]
		public static byte[] ReadByteArray(MemoryStream stream, int length) {
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);
			return buffer;
		}

		/// <summary>
		/// Reads a string from a given string. The string starts with a 4-byte int of the length of
		/// the string, followed by the string content itself.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[Obsolete("Use the extension function BL_ReadString instead")]
		public static string ReadString(MemoryStream stream) {
			Int32 length = ReadInt32(stream);
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);
			// The trimming is because all of these strings are 0 terminated. Weird that they also
			// include the length.
			return Encoding.ASCII.GetString(buffer).TrimEnd('\0');
		}

		/// <summary>
		/// Reads an <see cref="Int32"/> from the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[Obsolete("Just read the value")]
		public static Int32 ReadInt32(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToInt32(buffer, 0);
		}

		/// <summary>
		/// Reads a <see cref="UInt32"/> from the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[Obsolete("Just read the value")]
		public static UInt32 ReadUInt32(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToUInt32(buffer, 0);
		}

		/// <summary>
		/// Reads a <see cref="float"/> from the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[Obsolete("Just read the value")]
		public static float ReadFloat(MemoryStream stream) {
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToSingle(buffer, 0);
		}

		/// <summary>
		/// Writes a byte array to the stream.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="arr"></param>
		[Obsolete("Just write the array")]
		public static void WriteByteArray(BinaryWriter writer, byte[] arr) {
			writer.Write(arr);
		}

		/// <summary>
		/// Writes a string to the stream.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="str"></param>
		[Obsolete("Use the extension function BL_WriteString instead")]
		public static void WriteString(BinaryWriter writer, string str) {
			WriteInt32(writer, str.Length + 1);
			writer.Write(Encoding.ASCII.GetBytes(str));
			//writer.Write(str);
			// The terminating 0 was removed when reading, so we write it.
			writer.Write((byte)'\0');
		}

		/// <summary>
		/// Writes an <see cref="Int32"/> to the stream.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="i"></param>
		[Obsolete("Just write the value")]
		public static void WriteInt32(BinaryWriter writer, Int32 i) {
			writer.Write(i);
		}

		/// <summary>
		/// Writes a <see cref="UInt32"/> to the stream.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="i"></param>
		[Obsolete("Just write the value")]
		public static void WriteUInt32(BinaryWriter writer, UInt32 i) {
			writer.Write(i);
		}

		/// <summary>
		/// Writes a <see cref="float"/> to the stream.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="f"></param>
		[Obsolete("Just write the value")]
		public static void WriteFloat(BinaryWriter writer, float f) {
			writer.Write(f);
		}

		/// <summary>
		/// The header used by save formats.
		/// 
		/// TODO: This may encode the game version number. This assumes the game is on the latest
		/// version, but extensibility will want other versions.
		/// 
		/// TODO: Experiment what happens with other version numbers.
		/// </summary>
		public static readonly byte[] Header = { 0x57, 0x53, 0x47, 0x02, 0x00, 0x00, 0x00, 0x50, 0x4C, 0x59, 0x52, 0x23, 0x00, 0x00, 0x00 };

		/// <summary>
		/// Notes the amount oof experience to reach the given level of the index. Because the
		/// player starts at level 1 with 0XP, index 1 is also 0.
		/// 
		/// TODO: Could this be its own class similar to <see cref="Proficiency"/>?
		/// </summary>
		public static readonly Int32[] ExperienceToLevels = { 0, 0, 358, 1241, 2850, 5376, 8997, 13886, 20208, 28126, 37798, 49377, 63016, 78861, 97061, 117757, 141092, 167206, 196238, 228322, 263595, 302190, 344238, 389873, 439222, 492414, 549578, 610840, 676325, 746158, 820463, 899363, 982980, 1071435, 1164850, 1263343, 1367034, 1476041, 1590483, 1710476, 1836137, 1967582, 2104926, 2248285, 2397772, 2553501, 2715586, 2884139, 3059273, 3241098, 3429728, 3625271, 3827840, 4037543, 4254491, 4478792, 4710556, 4949890, 5196902, 5451701, 5714393, 5985086, 6263885, 6550897, 6846227, 7149982, 7462266, 7783184, 8112840, 8451340, 8798786, 9155282, 9520932 };
	}


}
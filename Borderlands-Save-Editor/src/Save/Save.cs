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
using System.Windows.Media;

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
			LocationsVisited = new HashSet<Location>();
			CurrentLocation = Location.Fyrestone;
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
			save.VehicleColor = reader.ReadInt32();
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
				save.LocationsVisited.Add(LocationExtensions.FromInternalName(reader.BL_ReadString()));
			}

			save.CurrentLocation = LocationExtensions.FromInternalName(reader.BL_ReadString());

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
			save.Color1ARGB = reader.ReadUInt32();
			save.Color2ARGB = reader.ReadUInt32();
			save.Color3ARGB = reader.ReadUInt32();

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
			var writer = new BinaryWriter(File.Create(Path.Combine(saveDirectory, SaveFileName)));
			//BinaryWriter writer = new BinaryWriter(stream);

			// Firstly, write the header.
			writer.Write(Header);

			Details.Write(writer);

			// Now the skills.
			writer.Write(Proficiencies.ActiveCount + Skills.ActiveCount);
			Proficiencies.WriteAllProficiencies(writer);
			Skills.WriteAllSkills(writer);

			// Some unknown stuff.
			writer.Write(VehicleColor);
			writer.Write(UnknownVariable3);
			writer.Write(UnknownVariable4);
			writer.Write(UnknownVariable5);

			// Ammo.
			writer.Write(Ammo.Count);
			foreach (var ammo in Ammo) {
				ammo.Value.Write(writer);
			}

			// Items.
			writer.Write(Items.Count);
			foreach (var item in Items) {
				item.Write(writer);
			}

			writer.Write(BackpackSlots);
			writer.Write(WeaponSlots);

			// Weapons.
			writer.Write(Weapons.Count);
			foreach (var weapon in Weapons) {
				weapon.Write(writer);
			}

			// Stats.
			StatTable.Write(writer);

			// Visited locations.
			writer.Write(LocationsVisited.Count);
			foreach (var location in LocationsVisited) {
				writer.BL_WriteString(location.InternalName());
			}

			writer.BL_WriteString(CurrentLocation.InternalName());

			writer.Write(UnknownVariable6);
			writer.Write(UnknownVariable7);
			writer.BL_WriteString(UnknownVariable8);
			writer.Write(UnknownVariable9);
			writer.Write(UnknownVariable10);
			writer.Write(SaveNumber);
			writer.Write(UnknownVariable12);
			writer.Write(0u);

			// Playthroughs.
			writer.Write(Playthroughs.Count);
			foreach (var playthrough in Playthroughs) {
				writer.Write(playthrough.Number);
				writer.BL_WriteString(playthrough.ActiveMissionName);
				writer.Write(playthrough.Missions.Count);
				foreach (var mission in playthrough.Missions) {
					writer.BL_WriteString(mission.InternalName);
					writer.Write(mission.MissionStatusFlag);
					writer.Write(mission.UnknownVariable2);
					writer.Write(mission.UnknownVariable3);
					writer.Write(mission.Details.Count);
					foreach (var detail in mission.Details) {
						writer.BL_WriteString(detail.UnknownString);
						writer.Write(detail.UnknownVariable);
					}
				}
			}

			writer.Write(PlayTimeSeconds);
			writer.BL_WriteString(SaveTimeString);
			writer.BL_WriteString(Name);
			writer.Write(Color1ARGB);
			writer.Write(Color2ARGB);
			writer.Write(Color3ARGB);

			writer.Write(UnknownVariable14);
			writer.Write(UnknownVariable15.Count);
			foreach (Int32 x in UnknownVariable15) {
				writer.Write(x);
			}
			writer.Write(UnknownVariable16.Count);
			foreach (Int32 x in UnknownVariable16) {
				writer.Write(x);
			}

			// Echo
			writer.Write(EchoPlaythroughs.Count);
			foreach (var echoPlaythrough in EchoPlaythroughs) {
				writer.Write(echoPlaythrough.Playthrough);
				writer.Write(echoPlaythrough.Echoes.Count);
				foreach (var echo in echoPlaythrough.Echoes) {
					writer.BL_WriteString(echo.InternalName);
					writer.Write(echo.UnknownVariable1);
					writer.Write(echo.UnknownVariable2);
				}
			}

			writer.Write(UnknownVariable17.Length);
			writer.Write(UnknownVariable17);

			writer.Close();
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
		/// Sets the color of the player's vehicle. Unlike the player colors, this is simply an
		/// integer between 0 and 7 inclusive for the 8 different kinds of colors. Any differing
		/// value will modulus to the correct value.
		/// </summary>
		public Int32 VehicleColor;

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
		/// The amount of weapon slots available to the player. This can be set to any amount,
		/// although any value less than 12 will automatically be bumped up to 12. The game can only
		/// represent the total capacity as 4 digits, but any number will work.
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
		/// All of the locations this player has visited.
		/// </summary>
		public HashSet<Location> LocationsVisited;

		/// <summary>
		/// The current location that the player was last saved at.
		/// 
		/// Changing this does move the player to that position in playthrough 1, but I have no idea
		/// where playthrough 2 is sitting.
		/// </summary>
		public Location CurrentLocation;

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
		public Int32 PlayTimeSeconds {
			get { return (Int32)PlayTime.TotalSeconds; }
			set { PlayTime = TimeSpan.FromSeconds(value); }
		}

		/// <summary>
		/// A way to represent the play time as a <see cref="TimeSpan"/> object for useful time
		/// calculations.
		/// </summary>
		public TimeSpan PlayTime;

		/// <summary>
		/// Represents the last save time as a string. This string is used internally by the save
		/// format. It is simply yyyymmddhhmmss in format.
		/// 
		/// TODO: Migrate towards storing the date as a <see cref="DateTime"/> and use a get/set on
		/// this field.
		/// </summary>
		public string SaveTimeString {
			get { return SaveTime.ToString("yyyyMMddhhmmss"); }
			set { SaveTime = new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(8, 2)), int.Parse(value.Substring(10, 2)), int.Parse(value.Substring(12, 2))); }
		}

		/// <summary>
		/// Represents the last save time as a <see cref="DateTime"/> object.
		/// </summary>
		public DateTime SaveTime;

		/// <summary>
		/// The name of the player's character.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The first color decided by the player. The color is stored in ARGB format in the save
		/// file. Even though the game only has a few dozen colors to choose from, any ARGB value
		/// can be used. Alpha is completely ignored in-game.
		/// </summary>
		public Color Color1;

		/// <summary>
		/// The second color of the player. The color is stored in ARGB format in the save file.
		/// </summary>
		public Color Color2;

		/// <summary>
		/// The third color of the player. The color is stored in ARGB format in the save file.
		/// </summary>
		public Color Color3;

		/// <summary>
		/// The first color decided by the player; this format is used by the save format.
		/// </summary>
		public UInt32 Color1ARGB {
			get { return (UInt32)(Color1.A << 24 | Color1.R << 16 | Color1.G << 8 | Color1.B); }
			set { Color1 = Color.FromArgb((byte)(value >> 24 % 256), (byte)(value >> 16 % 256), (byte)(value >> 8 % 256), (byte)(value % 256)); }
		}

		/// <summary>
		/// The second color decided by the player; this format is used by the save format.
		/// </summary>
		public UInt32 Color2ARGB {
			get { return (UInt32)(Color2.A << 24 | Color2.R << 16 | Color2.G << 8 | Color2.B); }
			set { Color2 = Color.FromArgb((byte)(value >> 24 % 256), (byte)(value >> 16 % 256), (byte)(value >> 8 % 256), (byte)(value % 256)); }
		}

		/// <summary>
		/// The third color decided by the player; this format is used by the save format.
		/// </summary>
		public UInt32 Color3ARGB {
			get { return (UInt32)(Color3.A << 24 | Color3.R << 16 | Color3.G << 8 | Color3.B); }
			set { Color3 = Color.FromArgb((byte)(value >> 24 % 256), (byte)(value >> 16 % 256), (byte)(value >> 8 % 256), (byte)(value % 256)); }
		}

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
		/// The header used by save formats.
		/// 
		/// TODO: This may encode the game version number. This assumes the game is on the latest
		/// version, but extensibility will want other versions.
		/// 
		/// TODO: Experiment what happens with other version numbers.
		/// </summary>
		public static readonly byte[] Header = { 0x57, 0x53, 0x47, 0x02, 0x00, 0x00, 0x00, 0x50, 0x4C, 0x59, 0x52, 0x23, 0x00, 0x00, 0x00 };
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Identifies a stat type.
	/// 
	/// TODO: Just assign each the underlying byte value used.
	/// </summary>
	public enum StatID : byte {
		// I'm lazy.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		ShotsFired,
		Kills,
		SkagKills,
		PistolKills,
		ShotgunKills,
		SMGKills,
		SniperKills,
		CombatRifleKills,
		RocketLauncherKills,
		GrenadeKills,
		VehicleKills,
		CorrosiveKills,
		FireKills,
		ShockKills,
		ExplosiveKills,
		CriticalHitKills,
		MeleeKills,
		SpiderantKills,
		HumanKills,
		RakkKills,
		ChestsOpened,
		LootablesOpened,
		BoughtItems,
		SoldItems,
		GuardianKills,
		MoneyEarned,
		VehicleHangTime,
		ActionSkillKills,
		ChainKills,
		DuelsWon, //? I don't know the ID of this one.
		Unknown0,
		Unknown1,
		Unknown2,
		Unknown3,
		Unknown4,
		Unknown5,
		Unknown6,
		Unknown7,
		Unknown8,
		Unknown9,
		Unknown10,
		Unknown11,
		Unknown12,
		Unknown13,
		Unknown14,
		Unknown15,
		Unknown16,
		Unknown17,
		Unknown18,
		Unknown86,
		Unknown87,
		Unknown88,
		Unknown89,
		Unknown90,
		Unknown91,
		Unknown93,
		Unknown94,
		Unknown105,
		Unknown111,
		Unknown112,
		Unknown113,
		Unknown115,
		Unknown116,
		Unknown117,
		Unknown118,
		Unknown122,
		Unknown123,
		Unknown124,
		Unknown125,
		Unknown126,
		Unknown127,
		Unknown128,
		Unknown129,
		Unknown130,
		Unknown131,
		Unknown132,
		Unknown135,
		Unknown136,
		Unknown137,
		Unknown138,
		Unknown139,
		Unknown140,
		Unknown143,
		Unknown144,
		Unknown145,
		Unknown146,
		Unknown147,
		Unknown148,
		Unknown149,
		Unknown150,
		Unknown151,
		Unknown152,
		Unknown153,
		Unknown154,
		Unknown155,
		Unknown156,
		Unknown158,
		Unknown159,
		Unknown160,
		Unknown163,
		Unknown164,
		Unknown165,
		Unknown167,
		Unknown168,
		Unknown169,
		Unknown170,
		Unknown171,
		Unknown172,
		Unknown173,
		Unknown174,
		Unknown175,
		Unknown176,
		Unknown177,
		Unknown179,
		Unknown180,
		Unknown181,
		Unknown183,
		Unknown184,
		Unknown185,
		Unknown186,
		Unknown187,
		Unknown188,
		Unknown189,
		Unknown190,
		Unknown191,
		Unknown192,
		Unknown193,
		Unknown194,
		Unknown195,
		Unknown196,
		Unknown197,
		Unknown198,
		Unknown199,
		Unknown200,
		Unknown201,
		Unknown202,
		Unknown203,
		Unknown204,
		Unknown205,
		Unknown206,
		Unknown207,
		Unknown208,
		Unknown209,
		Unknown210,
		Unknown211,
		Unknown212,
		Unknown213,
		Unknown214,
		Unknown215,
		Unknown216,
		Unknown217,
		Unknown218,
		Unknown219,
		Unknown220,
		Unknown221,
		Unknown222,
		Unknown223,
		Unknown224,
		Unknown225,
		Unknown226,
		Unknown227,
		Unknown228,
		Unknown229,
		Unknown230,
		Unknown231,
		Unknown232,
		Unknown233,
		Unknown234,
		Unknown235,
		Unknown236,
		Unknown237,
		Unknown238,
		Unknown239,
		Unknown240,
		Unknown241,
		Unknown242,
		Unknown243,
		Unknown244,
		Unknown245,
		Unknown246,
		Unknown247,
		Unknown248,
		Unknown249,
		Unknown250,
		Unknown251,
		Unknown252,
		Unknown253,
		Unknown254,
		Unknown255
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Stores all the properties of a stat.
	/// </summary>
	public class Stat {
		/// <summary>
		/// Constructs a stat with default properties.
		/// </summary>
		public Stat() {
			ID = 85;
			UnknownVariable1 = 6;
			UnknownVariable2 = 1;
			Value = 0u;
		}

		/// <summary>
		/// Reads in a single stat from a reader.
		/// </summary>
		/// <param name="reader"></param>
		public Stat(BinaryReader reader) {
			ID = reader.ReadByte();
			UnknownVariable1 = reader.ReadByte();
			UnknownVariable2 = reader.ReadByte();
			Value = reader.ReadUInt32();
		}

		/// <summary>
		/// Writes this stat to the given writer in the save format.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.Write(ID);
			writer.Write(UnknownVariable1);
			writer.Write(UnknownVariable2);
			writer.Write(Value);
		}

		/// <summary>
		/// The stat ID used by the save format.
		/// </summary>
		public Byte ID {
			get { return StatDefinitions.FirstOrDefault(x => x.Value == StatID).Key; }
			set { StatID = StatDefinitions[value]; }
		}

		/// <summary>
		/// The <see cref="StatID"/> of this stat.
		/// </summary>
		public StatID StatID;

		/// <summary>
		/// An unknown variable. It seems to always be 6.
		/// </summary>
		public Byte UnknownVariable1;

		/// <summary>
		/// An unknown variable. It seems to always be 1.
		/// </summary>
		public Byte UnknownVariable2;

		/// <summary>
		/// The current value of this stat.
		/// </summary>
		public UInt32 Value;

		/// <summary>
		/// Maps the number associated with a stat with a <see cref="StatID"/>.
		/// </summary>
		public static readonly Dictionary<Byte, StatID> StatDefinitions = new Dictionary<Byte, StatID> {
			{ 0, StatID.Unknown0 },
			{ 1, StatID.Unknown1 },
			{ 2, StatID.Unknown2 },
			{ 3, StatID.Unknown3 },
			{ 4, StatID.Unknown4 },
			{ 5, StatID.Unknown5 },
			{ 6, StatID.Unknown6 },
			{ 7, StatID.Unknown7 },
			{ 8, StatID.Unknown8 },
			{ 9, StatID.Unknown9 },
			{ 10, StatID.Unknown10 },
			{ 11, StatID.Unknown11 },
			{ 12, StatID.Unknown12 },
			{ 13, StatID.Unknown13 },
			{ 14, StatID.Unknown14 },
			{ 15, StatID.Unknown15 },
			{ 16, StatID.Unknown16 },
			{ 17, StatID.Unknown17 },
			{ 18, StatID.Unknown18 },
			{ 85, StatID.ShotsFired },
			{ 86, StatID.Unknown86 },
			{ 87, StatID.Unknown87 },
			{ 88, StatID.DuelsWon }, //? I've chucked duels here, but that's not where they are.
			{ 89, StatID.Unknown89 },
			{ 90, StatID.Unknown90 },
			{ 91, StatID.Unknown91 },
			{ 93, StatID.Unknown93 },
			{ 94, StatID.Unknown94 },
			{ 95, StatID.Kills },
			{ 96, StatID.SkagKills },
			{ 97, StatID.PistolKills },
			{ 98, StatID.ShotgunKills },
			{ 99, StatID.SMGKills },
			{ 100, StatID.SniperKills },
			{ 101, StatID.CombatRifleKills },
			{ 102, StatID.RocketLauncherKills },
			{ 103, StatID.GrenadeKills }, //? Double check.
			{ 104, StatID.VehicleKills },
			{ 105, StatID.Unknown105 },
			{ 106, StatID.CorrosiveKills },
			{ 107, StatID.FireKills },
			{ 108, StatID.ShockKills },
			{ 109, StatID.ExplosiveKills },
			{ 110, StatID.CriticalHitKills },
			{ 111, StatID.Unknown111 },
			{ 112, StatID.Unknown112 },
			{ 113, StatID.Unknown113 },
			{ 114, StatID.MeleeKills },
			{ 115, StatID.Unknown115 },
			{ 116, StatID.Unknown116 },
			{ 117, StatID.Unknown117 },
			{ 118, StatID.Unknown118 },
			{ 119, StatID.SpiderantKills }, //? Double check.
			{ 120, StatID.HumanKills },
			{ 121, StatID.RakkKills },
			{ 122, StatID.Unknown122 },
			{ 123, StatID.Unknown123 },
			{ 124, StatID.Unknown124 },
			{ 125, StatID.Unknown125 },
			{ 126, StatID.Unknown126 },
			{ 127, StatID.Unknown127 },
			{ 128, StatID.Unknown128 },
			{ 129, StatID.Unknown129 },
			{ 130, StatID.Unknown130 },
			{ 131, StatID.Unknown131 },
			{ 132, StatID.Unknown132 },
			{ 133, StatID.ChestsOpened },
			{ 134, StatID.LootablesOpened },
			{ 135, StatID.Unknown135 },
			{ 136, StatID.Unknown136 },
			{ 137, StatID.Unknown137 },
			{ 138, StatID.Unknown138 },
			{ 139, StatID.Unknown139 },
			{ 140, StatID.Unknown140 },
			{ 141, StatID.BoughtItems },
			{ 142, StatID.SoldItems },
			{ 143, StatID.Unknown143 },
			{ 144, StatID.Unknown144 },
			{ 145, StatID.Unknown145 },
			{ 146, StatID.Unknown146 },
			{ 147, StatID.Unknown147 },
			{ 148, StatID.Unknown148 },
			{ 149, StatID.Unknown149 },
			{ 150, StatID.Unknown150 },
			{ 151, StatID.Unknown151 },
			{ 152, StatID.Unknown152 },
			{ 153, StatID.Unknown153 },
			{ 154, StatID.Unknown154 },
			{ 155, StatID.Unknown155 },
			{ 156, StatID.Unknown156 },
			{ 157, StatID.GuardianKills }, //? Double check.
			{ 158, StatID.Unknown158 },
			{ 159, StatID.Unknown159 },
			{ 160, StatID.Unknown160 },
			{ 161, StatID.MoneyEarned },
			{ 162, StatID.VehicleHangTime },
			{ 163, StatID.Unknown163 },
			{ 164, StatID.Unknown164 },
			{ 165, StatID.Unknown165 },
			{ 166, StatID.ActionSkillKills },
			{ 167, StatID.Unknown167 },
			{ 168, StatID.Unknown168 },
			{ 169, StatID.Unknown169 },
			{ 170, StatID.Unknown170 },
			{ 171, StatID.Unknown171 },
			{ 172, StatID.Unknown172 },
			{ 173, StatID.Unknown173 },
			{ 174, StatID.Unknown174 },
			{ 175, StatID.Unknown175 },
			{ 176, StatID.Unknown176 },
			{ 177, StatID.Unknown177 },
			{ 178, StatID.ChainKills },
			{ 179, StatID.Unknown179 },
			{ 180, StatID.Unknown180 },
			{ 181, StatID.Unknown181 },
			{ 183, StatID.Unknown183 },
			{ 184, StatID.Unknown184 },
			{ 185, StatID.Unknown185 },
			{ 186, StatID.Unknown186 },
			{ 187, StatID.Unknown187 },
			{ 188, StatID.Unknown188 },
			{ 189, StatID.Unknown189 },
			{ 190, StatID.Unknown190 },
			{ 191, StatID.Unknown191 },
			{ 192, StatID.Unknown192 },
			{ 193, StatID.Unknown193 },
			{ 194, StatID.Unknown194 },
			{ 195, StatID.Unknown195 },
			{ 196, StatID.Unknown196 },
			{ 197, StatID.Unknown197 },
			{ 198, StatID.Unknown198 },
			{ 199, StatID.Unknown199 },
			{ 200, StatID.Unknown200 },
			{ 201, StatID.Unknown201 },
			{ 202, StatID.Unknown202 },
			{ 203, StatID.Unknown203 },
			{ 204, StatID.Unknown204 },
			{ 205, StatID.Unknown205 },
			{ 206, StatID.Unknown206 },
			{ 207, StatID.Unknown207 },
			{ 208, StatID.Unknown208 },
			{ 209, StatID.Unknown209 },
			{ 210, StatID.Unknown210 },
			{ 211, StatID.Unknown211 },
			{ 212, StatID.Unknown212 },
			{ 213, StatID.Unknown213 },
			{ 214, StatID.Unknown214 },
			{ 215, StatID.Unknown215 },
			{ 216, StatID.Unknown216 },
			{ 217, StatID.Unknown217 },
			{ 218, StatID.Unknown218 },
			{ 219, StatID.Unknown219 },
			{ 220, StatID.Unknown220 },
			{ 221, StatID.Unknown221 },
			{ 222, StatID.Unknown222 },
			{ 223, StatID.Unknown223 },
			{ 224, StatID.Unknown224 },
			{ 225, StatID.Unknown225 },
			{ 226, StatID.Unknown226 },
			{ 227, StatID.Unknown227 },
			{ 228, StatID.Unknown228 },
			{ 229, StatID.Unknown229 },
			{ 230, StatID.Unknown230 },
			{ 231, StatID.Unknown231 },
			{ 232, StatID.Unknown232 },
			{ 233, StatID.Unknown233 },
			{ 234, StatID.Unknown234 },
			{ 235, StatID.Unknown235 },
			{ 236, StatID.Unknown236 },
			{ 237, StatID.Unknown237 },
			{ 238, StatID.Unknown238 },
			{ 239, StatID.Unknown239 },
			{ 240, StatID.Unknown240 },
			{ 241, StatID.Unknown241 },
			{ 242, StatID.Unknown242 },
			{ 243, StatID.Unknown243 },
			{ 244, StatID.Unknown244 },
			{ 245, StatID.Unknown245 },
			{ 246, StatID.Unknown246 },
			{ 247, StatID.Unknown247 },
			{ 248, StatID.Unknown248 },
			{ 249, StatID.Unknown249 },
			{ 250, StatID.Unknown250 },
			{ 251, StatID.Unknown251 },
			{ 252, StatID.Unknown252 },
			{ 253, StatID.Unknown253 },
			{ 254, StatID.Unknown254 },
			{ 255, StatID.Unknown255 }
		};
	}
}

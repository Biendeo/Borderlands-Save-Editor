using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Stores all the stats for a character.
	/// </summary>
	public class StatsTable {
		/// <summary>
		/// Reads in the stats table from a save file with the reader at the correct position.
		/// </summary>
		/// <param name="reader"></param>
		public void ReadStats(BinaryReader reader) {
			Int32 statsLength = reader.ReadInt32();
			byte[] statsBuffer = reader.ReadBytes(statsLength);
			var statsStream = new MemoryStream(statsBuffer);
			var statsReader = new BinaryReader(statsStream);

			UnknownVariable1 = statsReader.ReadInt32();
			TotalBytesSize = statsReader.ReadInt32();
			TotalEntries = statsReader.ReadInt16();

			Stats.Clear();
			for (Int16 x = 0; x < TotalEntries; ++x) {
				var stat = new Stat(statsReader);
				Stats[stat.StatID] = stat;
			}
		}

		/// <summary>
		/// Writes this stats table to the given writer in the save format.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.Write(10 + Stats.Count * 7);
			writer.Write(UnknownVariable1);
			writer.Write(TotalBytesSize);
			writer.Write(TotalEntries);
			foreach (var stat in Stats.Values) {
				stat.Write(writer);
			}
		}

		/// <summary>
		/// An unknown variable.
		/// </summary>
		public Int32 UnknownVariable1;

		/// <summary>
		/// The total number of bytes for the stats block. Interestingly the stats section begins
		/// with this, maybe since the stats have an odd size.
		/// </summary>
		public Int32 TotalBytesSize;

		/// <summary>
		/// How many stats exist in the save file. Since all stats are active all the time, this
		/// is a bit redundant.
		/// </summary>
		public Int16 TotalEntries;

		/// <summary>
		/// All the stats in the game.
		/// </summary>
		public Dictionary<StatID, Stat> Stats;

		/// <summary>
		/// Constructs an empty stat table.
		/// </summary>
		public StatsTable() {
			Stats = new Dictionary<StatID, Stat>();
			foreach (StatID id in Enum.GetValues(typeof(StatID))) {
				Stats.Add(id, new Stat {
					StatID = id
				});
			}
		}
	}
}

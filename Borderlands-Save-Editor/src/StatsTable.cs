using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Stores all the stats for a character.
	/// </summary>
	public class StatsTable {
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
		/// A list of all the stats (should it be a map?).
		/// </summary>
		public List<Stat> Stats;

		/// <summary>
		/// Constructs an empty stat table.
		/// </summary>
		public StatsTable() {
			Stats = new List<Stat>();
		}
	}
}

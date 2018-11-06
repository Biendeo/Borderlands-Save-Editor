using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Stores all the echoes that have been acquired on a single playthrough.
	/// </summary>
	public class EchoPlaythrough {
		/// <summary>
		/// Notes which playthrough number this is.
		/// 
		/// TODO: Determine what numbers are valid.
		/// </summary>
		public Int32 Playthrough;

		/// <summary>
		/// A list of all the echoes found.
		/// </summary>
		public List<Echo> Echoes;

		/// <summary>
		/// Constructs an empty list of echoes.
		/// </summary>
		public EchoPlaythrough() {
			Echoes = new List<Echo>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public class EchoPlaythrough {
		public Int32 Playthrough;
		public List<Echo> Echoes;

		public EchoPlaythrough() {
			Echoes = new List<Echo>();
		}
	}
}

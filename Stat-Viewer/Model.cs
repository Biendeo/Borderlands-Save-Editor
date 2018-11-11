using Borderlands_Save_Editor.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stat_Viewer {
	internal class Model {
		public Save Save { get; set; }

		public Model() {
			Save = new Save();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public class Playthrough {
		public Int32 Number;
		public string ActiveMissionName;
		public Mission ActiveMission { get { return Missions.Find(x => x.InternalName == ActiveMissionName); } }
		public List<Mission> Missions;

		public Playthrough() {
			Missions = new List<Mission>();
		}
	}
}

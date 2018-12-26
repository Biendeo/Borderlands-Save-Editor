using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Stores properties related to a specific playthrough.
	/// 
	/// TODO: Just add echoes to the playthrough since they're hand in hand.
	/// </summary>
	public class Playthrough {
		/// <summary>
		/// The playthrough number.
		/// 
		/// TODO: See what values this could be.
		/// </summary>
		public Int32 Number;

		/// <summary>
		/// The name of the active mission on this playthrough.
		/// 
		/// TODO: Stronger typing later on.
		/// </summary>
		public string ActiveMissionName;

		/// <summary>
		/// The active mission object.
		/// </summary>
		public Mission ActiveMission { get { return Missions.First(x => x.Value.InternalName == ActiveMissionName).Value; } }

		/// <summary>
		/// A list of all missions associated with this playthrough.
		/// </summary>
		public Dictionary<MissionID, Mission> Missions;

		/// <summary>
		/// Constructs a new playthrough without any missions logged.
		/// </summary>
		public Playthrough() {
			Missions = new Dictionary<MissionID, Mission>();
			foreach (MissionID id in Enum.GetValues(typeof(MissionID))) {
				var newMission = new Mission {
					ID = id,
					Status = Mission.MissionStatus.Unknown
				};
				Missions.Add(id, newMission);
			}
		}

		/// <summary>
		/// Returns the total number of missions completed on this playthrough.
		/// </summary>
		public Int32 MissionsCompleted { get { return Missions.Sum(m => m.Value.Status == Mission.MissionStatus.Completed ? 1 : 0); } }

		/// <summary>
		/// Returns the total number of missions seen on this playthrough (i.e. not unknown).
		/// </summary>
		public Int32 FoundMissionsCount { get { return Missions.Sum(m => m.Value.Status != Mission.MissionStatus.Unknown ? 1 : 0); } }

		/// <summary>
		/// The total number of missions in the game.
		/// </summary>
		public const Int32 TotalMissions = 216;
	}
}

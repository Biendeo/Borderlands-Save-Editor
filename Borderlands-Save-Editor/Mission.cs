using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum MissionID {

	}

	public class Mission {
		public string InternalName;
		public Int32 MissionStatusFlag;
		public MissionStatus Status { get { return MissionStatusDefinitions[MissionStatusFlag]; } }
		public Int32 UnknownVariable2; //? Always 0.
		public Int32 UnknownVariable3; //? Always 0.
		public List<MissionDetails> Details;


		public Mission() {
			Details = new List<MissionDetails>();
		}

		public class MissionDetails {
			public string UnknownString;
			public Int32 UnknownVariable;
		}

		public enum MissionStatus {
			Unknown,
			InProgress,
			ReadyToTurnIn,
			Completed
		}

		public readonly Dictionary<Int32, MissionStatus> MissionStatusDefinitions = new Dictionary<Int32, MissionStatus> {
			{ 0, MissionStatus.Unknown },
			{ 1, MissionStatus.InProgress },
			{ 2, MissionStatus.ReadyToTurnIn },
			{ 4, MissionStatus.Completed },
		};
	}
}

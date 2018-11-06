using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Identifies a mission.
	/// </summary>
	public enum MissionID {

	}

	/// <summary>
	/// Properties that are held by missions.
	/// </summary>
	public class Mission {
		/// <summary>
		/// The internal name of this mission.
		/// </summary>
		public string InternalName;

		/// <summary>
		/// The status of this mission as a flag.
		/// </summary>
		public Int32 MissionStatusFlag;

		/// <summary>
		/// The status of this mission.
		/// </summary>
		public MissionStatus Status { get { return MissionStatusDefinitions[MissionStatusFlag]; } }

		/// <summary>
		/// An unknown variable; it appears to always be 0.
		/// </summary>
		public Int32 UnknownVariable2;

		/// <summary>
		/// An unknown variable; it appears to always be 0.
		/// </summary>
		public Int32 UnknownVariable3;

		/// <summary>
		/// The details of this mission (i.e. additional objectives).
		/// </summary>
		public List<MissionDetails> Details;

		/// <summary>
		/// Creates a blank mission.
		/// </summary>
		public Mission() {
			Details = new List<MissionDetails>();
		}

		/// <summary>
		/// Stores the details of a mission.
		/// </summary>
		public class MissionDetails {
			/// <summary>
			/// An unknown string.
			/// </summary>
			public string UnknownString;

			/// <summary>
			/// An unknown variable.
			/// </summary>
			public Int32 UnknownVariable;
		}

		/// <summary>
		/// Stores the status of a mission.
		/// </summary>
		public enum MissionStatus {
			/// <summary>
			/// This mission is not yet known to the player.
			/// </summary>
			Unknown,
			/// <summary>
			/// This mission is in progress.
			/// </summary>
			InProgress,
			/// <summary>
			/// This mission is ready to turn in.
			/// </summary>
			ReadyToTurnIn,
			/// <summary>
			/// This mission has been completed.
			/// </summary>
			Completed
		}

		/// <summary>
		/// Maps status flags to <see cref="MissionStatus"/>.
		/// </summary>
		public readonly Dictionary<Int32, MissionStatus> MissionStatusDefinitions = new Dictionary<Int32, MissionStatus> {
			{ 0, MissionStatus.Unknown },
			{ 1, MissionStatus.InProgress },
			{ 2, MissionStatus.ReadyToTurnIn },
			{ 4, MissionStatus.Completed },
		};
	}
}

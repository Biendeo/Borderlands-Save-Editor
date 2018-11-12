using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor.Save {
	/// <summary>
	/// Defines all the locations used in the game.
	/// </summary>
	public enum Location {
		// I'm lazy with the comments.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		Fyrestone,
		EntranceToSkagGully,
		SkagGully,
		AridHillsEntrance,
		Tunnel,
		DryEntranceSave,
		BunkerEntrance,
		Bunker,
		HeadstoneMine,
		TheLostCave,
		MineEntrance,
		LuckysLastChanceWateringHole,
		HeadlandElectricalStation,
		LudicrousSpeedway,
		DevilsGateHill,
		NewHaven,
		SkullBandit,
		TetanusWarrensEntrance,
		TetanusWarren,
		TheCesspool,
		OuterYard,
		TheUnderpass,
		TrashEntranceNorth,
		Scrapyard,
		PirateBay,
		MiddleofNoWhere,
		KCanyon,
		cauldren,
		Scraple,
		OldHavenEntrance,
		Interlude2Entrance,
		AqueductEncampment,
		TrashCoast,
		Thor_Trash,
		SaltFlats,
		Thor_Digtown,
		Thor_Digger,
		Thor_Cave,
		Interlude2Cave,
		Crimson_Fastness,
		Crimson_Fastness_Midpoint,
		CrimsonEnclave1,
		Thor_CrimsonEnclave,
		Thor_TheDescent,
		Descent1,
		EridianPromontory,
		TheVault,
		JakobsCove,
		DropShip,
		Coastline,
		NedsSwamp,
		Checkpoint_swamp_ned,
		SecretEntrance,
		MonsterHouseStart,
		DeadHaven,
		EntrancetoSawmill,
		SawMill,
		BossElevatorEntrance,
		BossBeginning,
		Coliseum,
		TBoneJunc,
		TartarusStation
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Extension methods related to locations.
	/// </summary>
	public static class LocationExtensions {
		/// <summary>
		/// Returns the a readable name for this location.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public static string ToString(this Location location) {
			return location.ProficiencyName();
		}

		/// <summary>
		/// Returns the <see cref="Location"/> that uses the given internal name.
		/// </summary>
		/// <param name="internalName"></param>
		/// <returns></returns>
		public static Location FromInternalName(string internalName) {
			return InternalNames.FirstOrDefault(x => x.Value == internalName).Key;
		}

		/// <summary>
		/// Returns the internal name for this location.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public static string InternalName(this Location location) {
			return InternalNames[location];
		}

		/// <summary>
		/// Returns a readable name for this location.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public static string ProficiencyName(this Location location) {
			return ProficiencyNames[location];
		}

		/// <summary>
		/// Maps <see cref="Location"/> to their internal names.
		/// </summary>
		public static readonly Dictionary<Location, string> InternalNames = new Dictionary<Location, string> {
			{ Location.Fyrestone, "Fyrestone" },
			{ Location.EntranceToSkagGully, "EntranceToSkagGully" },
			{ Location.SkagGully, "SkagGully" },
			{ Location.AridHillsEntrance, "AridHillsEntrance" },
			{ Location.Tunnel, "Tunnel" },
			{ Location.DryEntranceSave, "DryEntranceSave" },
			{ Location.BunkerEntrance, "BunkerEntrance" },
			{ Location.Bunker, "Bunker" },
			{ Location.HeadstoneMine, "HeadstoneMine" },
			{ Location.TheLostCave, "TheLostCave" },
			{ Location.MineEntrance, "MineEntrance" },
			{ Location.LuckysLastChanceWateringHole, "LuckysLastChanceWateringHole" },
			{ Location.HeadlandElectricalStation, "HeadlandElectricalStation" },
			{ Location.LudicrousSpeedway, "LudicrousSpeedway" },
			{ Location.DevilsGateHill, "Devil'sGateHill" },
			{ Location.NewHaven, "NewHaven" },
			{ Location.SkullBandit, "SkullBandit" },
			{ Location.TetanusWarrensEntrance, "TetanusWarrensEntrance" },
			{ Location.TetanusWarren, "TetanusWarren" },
			{ Location.TheCesspool, "TheCesspool" },
			{ Location.OuterYard, "OuterYard" },
			{ Location.TheUnderpass, "TheUnderpass" },
			{ Location.TrashEntranceNorth, "TrashEntranceNorth" },
			{ Location.Scrapyard, "Scrapyard" },
			{ Location.PirateBay, "PirateBay" },
			{ Location.MiddleofNoWhere, "MiddleofNoWhere" },
			{ Location.KCanyon, "KCanyon" },
			{ Location.cauldren, "cauldren" },
			{ Location.Scraple, "Scraple" },
			{ Location.OldHavenEntrance, "OldHavenEntrance" },
			{ Location.Interlude2Entrance, "Interlude2Entrance" },
			{ Location.AqueductEncampment, "AqueductEncampment" },
			{ Location.TrashCoast, "TrashCoast" },
			{ Location.Thor_Trash, "Thor_Trash" },
			{ Location.SaltFlats, "SaltFlats" },
			{ Location.Thor_Digtown, "Thor_Digtown" },
			{ Location.Thor_Digger, "Thor_Digger" },
			{ Location.Thor_Cave, "Thor_Cave" },
			{ Location.Interlude2Cave, "Interlude2Cave" },
			{ Location.Crimson_Fastness, "Crimson_Fastness" },
			{ Location.Crimson_Fastness_Midpoint, "Crimson_Fastness_Midpoint" },
			{ Location.CrimsonEnclave1, "CrimsonEnclave1" },
			{ Location.Thor_CrimsonEnclave, "Thor_CrimsonEnclave" },
			{ Location.Thor_TheDescent, "Thor_TheDescent" },
			{ Location.Descent1, "Descent1" },
			{ Location.EridianPromontory, "EridianPromontory" },
			{ Location.TheVault, "TheVault" },
			{ Location.JakobsCove, "JakobsCove" },
			{ Location.DropShip, "DropShip" },
			{ Location.Coastline, "Coastline" },
			{ Location.NedsSwamp, "NedsSwamp" },
			{ Location.Checkpoint_swamp_ned, "Checkpoint_swamp_ned" },
			{ Location.SecretEntrance, "SecretEntrance" },
			{ Location.MonsterHouseStart, "MonsterHouseStart" },
			{ Location.DeadHaven, "DeadHaven" },
			{ Location.EntrancetoSawmill, "EntrancetoSawmill" },
			{ Location.SawMill, "SawMill" },
			{ Location.BossElevatorEntrance, "BossElevatorEntrance" },
			{ Location.BossBeginning, "BossBeginning" },
			{ Location.Coliseum, "Coliseum" },
			{ Location.TBoneJunc, "TBoneJunc" },
			{ Location.TartarusStation, "TartarusStation" }
		};

		/// <summary>
		/// Maps <see cref="Location"/> to readable names.
		/// 
		/// TODO: I just copied these over. Clean up some of them to be more identifiable.
		/// TODO: Also change the enum names to be more appropriate.
		/// </summary>
		public static readonly Dictionary<Location, string> ProficiencyNames = new Dictionary<Location, string> {
			{ Location.Fyrestone, "Fyrestone" },
			{ Location.EntranceToSkagGully, "EntranceToSkagGully" },
			{ Location.SkagGully, "SkagGully" },
			{ Location.AridHillsEntrance, "AridHillsEntrance" },
			{ Location.Tunnel, "Tunnel" },
			{ Location.DryEntranceSave, "DryEntranceSave" },
			{ Location.BunkerEntrance, "BunkerEntrance" },
			{ Location.Bunker, "Bunker" },
			{ Location.HeadstoneMine, "HeadstoneMine" },
			{ Location.TheLostCave, "TheLostCave" },
			{ Location.MineEntrance, "MineEntrance" },
			{ Location.LuckysLastChanceWateringHole, "LuckysLastChanceWateringHole" },
			{ Location.HeadlandElectricalStation, "HeadlandElectricalStation" },
			{ Location.LudicrousSpeedway, "LudicrousSpeedway" },
			{ Location.DevilsGateHill, "Devil'sGateHill" },
			{ Location.NewHaven, "NewHaven" },
			{ Location.SkullBandit, "SkullBandit" },
			{ Location.TetanusWarrensEntrance, "TetanusWarrensEntrance" },
			{ Location.TetanusWarren, "TetanusWarren" },
			{ Location.TheCesspool, "TheCesspool" },
			{ Location.OuterYard, "OuterYard" },
			{ Location.TheUnderpass, "TheUnderpass" },
			{ Location.TrashEntranceNorth, "TrashEntranceNorth" },
			{ Location.Scrapyard, "Scrapyard" },
			{ Location.PirateBay, "PirateBay" },
			{ Location.MiddleofNoWhere, "MiddleofNoWhere" },
			{ Location.KCanyon, "KCanyon" },
			{ Location.cauldren, "cauldren" },
			{ Location.Scraple, "Scraple" },
			{ Location.OldHavenEntrance, "OldHavenEntrance" },
			{ Location.Interlude2Entrance, "Interlude2Entrance" },
			{ Location.AqueductEncampment, "AqueductEncampment" },
			{ Location.TrashCoast, "TrashCoast" },
			{ Location.Thor_Trash, "Thor_Trash" },
			{ Location.SaltFlats, "SaltFlats" },
			{ Location.Thor_Digtown, "Thor_Digtown" },
			{ Location.Thor_Digger, "Thor_Digger" },
			{ Location.Thor_Cave, "Thor_Cave" },
			{ Location.Interlude2Cave, "Interlude2Cave" },
			{ Location.Crimson_Fastness, "Crimson_Fastness" },
			{ Location.Crimson_Fastness_Midpoint, "Crimson_Fastness_Midpoint" },
			{ Location.CrimsonEnclave1, "CrimsonEnclave1" },
			{ Location.Thor_CrimsonEnclave, "Thor_CrimsonEnclave" },
			{ Location.Thor_TheDescent, "Thor_TheDescent" },
			{ Location.Descent1, "Descent1" },
			{ Location.EridianPromontory, "EridianPromontory" },
			{ Location.TheVault, "TheVault" },
			{ Location.JakobsCove, "JakobsCove" },
			{ Location.DropShip, "DropShip" },
			{ Location.Coastline, "Coastline" },
			{ Location.NedsSwamp, "NedsSwamp" },
			{ Location.Checkpoint_swamp_ned, "Checkpoint_swamp_ned" },
			{ Location.SecretEntrance, "SecretEntrance" },
			{ Location.MonsterHouseStart, "MonsterHouseStart" },
			{ Location.DeadHaven, "DeadHaven" },
			{ Location.EntrancetoSawmill, "EntrancetoSawmill" },
			{ Location.SawMill, "SawMill" },
			{ Location.BossElevatorEntrance, "BossElevatorEntrance" },
			{ Location.BossBeginning, "BossBeginning" },
			{ Location.Coliseum, "Coliseum" },
			{ Location.TBoneJunc, "TBoneJunc" },
			{ Location.TartarusStation, "TartarusStation" }
		};
	}
}

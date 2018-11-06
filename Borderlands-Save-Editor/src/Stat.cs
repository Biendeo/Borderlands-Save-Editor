using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Identifies a stat type.
	/// </summary>
	public enum StatID {
		// I'm lazy.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		ShotsFired,
		Kills,
		SkagKills,
		PistolKills,
		ShotgunKills,
		SMGKills,
		SniperKills,
		CombatRifleKills,
		RocketLauncherKills,
		GrenadeKills,
		VehicleKills,
		CorrosiveKills,
		FireKills,
		ShockKills,
		ExplosiveKills,
		CriticalHitKills,
		MeleeKills,
		SpiderantKills,
		HumanKills,
		RakkKills,
		ChestsOpened,
		LootablesOpened,
		BoughtItems,
		SoldItems,
		GuardianKills,
		MoneyEarned,
		VehicleHangTime,
		ActionSkillKills,
		ChainKills,
		DuelsWon //? I don't know the ID of this one.
				 //? Maybe chuck dummy stats to bind the rest?
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Stores all the properties of a stat.
	/// </summary>
	public class Stat {
		/// <summary>
		/// The stat ID used by the save format.
		/// </summary>
		public Byte ID;

		/// <summary>
		/// The <see cref="StatID"/> of this stat.
		/// </summary>
		public StatID StatID { get { return StatDefinitions[ID]; } }

		/// <summary>
		/// An unknown variable. It seems to always be 6.
		/// </summary>
		public Byte UnknownVariable1;

		/// <summary>
		/// An unknown variable. It seems to always be 1.
		/// </summary>
		public Byte UnknownVariable2;

		/// <summary>
		/// The current value of this stat.
		/// </summary>
		public UInt32 Value;

		/// <summary>
		/// Maps the number associated with a stat with a <see cref="StatID"/>.
		/// </summary>
		public static readonly Dictionary<Byte, StatID> StatDefinitions = new Dictionary<Byte, StatID> {
			{ 85, StatID.ShotsFired },
			{ 95, StatID.Kills },
			{ 96, StatID.SkagKills },
			{ 97, StatID.PistolKills },
			{ 98, StatID.ShotgunKills },
			{ 99, StatID.SMGKills },
			{ 100, StatID.SniperKills },
			{ 101, StatID.CombatRifleKills },
			{ 102, StatID.RocketLauncherKills },
			{ 103, StatID.GrenadeKills }, //? Double check.
			{ 104, StatID.VehicleKills },
			{ 106, StatID.CorrosiveKills },
			{ 107, StatID.FireKills },
			{ 108, StatID.ShockKills },
			{ 109, StatID.ExplosiveKills },
			{ 110, StatID.CriticalHitKills },
			{ 114, StatID.MeleeKills },
			{ 119, StatID.SpiderantKills }, //? Double check.
			{ 120, StatID.HumanKills },
			{ 121, StatID.RakkKills },
			{ 133, StatID.ChestsOpened },
			{ 134, StatID.LootablesOpened },
			{ 141, StatID.BoughtItems },
			{ 142, StatID.SoldItems },
			{ 157, StatID.GuardianKills }, //? Double check.
			{ 161, StatID.MoneyEarned },
			{ 162, StatID.VehicleHangTime },
			{ 166, StatID.ActionSkillKills },
			{ 178, StatID.ChainKills }
		};
	}
}

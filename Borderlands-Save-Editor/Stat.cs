using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum StatID {
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
	}

	public class Stat {
		public Byte ID;
		public StatID StatID { get { return StatDefinitions[ID]; } }
		public Byte UnknownVariable1; //? Always 6?
		public Byte UnknownVariable2; //? Always 1?
		public UInt32 Value;

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

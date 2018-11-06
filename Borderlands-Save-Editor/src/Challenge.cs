using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Describes a challenge that is tracked in-game.
	/// </summary>
	public class Challenge {
		/// <summary>
		/// The name of the challenge.
		/// </summary>
		public string Name;

		/// <summary>
		/// A list of all the trasks that must be achieved in order to complete this challenge.
		/// This is only necessary for the "12 Days of Pandora" challenge, as every other challenge
		/// only follows on stat.
		/// </summary>
		public List<ChallengeGoal> Goals;

		/// <summary>
		/// The amount of experience rewarded for completing the challenge.
		/// </summary>
		public Int32 XpReward;

		/// <summary>
		/// Constructs a <see cref="Challenge"/> with the given properties.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="goals"></param>
		/// <param name="xpReward"></param>
		public Challenge(string name, List<ChallengeGoal> goals, Int32 xpReward) {
			Name = name;
			Goals = goals;
			XpReward = xpReward;
		}

		/// <summary>
		/// A single goal of a challenge.
		/// </summary>
		public struct ChallengeGoal {
			/// <summary>
			/// The stat that is being tracked. Conveniently no challenges use anything but stats.
			/// </summary>
			public StatID Stat;

			/// <summary>
			/// The value that the stat must reach.
			/// </summary>
			public Int32 Count;

			/// <summary>
			/// Constructs a goal using th stat and the given value.
			/// </summary>
			/// <param name="stat"></param>
			/// <param name="count"></param>
			public ChallengeGoal(StatID stat, Int32 count) {
				Stat = stat;
				Count = count;
			}
		}

		/// <summary>
		/// A list of all the challenges in the game.
		/// </summary>
		public static readonly List<Challenge> Challenges = new List<Challenge> {
			new Challenge("12 Days of Pandora", new List<ChallengeGoal> {
				new ChallengeGoal(StatID.CombatRifleKills, 12),
				new ChallengeGoal(StatID.PistolKills, 11),
				new ChallengeGoal(StatID.ShotgunKills, 10),
				new ChallengeGoal(StatID.SMGKills, 9),
				new ChallengeGoal(StatID.SniperKills, 8),
				new ChallengeGoal(StatID.MeleeKills, 7),
				new ChallengeGoal(StatID.CriticalHitKills, 6),
				new ChallengeGoal(StatID.ExplosiveKills, 5),
				new ChallengeGoal(StatID.ShockKills, 4),
				new ChallengeGoal(StatID.FireKills, 3),
				new ChallengeGoal(StatID.CorrosiveKills, 2),
				new ChallengeGoal(StatID.GrenadeKills, 1),
			}, 5000),
			new Challenge("Headhunter", new List<ChallengeGoal>{ new ChallengeGoal(StatID.HumanKills, 50) }, 1000),
			new Challenge("Not Really a People Person", new List<ChallengeGoal>{ new ChallengeGoal(StatID.HumanKills, 250) }, 2000),
			new Challenge("Misanthrope", new List<ChallengeGoal>{ new ChallengeGoal(StatID.HumanKills, 1000) }, 5000),
			new Challenge("War Criminal", new List<ChallengeGoal>{ new ChallengeGoal(StatID.HumanKills, 2500) }, 20000),
			new Challenge("Lucky Shot!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CriticalHitKills, 100) }, 1000),
			new Challenge("Crack Shot", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CriticalHitKills, 250) }, 2000),
			new Challenge("Don't You Ever Miss?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CriticalHitKills, 1000) }, 5000),
			new Challenge("Brain Surgeon", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CriticalHitKills, 2500) }, 20000),
			new Challenge("Relentless", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChainKills, 5) }, 500),
			new Challenge("Chain Killer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChainKills, 10) }, 1000),
			new Challenge("Killing Spree", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChainKills, 15) }, 2000),
			new Challenge("Conveyor of Death", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChainKills, 25) }, 5000),
			new Challenge("Action Hero", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ActionSkillKills, 50) }, 1000),
			new Challenge("Beware! Mad Skills!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ActionSkillKills, 250) }, 2000),
			new Challenge("Why do you even carry a gun?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ActionSkillKills, 1000) }, 5000),
			new Challenge("Your Kung Fu is Best", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ActionSkillKills, 2500) }, 20000),
			new Challenge("Seasonsed Killer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.Kills, 500) }, 1000),
			new Challenge("Numb to the Voices", new List<ChallengeGoal>{ new ChallengeGoal(StatID.Kills, 1000) }, 2000),
			new Challenge("Terror of Pandora", new List<ChallengeGoal>{ new ChallengeGoal(StatID.Kills, 4000) }, 5000),
			new Challenge("I am become Death...", new List<ChallengeGoal>{ new ChallengeGoal(StatID.Kills, 10000) }, 20000),
			new Challenge("Pocket Change", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MoneyEarned, 10000) }, 1000),
			new Challenge("Money. It buys Happiness", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MoneyEarned, 250000) }, 2000),
			new Challenge("The Rich get Richer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MoneyEarned, 1000000) }, 5000),
			new Challenge("How Much For The Planet?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MoneyEarned, 9999999) }, 20000),
			new Challenge("Duelist", new List<ChallengeGoal>{ new ChallengeGoal(StatID.DuelsWon, 10) }, 500),
			new Challenge("Smack Down!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.DuelsWon, 50) }, 1000),
			new Challenge("Bragging Rights", new List<ChallengeGoal>{ new ChallengeGoal(StatID.DuelsWon, 250) }, 2000),
			new Challenge("Invicible", new List<ChallengeGoal>{ new ChallengeGoal(StatID.DuelsWon, 1000) }, 5000),
			new Challenge("Skag Slayer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SkagKills, 50) }, 1000),
			new Challenge("SIT! STAY! PLAY DEAD!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SkagKills, 250) }, 2000),
			new Challenge("I hate dogs", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SkagKills, 1000) }, 5000),
			new Challenge("You Are The Skagpocalypse", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SkagKills, 2500) }, 20000),
			new Challenge("Spiderant Slayer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SpiderantKills, 50) }, 1000),
			new Challenge("Bug Crusher", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SpiderantKills, 250) }, 2000),
			new Challenge("I hate bugs", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SpiderantKills, 1000) }, 5000),
			new Challenge("The Exterminator", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SpiderantKills, 2500) }, 20000),
			new Challenge("Rakk Slayer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RakkKills, 50) }, 1000),
			new Challenge("If it flies, it dies", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RakkKills, 250) }, 2000),
			new Challenge("Rakk Flak", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RakkKills, 1000) }, 5000),
			new Challenge("If I can't fly, no one flies", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RakkKills, 2500) }, 20000),
			new Challenge("What is that thing?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.GuardianKills, 50) }, 1000),
			new Challenge("The Vault is mine", new List<ChallengeGoal>{ new ChallengeGoal(StatID.GuardianKills, 250) }, 2000),
			new Challenge("Xenophobe", new List<ChallengeGoal>{ new ChallengeGoal(StatID.GuardianKills, 1000) }, 5000),
			new Challenge("Dominant species", new List<ChallengeGoal>{ new ChallengeGoal(StatID.GuardianKills, 2500) }, 20000),
			new Challenge("Spray and Prey", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CombatRifleKills, 50) }, 1000),
			new Challenge("Size matters", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CombatRifleKills, 250) }, 2000),
			new Challenge("Make Chunks", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CombatRifleKills, 1000) }, 5000),
			new Challenge("One Man Army", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CombatRifleKills, 2500) }, 20000),
			new Challenge("Draw!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.PistolKills, 50) }, 1000),
			new Challenge("Size doesn't matter", new List<ChallengeGoal>{ new ChallengeGoal(StatID.PistolKills, 250) }, 2000),
			new Challenge("No one's laughing now", new List<ChallengeGoal>{ new ChallengeGoal(StatID.PistolKills, 1000) }, 5000),
			new Challenge("Pistolero", new List<ChallengeGoal>{ new ChallengeGoal(StatID.PistolKills, 2500) }, 20000),
			new Challenge("Get off my lawn!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShotgunKills, 50) }, 1000),
			new Challenge("Taste the red mist", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShotgunKills, 250) }, 2000),
			new Challenge("Instant Autopsy", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShotgunKills, 1000) }, 5000),
			new Challenge("Buckshot Legend", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShotgunKills, 2500) }, 20000),
			new Challenge("Boom", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RocketLauncherKills, 50) }, 1000),
			new Challenge("BOOM", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RocketLauncherKills, 250) }, 2000),
			new Challenge("KaBOOM!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RocketLauncherKills, 1000) }, 5000),
			new Challenge("KA BOOOOOOOM!!!!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.RocketLauncherKills, 2500) }, 20000),
			new Challenge("Rat a Tat!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SMGKills, 50) }, 1000),
			new Challenge("So many little holes...", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SMGKills, 250) }, 2000),
			new Challenge("Bullet Hose", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SMGKills, 1000) }, 5000),
			new Challenge("Human Tsunami", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SMGKills, 2500) }, 20000),
			new Challenge("One Shot, One Kill", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SniperKills, 50) }, 1000),
			new Challenge("I'll wait back here...", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SniperKills, 250) }, 2000),
			new Challenge("Brain Ventilator", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SniperKills, 1000) }, 5000),
			new Challenge("Finger of God", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SniperKills, 2500) }, 20000),
			new Challenge("Punchy", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MeleeKills, 50) }, 1000),
			new Challenge("Brawler", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MeleeKills, 250) }, 2000),
			new Challenge("Boxer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MeleeKills, 1000) }, 5000),
			new Challenge("Heavyweight Champion", new List<ChallengeGoal>{ new ChallengeGoal(StatID.MeleeKills, 2500) }, 20000),
			new Challenge("Hot! Too Hot!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.FireKills, 25) }, 1000),
			new Challenge("Toasty!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.FireKills, 250) }, 2000),
			new Challenge("Plays with matches", new List<ChallengeGoal>{ new ChallengeGoal(StatID.FireKills, 1000) }, 5000),
			new Challenge("Pyromaniac", new List<ChallengeGoal>{ new ChallengeGoal(StatID.FireKills, 2500) }, 20000),
			new Challenge("Zap!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShockKills, 25) }, 1000),
			new Challenge("Nikola is a friend of mine", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShockKills, 250) }, 2000),
			new Challenge("Puts forks in sockets", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShockKills, 1000) }, 5000),
			new Challenge("Shocker", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ShockKills, 2500) }, 20000),
			new Challenge("Chemistry Rocks!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CorrosiveKills, 25) }, 1000),
			new Challenge("Why is the floor all gooey?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CorrosiveKills, 250) }, 2000),
			new Challenge("Mixes household chemicals", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CorrosiveKills, 1000) }, 5000),
			new Challenge("Chemist", new List<ChallengeGoal>{ new ChallengeGoal(StatID.CorrosiveKills, 2500) }, 20000),
			new Challenge("Boom goes the Dynamite!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ExplosiveKills, 25) }, 1000),
			new Challenge("Too bad about the Tinnitus", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ExplosiveKills, 250) }, 2000),
			new Challenge("Crosses border for fireworks", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ExplosiveKills, 1000) }, 5000),
			new Challenge("Demolitionist", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ExplosiveKills, 2500) }, 20000),
			new Challenge("Road Rage", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleKills, 25) }, 1000),
			new Challenge("Vehicular Manslaughter", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleKills, 150) }, 2000),
			new Challenge("School bus Driver", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleKills, 500) }, 5000),
			new Challenge("Hell on Wheels", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleKills, 1000) }, 20000),
			new Challenge("What's in here?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.LootablesOpened, 50) }, 1000),
			new Challenge("Scavenger", new List<ChallengeGoal>{ new ChallengeGoal(StatID.LootablesOpened, 250) }, 2000),
			new Challenge("Wilderness Survivor", new List<ChallengeGoal>{ new ChallengeGoal(StatID.LootablesOpened, 1000) }, 5000),
			new Challenge("No stone unturned", new List<ChallengeGoal>{ new ChallengeGoal(StatID.LootablesOpened, 5000) }, 20000),
			new Challenge("Ooo! Shiney!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChestsOpened, 5) }, 1000),
			new Challenge("Treasure Hunter", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChestsOpened, 25) }, 2000),
			new Challenge("Swag Master", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChestsOpened, 100) }, 5000),
			new Challenge("Envy of Pirates", new List<ChallengeGoal>{ new ChallengeGoal(StatID.ChestsOpened, 500) }, 20000),
			new Challenge("How much for the gun?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SoldItems, 50) }, 1000),
			new Challenge("Gun runner", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SoldItems, 100) }, 2000),
			new Challenge("Arms dealer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SoldItems, 400) }, 5000),
			new Challenge("Merchant of Death", new List<ChallengeGoal>{ new ChallengeGoal(StatID.SoldItems, 1200) }, 20000),
			new Challenge("Impulse buyer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 5) }, 1000),
			new Challenge("Good Consumer", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 50) }, 2000),
			new Challenge("I want it all!", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 200) }, 5000),
			new Challenge("Self contained economy", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 500) }, 20000),
			new Challenge("Lots of shots", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 2000) }, 1000),
			new Challenge("Keep firing", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 10000) }, 2000),
			new Challenge("Who made that man a gunner?", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 25000) }, 5000),
			new Challenge("I fired every bullet ever", new List<ChallengeGoal>{ new ChallengeGoal(StatID.BoughtItems, 100000) }, 20000),
			new Challenge("Hang Time", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleHangTime, 2000) }, 500),
			new Challenge("Airborne", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleHangTime, 3000) }, 1000),
			new Challenge("This is not a flight simulator", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleHangTime, 4000) }, 2000),
			new Challenge("Orbit Achieved", new List<ChallengeGoal>{ new ChallengeGoal(StatID.VehicleHangTime, 5000) }, 5000)
		};
	}
}

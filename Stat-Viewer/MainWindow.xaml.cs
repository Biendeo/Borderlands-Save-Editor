using Borderlands_Save_Editor;
using Borderlands_Save_Editor.Player.Proficiency;
using Borderlands_Save_Editor.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stat_Viewer {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public Save currentSave;
		public static readonly string filePath = "D:/Documents/my games/borderlands/savedata/";
		public static readonly string fileName = "save0009.sav";

		public MainWindow() {
			currentSave = Save.Read(filePath + fileName);
			FileSystemWatcher watcher = new FileSystemWatcher {
				Path = filePath,
				NotifyFilter = NotifyFilters.LastWrite,
				Filter = fileName
			};

			watcher.Changed += new FileSystemEventHandler(OnChanged);

			watcher.EnableRaisingEvents = true;

			InitializeComponent();

			UpdateLabels();
		}

		private void OnChanged(object source, FileSystemEventArgs e) {
			while (true) {
				try {
					currentSave = Save.Read(filePath + fileName);
					break;
				} catch {

				}
			}
			UpdateLabels();
		}

		private void UpdateLabels() {
			const Int32 MAX_LEVEL = 69;
			const Int32 NUM_MISSIONS = 216;
			const Int32 NUM_LOGS = 188;
			Int32 NUM_CHALLENGES = Challenge.Challenges.Count;

			Dispatcher.Invoke(() => {
				Int32 xpLevelValue = currentSave.Details.Level;
				Int32 xpLevelLimit = MAX_LEVEL;
				double xpLevelPercentage = xpLevelValue * 100.0 / xpLevelLimit;
				Int32 xpLevelXpValue = currentSave.Details.Experience - Save.ExperienceToLevels[xpLevelValue];
				Int32 xpLevelXpLimit = Save.ExperienceToLevels[Clamp(xpLevelValue + 1, 0, MAX_LEVEL)] - Save.ExperienceToLevels[Clamp(xpLevelValue, 0, MAX_LEVEL - 1)];
				double xpLevelXpPercentage = xpLevelXpValue * 100.0 / xpLevelXpLimit;
				Int32 xpTotalXpValue = currentSave.Details.Experience;
				Int32 xpTotalXpLimit = Save.ExperienceToLevels[MAX_LEVEL];
				double xpTotalXpPercentage = xpTotalXpValue * 100.0 / xpTotalXpLimit;
				XpLevelValueLabel.Content = $"{xpLevelValue}";
				XpLevelLimitLabel.Content = $"/{xpLevelLimit}";
				XpLevelProgress.Value = xpLevelPercentage;
				XpLevelXpValueLabel.Content = $"{xpLevelXpValue}";
				XpLevelXpLimitLabel.Content = $"/{xpLevelXpLimit}";
				XpLevelXpProgress.Value = xpLevelXpPercentage;
				XpTotalXpValueLabel.Content = $"{xpTotalXpValue}";
				XpTotalXpLimitLabel.Content = $"/{xpTotalXpLimit}";
				XpTotalXpProgress.Value = xpTotalXpPercentage;

				var pistolProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.Pistol);
				UInt32 pistolProficiencyLevelValue = pistolProficiency.Level;
				UInt32 pistolProficiencyLevelLimit = Proficiency.MaximumLevel;
				double pistolProficiencyLevelPercentage = pistolProficiencyLevelValue * 100.0 / pistolProficiencyLevelLimit;
				Int32 pistolProficiencyLevelXpValue = pistolProficiency.Points;
				Int32 pistolProficiencyLevelXpLimit = pistolProficiency.PointsForNextLevel;
				double pistolProficiencyLevelXpPercentage = pistolProficiencyLevelXpValue * 100.0 / pistolProficiencyLevelXpLimit;
				Int32 pistolProficiencyTotalXpValue = pistolProficiency.TotalPoints;
				Int32 pistolProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double pistolProficiencyTotalXpPercentage = pistolProficiencyTotalXpValue * 100.0 / pistolProficiencyTotalXpLimit;
				PistolProficiencyLevelValueLabel.Content = $"{pistolProficiencyLevelValue}";
				PistolProficiencyLevelLimitLabel.Content = $"/{pistolProficiencyLevelLimit}";
				PistolProficiencyLevelProgress.Value = pistolProficiencyLevelPercentage;
				PistolProficiencyLevelXpValueLabel.Content = $"{pistolProficiencyLevelXpValue}";
				PistolProficiencyLevelXpLimitLabel.Content = $"/{pistolProficiencyLevelXpLimit}";
				PistolProficiencyLevelXpProgress.Value = pistolProficiencyLevelXpPercentage;
				PistolProficiencyTotalXpValueLabel.Content = $"{pistolProficiencyTotalXpValue}";
				PistolProficiencyTotalXpLimitLabel.Content = $"/{pistolProficiencyTotalXpLimit}";
				PistolProficiencyTotalXpProgress.Value = pistolProficiencyTotalXpPercentage;

				var smgProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.SMG);
				UInt32 smgProficiencyLevelValue = smgProficiency.Level;
				UInt32 smgProficiencyLevelLimit = Proficiency.MaximumLevel;
				double smgProficiencyLevelPercentage = smgProficiencyLevelValue * 100.0 / smgProficiencyLevelLimit;
				Int32 smgProficiencyLevelXpValue = smgProficiency.Points;
				Int32 smgProficiencyLevelXpLimit = smgProficiency.PointsForNextLevel;
				double smgProficiencyLevelXpPercentage = smgProficiencyLevelXpValue * 100.0 / smgProficiencyLevelXpLimit;
				Int32 smgProficiencyTotalXpValue = smgProficiency.TotalPoints;
				Int32 smgProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double smgProficiencyTotalXpPercentage = smgProficiencyTotalXpValue * 100.0 / smgProficiencyTotalXpLimit;
				SMGProficiencyLevelValueLabel.Content = $"{smgProficiencyLevelValue}";
				SMGProficiencyLevelLimitLabel.Content = $"/{smgProficiencyLevelLimit}";
				SMGProficiencyLevelProgress.Value = smgProficiencyLevelPercentage;
				SMGProficiencyLevelXpValueLabel.Content = $"{smgProficiencyLevelXpValue}";
				SMGProficiencyLevelXpLimitLabel.Content = $"/{smgProficiencyLevelXpLimit}";
				SMGProficiencyLevelXpProgress.Value = smgProficiencyLevelXpPercentage;
				SMGProficiencyTotalXpValueLabel.Content = $"{smgProficiencyTotalXpValue}";
				SMGProficiencyTotalXpLimitLabel.Content = $"/{smgProficiencyTotalXpLimit}";
				SMGProficiencyTotalXpProgress.Value = smgProficiencyTotalXpPercentage;

				var shotgunProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.Shotgun);
				UInt32 shotgunProficiencyLevelValue = shotgunProficiency.Level;
				UInt32 shotgunProficiencyLevelLimit = Proficiency.MaximumLevel;
				double shotgunProficiencyLevelPercentage = shotgunProficiencyLevelValue * 100.0 / shotgunProficiencyLevelLimit;
				Int32 shotgunProficiencyLevelXpValue = shotgunProficiency.Points;
				Int32 shotgunProficiencyLevelXpLimit = shotgunProficiency.PointsForNextLevel;
				double shotgunProficiencyLevelXpPercentage = shotgunProficiencyLevelXpValue * 100.0 / shotgunProficiencyLevelXpLimit;
				Int32 shotgunProficiencyTotalXpValue = shotgunProficiency.TotalPoints;
				Int32 shotgunProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double shotgunProficiencyTotalXpPercentage = shotgunProficiencyTotalXpValue * 100.0 / shotgunProficiencyTotalXpLimit;
				ShotgunProficiencyLevelValueLabel.Content = $"{shotgunProficiencyLevelValue}";
				ShotgunProficiencyLevelLimitLabel.Content = $"/{shotgunProficiencyLevelLimit}";
				ShotgunProficiencyLevelProgress.Value = shotgunProficiencyLevelPercentage;
				ShotgunProficiencyLevelXpValueLabel.Content = $"{shotgunProficiencyLevelXpValue}";
				ShotgunProficiencyLevelXpLimitLabel.Content = $"/{shotgunProficiencyLevelXpLimit}";
				ShotgunProficiencyLevelXpProgress.Value = shotgunProficiencyLevelXpPercentage;
				ShotgunProficiencyTotalXpValueLabel.Content = $"{shotgunProficiencyTotalXpValue}";
				ShotgunProficiencyTotalXpLimitLabel.Content = $"/{shotgunProficiencyTotalXpLimit}";
				ShotgunProficiencyTotalXpProgress.Value = shotgunProficiencyTotalXpPercentage;

				var combatRifleProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.CombatRifle);
				UInt32 combatRifleProficiencyLevelValue = combatRifleProficiency.Level;
				UInt32 combatRifleProficiencyLevelLimit = Proficiency.MaximumLevel;
				double combatRifleProficiencyLevelPercentage = combatRifleProficiencyLevelValue * 100.0 / combatRifleProficiencyLevelLimit;
				Int32 combatRifleProficiencyLevelXpValue = combatRifleProficiency.Points;
				Int32 combatRifleProficiencyLevelXpLimit = combatRifleProficiency.PointsForNextLevel;
				double combatRifleProficiencyLevelXpPercentage = combatRifleProficiencyLevelXpValue * 100.0 / combatRifleProficiencyLevelXpLimit;
				Int32 combatRifleProficiencyTotalXpValue = combatRifleProficiency.TotalPoints;
				Int32 combatRifleProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double combatRifleProficiencyTotalXpPercentage = combatRifleProficiencyTotalXpValue * 100.0 / combatRifleProficiencyTotalXpLimit;
				CombatRifleProficiencyLevelValueLabel.Content = $"{combatRifleProficiencyLevelValue}";
				CombatRifleProficiencyLevelLimitLabel.Content = $"/{combatRifleProficiencyLevelLimit}";
				CombatRifleProficiencyLevelProgress.Value = combatRifleProficiencyLevelPercentage;
				CombatRifleProficiencyLevelXpValueLabel.Content = $"{combatRifleProficiencyLevelXpValue}";
				CombatRifleProficiencyLevelXpLimitLabel.Content = $"/{combatRifleProficiencyLevelXpLimit}";
				CombatRifleProficiencyLevelXpProgress.Value = combatRifleProficiencyLevelXpPercentage;
				CombatRifleProficiencyTotalXpValueLabel.Content = $"{combatRifleProficiencyTotalXpValue}";
				CombatRifleProficiencyTotalXpLimitLabel.Content = $"/{combatRifleProficiencyTotalXpLimit}";
				CombatRifleProficiencyTotalXpProgress.Value = combatRifleProficiencyTotalXpPercentage;

				var sniperRifleProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.SniperRifle);
				UInt32 sniperRifleProficiencyLevelValue = sniperRifleProficiency.Level;
				UInt32 sniperRifleProficiencyLevelLimit = Proficiency.MaximumLevel;
				double sniperRifleProficiencyLevelPercentage = sniperRifleProficiencyLevelValue * 100.0 / sniperRifleProficiencyLevelLimit;
				Int32 sniperRifleProficiencyLevelXpValue = sniperRifleProficiency.Points;
				Int32 sniperRifleProficiencyLevelXpLimit = sniperRifleProficiency.PointsForNextLevel;
				double sniperRifleProficiencyLevelXpPercentage = sniperRifleProficiencyLevelXpValue * 100.0 / sniperRifleProficiencyLevelXpLimit;
				Int32 sniperRifleProficiencyTotalXpValue = sniperRifleProficiency.TotalPoints;
				Int32 sniperRifleProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double sniperRifleProficiencyTotalXpPercentage = sniperRifleProficiencyTotalXpValue * 100.0 / sniperRifleProficiencyTotalXpLimit;
				SniperRifleProficiencyLevelValueLabel.Content = $"{sniperRifleProficiencyLevelValue}";
				SniperRifleProficiencyLevelLimitLabel.Content = $"/{sniperRifleProficiencyLevelLimit}";
				SniperRifleProficiencyLevelProgress.Value = sniperRifleProficiencyLevelPercentage;
				SniperRifleProficiencyLevelXpValueLabel.Content = $"{sniperRifleProficiencyLevelXpValue}";
				SniperRifleProficiencyLevelXpLimitLabel.Content = $"/{sniperRifleProficiencyLevelXpLimit}";
				SniperRifleProficiencyLevelXpProgress.Value = sniperRifleProficiencyLevelXpPercentage;
				SniperRifleProficiencyTotalXpValueLabel.Content = $"{sniperRifleProficiencyTotalXpValue}";
				SniperRifleProficiencyTotalXpLimitLabel.Content = $"/{sniperRifleProficiencyTotalXpLimit}";
				SniperRifleProficiencyTotalXpProgress.Value = sniperRifleProficiencyTotalXpPercentage;

				var rocketLauncherProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.RocketLauncher);
				UInt32 rocketLauncherProficiencyLevelValue = rocketLauncherProficiency.Level;
				UInt32 rocketLauncherProficiencyLevelLimit = Proficiency.MaximumLevel;
				double rocketLauncherProficiencyLevelPercentage = rocketLauncherProficiencyLevelValue * 100.0 / rocketLauncherProficiencyLevelLimit;
				Int32 rocketLauncherProficiencyLevelXpValue = rocketLauncherProficiency.Points;
				Int32 rocketLauncherProficiencyLevelXpLimit = rocketLauncherProficiency.PointsForNextLevel;
				double rocketLauncherProficiencyLevelXpPercentage = rocketLauncherProficiencyLevelXpValue * 100.0 / rocketLauncherProficiencyLevelXpLimit;
				Int32 rocketLauncherProficiencyTotalXpValue = rocketLauncherProficiency.TotalPoints;
				Int32 rocketLauncherProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double rocketLauncherProficiencyTotalXpPercentage = rocketLauncherProficiencyTotalXpValue * 100.0 / rocketLauncherProficiencyTotalXpLimit;
				RocketLauncherProficiencyLevelValueLabel.Content = $"{rocketLauncherProficiencyLevelValue}";
				RocketLauncherProficiencyLevelLimitLabel.Content = $"/{rocketLauncherProficiencyLevelLimit}";
				RocketLauncherProficiencyLevelProgress.Value = rocketLauncherProficiencyLevelPercentage;
				RocketLauncherProficiencyLevelXpValueLabel.Content = $"{rocketLauncherProficiencyLevelXpValue}";
				RocketLauncherProficiencyLevelXpLimitLabel.Content = $"/{rocketLauncherProficiencyLevelXpLimit}";
				RocketLauncherProficiencyLevelXpProgress.Value = rocketLauncherProficiencyLevelXpPercentage;
				RocketLauncherProficiencyTotalXpValueLabel.Content = $"{rocketLauncherProficiencyTotalXpValue}";
				RocketLauncherProficiencyTotalXpLimitLabel.Content = $"/{rocketLauncherProficiencyTotalXpLimit}";
				RocketLauncherProficiencyTotalXpProgress.Value = rocketLauncherProficiencyTotalXpPercentage;

				var eridianProficiency = currentSave.Proficiencies.GetProficiency(ProficiencyType.Eridian);
				UInt32 eridianProficiencyLevelValue = eridianProficiency.Level;
				UInt32 eridianProficiencyLevelLimit = Proficiency.MaximumLevel;
				double eridianProficiencyLevelPercentage = eridianProficiencyLevelValue * 100.0 / eridianProficiencyLevelLimit;
				Int32 eridianProficiencyLevelXpValue = eridianProficiency.Points;
				Int32 eridianProficiencyLevelXpLimit = eridianProficiency.PointsForNextLevel;
				double eridianProficiencyLevelXpPercentage = eridianProficiencyLevelXpValue * 100.0 / eridianProficiencyLevelXpLimit;
				Int32 eridianProficiencyTotalXpValue = eridianProficiency.TotalPoints;
				Int32 eridianProficiencyTotalXpLimit = Proficiency.TotalPointsForMaximumLevel;
				double eridianProficiencyTotalXpPercentage = eridianProficiencyTotalXpValue * 100.0 / eridianProficiencyTotalXpLimit;
				EridianProficiencyLevelValueLabel.Content = $"{eridianProficiencyLevelValue}";
				EridianProficiencyLevelLimitLabel.Content = $"/{eridianProficiencyLevelLimit}";
				EridianProficiencyLevelProgress.Value = eridianProficiencyLevelPercentage;
				EridianProficiencyLevelXpValueLabel.Content = $"{eridianProficiencyLevelXpValue}";
				EridianProficiencyLevelXpLimitLabel.Content = $"/{eridianProficiencyLevelXpLimit}";
				EridianProficiencyLevelXpProgress.Value = eridianProficiencyLevelXpPercentage;
				EridianProficiencyTotalXpValueLabel.Content = $"{eridianProficiencyTotalXpValue}";
				EridianProficiencyTotalXpLimitLabel.Content = $"/{eridianProficiencyTotalXpLimit}";
				EridianProficiencyTotalXpProgress.Value = eridianProficiencyTotalXpPercentage;

				MoneyLabel.Content = $"${currentSave.Details.Money}";

				Int32 missionsCompleted1Value = 0;
				Int32 missionsCompleted2Value = 0;
				foreach (var mission in currentSave.Playthroughs[0].Missions) {
					// Figure out how the missions are completed.
					if (mission.MissionStatusFlag == 4) {
						++missionsCompleted1Value;
					}
				}
				foreach (var mission in currentSave.Playthroughs[1].Missions) {
					if (mission.MissionStatusFlag == 4) {
						++missionsCompleted1Value;
					}
				}
				double missionsCompleted1Percentage = missionsCompleted1Value * 100.0 / NUM_MISSIONS;
				double missionsCompleted2Percentage = missionsCompleted2Value * 100.0 / NUM_MISSIONS;
				MissionsCompleted1ValueLabel.Content = $"{missionsCompleted1Value}";
				MissionsCompleted1TotalLabel.Content = $"/{NUM_MISSIONS}";
				MissionsCompleted1Progress.Value = missionsCompleted1Percentage;
				MissionsCompleted2ValueLabel.Content = $"{missionsCompleted2Value}";
				MissionsCompleted2TotalLabel.Content = $"/{NUM_MISSIONS}";
				MissionsCompleted2Progress.Value = missionsCompleted2Percentage;

				Int32 logsAcquired1Value = 0;
				Int32 logsAcquired2Value = 0;
				foreach (var log in currentSave.EchoPlaythroughs[0].Echoes) {
					++logsAcquired1Value;
				}
				if (currentSave.EchoPlaythroughs.Count > 1) {
					foreach (var log in currentSave.EchoPlaythroughs[1].Echoes) {
						++logsAcquired2Value;
					}
				}
				double logsAcquired1Percentage = logsAcquired1Value * 100.0 / NUM_LOGS;
				double logsAcquired2Percentage = logsAcquired2Value * 100.0 / NUM_LOGS;
				LogsAcquired1ValueLabel.Content = $"{logsAcquired1Value}";
				LogsAcquired1TotalLabel.Content = $"/{NUM_LOGS}";
				LogsAcquired1Progress.Value = logsAcquired1Percentage;
				LogsAcquired2ValueLabel.Content = $"{logsAcquired2Value}";
				LogsAcquired2TotalLabel.Content = $"/{NUM_LOGS}";
				LogsAcquired2Progress.Value = logsAcquired2Percentage;

				List<Challenge> uncompletedChallenges = new List<Challenge>();
				List<Int32> uncompletedChallengeValues = new List<Int32>();
				List<Int32> uncompletedChallengeTotals = new List<Int32>();
				List<double> uncompletedChallengePercentages = new List<double>();

				Int32 challengesCompletedValue = 0;
				foreach (var challenge in Challenge.Challenges) {
					Int32 challengeValue = 0;
					Int32 challengeTotal = 0;
					foreach (var goal in challenge.Goals) {
						challengeTotal += goal.Count;
						//? Once I know the values, there shouldn't be a try/catch here.
						try {
							challengeValue += Math.Min(goal.Count, (int)currentSave.StatTable.Stats[goal.Stat].Value);
						} catch {

						}
					}
					double challengePercentage = challengeValue * 100.0 / challengeTotal;
					if (challengePercentage < 100.0) {
						// Find the position for this.
						int index = 0;
						while (index < uncompletedChallenges.Count && challengePercentage <= uncompletedChallengePercentages[index]) {
							++index;
						}
						uncompletedChallenges.Insert(index, challenge);
						uncompletedChallengeValues.Insert(index, challengeValue);
						uncompletedChallengeTotals.Insert(index, challengeTotal);
						uncompletedChallengePercentages.Insert(index, challengePercentage);
					} else {
						++challengesCompletedValue;
					}
				}
				double challengesCompletedPercentage = challengesCompletedValue * 100.0 / NUM_CHALLENGES;
				ChallengesCompletedValueLabel.Content = $"{challengesCompletedValue}";
				ChallengesCompletedTotalLabel.Content = $"/{NUM_CHALLENGES}";
				ChallengesCompletedProgress.Value = challengesCompletedPercentage;

				if (uncompletedChallenges.Count > 0) {
					Challenge1Label.Content = $"{uncompletedChallenges[0].Name}: ";
					Challenge1ValueLabel.Content = $"{uncompletedChallengeValues[0]}";
					Challenge1TotalLabel.Content = $"/{uncompletedChallengeTotals[0]}";
					Challenge1Progress.Value = uncompletedChallengePercentages[0];
				} else {
					Challenge1Label.Content = "";
					Challenge1ValueLabel.Content = "";
					Challenge1TotalLabel.Content = "";
					Challenge1Progress.Value = 100.0;
				}

				if (uncompletedChallenges.Count > 1) {
					Challenge2Label.Content = $"{uncompletedChallenges[1].Name}: ";
					Challenge2ValueLabel.Content = $"{uncompletedChallengeValues[1]}";
					Challenge2TotalLabel.Content = $"/{uncompletedChallengeTotals[1]}";
					Challenge2Progress.Value = uncompletedChallengePercentages[1];
				} else {
					Challenge2Label.Content = "";
					Challenge2ValueLabel.Content = "";
					Challenge2TotalLabel.Content = "";
					Challenge2Progress.Value = 100.0;
				}

				if (uncompletedChallenges.Count > 2) {
					Challenge3Label.Content = $"{uncompletedChallenges[2].Name}: ";
					Challenge3ValueLabel.Content = $"{uncompletedChallengeValues[2]}";
					Challenge3TotalLabel.Content = $"/{uncompletedChallengeTotals[2]}";
					Challenge3Progress.Value = uncompletedChallengePercentages[2];
				} else {
					Challenge3Label.Content = "";
					Challenge3ValueLabel.Content = "";
					Challenge3TotalLabel.Content = "";
					Challenge3Progress.Value = 100.0;
				}

				if (uncompletedChallenges.Count > 3) {
					Challenge4Label.Content = $"{uncompletedChallenges[3].Name}: ";
					Challenge4ValueLabel.Content = $"{uncompletedChallengeValues[3]}";
					Challenge4TotalLabel.Content = $"/{uncompletedChallengeTotals[3]}";
					Challenge4Progress.Value = uncompletedChallengePercentages[3];
				} else {
					Challenge4Label.Content = "";
					Challenge4ValueLabel.Content = "";
					Challenge4TotalLabel.Content = "";
					Challenge4Progress.Value = 100.0;
				}

			});
		}

		public static T Clamp<T>(T val, T min, T max) where T : IComparable<T> {
			if (val.CompareTo(min) < 0) return min;
			else if (val.CompareTo(max) > 0) return max;
			else return val;
		}
	}
}

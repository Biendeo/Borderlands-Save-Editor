using Borderlands_Save_Editor;
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
			const Int32 MAX_PROFICIENCY_LEVEL = 50;
			const Int32 NUM_MISSIONS = 216;
			const Int32 NUM_LOGS = 188;
			Int32 NUM_CHALLENGES = Challenge.Challenges.Count;

			Dispatcher.Invoke(() => {
				Int32 xpLevelValue = currentSave.Level;
				Int32 xpLevelLimit = MAX_LEVEL;
				double xpLevelPercentage = xpLevelValue * 100.0 / xpLevelLimit;
				Int32 xpLevelXpValue = currentSave.Experience - Save.ExperienceToLevels[xpLevelValue];
				Int32 xpLevelXpLimit = Save.ExperienceToLevels[Clamp(xpLevelValue + 1, 0, MAX_LEVEL)] - Save.ExperienceToLevels[Clamp(xpLevelValue, 0, MAX_LEVEL - 1)];
				double xpLevelXpPercentage = xpLevelXpValue * 100.0 / xpLevelXpLimit;
				Int32 xpTotalXpValue = currentSave.Experience;
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

				Int32 pistolProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.Pistol].Level;
				Int32 pistolProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double pistolProficiencyLevelPercentage = pistolProficiencyLevelValue * 100.0 / pistolProficiencyLevelLimit;
				Int32 pistolProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.Pistol].Points;
				Int32 pistolProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(pistolProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(pistolProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double pistolProficiencyLevelXpPercentage = pistolProficiencyLevelXpValue * 100.0 / pistolProficiencyLevelXpLimit;
				Int32 pistolProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.Pistol].Points + Proficiency.PointsToLevel[pistolProficiencyLevelValue];
				Int32 pistolProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 smgProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.SMG].Level;
				Int32 smgProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double smgProficiencyLevelPercentage = smgProficiencyLevelValue * 100.0 / smgProficiencyLevelLimit;
				Int32 smgProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.SMG].Points;
				Int32 smgProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(smgProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(smgProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double smgProficiencyLevelXpPercentage = smgProficiencyLevelXpValue * 100.0 / smgProficiencyLevelXpLimit;
				Int32 smgProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.SMG].Points + Proficiency.PointsToLevel[smgProficiencyLevelValue];
				Int32 smgProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 shotgunProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.Shotgun].Level;
				Int32 shotgunProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double shotgunProficiencyLevelPercentage = shotgunProficiencyLevelValue * 100.0 / shotgunProficiencyLevelLimit;
				Int32 shotgunProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.Shotgun].Points;
				Int32 shotgunProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(shotgunProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(shotgunProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double shotgunProficiencyLevelXpPercentage = shotgunProficiencyLevelXpValue * 100.0 / shotgunProficiencyLevelXpLimit;
				Int32 shotgunProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.Shotgun].Points + Proficiency.PointsToLevel[shotgunProficiencyLevelValue];
				Int32 shotgunProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 combatRifleProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.CombatRifle].Level;
				Int32 combatRifleProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double combatRifleProficiencyLevelPercentage = combatRifleProficiencyLevelValue * 100.0 / combatRifleProficiencyLevelLimit;
				Int32 combatRifleProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.CombatRifle].Points;
				Int32 combatRifleProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(combatRifleProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(combatRifleProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double combatRifleProficiencyLevelXpPercentage = combatRifleProficiencyLevelXpValue * 100.0 / combatRifleProficiencyLevelXpLimit;
				Int32 combatRifleProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.CombatRifle].Points + Proficiency.PointsToLevel[combatRifleProficiencyLevelValue];
				Int32 combatRifleProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 sniperRifleProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.SniperRifle].Level;
				Int32 sniperRifleProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double sniperRifleProficiencyLevelPercentage = sniperRifleProficiencyLevelValue * 100.0 / sniperRifleProficiencyLevelLimit;
				Int32 sniperRifleProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.SniperRifle].Points;
				Int32 sniperRifleProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(sniperRifleProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(sniperRifleProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double sniperRifleProficiencyLevelXpPercentage = sniperRifleProficiencyLevelXpValue * 100.0 / sniperRifleProficiencyLevelXpLimit;
				Int32 sniperRifleProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.SniperRifle].Points + Proficiency.PointsToLevel[sniperRifleProficiencyLevelValue];
				Int32 sniperRifleProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 rocketLauncherProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.RocketLauncher].Level;
				Int32 rocketLauncherProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double rocketLauncherProficiencyLevelPercentage = rocketLauncherProficiencyLevelValue * 100.0 / rocketLauncherProficiencyLevelLimit;
				Int32 rocketLauncherProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.RocketLauncher].Points;
				Int32 rocketLauncherProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(rocketLauncherProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(rocketLauncherProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double rocketLauncherProficiencyLevelXpPercentage = rocketLauncherProficiencyLevelXpValue * 100.0 / rocketLauncherProficiencyLevelXpLimit;
				Int32 rocketLauncherProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.RocketLauncher].Points + Proficiency.PointsToLevel[rocketLauncherProficiencyLevelValue];
				Int32 rocketLauncherProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				Int32 eridianProficiencyLevelValue = currentSave.Proficiencies[ProficiencyType.Eridian].Level;
				Int32 eridianProficiencyLevelLimit = MAX_PROFICIENCY_LEVEL;
				double eridianProficiencyLevelPercentage = eridianProficiencyLevelValue * 100.0 / eridianProficiencyLevelLimit;
				Int32 eridianProficiencyLevelXpValue = currentSave.Proficiencies[ProficiencyType.Eridian].Points;
				Int32 eridianProficiencyLevelXpLimit = Proficiency.PointsToLevel[Clamp(eridianProficiencyLevelValue + 1, 0, MAX_PROFICIENCY_LEVEL)] - Proficiency.PointsToLevel[Clamp(eridianProficiencyLevelValue, 0, MAX_PROFICIENCY_LEVEL - 1)];
				double eridianProficiencyLevelXpPercentage = eridianProficiencyLevelXpValue * 100.0 / eridianProficiencyLevelXpLimit;
				Int32 eridianProficiencyTotalXpValue = currentSave.Proficiencies[ProficiencyType.Eridian].Points + Proficiency.PointsToLevel[eridianProficiencyLevelValue];
				Int32 eridianProficiencyTotalXpLimit = Proficiency.PointsToLevel[MAX_PROFICIENCY_LEVEL];
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

				MoneyLabel.Content = $"{currentSave.Money}";

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
							challengeValue += Math.Min(goal.Count, (int)currentSave.Stats[goal.Stat].Value);
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

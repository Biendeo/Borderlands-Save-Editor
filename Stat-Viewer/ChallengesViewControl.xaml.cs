using Borderlands_Save_Editor;
using System;
using System.Collections.Generic;
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
	/// Interaction logic for ChallengesViewControl.xaml
	/// </summary>
	public partial class ChallengesViewControl : UserControl {
		public ChallengesViewControl() {
			InitializeComponent();
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		public void UpdateAllElements() {
			ChallengesGrid.Children.Clear();
			ChallengesGrid.RowDefinitions.Clear();

			ChallengesGrid.RowDefinitions.Add(new RowDefinition());
			{
				var nameLabel = new Label() {
					Content = "Name",
					HorizontalAlignment = HorizontalAlignment.Center
				};
				Grid.SetRow(nameLabel, 0);
				Grid.SetColumn(nameLabel, 0);
				ChallengesGrid.Children.Add(nameLabel);

				var rewardLabel = new Label() {
					Content = "Reward",
					HorizontalAlignment = HorizontalAlignment.Center
				};
				Grid.SetRow(rewardLabel, 0);
				Grid.SetColumn(rewardLabel, 1);
				ChallengesGrid.Children.Add(rewardLabel);

				var progressLabel = new Label() {
					Content = "Progress",
					HorizontalAlignment = HorizontalAlignment.Center
				};
				Grid.SetRow(progressLabel, 0);
				Grid.SetColumn(progressLabel, 2);
				Grid.SetColumnSpan(progressLabel, 3);
				ChallengesGrid.Children.Add(progressLabel);
			}

			var challengeValues = new List<Tuple<int, double>>();
			foreach (var challenge in Challenge.Challenges) {
				ChallengesGrid.RowDefinitions.Add(new RowDefinition());

				int progressNumerator = 0;
				int progressDenominator = 0;
				foreach (var goal in challenge.Goals) {
					progressDenominator += goal.Count;
					progressNumerator += (int)Model.Save.StatTable.Stats[goal.Stat].Value.Clamp(0u, (uint)goal.Count);
				}

				challengeValues.Add(new Tuple<int, double>(ChallengesGrid.RowDefinitions.Count - 2, (double)progressNumerator / progressDenominator));
			}

			int p(Tuple<int, double> a, Tuple<int, double> b) {
				if (a.Item2 == 1.0 && b.Item2 == 1.0) {
					return b.Item1.CompareTo(a.Item1);
				} else if (a.Item2 == 1.0) {
					return 1;
				} else if (b.Item2 == 1.0) {
					return -1;
				} else if (a.Item2 == b.Item2) {
					return b.Item1.CompareTo(a.Item1);
				} else {
					return b.Item2.CompareTo(a.Item2);
				}
			}
			challengeValues.Sort(p);

			int i = 1;
			foreach (var t in challengeValues) {
				var challenge = Challenge.Challenges[t.Item1];
				var nameLabel = new Label() {
					Content = challenge.Name,
					HorizontalAlignment = HorizontalAlignment.Right
				};
				Grid.SetRow(nameLabel, i);
				Grid.SetColumn(nameLabel, 0);
				ChallengesGrid.Children.Add(nameLabel);

				var rewardLabel = new Label() {
					Content = $"{challenge.XpReward} XP",
					HorizontalAlignment = HorizontalAlignment.Right
				};
				Grid.SetRow(rewardLabel, i);
				Grid.SetColumn(rewardLabel, 1);
				ChallengesGrid.Children.Add(rewardLabel);

				int progressNumerator = 0;
				int progressDenominator = 0;
				foreach (var goal in challenge.Goals) {
					progressDenominator += goal.Count;
					progressNumerator += (int)Model.Save.StatTable.Stats[goal.Stat].Value.Clamp(0u, (uint)goal.Count);
				}
				var progressLabel = new Label() {
					Content = $"{progressNumerator}/{progressDenominator}",
					HorizontalAlignment = HorizontalAlignment.Right
				};
				Grid.SetRow(progressLabel, i);
				Grid.SetColumn(progressLabel, 2);
				ChallengesGrid.Children.Add(progressLabel);

				var progressGrid = new Grid();
				var progressBar = new ProgressBar() {
					Value = progressNumerator * 100.0 / progressDenominator,
					Minimum = 0.0,
					Maximum = 100.0
				};
				var progressBarLabel = new Label() {
					Content = $"{progressBar.Value.ToString("0.0000")}%",
					HorizontalAlignment = HorizontalAlignment.Center
				};

				progressGrid.Children.Add(progressBar);
				progressGrid.Children.Add(progressBarLabel);

				Grid.SetRow(progressGrid, i);
				Grid.SetColumn(progressGrid, 3);
				ChallengesGrid.Children.Add(progressGrid);

				++i;
			}
		}
	}
}

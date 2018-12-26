using Borderlands_Save_Editor;
using Borderlands_Save_Editor.Save;
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
	/// Interaction logic for PlaythroughsViewControl.xaml
	/// </summary>
	public partial class PlaythroughsViewControl : UserControl {
		public PlaythroughsViewControl() {
			InitializeComponent();
			Location1Combo.ItemsSource = Enum.GetValues(typeof(Location));
			Location2Combo.ItemsSource = Enum.GetValues(typeof(Location));
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		public void UpdateAllElements() {
			UpdateLocations();
			UpdateQuests();
		}

		public void UpdateLocations() {
			//TODO: Clearly both are not the same location.
			Location1Combo.SelectedItem = Model.Save.CurrentLocation;
			Location2Combo.SelectedItem = Model.Save.CurrentLocation;
		}

		public void UpdateQuests() {
			QuestsCompleted1Label.Content = $"{Model.Save.Playthroughs[0].MissionsCompleted}/{Playthrough.TotalMissions}";
			QuestsCompleted1ProgressBar.Value = Model.Save.Playthroughs[0].MissionsCompleted * 100.0 / Playthrough.TotalMissions;
			QuestsCompleted2Label.Content = $"{Model.Save.Playthroughs[1].MissionsCompleted}/{Playthrough.TotalMissions}";
			QuestsCompleted2ProgressBar.Value = Model.Save.Playthroughs[1].MissionsCompleted * 100.0 / Playthrough.TotalMissions;

			QuestGrid.Children.Clear();
			QuestGrid.RowDefinitions.Clear();

			var seenQuests = new List<Tuple<Mission, Mission>>();

			foreach (MissionID id in Enum.GetValues(typeof(MissionID))) {
				seenQuests.Add(new Tuple<Mission, Mission>(Model.Save.Playthroughs[0].Missions[id], Model.Save.Playthroughs[1].Missions[id]));
			}

			int p(Tuple<Mission, Mission> a, Tuple<Mission, Mission> b) {
				if (a.Item1.Status == b.Item1.Status && a.Item2.Status == b.Item2.Status) {
					return a.Item1.ID.CompareTo(b.Item1.ID);
				} else if (a.Item1.Status == b.Item1.Status) {
					int x = a.Item2.Status == Mission.MissionStatus.Completed ? -1 : (int)a.Item2.Status;
					int y = b.Item2.Status == Mission.MissionStatus.Completed ? -1 : (int)b.Item2.Status;
					return y - x;
				} else {
					int x = a.Item1.Status == Mission.MissionStatus.Completed ? -1 : (int)a.Item1.Status;
					int y = b.Item1.Status == Mission.MissionStatus.Completed ? -1 : (int)b.Item1.Status;
					return y - x;
				}
			}

			seenQuests.Sort(p);

			foreach (var mission in seenQuests) {
				QuestGrid.RowDefinitions.Add(new RowDefinition());

				var questLabel = new Label {
					Content = mission.Item1.ReadableName,
					HorizontalAlignment = HorizontalAlignment.Right
				};
				QuestGrid.Children.Add(questLabel);
				Grid.SetRow(questLabel, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(questLabel, 0);

				var playthrough1Label = new Label {
					Content = mission.Item1.Status.ToString(),
					HorizontalAlignment = HorizontalAlignment.Right
				};
				QuestGrid.Children.Add(playthrough1Label);
				Grid.SetRow(playthrough1Label, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(playthrough1Label, 1);

				var playthrough2Label = new Label {
					Content = mission.Item2.Status.ToString(),
					HorizontalAlignment = HorizontalAlignment.Right
				};
				QuestGrid.Children.Add(playthrough2Label);
				Grid.SetRow(playthrough2Label, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(playthrough2Label, 2);
			}
		}
	}
}

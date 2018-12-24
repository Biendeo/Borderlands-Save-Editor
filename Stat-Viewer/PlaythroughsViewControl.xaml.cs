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

			var seenQuests = new HashSet<string>();
			foreach (var playthrough in Model.Save.Playthroughs) {
				foreach (var mission in playthrough.Missions) {
					seenQuests.Add(mission.InternalName);
				}
			}

			foreach (string questName in seenQuests) {
				QuestGrid.RowDefinitions.Add(new RowDefinition());

				var questLabel = new Label {
					Content = questName,
					HorizontalAlignment = HorizontalAlignment.Right
				};
				QuestGrid.Children.Add(questLabel);
				Grid.SetRow(questLabel, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(questLabel, 0);

				string playthrough1Text = "Not found";
				var playthrough1 = Model.Save.Playthroughs[0].Missions.Find(m => m.InternalName == questName);
				if (playthrough1 != null) {
					playthrough1Text = playthrough1.Status.ToString();
				}

				var playthrough1Label = new Label {
					Content = playthrough1Text,
					HorizontalAlignment = HorizontalAlignment.Center
				};
				QuestGrid.Children.Add(playthrough1Label);
				Grid.SetRow(playthrough1Label, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(playthrough1Label, 1);

				string playthrough2Text = "Not found";
				var playthrough2 = Model.Save.Playthroughs[1].Missions.Find(m => m.InternalName == questName);
				if (playthrough2 != null) {
					playthrough2Text = playthrough1.Status.ToString();
				}

				var playthrough2Label = new Label {
					Content = playthrough2Text,
					HorizontalAlignment = HorizontalAlignment.Center
				};
				QuestGrid.Children.Add(playthrough2Label);
				Grid.SetRow(playthrough2Label, QuestGrid.RowDefinitions.Count - 1);
				Grid.SetColumn(playthrough2Label, 2);
			}
		}
	}
}

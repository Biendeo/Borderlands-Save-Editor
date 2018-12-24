using Borderlands_Save_Editor.Player.Class;
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
	/// Interaction logic for CharacterViewControl.xaml
	/// </summary>
	public partial class CharacterViewControl : UserControl {
		public CharacterViewControl() {
			InitializeComponent();
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		public void UpdateAllElements() {
			UpdateLabelContent();
			UpdateLevelBars();
			UpdateColorElements();
		}

		private void UpdateLabelContent() {
			try {
				CharacterNameLabel.Content = Model.Save.Name;
				ClassLabel.Content = Model.Save.Details.ClassType.ClassName();
				MoneyLabel.Content = $"${Model.Save.Details.Money}";
				SaveNumberLabel.Content = Model.Save.SaveNumber.ToString();
			} catch (NullReferenceException) { }
		}

		private void UpdateLevelBars() {
			LevelQuotientLabel.Content = $"{Model.Save.Details.Level}/{CharacterDetails.MaximumLevel}";
			LevelProgressBar.Value = Model.Save.Details.Level * 100.0 / CharacterDetails.MaximumLevel;
			XPQuotientLabel.Content = $"{Model.Save.Details.LevelExperience}/{Model.Save.Details.ExperienceForNextLevel}";
			XPProgressBar.Value = Model.Save.Details.LevelExperience * 100.0 / Model.Save.Details.ExperienceForNextLevel;
			TotalXPQuotientLabel.Content = $"{Model.Save.Details.TotalExperience}/{CharacterDetails.TotalExperienceForMaximumLevel}";
			TotalXPProgressBar.Value = Model.Save.Details.TotalExperience * 100.0 / CharacterDetails.TotalExperienceForMaximumLevel;
		}

		private void UpdateColorElements() {
			try {
				Color1Rectangle.Fill = new SolidColorBrush(Model.Save.Color1);
				Color2Rectangle.Fill = new SolidColorBrush(Model.Save.Color2);
				Color3Rectangle.Fill = new SolidColorBrush(Model.Save.Color3);
			} catch (NullReferenceException) { }
		}
	}
}

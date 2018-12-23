using Borderlands_Save_Editor;
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
	/// Interaction logic for CharacterEditControl.xaml
	/// </summary>
	public partial class CharacterEditControl : UserControl {
		public CharacterEditControl() {
			InitializeComponent();
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		public void UpdateAllElements() {
			UpdateTextBoxElements();
			UpdateXPEditElements();
			UpdateColorElements();
		}

		private void UpdateTextBoxElements() {
			try {
				CharacterNameTextBox.Text = Model.Save.Name;
				SaveNumberTextBox.Text = Model.Save.SaveNumber.ToString();
			} catch (NullReferenceException) { }
		}

		private void UpdateXPEditElements() {
			try {
				LevelSlider.Value = Model.Save.Details.Level;
				XPSlider.Maximum = (CharacterDetails.ExperienceToLevel(Model.Save.Details.Level + 1) - 1).Clamp(0, int.MaxValue);
				XPSlider.Value = Model.Save.Details.LevelExperience;
				TotalXPSlider.Value = Model.Save.Details.TotalExperience;
			} catch (NullReferenceException) { }
		}

		private void UpdateColorElements() {
			try {
				Color1Rectangle.Fill = new SolidColorBrush(Model.Save.Color1);
				Color2Rectangle.Fill = new SolidColorBrush(Model.Save.Color2);
				Color3Rectangle.Fill = new SolidColorBrush(Model.Save.Color3);
			} catch (NullReferenceException) { }
		}

		private void XPSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateXPEditElements();
		}

		private void Color1Rectangle_MouseDown(object sender, MouseButtonEventArgs e) {

		}

		private void Color2Rectangle_MouseDown(object sender, MouseButtonEventArgs e) {

		}

		private void Color3Rectangle_MouseDown(object sender, MouseButtonEventArgs e) {

		}
	}
}

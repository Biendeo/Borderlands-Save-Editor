using Borderlands_Save_Editor;
using Borderlands_Save_Editor.Player.Proficiency;
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
	/// Interaction logic for ProficiencyEditControl.xaml
	/// </summary>
	public partial class ProficiencyEditControl : UserControl {
		public ProficiencyEditControl() {
			InitializeComponent();
		}

		public void UpdateAllElements() {
			UpdatePistolProfEditElements();
			UpdateSMGProfEditElements();
			UpdateShotgunProfEditElements();
			UpdateCombatRifleProfEditElements();
			UpdateSniperRifleProfEditElements();
			UpdateRocketLauncherProfEditElements();
			UpdateEridianProfEditElements();
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		private void UpdatePistolProfEditElements() {
			PistolProfLevelSlider.Value = Model.Save.Proficiencies.Pistol.Level;
			PistolProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.Pistol.Level + 1) - 1).Clamp(0, int.MaxValue);
			PistolProfPointsSlider.Value = Model.Save.Proficiencies.Pistol.Points;
			PistolProfTotalPointsSlider.Value = Model.Save.Proficiencies.Pistol.TotalPoints;
		}

		private void UpdateSMGProfEditElements() {
			SMGProfLevelSlider.Value = Model.Save.Proficiencies.SMG.Level;
			SMGProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.SMG.Level + 1) - 1).Clamp(0, int.MaxValue);
			SMGProfPointsSlider.Value = Model.Save.Proficiencies.SMG.Points;
			SMGProfTotalPointsSlider.Value = Model.Save.Proficiencies.SMG.TotalPoints;
		}

		private void UpdateShotgunProfEditElements() {
			ShotgunProfLevelSlider.Value = Model.Save.Proficiencies.Shotgun.Level;
			ShotgunProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.Shotgun.Level + 1) - 1).Clamp(0, int.MaxValue);
			ShotgunProfPointsSlider.Value = Model.Save.Proficiencies.Shotgun.Points;
			ShotgunProfTotalPointsSlider.Value = Model.Save.Proficiencies.Shotgun.TotalPoints;
		}

		private void UpdateCombatRifleProfEditElements() {
			CombatRifleProfLevelSlider.Value = Model.Save.Proficiencies.CombatRifle.Level;
			CombatRifleProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.CombatRifle.Level + 1) - 1).Clamp(0, int.MaxValue);
			CombatRifleProfPointsSlider.Value = Model.Save.Proficiencies.CombatRifle.Points;
			CombatRifleProfTotalPointsSlider.Value = Model.Save.Proficiencies.CombatRifle.TotalPoints;
		}

		private void UpdateSniperRifleProfEditElements() {
			SniperRifleProfLevelSlider.Value = Model.Save.Proficiencies.SniperRifle.Level;
			SniperRifleProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.SniperRifle.Level + 1) - 1).Clamp(0, int.MaxValue);
			SniperRifleProfPointsSlider.Value = Model.Save.Proficiencies.SniperRifle.Points;
			SniperRifleProfTotalPointsSlider.Value = Model.Save.Proficiencies.SniperRifle.TotalPoints;
		}

		private void UpdateRocketLauncherProfEditElements() {
			RocketLauncherProfLevelSlider.Value = Model.Save.Proficiencies.RocketLauncher.Level;
			RocketLauncherProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.RocketLauncher.Level + 1) - 1).Clamp(0, int.MaxValue);
			RocketLauncherProfPointsSlider.Value = Model.Save.Proficiencies.RocketLauncher.Points;
			RocketLauncherProfTotalPointsSlider.Value = Model.Save.Proficiencies.RocketLauncher.TotalPoints;
		}

		private void UpdateEridianProfEditElements() {
			EridianProfLevelSlider.Value = Model.Save.Proficiencies.Eridian.Level;
			EridianProfPointsSlider.Maximum = (Proficiency.PointsToLevel(Model.Save.Proficiencies.Eridian.Level + 1) - 1).Clamp(0, int.MaxValue);
			EridianProfPointsSlider.Value = Model.Save.Proficiencies.Eridian.Points;
			EridianProfTotalPointsSlider.Value = Model.Save.Proficiencies.Eridian.TotalPoints;
		}

		private void PistolProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdatePistolProfEditElements();
		}

		private void SMGProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateSMGProfEditElements();
		}

		private void ShotgunProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateShotgunProfEditElements();
		}

		private void CombatRifleProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateCombatRifleProfEditElements();
		}

		private void SniperRifleProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateSniperRifleProfEditElements();
		}

		private void RocketLauncherProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateRocketLauncherProfEditElements();
		}

		private void EridianProfSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
			UpdateEridianProfEditElements();
		}
	}
}

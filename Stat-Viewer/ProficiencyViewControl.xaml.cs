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
	/// Interaction logic for ProficiencyViewControl.xaml
	/// </summary>
	public partial class ProficiencyViewControl : UserControl {
		public ProficiencyViewControl() {
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
			PistolLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.Pistol.Level}/{Proficiency.MaximumLevel}";
			PistolLevelProgressBar.Value = Model.Save.Proficiencies.Pistol.Level * 100.0 / Proficiency.MaximumLevel;
			PistolXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Pistol.Points}/{Model.Save.Proficiencies.Pistol.PointsForNextLevel}";
			PistolXPProgressBar.Value = Model.Save.Proficiencies.Pistol.Points * 100.0 / Model.Save.Proficiencies.Pistol.PointsForNextLevel;
			PistolTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Pistol.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			PistolTotalXPProgressBar.Value = Model.Save.Proficiencies.Pistol.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateSMGProfEditElements() {
			SMGLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.SMG.Level}/{Proficiency.MaximumLevel}";
			SMGLevelProgressBar.Value = Model.Save.Proficiencies.SMG.Level * 100.0 / Proficiency.MaximumLevel;
			SMGXPQuotientLabel.Content = $"{Model.Save.Proficiencies.SMG.Points}/{Model.Save.Proficiencies.SMG.PointsForNextLevel}";
			SMGXPProgressBar.Value = Model.Save.Proficiencies.SMG.Points * 100.0 / Model.Save.Proficiencies.SMG.PointsForNextLevel;
			SMGTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.SMG.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			SMGTotalXPProgressBar.Value = Model.Save.Proficiencies.SMG.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateShotgunProfEditElements() {
			ShotgunLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.Shotgun.Level}/{Proficiency.MaximumLevel}";
			ShotgunLevelProgressBar.Value = Model.Save.Proficiencies.Shotgun.Level * 100.0 / Proficiency.MaximumLevel;
			ShotgunXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Shotgun.Points}/{Model.Save.Proficiencies.Shotgun.PointsForNextLevel}";
			ShotgunXPProgressBar.Value = Model.Save.Proficiencies.Shotgun.Points * 100.0 / Model.Save.Proficiencies.Shotgun.PointsForNextLevel;
			ShotgunTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Shotgun.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			ShotgunTotalXPProgressBar.Value = Model.Save.Proficiencies.Shotgun.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateCombatRifleProfEditElements() {
			CombatRifleLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.CombatRifle.Level}/{Proficiency.MaximumLevel}";
			CombatRifleLevelProgressBar.Value = Model.Save.Proficiencies.CombatRifle.Level * 100.0 / Proficiency.MaximumLevel;
			CombatRifleXPQuotientLabel.Content = $"{Model.Save.Proficiencies.CombatRifle.Points}/{Model.Save.Proficiencies.CombatRifle.PointsForNextLevel}";
			CombatRifleXPProgressBar.Value = Model.Save.Proficiencies.CombatRifle.Points * 100.0 / Model.Save.Proficiencies.CombatRifle.PointsForNextLevel;
			CombatRifleTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.CombatRifle.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			CombatRifleTotalXPProgressBar.Value = Model.Save.Proficiencies.CombatRifle.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateSniperRifleProfEditElements() {
			SniperRifleLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.SniperRifle.Level}/{Proficiency.MaximumLevel}";
			SniperRifleLevelProgressBar.Value = Model.Save.Proficiencies.SniperRifle.Level * 100.0 / Proficiency.MaximumLevel;
			SniperRifleXPQuotientLabel.Content = $"{Model.Save.Proficiencies.SniperRifle.Points}/{Model.Save.Proficiencies.SniperRifle.PointsForNextLevel}";
			SniperRifleXPProgressBar.Value = Model.Save.Proficiencies.SniperRifle.Points * 100.0 / Model.Save.Proficiencies.SniperRifle.PointsForNextLevel;
			SniperRifleTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.SniperRifle.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			SniperRifleTotalXPProgressBar.Value = Model.Save.Proficiencies.SniperRifle.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateRocketLauncherProfEditElements() {
			RocketLauncherLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.RocketLauncher.Level}/{Proficiency.MaximumLevel}";
			RocketLauncherLevelProgressBar.Value = Model.Save.Proficiencies.RocketLauncher.Level * 100.0 / Proficiency.MaximumLevel;
			RocketLauncherXPQuotientLabel.Content = $"{Model.Save.Proficiencies.RocketLauncher.Points}/{Model.Save.Proficiencies.RocketLauncher.PointsForNextLevel}";
			RocketLauncherXPProgressBar.Value = Model.Save.Proficiencies.RocketLauncher.Points * 100.0 / Model.Save.Proficiencies.RocketLauncher.PointsForNextLevel;
			RocketLauncherTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.RocketLauncher.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			RocketLauncherTotalXPProgressBar.Value = Model.Save.Proficiencies.RocketLauncher.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
		}

		private void UpdateEridianProfEditElements() {
			EridianLevelQuotientLabel.Content = $"{Model.Save.Proficiencies.Eridian.Level}/{Proficiency.MaximumLevel}";
			EridianLevelProgressBar.Value = Model.Save.Proficiencies.Eridian.Level * 100.0 / Proficiency.MaximumLevel;
			EridianXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Eridian.Points}/{Model.Save.Proficiencies.Eridian.PointsForNextLevel}";
			EridianXPProgressBar.Value = Model.Save.Proficiencies.Eridian.Points * 100.0 / Model.Save.Proficiencies.Eridian.PointsForNextLevel;
			EridianTotalXPQuotientLabel.Content = $"{Model.Save.Proficiencies.Eridian.TotalPoints}/{Proficiency.TotalPointsForMaximumLevel}";
			EridianTotalXPProgressBar.Value = Model.Save.Proficiencies.Eridian.TotalPoints * 100.0 / Proficiency.TotalPointsForMaximumLevel;
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

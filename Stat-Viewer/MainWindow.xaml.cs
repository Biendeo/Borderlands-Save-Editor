using Borderlands_Save_Editor;
using Borderlands_Save_Editor.Player.Class;
using Borderlands_Save_Editor.Player.Proficiency;
using Borderlands_Save_Editor.Save;
using Microsoft.Win32;
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
		public bool EditMode { get; set; }

		public static readonly string ViewModeTitle = "Borderlands Save Viewer";
		public static readonly string EditModeTitle = "Borderlands Save Editor";

		public string CurrentFolder { get; private set; }
		public string CurrentFileName { get; private set; }

		public FileSystemWatcher SaveWatcher;

		public MainWindow() {
			SaveWatcher = null;

			EditMode = false;

			InitializeComponent();
		}

		internal Model Model {
			get { return DataContext as Model; }
		}

		internal void SetupSaveWatcher(string folder, string fileName) {
			if (SaveWatcher != null) {
				SaveWatcher.Dispose();
			}
			SaveWatcher = new FileSystemWatcher {
				Path = folder,
				NotifyFilter = NotifyFilters.LastWrite,
				Filter = fileName
			};
			CurrentFolder = folder;
			CurrentFileName = fileName;

			SaveWatcher.Changed += new FileSystemEventHandler(OnChanged);
			SaveWatcher.EnableRaisingEvents = true;
		}

		private void OnChanged(object source, FileSystemEventArgs e) {
			if (!EditMode) {
				while (true) {
					try {
						Model.Save = Save.Read(CurrentFolder + CurrentFileName);
						break;
					} catch {

					}
				}
				UpdateAllElements();
			}
		}

		public void UpdateAllElements() {
			CharacterEditControl.UpdateAllElements();
			CharacterViewControl.UpdateAllElements();
			ProficiencyEditControl.UpdateAllElements();
		}

		private void CommandAlwaysEnabled(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = true;
		}

		private void NewCommandExecute(object sender, ExecutedRoutedEventArgs e) {
			Model.Save = new Save();
			UpdateAllElements();
		}

		private void OpenCommandExecute(object sender, ExecutedRoutedEventArgs e) {
			var openDialog = new OpenFileDialog {
				DefaultExt = "sav",
				CheckFileExists = true,
				Multiselect = false
			};
			if (openDialog.ShowDialog() == true) {
				Model.Save = Save.Read(openDialog.FileName);
				UpdateAllElements();
			}
		}

		private void SaveCommandExecute(object sender, ExecutedRoutedEventArgs e) {
			var saveDialog = new System.Windows.Forms.FolderBrowserDialog();
			if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				string folder = saveDialog.SelectedPath;
				string fullPath = System.IO.Path.Combine(folder, Model.Save.SaveFileName);
				bool fileExists = File.Exists(fullPath);
				if (fileExists) {
					var fileExistsResult = MessageBox.Show($"Folder already has a save file \"{Model.Save.SaveFileName}\". Overwrite?", "Overwrite Save?", MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (fileExistsResult != MessageBoxResult.Yes) {
						return;
					}
				}
				Model.Save.Write(folder);
			}
		}

		private void QuitCommandExecute(object sender, ExecutedRoutedEventArgs e) {
			//TODO: Check if the file is modified.
			Application.Current.Shutdown();
		}

		private void AboutCommandExecute(object sender, ExecutedRoutedEventArgs e) {
			var aboutBox = new AboutWindow();
			aboutBox.ShowDialog();
		}

		private void EditModeChecked(object sender, RoutedEventArgs e) {
			EditMode = true;

			CharacterEditControl.Visibility = Visibility.Visible;
			CharacterViewControl.Visibility = Visibility.Collapsed;
			ProficiencyEditControl.Visibility = Visibility.Visible;
		}

		private void EditModeUnchecked(object sender, RoutedEventArgs e) {
			EditMode = false;
			CharacterEditControl.Visibility = Visibility.Collapsed;
			CharacterViewControl.Visibility = Visibility.Visible;
			ProficiencyEditControl.Visibility = Visibility.Collapsed;
		}
	}
}

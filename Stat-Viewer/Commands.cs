using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stat_Viewer {
	public class Commands {
		public static RoutedCommand QuitCommand = new RoutedCommand("QuitCommand", typeof(MainWindow));
		public static RoutedCommand AboutCommand = new RoutedCommand("AboutCommand", typeof(MainWindow));
	}
}

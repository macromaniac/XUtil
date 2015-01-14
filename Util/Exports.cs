using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace XUtil {
	/// <summary>
	/// Contains functions related to files and processes
	/// </summary>
	public static class Meta {
		/// <summary>
		/// Gets the parent of the current directory. If this is run from
		/// debug this will be the project directory. I would use reflection
		/// but I don't have the time
		/// </summary>
		/// <returns>Returns DirectoryInfo of the project assuming you're running in debug/release</returns>
		public static DirectoryInfo ProjectDirectory() {
			return Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
		}
		/// <summary>
		/// Runs filename, runs PATH/filename if filename is not found.
		/// </summary>
		///<returns>returns console output of the process</returns>
		public static string OutputFromProcess(string fileName, string args) {
			return ProcessStarter.ProcessOutput(fileName, args);
		}
		private static ConfManager configManager = null;
		/// <summary>
		///Checks project first, then parent for "Conf.json" file.
		///This dictionary is then cached. Call Conf.variablename to retrieve
		///a variable which you must then cast.
		/// </summary>
		public static dynamic Conf {
			get {
				if (configManager == null)
					configManager = new ConfManager();
				return configManager.ConfData;
			}
		}
	}
	/// <summary>
	/// Misc extension methods
	/// </summary>
	public static class ExtensionMethods {
		private static Job j = null;
		/// <summary>
		/// Kills the process whenver the current process ends.
		/// This is useful to prevent processes from running in the
		/// background after having started them
		/// </summary>
		public static void KillProcessOnExit(this Process p) {
			if (j == null)
				j = new Job();
			j.AddProcess(p.Id);
		}

		/// <summary>
		/// This function is called on a global keyboard press. Note that
		/// this requires administrator mode on windows 8
		/// </summary>
		/// <param name="key">Key that triggers action</param>
		/// <param name="modKeys">You must specify a modifier key for Global Hotkeys</param>
		/// <param name="action"> Action triggered by keypress</param>
		/// <returns></returns>
		public static KeyboardHook OnGlobalPress
				(this Keys key, ModifierKeys modKeys, Action action) {
			KeyboardHook hook = new KeyboardHook();
			hook.KeyPressed +=
		 new EventHandler<KeyPressedEventArgs>((sender, keyArgs) => action());
			hook.RegisterHotKey(modKeys, key);
			return hook;
		}


	}
}

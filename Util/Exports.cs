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
		[DllImport("User32")]
		private static extern int ShowWindow(int hwnd, int nCmdShow);

		/// <summary>
		/// Hides the window associated with a process, this only works when the project doesn't request focus
		/// </summary>
		/// <param name="p">process that has a window handle that gets hidden</param>
		public static void HideWindow(Process p) {
			ShowWindow(p.MainWindowHandle.ToInt32(), 6);
			ShowWindow(p.MainWindowHandle.ToInt32(), 0);
		}
		/// <summary>
		/// Shows the window, used to show windows after they have been hidden using User32
		/// </summary>
		/// <param name="p">process that has a window handle that gets shown</param>
		public static void ShowWindow(Process p) {
			ShowWindow(p.MainWindowHandle.ToInt32(),5);
		}
		/// <summary>
		/// Finds and returns the full non-relative file name from the current
		///  directory or any parent directory, throws FNF exception if 
		/// file with name <paramref name="filename"/> can't be found
		/// </summary>
		/// <param name="filename">The relative name of the file we're looking for</param>
		/// <returns>the full path of the filename</returns>
		public static string QuickFindFilePath(string filename) {
			return FindFileInPathToRoot(Directory.GetCurrentDirectory(), filename);
		}

		/// <summary>
		/// Searches every folder from <paramref name="path"/> up until root looking for the file
	 ///  named <paramref name="filename"/>, this search includes <paramref name="path"/>
		/// Returns the full file name. throws exception FNF if file couldn't be found
		/// </summary>
		/// <param name="path">the path we start looking from</param>
		/// <param name="filename">the relative name of the file we're looking for</param>
		/// <returns>the full path of the filename</returns>
		public static string FindFileInPathToRoot(string path, string filename) {
			//Get the current directory
			DirectoryInfo dir = new DirectoryInfo(path);
			//While we aren't at the top directory
			while (dir.Root.FullName != dir.FullName)
				if (File.Exists(dir.FullName + @"\" + filename))
					return dir.FullName + @"\" + filename;
				else
					dir = dir.Parent;
			throw new FileNotFoundException("File " + filename + " was not found in the current," +
				" or in any parent directories");
		}
		/// <summary>
		/// returns the string of a file at the particular location, strips SIG if it exists
		/// </summary>
		/// <param name="fullFilename">the full filename of the file</param>
		/// <returns>returns the file data as a string</returns>
		public static string ReadToEndNoBOM(string fullFilename) {
			using (StreamReader sr = new StreamReader(fullFilename, true))
				return sr.ReadToEnd();
			throw new Exception("Could not read file " +fullFilename);
		}
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

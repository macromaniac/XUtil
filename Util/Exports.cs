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
	public static class Meta {
		public static DirectoryInfo ProjectDirectory() {
			return Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
		}
	}
	public static class ExtensionMethods {
		//This makes sure that a process dies when the parent process dies
		static Job j = null;
		public static void KillProcessOnExit(this Process p) {
			if (j == null)
				j = new Job();
			j.AddProcess(p.Id);
		}

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

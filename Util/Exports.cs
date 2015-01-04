﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace XUtil {
	public static class Meta {
		public static string ProjectDirectory() {
			return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
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



	}
}

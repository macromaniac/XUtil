using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUtil {
	internal static class ProcessStarter {
		internal static string ProcessOutput(string fileName, string args) {
			Process p = new Process();
			p.StartInfo.FileName = fileName;
			p.StartInfo.Arguments = args;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.Start();

			string output = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			return output;
		}

	}
}

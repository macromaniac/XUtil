using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUtil {
	internal static class ProcessStarter {
		internal static string ProcessOutput(string fileName, string args) {
			if (File.Exists(fileName) == false) {
				string pathFile = "";
				//if we can't find the file we will search the PATH enviro var
				string varsString = Environment.GetEnvironmentVariable("PATH");
				string[] vars = varsString.Split(';');
				foreach (string dirString in vars) {
					if (File.Exists(dirString + @"\" + fileName))
						pathFile = dirString + @"\" + fileName;
					if (File.Exists(dirString + @"\" + fileName + ".exe"))
						pathFile = dirString + @"\" + fileName + ".exe";
				}
				if (pathFile == "")
					throw new FileNotFoundException(
						"Could not find " + fileName + " in PATH or by itself");
			}
			Process p = new Process();
			p.StartInfo.FileName = fileName;
			p.StartInfo.Arguments = args;
			//can't redirect output if we use the shell, otherwise calling
			//path programs would be easy
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.Start();
			string output = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			return output;
		}

	}
}

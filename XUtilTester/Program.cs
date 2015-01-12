using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XUtil;

namespace XUtilTester {
	class Program {
		static void Main(string[] args) {
			string output = XUtil.Meta.OutputFromProcess(
				"python", @"C:\Users\macro\git\Blocker\BlockerBuild\BlockerBuild.py");

			Console.WriteLine(output);
			Console.ReadKey();
		}
	}
}

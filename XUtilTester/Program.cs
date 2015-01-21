using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XUtil;

namespace XUtilTester {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine(Meta.Conf.program_name);
			Console.WriteLine(Meta.QuickFindFilePath("Program.cs"));
			Console.WriteLine(Meta.ReadToEndNoBOM(Meta.QuickFindFilePath("BOMTest.txt")));

			Console.ReadKey();
		}
	}
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUtil {
	internal class ConfManager {

		private FileInfo GetConfFile(DirectoryInfo di) {
			var files = di.GetFiles();
			return files.FirstOrDefault((fi) => fi.Name == "Conf.json");
		}
		dynamic data = null;
		FileInfo confFile = null;
		internal ConfManager() {
			DirectoryInfo di = Meta.ProjectDirectory();
			confFile = GetConfFile(di);
			if (confFile == null)
				confFile = GetConfFile(di.Parent);
			if (confFile == null)
				throw new FileNotFoundException("Conf.json was not found in the project directory or in the solution directory");
			JsonSerializer se = new JsonSerializer();

			StreamReader re = new StreamReader(confFile.FullName);
			JsonTextReader reader = new JsonTextReader(re);

			data = se.Deserialize(reader);
		}
		public dynamic ConfData {
			get {
				return data;
			}
		}
	}
}

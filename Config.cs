using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_Generator
{
	static class Config
	{
		static private string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Struct_Generator";
		public static bool projectEnvironment()
		{
			try
			{
				if (!Directory.Exists(configPath))
					Directory.CreateDirectory(configPath);
					
				return true;
			}
			catch
			{
				return false;
			}
			
				
		}
	}
}

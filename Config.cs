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
		static public string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Struct_Generator";
		static public string templatesPath = configPath + "\\templates";
		static public string aplicationLocation = AppDomain.CurrentDomain.BaseDirectory;
		public static bool projectEnvironment()
		{
			//Create a dir application data
			try
			{
				if (!Directory.Exists(configPath))
					Directory.CreateDirectory(configPath);

				if (!Directory.Exists(configPath + '\\' + "templates"))
					Directory.CreateDirectory(configPath + '\\' + "templates");

				return true;
			}
			catch
			{
				return false;
			}

		}
		public static bool setPath()
		{
			Console.Clear();
			try
			{
				var scope = EnvironmentVariableTarget.Machine;
				string oldValue = Environment.GetEnvironmentVariable("Path", scope);
				Environment.SetEnvironmentVariable("Path", oldValue + ";" + aplicationLocation, scope);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Added to the path.");
				Console.ForegroundColor = ConsoleColor.White;
				return true;
			}
			catch
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Is mandatory open with elevated permisions");
				Console.ForegroundColor = ConsoleColor.White;
				return false;
			}

		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_Generator
{
	static class Commands
	{
		public static void list()
		{
			Console.Clear();
			Console.WriteLine("list:	Display all commands available.\r\n");
			Console.WriteLine("templates:	Display all templates available(" + Config.configPath + ")\r\n");
			Console.WriteLine("templates [template]:	Open template file.");
			Console.WriteLine("-t [template]:	Set a template for a target folder\r\n");
			Console.WriteLine("setpath:	Add a path variable to this aplication (if you move this application to a new folder you need to set again this command)\r\n");
		}
		public static void templates()
		{
			Console.Clear();
			try
			{
				//Get files and verify if is valid
				string[] valid_templates = validateTemplates(Directory.GetFiles(Config.templatesPath));

				if (valid_templates.Length > 0)
				{
					Console.WriteLine("Templates found:(" + valid_templates.Length + ")\r\n");
				
					foreach (string template in valid_templates)
					{
							Console.WriteLine(template);
					}
				}
				else
				{
					Console.WriteLine("No templates found!");
				}
				
			}
			catch
			{
				Console.WriteLine("Cant read templates!");
			}
			
		}
		private static string[] validateTemplates(string[] templates)
		{
			string[] valid_templates = new string[0];

			foreach (string template in templates)
			{
				int len = template.Split('\\').Length;

				//search for a valid extension .json
				if (template.Split('\\')[len - 1].Split('.')[1] == "json")
					valid_templates = valid_templates.Concat(new string[] { template.Split('\\')[len - 1].Split('.')[0] }).ToArray();
			}
			return valid_templates;
		}
		public static bool setPath()
		{
			Console.Clear();
			try
			{
				var scope = EnvironmentVariableTarget.Machine;
				string oldValue = Environment.GetEnvironmentVariable("Path", scope);
				Environment.SetEnvironmentVariable("Path", oldValue + ";" + Config.aplicationLocation, scope);
				Console.WriteLine("Variable de entorno añadida.");
				return true;
			}
			catch
			{
				Console.WriteLine("Es necesario permisos de administrador para esta acción!");
				return false;
			}
			
		}
		public static void openTemplate(string file)
		{
			
			if (validateTemplates(Directory.GetFiles(Config.templatesPath)).Any(file.Contains)){
				try
				{
					file += ".json";
					Process.Start(Config.templatesPath + "\\" + file);
					Console.WriteLine("abierto");
				}
				catch
				{
					Console.WriteLine("error");
				}

			}
			else
			{
				Console.WriteLine("Template not found!");
			}

		}
	}
}

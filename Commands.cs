using Newtonsoft.Json.Linq;
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
			Console.WriteLine("list:			Display all commands available.\r\n");
			Console.WriteLine("help -n:		Display and explain an expample template.\r\n");
			Console.WriteLine("templates:		Display all templates available (" + Config.configPath + ")\r\n");
			Console.WriteLine("templates [template]:	Open template file.\r\n");
			Console.WriteLine("templates -c:		Create a template that contains all of target folder (BETA)\r\n");
			Console.WriteLine("-n [template]:		Create a new template.\r\n");
			Console.WriteLine("-a [template]:		Generate structure of the template to a target folder\r\n");
			Console.WriteLine("setpath:		Add a path variable to this aplication (if you move this application to a new folder you need to set again this command)\r\n");
		}
		public static void templates()
		{
			Console.Clear();
			try
			{
				//Get files and verify if is valid and return a list of templates
				string[] valid_templates = validateTemplates(Directory.GetFiles(Config.templatesPath));

				if (valid_templates.Length > 0)
				{
					Console.WriteLine("Templates found:(" + valid_templates.Length + ")\r\n");
					Console.ForegroundColor = ConsoleColor.Blue;
					foreach (string template in valid_templates)
					{

						Console.WriteLine(template);
					}
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("No templates found!");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			catch
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Cant read templates!");
				Console.ForegroundColor = ConsoleColor.White;
			}

		}
		private static string[] validateTemplates(string[] templates)
		{
			//verify .json files
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
		public static void openTemplate(string file)
		{
			//Open a template if is valid.
			if (validateTemplates(Directory.GetFiles(Config.templatesPath)).Any(file.Contains))
			{
				try
				{
					file += ".json";
					Process.Start(Config.templatesPath + "\\" + file);
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
		public static void createTemplateExample(string name)
		{
			//Create and open an new template

			if (validateTemplates(Directory.GetFiles(Config.templatesPath)).Any(name.Equals))
			{
				name += ".json";

				Process template = new Process();
				template.StartInfo.FileName = Config.templatesPath + "\\" + name;
				template.EnableRaisingEvents = true;
				Console.WriteLine("There is a template with that name");
				template.Start();
				template.WaitForExit();


			}
			else
			{
				name += ".json";
				StreamWriter sw = File.CreateText(Config.templatesPath + "\\" + name);
				sw.WriteLine("{}");
				sw.Close();
				sw.Dispose();

				Process template = new Process();
				template.StartInfo.RedirectStandardOutput = false;
				template.StartInfo.RedirectStandardError = false;
				template.StartInfo.FileName = Config.templatesPath + "\\" + name;
				template.EnableRaisingEvents = true;

				template.Start();
				template.WaitForExit();
				template.Dispose();

				//Console.WriteLine("Nuevo");
			}

			Console.Clear();
			Console.WriteLine("Name of template: " + name.Split('.')[0]);

		}
		public static void createTemplateBase(string templateName)
		{
			//Create new template based on existing estructure.

			Console.WriteLine("Please write a name for the template");
			string name = templateName == "" ? Console.ReadLine() : templateName;

			while (validateTemplates(Directory.GetFiles(Config.templatesPath)).Any(name.Equals))
			{
				Console.WriteLine("There is already a template with that name! Please write a name for the template");
				name = Console.ReadLine();
			}
			name += ".json";
			JObject template = new JObject();

			string[] folders = Directory.GetDirectories(Environment.CurrentDirectory);

			//add all dirs from root folder target

			foreach (string folder in folders)
			{
				string folder_name = folder.Split('\\')[folder.Split('\\').Length - 1];
				template.Add(new JProperty(folder_name, new JArray(folderContent(folder_name, new JObject()))));

			}

			//add files from root folder target
			foreach (string file in Directory.GetFiles(Environment.CurrentDirectory))
			{
				string file_name = file.Split('\\')[file.Split('\\').Length - 1];
				string content = File.ReadAllText(file);

				template.Add(new JProperty(file_name, content));
				Console.WriteLine("Adding -> " + file_name);

			}

			Console.WriteLine("Saved as: " + name);
			File.WriteAllText(Config.templatesPath + "\\" + name, template.ToString());

		}
		private static JObject folderContent(string folder, JObject parent_folder)
		{
			//Recursive method that adds all files and dirs to a template. 

			Console.WriteLine("Adding -> " + folder);


			if (Directory.GetDirectories(folder).Length > 0 || Directory.GetFiles(folder).Length > 0)
			{
				string[] sub_directories = Directory.GetDirectories(folder);

				foreach (string dir in sub_directories)
				{
					string dir_name = dir.Split('\\')[dir.Split('\\').Length - 1];

					parent_folder.Add(new JProperty(dir_name,
						new JArray(folderContent(dir, new JObject()))));

					if (Directory.GetFiles(dir).Length > 0)
					{

						JArray myarray = (JArray)parent_folder[dir_name];

						JObject item = (JObject)myarray[0];


						foreach (string file in Directory.GetFiles(dir))
						{
							Console.WriteLine("Adding -> " + file);

							string file_name = file.Split('\\')[file.Split('\\').Length - 1];
							string content = File.ReadAllText(file);

							item.Add(new JProperty(file_name, content));

						}
					}

				}

				return parent_folder;
			}
			return parent_folder;
		}

		public static void createStructure(string templateName)
		{
			//Create all structure from template to the target folder. PD Care of binary files!
			Console.WriteLine(templateName);
			if (validateTemplates(Directory.GetFiles(Config.templatesPath)).Any(templateName.Equals))
			{
				templateName += ".json";
				JObject template = JObject.Parse(File.ReadAllText(Config.templatesPath + "\\" + templateName));
				createTemplateFolder(template, Environment.CurrentDirectory);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Structure done!");
				Console.ForegroundColor = ConsoleColor.White;
			}

			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Couldn't find any template with that name!");
				Console.ForegroundColor = ConsoleColor.White;

				Console.WriteLine("List of templates:");
				Console.ForegroundColor = ConsoleColor.Blue;

				foreach (string template in  validateTemplates(Directory.GetFiles(Config.templatesPath)))
				{
					Console.WriteLine(template);
				}
				Console.ForegroundColor = ConsoleColor.White;
			}

		}

		private static void createTemplateFolder(JObject folder, string path)
		{
			//Create all files and directories from template 
			foreach (var subfolder in folder)
			{
				if (folder[subfolder.Key].Type == JTokenType.Array)
				{
					Directory.CreateDirectory(path + "\\" + subfolder.Key);
					Console.WriteLine("Adding Dir ->	" + path + "\\" + subfolder.Key);
				}
				else
				{
					StreamWriter sw = File.CreateText(path + "\\" + subfolder.Key);
					sw.WriteLine(subfolder.Value.ToString());
					sw.Close();
					sw.Dispose();
					Console.WriteLine("Adding File ->	" + path + "\\" + subfolder.Key);
				}

				foreach (JObject y in folder[subfolder.Key])
				{
					createTemplateFolder(y, path + "\\" + subfolder.Key);
					
				}

			}
		}
		public static void example()
		{
			//not finished
			Console.Clear();
			Console.WriteLine("All templates must be.json to apply. Please consider to read documetation of application.");
			Console.WriteLine("Default template folder(can't be changed): " + Config.templatesPath + "\r\n");
			Console.WriteLine("All folders have the following syntax ' \"#name\" :[contents]', and inside this brackets you can create more folders or any file '{\"#name\" : \"content\"}\r\n");
			Console.WriteLine("Example: ");
			Console.Write(@"{

		""css"": [
			{
				""styles.css"": "".example{margin:0px;}"",
					""styles2.css"": ""css code...""
			}
			],
        ""config.ini"": ""config-file:..."",
        ""img"": [
                  {
                        ""Exterior"": [
				     {
                                        ""file.txt"": ""file content"",
                                        ""file2.txt"": ""file content""
				     }
                        ],
                        ""Interior"": [
                                     {
                                        ""another.txt"": ""content"",
                                        ""another2.txt"": ""more content""
				      }
                        ]
                  }
        ]
}");
			Console.WriteLine("\r\n...");
			Console.SetWindowPosition(0, 0);

			Console.ReadLine();


			Console.WriteLine("Structre of example:\r\n");
			Console.WriteLine(@"
├────Target_folder
│	├────css
		 ├────styles.css
		 ├────styles2.css");


		}
	}

}

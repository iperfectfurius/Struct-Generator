using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Struct_Generator
{
	class Program
	{
		static private string options;//Futuro
		static private string current_directory = Environment.CurrentDirectory;
		static void Main(string[] args)
		{
			Console.Clear();
			Console.Title = "Struct Generator";

			bool arguments = false;

			if (Config.projectEnvironment())
			{
				if (args.Length < 1)
				{
					
					Console.SetCursorPosition(Console.WindowWidth - ("Version: ".Length + Assembly.GetExecutingAssembly().GetName().Version.ToString().Length), 0);
					Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
					Console.WriteLine("Please write any arguments:('-i' for interface,type help for more info)");
					Console.WriteLine("Folder target: " + current_directory);
				}
				else
					arguments = true;

				while (!arguments)
				{
					string line = Console.ReadLine();

					switch (line.ToLower())
					{
						case "done":
						case "":
							arguments = true;
							continue;
						case "list":
						case "help":
							Commands.list();
							break;
						case "help -n":
							Commands.example();
							break;
						case "templates":
							Commands.templates();
							break;
						case "templates -c":
							Commands.createTemplateBase("");						
							break;
						case var template when (Regex.Match(line.ToLower(), @"\b(templates)\b -a \w{2}", RegexOptions.IgnoreCase).Success):
							Commands.createStructure(line.ToLower().Split(' ')[2]);
							break;
						case var template when (Regex.Match(line.ToLower(), @"\b(templates)\b \w*", RegexOptions.IgnoreCase).Success):
							Commands.openTemplate(line.ToLower().Split(' ')[1]);
							break;
						case var template when (Regex.Match(line.ToLower(), @".[-n] \w*", RegexOptions.IgnoreCase).Success):
							if (line.ToLower().Trim().Length < 5)
							{
								Console.WriteLine("Name length is too small!");
								continue;
							}
							Commands.createTemplateExample(line.ToLower().Split(' ')[1]);
							break;
						case "setpath":
							Config.setPath();
							break;
						default:
							Console.WriteLine("Invalid command or argument");
							break;

					}


				}
				
			}

			switch (String.Join(" ", args))
			{
				case var template when (Regex.Match(String.Join(" ", args).ToLower(), @"-a \w{1}", RegexOptions.IgnoreCase).Success):
					Commands.createStructure(args[args.Length-1]);
					break;
				case var template when (Regex.Match(String.Join(" ", args).ToLower(), @"-c \w{1}", RegexOptions.IgnoreCase).Success):
					Commands.createTemplateBase(args[1]);
					break;
				case "templates":
					Commands.templates();
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Invalid Arguments, write structe_generator whithout arguments for more info.");
					break;
			}
			Console.ForegroundColor = ConsoleColor.White;

		}
	}
}

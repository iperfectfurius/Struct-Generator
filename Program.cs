using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Struct_Generator
{
	class Program
	{
		static private string template;
		static private string options;//Futuro
		static private bool arguments;
		static private string[] commands = {"list","generators"};
		static private string current_directory = Environment.CurrentDirectory;
		static void Main(string[] args)
		{		

			if (Config.projectEnvironment())
			{
				if (args.Length < 1)
				{
					Console.WriteLine("Current Version: "  + Assembly.GetExecutingAssembly().GetName().Version.ToString());
					Console.WriteLine("Please write any arguments:('-i' for interface,type help for more info)");
					Console.WriteLine("Folder target: " + current_directory);
				}
				else
					arguments = true;

				while(!arguments)
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
						case "templates":
							Commands.templates();
							break;
						case var template when (Regex.Match(line.ToLower(), @"\b(templates)\b \w*", RegexOptions.IgnoreCase).Success):
							Console.WriteLine("Regex");
							Commands.openTemplate(line.ToLower().Split(' ')[1]);
							break;
						case "setpath":
							Commands.setPath();
							break;
						default:
							foreach (string s in line.Split(' '))
								args = args.Concat(new string[] { s }).ToArray();
							break;

					}
					


				}
				Console.WriteLine("Parametros guardados:");

				foreach (string s in args)
				{
					Console.WriteLine(s);
				}
			}
			
			Console.Read();
			
		}
	}
}

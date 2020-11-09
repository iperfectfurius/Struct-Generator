using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
					Console.WriteLine("Please write any arguments:('i' for interface)");
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
							arguments = true;
							break;
						case "list":

							break;
						default:
							args.Append(line);
							break;
					}
						
				}
				
				
				foreach (string s in args)
				{
					Console.WriteLine(s);
					switch (s)
					{
						default:
							break;
					}
				}
			}
			
			Console.Read();
			
		}
	}
}

using System;
using System.Threading;
using TODO.Models;
namespace TODO
{
	public class TODO_Monitor
	{
		public static List<Task> tasks = new();
		public static void TODORunner()
		{
			Console.Title = "\t - - - - - WELCOME TO TO-DO LIST APP - - - - - ";
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.BackgroundColor = ConsoleColor.Cya;
			
			tasks.Add(new() { Finished_Status = false, TaskID = tasks.Count == 0 ? 1 : tasks.Count, Title = " To do Hometasks " });
			tasks.Add(new() { Finished_Status = false, TaskID = tasks.Last().TaskID + 1, Title = " Reading a book " });
			tasks.Add(new() { Finished_Status = false, TaskID = tasks.Last().TaskID + 1, Title = " Go running" });
			bool doTask = true;
			while (doTask)
			{
				Console.WriteLine("Choose one of the options : " +
					"\n1.Add new task " +
					"\n2.Mark a Task as finished " +
					"\n3.Search task by ID " +
					"\n4.Get the list of Non-finished tasks " +
					"\n5.Get the list of Finished tasks " +
					"\n6.Show Todo list " +
					"\n0. Enter '0' or any non digit character to exit ");

				int option = int.Parse(Console.ReadLine());

				switch (option)
				{
					case 1:
						{
							Methods.AddTask(out string title);
                        }
						break;
					case 2:
						{
							Methods.MarkAsFinished();
						}
						break;
					case 3:
						{
							Console.Write("Enter the task id ");
							int id = int.Parse(Console.ReadLine());
							Console.WriteLine("\v\t"+Methods.GetTask(id).ToString() ?? "Task not found");
						}break;
					case 4:
						{
							Methods.GetAll(false).ForEach(x => Console.WriteLine(x.Title));
						}break;
					case 5:
						{
							Methods.GetAll(true).ForEach(x => Console.WriteLine(x.Title));
						}break;
					case 6: { tasks.ForEach(x => Console.WriteLine("\t"+x +"\v")); }break;
					default:
						{
							doTask = false;
							Console.WriteLine(" \a\v\tThanks for using \n\t Good bye;) !!!\a\a"); continue;
						}		// if case 1-6 was entered ,it goes to "check if user wants to operate any task"
								// else it finishes the app without checking
						
				}
                //check if user wants to operate any task
                Console.WriteLine("\v\tDo you want to do another operation ?\n\t 'yes' or 1 for yes \n\t 'no' or 0 to exit ");
				
				byte maxAttempt = 3;
				while (maxAttempt!=0)
				{
					Console.Write(" Enter your choice : ");
					string? choice = Console.ReadLine();
					if (!(choice == "1" | choice == "yes"))
					{
						if (!(choice == "no" | choice == "0"))
						{
                            maxAttempt--;
                            Console.WriteLine("\a\a\a\a Invalid command entry (attempts left :{0} times ) ", maxAttempt);
							if (maxAttempt == 0) doTask = false;
						}
						else
						{
                            doTask = false; Console.WriteLine("Thanks for using ,bye-bye !");break;
                        }
					}
				}
				Console.Clear();
			}
		}
	}
}


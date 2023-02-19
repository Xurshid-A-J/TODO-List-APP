using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TODO.Models
{
	public static class Methods
	{
        
		static readonly ReaderWriterLockSlim rwLock = new();

		public static void AddTask(out string title)
		{
			Console.Write("Enter the task title :" );
			title = Console.ReadLine() ?? " No title ";
			TODO_Monitor.tasks.Add(new Task(title));

			Thread thread1 = new(() => UpdateDatabase());
			thread1.Start();
		}

		public static void MarkAsFinished()
		{
			byte attempts = 3;
			bool taskFound = false;
			while (attempts !=0 & !taskFound)
			{
				Console.Write(" Enter the task ID : ");
				int taskID = int.Parse(Console.ReadLine());
				if (TODO_Monitor.tasks.Any(x =>
											 {
												if (x.TaskID == taskID)
												{
													x.Finished_Status = true;
													return true;
												}
												else return false;
											 }
										   )
				   )
				{
					Console.WriteLine(" Marked as Finished !!!"); taskFound = true;
                    Thread thread2 = new(() => UpdateDatabase());
					thread2.Start();
                }
				else { attempts--; Console.WriteLine(" Couldnt find the task with this ID,(attempts left :{0} times ",attempts); }
			}

		if(!taskFound) Console.WriteLine("Maximum attempts exceeded. Please try again later.");
		}


		public static Task? GetTask(int? Id)
		{
            byte attempts = 3;
            bool taskFound = false;
            while (attempts !=0 & !taskFound)
            {
                Console.Write(" Enter the task ID");
                 Id = int.Parse(Console.ReadLine()?? "-1");
				if (TODO_Monitor.tasks.Any(x =>
												{
													return (x.TaskID == Id) ? true : false;
												} ))
				{ // if any task with Id exists ,it enters here
					taskFound = true; return TODO_Monitor.tasks.Find(x => x.TaskID == Id);
				}
				else
				{ 
					attempts--;
					Console.WriteLine(" \a\a\a Incorrect ID !!! \a\a\a");
					Console.WriteLine(" Couldnt find the task with this ID,(attempts left :{0} times ", attempts);
				}
            }

            if (!taskFound) Console.WriteLine("Maximum attempts exceeded. Please try again later.");
			return null;
		}

		public static List<Task> GetAll(bool finished)
		{ 
			if(finished) return TODO_Monitor.tasks.FindAll(x => x.Finished_Status == true);
			else return  TODO_Monitor.tasks.FindAll(x => x.Finished_Status == false);
		}

		public static void UpdateDatabase()
		{
			try
			{ rwLock.EnterWriteLock();
				{
					string filePath = GetDataPath() + "/database.json";
					string serializedToJson = JsonConvert.SerializeObject(TODO_Monitor.tasks);
					File.WriteAllText(filePath, serializedToJson);
				}
			}
			finally { rwLock.ExitWriteLock(); }
        }

        public static string GetDataPath()
        {
            string path = (new DirectoryInfo(Directory.GetCurrentDirectory())).Parent?.Parent?.Parent?.FullName + "/Data" ?? "No Directory exists";
            return path;
        }

		

    }
}


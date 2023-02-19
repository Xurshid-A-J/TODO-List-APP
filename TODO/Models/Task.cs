using System;
using System.Text.RegularExpressions;

namespace TODO
{
	public class Task
	{
		public int TaskID { get; set; }

		private string title;
		public string? Title
		{ get=>title;
			set
			{ if (String.IsNullOrEmpty(value))
				{ throw new Exception(" Title cannot be null or empty "); }
				title = value;
			}
		}

		public bool Finished_Status { get; set; } = false;

		public Task(string title):this()
		{
			Title = title;
		}
		public Task()
		{
           TaskID= TODO_Monitor.tasks.Count()+ 1;

        }
        public override string ToString()
        {
			string a = ("Task ID :{0} \n" +
				" Title : {1} \n" +
                " Finished Status : {2} ",TaskID,Title,Finished_Status).ToString();
            return a;
        }
       

        //if (!Regex.IsMatch(value.ToString(), @"^\d*$")) Console.WriteLine(" Task number should only be digits !");
        //	else if(!(value > 0)) Console.WriteLine(" Task number incorrect,only positive numbers are allowed ");
        //	else if (TODO_Monitor.tasks.Any(x => x.TaskID == value)) Console.WriteLine(" Task with this number already exists");
        //	else { TaskID = value;}
    }
}


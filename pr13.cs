using System;

namespace TaskExample
{
    class TaskRunner
    {
        public event EventHandler TaskCompleted;

        public void RunTask()
        {
            Console.WriteLine("Loading...");

            OnTaskCompleted();
        }
        protected virtual void OnTaskCompleted()
        {
            if (TaskCompleted != null)
            {
                TaskCompleted(this, EventArgs.Empty);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            TaskRunner task = new TaskRunner();

            task.TaskCompleted += (sender, args) =>
            {
                Console.WriteLine("All!");
            };

            task.RunTask();
        }
    }
}

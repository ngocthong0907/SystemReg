using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace NinjaSystem
{
    public class TaskController
    {
        public static Task[] createTask(int numthread)
        {
            Task[] tasks = new Task[numthread];
            return tasks;
            
        }
        public static bool checkAvailableTask(Task[] list_task)
        {
            try
            {
                foreach (Task t in list_task)
                {
                    if (t == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (t.Status != TaskStatus.Running)
                        {
                            return true;
                        }
                    }

                }
            }
            catch
            { }
           
            return false;
        }
        public static int getAvailableTask(Task[] list_task)
        {
            int index = -1;
            try
            {
                foreach (Task t in list_task)
                {
                    index++;
                    if (t == null)
                    {
                        return index;
                    }
                    else
                    {
                        if (t.Status != TaskStatus.Running)
                        {
                            return index;
                        }

                    }

                }
            }
            catch { }
           
            return -1;
        }
    }
}

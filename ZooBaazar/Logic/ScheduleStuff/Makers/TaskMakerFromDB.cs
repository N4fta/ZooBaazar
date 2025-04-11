using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScheduleStuff.Makers
{
    public class TaskMakerFromDB : ITaskMaker
    {
        private readonly List<Task> tasksDB;

        public TaskMakerFromDB(List<Task> tasksDB)
        {
            this.tasksDB = tasksDB;
        }

        public List<Task> GenerateTasks()
        {
            List<Task> repoTasks = tasksDB;

            repoTasks.ForEach(task => { task.ScheduleTask = true; });
            return repoTasks;
        }
    }
}

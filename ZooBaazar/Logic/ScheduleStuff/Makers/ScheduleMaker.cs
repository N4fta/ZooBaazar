using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScheduleStuff.Makers
{
    public class ScheduleMaker : IScheduleMaker
    {
        private readonly ShiftMakerDefault shiftMaker;
        private readonly List<ITaskMaker> taskMakers;
        private readonly int startDay;
        private readonly int endDay;

        public ScheduleMaker(List<ITaskMaker> taskMakers, ShiftMakerDefault shiftMaker, int startOfDay = 7, int endOfDay = 19)
        {
            this.taskMakers = taskMakers;
            this.shiftMaker = shiftMaker;
            if (startOfDay < 0 || startOfDay > endOfDay) throw new Exception("Start of Day must be between 0 and End of Day");
            startDay = startOfDay;
            if (endOfDay < startOfDay || endOfDay > 24) throw new Exception("End of Day must be between Start of Day and 24 o'clock");
            endDay = endOfDay;
        }

        public Schedule GenerateSchedule(DateTime validUntil)
        {
            // Shifts
            List<KeyValuePair<int, Shift>> employeeShiftsPerWeek = new(); // <Amount of shifts per employee + Shift object> per Week
            employeeShiftsPerWeek.AddRange(shiftMaker.GenerateShifts());
            // Align times to start/end of day
            foreach (var shiftPair in employeeShiftsPerWeek)
            {
                Shift shift = shiftPair.Value;
                int shiftDuration = GetShiftDuration(shift);

                shift.StartDate = shift.StartDate.Value.AddHours(-shift.StartDate.Value.Hour);
                shift.StartDate = shift.StartDate.Value.AddMinutes(-shift.StartDate.Value.Minute);
                shift.StartDate = shift.StartDate.Value.AddSeconds(-shift.StartDate.Value.Second);
                shift.EndDate = shift.EndDate.Value.AddHours(-shift.EndDate.Value.Hour);
                shift.EndDate = shift.EndDate.Value.AddMinutes(-shift.EndDate.Value.Minute);
                shift.EndDate = shift.EndDate.Value.AddSeconds(-shift.EndDate.Value.Second);
                if (shift.DayShift)
                {
                    shift.StartDate = shift.StartDate.Value.AddHours(startDay);
                    shift.EndDate = shift.StartDate.Value.AddHours(shiftDuration);
                }
                else
                {
                    shift.StartDate = shift.StartDate.Value.AddHours(endDay);
                    shift.EndDate = shift.StartDate.Value.AddHours(shiftDuration);
                }
            }

            // Tasks
            List<Task> tasks = new();
            foreach (ITaskMaker taskMaker in taskMakers)
            {
                tasks.AddRange(taskMaker.GenerateTasks());
            }
            // Align times to next Monday
            DateTime nextMonday = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i)).Single(day => day.DayOfWeek == DayOfWeek.Monday); // Creates an Enumerable with all Days of the week and selects Monday
            foreach (var task in tasks)
            {
                while (task.StartDate.Day != nextMonday.Day)
                {
                    task.StartDate = task.StartDate.AddDays(1); // Progresses to next Day
                    task.EndDate = task.EndDate.AddDays(1);
                }
            }

            // Daily Tasks
            var dailyTasks = tasks.Where(t => t.RepeatType == RepeatEnum.Daily).ToList();
            var baseDay = DistributeTasksThroughDay(dailyTasks);

            // Weekly Tasks
            var baseWeek = CreateWeek(baseDay);
            var weeklyTasks = tasks.Where(t => t.RepeatType == RepeatEnum.Weekly).ToList(); // Foreach Weekly task
            var weekTasks = DistributeTasksThroughWeek(weeklyTasks, baseWeek);

            // Assign employees
            Dictionary<DayOfWeek, List<Task>> weekShifts = new();
            AssignTasksToShiftsForWeek(weekTasks, weekShifts, employeeShiftsPerWeek);

            // Duplicate base weeks
            Schedule schedule = new (validUntil, new() { { DateTime.Now, weekTasks } }, new() { { DateTime.Now, weekShifts } });

            //Dictionary<DayOfWeek, List<Task>> week2 = new();
            //Dictionary<DayOfWeek, List<Task>> week3 = new();
            //foreach (var day in weekTasks)
            //{
            //    week2.Add(day.Key, new());
            //    week3.Add(day.Key, new());
            //    foreach (var task in day.Value)
            //    {
            //        week2[day.Key].Add(task.ShallowCloneWithExtraDays(7));
            //        week3[day.Key].Add(task.ShallowCloneWithExtraDays(14));
            //    }
            //}
            //schedule.TasksPerWeek.Add(DateTime.Now.AddDays(7), week2);
            //schedule.TasksPerWeek.Add(DateTime.Now.AddDays(14), week3);

            return schedule;
        }


        // Takes a list of the days' Tasks and returns a list which represents a day
        // Aligns the tasks to the correct timeslots
        private List<Task> DistributeTasksThroughDay(List<Task> daysTask)
        {
            var baseDay = new List<Task>();
            foreach (Task templateTask in daysTask) // Foreach task
            {

                int taskDuration = templateTask.EndDate.Subtract(templateTask.StartDate).Hours;
                int dailyAmount = templateTask.NumberOfRepeats;
                int intervalBetweenTasksInMinutes = 0;
                if (templateTask.DayTask && templateTask.NightTask)
                {
                    if (dailyAmount > 1)
                    {
                        // Available Time minus amount of time needed for tasks = hours of "rest"
                        intervalBetweenTasksInMinutes = (24 - taskDuration * dailyAmount) / dailyAmount - 1;
                        if (intervalBetweenTasksInMinutes < 0)
                        {
                            throw new Exception("Not enought Time to complete all tasks");
                        }
                    }
                }
                else if (templateTask.DayTask)
                {
                    if (dailyAmount > 1)
                    {
                        // Day Time minus amount of time needed for tasks = hours of "rest"
                        intervalBetweenTasksInMinutes = (endDay - startDay - taskDuration * dailyAmount) / (dailyAmount - 1) * 60;
                        if (intervalBetweenTasksInMinutes < 0)
                        {
                            throw new Exception("Not enought Day Time to complete all tasks");
                        }
                    }
                    // Align Times to start of day
                    templateTask.StartDate = templateTask.StartDate.AddHours(startDay);
                    templateTask.EndDate = templateTask.EndDate.AddHours(startDay);
                }
                else if (templateTask.NightTask)
                {
                    if (dailyAmount > 1)
                    {
                        // Night Time minus amount of time needed for tasks = hours of "rest"
                        intervalBetweenTasksInMinutes = (startDay + (24 - endDay) - taskDuration * dailyAmount) / (dailyAmount - 1) * 60;
                        if (intervalBetweenTasksInMinutes < 0)
                        {
                            throw new Exception("Not enought Night Time to complete all tasks");
                        }
                    }
                    // Align Times to end of day
                    templateTask.StartDate = templateTask.StartDate.AddHours(endDay);
                    templateTask.EndDate = templateTask.EndDate.AddHours(endDay);
                }
                // Add Tasks in correct time slots
                for (int i = 0; i < dailyAmount; i++)
                {
                    var task = templateTask.ShallowClone();
                    task.StartDate = task.StartDate.AddMinutes((taskDuration * 60 + intervalBetweenTasksInMinutes) * i); // Add duration of i tasks and i breaks
                    task.EndDate = task.EndDate.AddMinutes((taskDuration * 60 + intervalBetweenTasksInMinutes) * i);
                    baseDay.Add(task);
                }
            }
            return baseDay;
        }

        private Dictionary<DayOfWeek, List<Task>> DistributeTasksThroughWeek(List<Task> weeklyTask, Dictionary<DayOfWeek, List<Task>> week)
        {
            foreach (Task templateTask in weeklyTask) // Foreach task
            {
                var numberOfRepeats = templateTask.NumberOfRepeats;
                var intervalInDays = (7 - numberOfRepeats) / numberOfRepeats; // Rounds to 0 when bellow 1 day between tasks
                if (intervalInDays<1) intervalInDays = 1;

                if (templateTask.DayTask)
                {
                    // Align Times to start of day
                    templateTask.StartDate = templateTask.StartDate.AddHours(startDay);
                    templateTask.EndDate = templateTask.EndDate.AddHours(startDay);
                }
                else if (templateTask.NightTask)
                {
                    // Align Times to end of day
                    templateTask.StartDate = templateTask.StartDate.AddHours(endDay);
                    templateTask.EndDate = templateTask.EndDate.AddHours(endDay);
                }

                DayOfWeek currentDay = DayOfWeek.Monday;
                for (int i = 0; i < numberOfRepeats; i++)
                {
                    week[currentDay].Add(templateTask.ShallowClone());
                    for (int j = 0; j < intervalInDays; j++)
                    {
                        currentDay = (DayOfWeek)(((int)currentDay + 1) % 7); // Progresses to next Day
                        templateTask.StartDate = templateTask.StartDate.AddDays(1);
                        templateTask.EndDate = templateTask.EndDate.AddDays(1);
                    }
                }
            }
            return week;
        }

        private void AssignTasksToShiftsForWeek(Dictionary<DayOfWeek, List<Task>> week, Dictionary<DayOfWeek, List<Task>> weekShifts, List<KeyValuePair<int, Shift>> employeeShiftsPerWeeek)
        {
            foreach (var day in week) // Foreach day in week, try assigning employees to tasks based on shifts
            {
                List<Shift> shiftsToday = new();

                foreach (Task newTask in day.Value)
                {
                    bool taskAssigned = false;
                    // Try to find shifts that match the tasks criteria in existing Shifts
                    List<Shift>? existingShifts = shiftsToday.Where(shift =>
                        (shift.DayShift == newTask.DayTask || shift.NightShift == newTask.NightTask) // Day or Night shift match
                        && shift.Employee.Contract.workDays.Contains(newTask.StartDate.DayOfWeek) // Day of week match
                        && shift.Employee.Role == newTask.WorkType // WorkType/Role match
                        ).ToList();
                    foreach (var existingShift in existingShifts)
                    {
                        // Find out if there is free time in shift
                        int existingTasksDuration = existingShift.Tasks.Select(task => GetTaskDuration(task)).Sum();
                        if (shiftMaker.shiftLenghtInHours - existingTasksDuration < GetTaskDuration(newTask))
                        {
                            // Not enought time left in shift
                            continue;  // Go to next shift
                        }

                        // Check Shift free time is not too different from task
                        if (!(existingShift.StartDate.Value < newTask.StartDate.AddHours(1)
                            && existingShift.EndDate.Value > newTask.EndDate.AddHours(-1)))
                        {
                            // Task is outside Shift times
                            continue;  // Go to next shift
                        }

                        // Find occupied time
                        DateTime StartOccupiedTime = existingShift.StartDate.Value;
                        DateTime EndOccupiedTime = existingShift.StartDate.Value;
                        foreach (Task shiftTask in existingShift.Tasks)
                        {
                            if (shiftTask.StartDate < StartOccupiedTime) StartOccupiedTime = shiftTask.StartDate;
                            if (shiftTask.EndDate > EndOccupiedTime) EndOccupiedTime = shiftTask.EndDate;
                        }
                        // Checks if there is enough time at the end of the shift
                        if (existingShift.EndDate - EndOccupiedTime > newTask.StartDate - newTask.EndDate)
                        {
                            // This is a valid shift
                            // Adjust Task time
                            var durationNewTask = GetTaskDuration(newTask);
                            newTask.StartDate = EndOccupiedTime;
                            newTask.EndDate = newTask.StartDate.AddHours(durationNewTask);

                            newTask.Employees.Add(existingShift.Employee); // Add employee to Task
                            existingShift.Tasks.Add(newTask); // Add Task to shift
                            taskAssigned = true; // Notify program not to search for new Shift
                            break;
                        }
                    }
                    if (!taskAssigned)
                    {

                        // This shift does not have space

                        // Find new shift
                        var oldPair = employeeShiftsPerWeeek.Find(pair =>
                        (pair.Value.DayShift == newTask.DayTask || pair.Value.NightShift == newTask.NightTask) // Day or Night shift match
                        && pair.Value.Employee.Contract.workDays.Contains(newTask.StartDate.DayOfWeek) // Day of week match
                        && pair.Value.Employee.Role == newTask.WorkType // WorkType/Role match
                        && existingShifts.Where(shift => shift.Employee == pair.Value.Employee
                                            && (newTask.EndDate > shift.EndDate && newTask.StartDate < shift.EndDate
                                                || newTask.EndDate < shift.EndDate && newTask.StartDate > shift.StartDate)
                                            ).Count() == 0 // Employee is not already busy
                        );
                        if (oldPair.Value == null)
                        {
                            throw new Exception($"Not enough employees for all {newTask.WorkType} Type tasks");
                        }
                        // Clone shift
                        KeyValuePair<int, Shift> newPair = new(oldPair.Key, oldPair.Value.ShallowClone());
                        // Adjust Shift time
                        var duration = GetShiftDuration(newPair.Value);
                        newPair.Value.StartDate = newTask.StartDate;
                        newPair.Value.EndDate = newPair.Value.StartDate.Value.AddHours(duration);

                        newTask.Employees.Add(newPair.Value.Employee); // Assign Employee to Task
                        newPair.Value.Tasks.Add(newTask);// Add Task to shift
                        shiftsToday.Add(newPair.Value); // Add shift to shifts in use

                        // Decrease the amount of weekly shifts
                        employeeShiftsPerWeeek.Remove(oldPair);
                        employeeShiftsPerWeeek.Add(new(newPair.Key - 1, newPair.Value));
                    }
                }

                weekShifts.Add(day.Key, shiftsToday.ConvertAll(shift => shift.ConvertToTask()));
            }
        }

        // Clones a Day (List Of Tasks) and creates a Week
        private Dictionary<DayOfWeek, List<Task>> CreateWeek(List<Task> baseDay)
        {
            // Week Array
            var baseWeek = new Dictionary<DayOfWeek, List<Task>>();
            // Lots of cloning and progressing one day
            var currentDay = DayOfWeek.Monday;
            for (int i = 0; i < 7; i++)
            {
                baseWeek.Add(currentDay, baseDay.Select(task => task.ShallowClone()).ToList());
                baseDay.ForEach(task => task.StartDate = task.StartDate.AddDays(1));
                baseDay.ForEach(task => task.EndDate = task.EndDate.AddDays(1));

                currentDay = (DayOfWeek)(((int)currentDay + 1) % 7);
            }
            return baseWeek;
        }

        private int GetTaskDuration(Task task)
        {
            int duration = 0;
            if (task.EndDate.Day != task.StartDate.Day)
            {
                duration = 24 - task.StartDate.Hour;
                duration += task.EndDate.Hour;
            }

            else
            {
                duration = task.EndDate.Hour - task.StartDate.Hour;
            }
            return duration;
        }
        private int GetShiftDuration(Shift shift)
        {
            int duration = 0;
            if (shift.EndDate.Value.Day != shift.StartDate.Value.Day)
            {
                duration = 24 - shift.StartDate.Value.Hour;
                duration += shift.EndDate.Value.Hour;
            }

            else
            {
                duration = shift.EndDate.Value.Hour - shift.StartDate.Value.Hour;
            }
            return duration;
        }
    }
}

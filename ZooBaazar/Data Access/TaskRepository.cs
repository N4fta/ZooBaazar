using Data_Access;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class TaskRepository : DatabaseConnection, ITaskRepository
    {
        public Result InsertTask(TaskDTO taskDTO)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            if (taskDTO.Id != 0)
            {
                cmd.CommandText = @"SET IDENTITY_INSERT [Task] ON
                                    INSERT INTO [Task] (TaskID, Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, LocationID) 
                                    VALUES (@TaskID, @Name, @Description, @StartDate, @EndDate, @Repeat, @NumberOfRepeats, @DayTask, @NightTask, @WorkType, @LocationID)";

                cmd.Parameters.AddWithValue("@TaskID", taskDTO.Id);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO [Task] (Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, LocationID) 
                                    VALUES (@Name, @Description, @StartDate, @EndDate, @Repeat, @NumberOfRepeats, @DayTask, @NightTask, @WorkType, @LocationID)";
            }

            cmd.Parameters.AddWithValue("@Name", taskDTO.Name);
            cmd.Parameters.AddWithValue("@Description", taskDTO.Description);
            cmd.Parameters.AddWithValue("@StartDate", taskDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            cmd.Parameters.AddWithValue("@EndDate", taskDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            cmd.Parameters.AddWithValue("@Repeat", taskDTO.RepeatType);
            cmd.Parameters.AddWithValue("@NumberOfRepeats", taskDTO.NumberOfRepeats);
            cmd.Parameters.AddWithValue("@DayTask", taskDTO.DayTask);
            cmd.Parameters.AddWithValue("@NightTask", taskDTO.NightTask);
            cmd.Parameters.AddWithValue("@WorkType", taskDTO.WorkType);
            cmd.Parameters.AddWithValue("@LocationID", taskDTO.LocationID);
            cmd.Parameters.AddWithValue("@EmployeeID", taskDTO.EmployeeID);
            cmd.Parameters.AddWithValue("@ScheduleTask", taskDTO.ScheduleTask);

            try
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                return new Result { Success = true, Message = "Task inserted successfully." };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Message = $"SQL Exception: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Message = $"Exception: {ex.Message}" };
            }
        }

        public List<TaskDTO> LoadTasks()
        {
            List<TaskDTO> taskDTOs = new();

            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"SELECT TaskID, Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, LocationID
                            FROM Task WHERE Active = 1 AND ScheduleTask = 0";

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    TaskDTO task = new();
                    task.Id = reader.GetInt32("TaskID");
                    task.Name = reader.GetString("Name");
                    task.Description = reader.GetString("Description");
                    task.StartDate = reader.GetDateTime("StartDate");
                    task.EndDate = reader.GetDateTime("EndDate");
                    task.RepeatType = reader.GetString("RepeatType");
                    task.NumberOfRepeats = reader.GetInt32("NumberOfRepeats");
                    task.DayTask = reader.GetBoolean("DayTask");
                    task.NightTask = reader.GetBoolean("NightTask");
                    task.WorkType = reader.GetString("WorkType");
                    task.LocationID = reader.GetInt32("LocationID");

                    taskDTOs.Add(task);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Something went wrong with the Database", ex);
            }
            return taskDTOs;
        }

        public Result UpdateTask(TaskDTO taskDTO)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Task SET Name = @Name, Description = @Description, StartDate = @StartDate, EndDate = @EndDate, RepeatType = @RepeatType, NumberOfRepeats = @NumberOfRepeats, DayTask = @DayTask, NightTask = @NightTask, WorkType = @WorkType, LocationID = @LocationID 
                                    WHERE TaskID = @TaskID";

            cmd.Parameters.AddWithValue("@TaskID", taskDTO.Id);
            cmd.Parameters.AddWithValue("@Name", taskDTO.Name);
            cmd.Parameters.AddWithValue("@Description", taskDTO.Description);
            cmd.Parameters.AddWithValue("@StartDate", taskDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            cmd.Parameters.AddWithValue("@EndDate", taskDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            cmd.Parameters.AddWithValue("@RepeatType", taskDTO.RepeatType);
            cmd.Parameters.AddWithValue("@NumberOfRepeats", taskDTO.NumberOfRepeats);
            cmd.Parameters.AddWithValue("@DayTask", taskDTO.DayTask);
            cmd.Parameters.AddWithValue("@NightTask", taskDTO.NightTask);
            cmd.Parameters.AddWithValue("@WorkType", taskDTO.WorkType);
            cmd.Parameters.AddWithValue("@LocationID", taskDTO.LocationID);

            try
            {
                conn.Open();
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return new Result { Success = true, Message = "Task updated successfully." };
                }
                return new Result { Success = false, Message = "No columns altered." };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Message = $"SQL Exception: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Message = $"Exception: {ex.Message}" };
            }
        }

        public Result DeleteTask(TaskDTO taskDTO)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Task SET Active = 0 
                                    WHERE TaskID = @TaskID";

            cmd.Parameters.AddWithValue("@TaskID", taskDTO.Id);
            try
            {
                conn.Open();
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return new Result { Success = true, Message = "Task deleted successfully." };
                }
                return new Result { Success = false, Message = "No columns altered." };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Message = $"SQL Exception: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Message = $"Exception: {ex.Message}" };
            }
        }
        
    }
}




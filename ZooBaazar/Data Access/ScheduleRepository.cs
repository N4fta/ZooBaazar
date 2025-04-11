using Data_Access.DTO_s;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class ScheduleRepository : DatabaseConnection
    {
        public Result InsertTasks(List<TaskDTO> taskDTOs, List<TaskDTO> shiftDTOs)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // DRemoves old schedule tasks (to improve performance)
            cmd.CommandText = """
                SET XACT_ABORT ON
                BEGIN
                DELETE FROM Task WHERE ScheduleTask=1
                """;

            int count = 0;
            for (int i = 0; i < taskDTOs.Count; i++)
            {
                TaskDTO? taskDTO = taskDTOs[i];
                cmd.CommandText += @$" INSERT INTO [Task] (Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, EmployeeID, LocationID, ScheduleTask) 
                                    VALUES (@Name{count}, @Description{count}, @StartDate{count}, @EndDate{count}, @Repeat{count}, @NumberOfRepeats{count}, @DayTask{count}, @NightTask{count}, @WorkType{count}, @EmployeeID{count}, @LocationID{count}, 1) ";

                cmd.Parameters.AddWithValue($"@Name{count}", taskDTO.Name);
                cmd.Parameters.AddWithValue($"@Description{count}", taskDTO.Description);
                cmd.Parameters.AddWithValue($"@StartDate{count}", taskDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cmd.Parameters.AddWithValue($"@EndDate{count}", taskDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cmd.Parameters.AddWithValue($"@Repeat{count}", taskDTO.RepeatType);
                cmd.Parameters.AddWithValue($"@NumberOfRepeats{count}", taskDTO.NumberOfRepeats);
                cmd.Parameters.AddWithValue($"@DayTask{count}", taskDTO.DayTask);
                cmd.Parameters.AddWithValue($"@NightTask{count}", taskDTO.NightTask);
                cmd.Parameters.AddWithValue($"@WorkType{count}", taskDTO.WorkType);
                cmd.Parameters.AddWithValue($"@LocationID{count}", taskDTO.LocationID);
                cmd.Parameters.AddWithValue($"@EmployeeID{count}", taskDTO.EmployeeID);
                cmd.Parameters.AddWithValue($"@ScheduleTask{count}", taskDTO.ScheduleTask);
                count++;
            }
            // Shifts
            for (int i = 0; i < shiftDTOs.Count; i++)
            {

                TaskDTO? shiftDTO = shiftDTOs[i];
                cmd.CommandText += @$" INSERT INTO [Task] (Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, EmployeeID, LocationID, RepresentsShift, ScheduleTask) 
                                    VALUES (@Name{count}, @Description{count}, @StartDate{count}, @EndDate{count}, @Repeat{count}, @NumberOfRepeats{count}, @DayTask{count}, @NightTask{count}, @WorkType{count}, @EmployeeID{count}, @LocationID{count}, 1, 1) ";

                cmd.Parameters.AddWithValue($"@Name{count}", shiftDTO.Name);
                cmd.Parameters.AddWithValue($"@Description{count}", shiftDTO.Description);
                cmd.Parameters.AddWithValue($"@StartDate{count}", shiftDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cmd.Parameters.AddWithValue($"@EndDate{count}", shiftDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cmd.Parameters.AddWithValue($"@Repeat{count}", shiftDTO.RepeatType);
                cmd.Parameters.AddWithValue($"@NumberOfRepeats{count}", shiftDTO.NumberOfRepeats);
                cmd.Parameters.AddWithValue($"@DayTask{count}", shiftDTO.DayTask);
                cmd.Parameters.AddWithValue($"@NightTask{count}", shiftDTO.NightTask);
                cmd.Parameters.AddWithValue($"@WorkType{count}", shiftDTO.WorkType);
                cmd.Parameters.AddWithValue($"@LocationID{count}", shiftDTO.LocationID);
                cmd.Parameters.AddWithValue($"@EmployeeID{count}", shiftDTO.EmployeeID);
                cmd.Parameters.AddWithValue($"@ScheduleTask{count}", shiftDTO.ScheduleTask);
                count++;
            }
            cmd.CommandText += " END ";

            try
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                return new Result { Success = true, Message = "Schedule Tasks inserted successfully." };
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

        public List<TaskDTO> GetTasksForUser(string userName)
        {
            List<TaskDTO> taskDTOs = new();

            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"
                SELECT T.TaskID, T.Name, T.Description, T.StartDate, T.EndDate, T.RepeatType, T.NumberOfRepeats, T.DayTask, T.NightTask, T.WorkType, T.EmployeeID, T.LocationID, T.ScheduleTask, T.RepresentsShift
                FROM Task T
                INNER JOIN Employee E ON T.EmployeeID = E.EmployeeID
                WHERE E.UserName = @UserName AND T.Active = 1 AND ScheduleTask=1";

            cmd.Parameters.AddWithValue("@UserName", userName);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaskDTO task = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TaskID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                        EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                        RepeatType = reader.GetString(reader.GetOrdinal("RepeatType")),
                        NumberOfRepeats = reader.GetInt32(reader.GetOrdinal("NumberOfRepeats")),
                        DayTask = reader.GetBoolean(reader.GetOrdinal("DayTask")),
                        NightTask = reader.GetBoolean(reader.GetOrdinal("NightTask")),
                        WorkType = reader.GetString(reader.GetOrdinal("WorkType")),
                        //EmployeeID = reader.GetInt32("EmployeeID"),
                        LocationID = reader.GetInt32(reader.GetOrdinal("LocationID")),
                        ScheduleTask = reader.GetBoolean("ScheduleTask"),
                        RepresentsShift = reader.GetBoolean("RepresentsShift")
                    };

                    taskDTOs.Add(task);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Something went wrong with the Database", ex);
            }

            return taskDTOs;
        }


        //public Result InsertSchedule(ScheduleDTO scheduleDTO)
        //{
        //    using SqlConnection conn = GetSqlConnection();
        //    using SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = """
        //        BEGIN

        //        DECLARE @ID_Table TABLE (
        //        	TaskID int,
        //        	ShiftID int,
        //        	DayID int,
        //        	WeekID int,
        //        	ScheduleID int
        //        )

        //        /* Deactivate old Schedule and new Insert Schedule */
        //        update Schedule
        //        set ScheduleID = 0
        //        Insert into Schedule (DateCreated, Active)
        //        	output INSERTED.ScheduleID
        //        		INTO @ID_Table (ScheduleID)
        //        Values  (@Schedule_DateCreated,1)

        //        """;
        //    var x ="""
        //        foreach () { }
        //        /* Insert Day*/
        //        insert into Day (DayOfWeek)
        //        	OUTPUT INSERTED.DayID
        //        		INTO @ID_Table (DayID)
        //        values (@Day_DayOfWeek)

        //        /* Insert Task*/
        //        INSERT INTO [Task] (Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, LocationID, ScheduleTask) 
        //        	OUTPUT INSERTED.TaskID
        //        		INTO @ID_Table (TaskID)
        //        VALUES (@Name, @Description, @StartDate, @EndDate, @Repeat, @NumberOfRepeats, @DayTask, @NightTask, @WorkType, @LocationID, 1)

        //        insert into Task_Day (TaskID, DayID)
        //        Values ((Select TaskID FROM @ID_Table Where TaskID IS NOT NULL), (Select DayID FROM @ID_Table Where DayID IS NOT NULL))

        //        DELETE FROM @ID_Table WHERE TaskID IS NOT NULL
        //        /* Repeat Insert Task for All Tasks */

        //        insert into Week (DayID, StartDate)
        //        	OUTPUT INSERTED.WeekID
        //        			INTO @ID_Table (WeekID)
        //        values ((Select DayID FROM @ID_Table Where DayID IS NOT NULL),@Week_StartDate)

        //        DELETE FROM @ID_Table WHERE DayID IS NOT NULL

        //        insert into WeekSchedule (WeekID, ScheduleID)
        //        values ((Select WeekID FROM @ID_Table Where WeekID IS NOT NULL), (Select ScheduleID FROM @ID_Table Where ScheduleID IS NOT NULL))

        //        DELETE FROM @ID_Table WHERE WeekID IS NOT NULL
        //        /* Repeat Insert Day for All Days */


        //        END
        //        """;

        //    //cmd.Parameters.AddWithValue("@Name", taskDTO.Name);
        //    //cmd.Parameters.AddWithValue("@Description", taskDTO.Description);
        //    //cmd.Parameters.AddWithValue("@StartDate", taskDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        //    //cmd.Parameters.AddWithValue("@EndDate", taskDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        //    //cmd.Parameters.AddWithValue("@Repeat", taskDTO.RepeatType);
        //    //cmd.Parameters.AddWithValue("@NumberOfRepeats", taskDTO.NumberOfRepeats);
        //    //cmd.Parameters.AddWithValue("@DayTask", taskDTO.DayTask);
        //    //cmd.Parameters.AddWithValue("@NightTask", taskDTO.NightTask);
        //    //cmd.Parameters.AddWithValue("@WorkType", taskDTO.WorkType);
        //    //cmd.Parameters.AddWithValue("@LocationID", taskDTO.LocationID);
        //    //cmd.Parameters.AddWithValue("@EmployeeID", taskDTO.EmployeeID);
        //    //cmd.Parameters.AddWithValue("@ScheduleTask", taskDTO.ScheduleTask);

        //    try
        //    {
        //        conn.Open();
        //        var result = cmd.ExecuteScalar();
        //        return new Result { Success = true, Message = "Task inserted successfully." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new Result { Success = false, Message = $"SQL Exception: {ex.Message}" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result { Success = false, Message = $"Exception: {ex.Message}" };
        //    }
        //}

        //public ScheduleDTO LoadActiveSchedule(int employeeID = 0)
        //{
        //    List<TaskDTO> taskDTOs = new();

        //    using SqlConnection conn = GetSqlConnection();
        //    using SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;
        //    if (employeeID != 0)
        //    {
        //        cmd.CommandText = @"SELECT T.TaskID, T.Name, T.Description, T.StartDate, T.EndDate, T.RepeatType, T.NumberOfRepeats, T.WorkType, T.LocationID
        //                            FROM 
        //                             Task T
        //                            INNER JOIN
        //                             EmployeeTask ET ON T.TaskID = ET.TaskID 
        //                            WHERE 
        //                             EmployeeID = @EmployeeID AND T.Active = 1 AND ScheduleTask = 0";

        //        //inner join with employee task
        //        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
        //    }
        //    else
        //    {
        //        cmd.CommandText = @"SELECT TaskID, Name, Description, StartDate, EndDate, RepeatType, NumberOfRepeats, DayTask, NightTask, WorkType, LocationID
        //                    FROM Task WHERE Active = 1 AND ScheduleTask = 0";
        //    }

        //    try
        //    {
        //        conn.Open();
        //        using SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            // Review Column Example:
        //            // ID | Name	| Description                 | StartDate  | EndDate	| Repeat | NumberOfRepeats | DayTask | NightTask | WorkType | LocationID 
        //            // 1  | Test    | This feed was extremely meh | 11-01-2001 | 11-01-2021 | Daily  | 2               | 1       | 0         | Vet      | 1          
        //            TaskDTO task = new();
        //            task.Id = reader.GetInt32("TaskID");
        //            task.Name = reader.GetString("Name");
        //            task.Description = reader.GetString("Description");
        //            task.StartDate = reader.GetDateTime("StartDate");
        //            task.EndDate = reader.GetDateTime("EndDate");
        //            task.RepeatType = reader.GetString("RepeatType");
        //            task.NumberOfRepeats = reader.GetInt32("NumberOfRepeats");
        //            task.DayTask = reader.GetBoolean("DayTask");
        //            task.NightTask = reader.GetBoolean("NightTask");
        //            task.WorkType = reader.GetString("WorkType");
        //            task.LocationID = reader.GetInt32("LocationID");

        //            if (employeeID != 0) task.ScheduleTask = reader.GetBoolean("ScheduleTask");

        //            taskDTOs.Add(task);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception("Something went wrong with the Database", ex);
        //    }
        //    return null;
        //}

        //WebApp
    }
}

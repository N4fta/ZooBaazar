using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using static Azure.Core.HttpHeader;

namespace Data_Access
{
    public class EmployeeRepository : DatabaseConnection, IEmployeeRepository
    {
        public Result InsertEmployee(EmployeeDTO employeeDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                try
                {
                    conn.Open();

                    // Insert Employee

                    cmd.CommandText = @"INSERT INTO [Employee]  
							(Username, Password, Name, Email, PhoneNumber, Address, Notes, Birthday) 
							VALUES (@Username, @Password, @Name, @Email, @PhoneNumber, @Address, @Notes, @Birthday)
							SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@Username", employeeDTO.Username);
                    cmd.Parameters.AddWithValue("@Password", employeeDTO.Password);
                    cmd.Parameters.AddWithValue("@Name", employeeDTO.Name);
                    cmd.Parameters.AddWithValue("@Email", employeeDTO.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employeeDTO.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employeeDTO.Address);
                    cmd.Parameters.AddWithValue("@Notes", employeeDTO.Notes);
                    // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                    string BirthdayString = employeeDTO.Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters.AddWithValue("@Birthday", BirthdayString);

                    var result = cmd.ExecuteScalar();
                    int newEmployeeId = GetEmployeeByEmail(employeeDTO);
                    if (int.TryParse(result?.ToString(), out newEmployeeId))
                    {
                        employeeDTO.EmployeeID = newEmployeeId;

                        // Insert Contract
                        cmd.CommandText = @"INSERT INTO [Contract] 
                            (EmployeeID, StartDate, EndDate, Salary, Role, BSN, BankNumber, HoursPerWeek, ChangeShifts, Overtime, DayShifts, NightShifts, PaidLeaveDays, UnPaidLeaveDays, Notes, ContractType)
                            VALUES (@EmployeeID, @StartDate, @EndDate, @Salary, @Role, @BSN, @BankNumber, @HoursPerWeek, @ChangeShifts, @Overtime, @DayShifts, @NightShifts, @PaidLeaveDays, @UnPaidLeaveDays, @ContractNotes, @ContractType)
                            SELECT SCOPE_IDENTITY()";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmployeeID", newEmployeeId);
                        cmd.Parameters.AddWithValue("@StartDate", employeeDTO.Contracts.First().startDate);
                        cmd.Parameters.AddWithValue("@EndDate", employeeDTO.Contracts.First().endDate);
                        cmd.Parameters.AddWithValue("@Salary", employeeDTO.Contracts.First().salary);
                        cmd.Parameters.AddWithValue("@Role", employeeDTO.Contracts.First().role);
                        cmd.Parameters.AddWithValue("@BSN", employeeDTO.Contracts.First().bsn);
                        cmd.Parameters.AddWithValue("@BankNumber", employeeDTO.Contracts.First().bankNumber);
                        cmd.Parameters.AddWithValue("@HoursPerWeek", employeeDTO.Contracts.First().hoursPerWeek);
                        cmd.Parameters.AddWithValue("@ChangeShifts", employeeDTO.Contracts.First().changeShifts);
                        cmd.Parameters.AddWithValue("@Overtime", employeeDTO.Contracts.First().overtime);
                        cmd.Parameters.AddWithValue("@DayShifts", employeeDTO.Contracts.First().dayShifts);
                        cmd.Parameters.AddWithValue("@NightShifts", employeeDTO.Contracts.First().nightShifts);
                        cmd.Parameters.AddWithValue("@PaidLeaveDays", employeeDTO.Contracts.First().paidLeaveDays);
                        cmd.Parameters.AddWithValue("@UnPaidLeaveDays", employeeDTO.Contracts.First().unpaidLeaveDays);
                        cmd.Parameters.AddWithValue("@ContractNotes", employeeDTO.Contracts.First().Notes);
                        cmd.Parameters.AddWithValue("@ContractType", employeeDTO.Contracts.First().contractType);

                        result = cmd.ExecuteScalar();
                        int newContractId;
                        if (int.TryParse(result?.ToString(), out newContractId))
                        {
                            // Insert ContractWeekDay
                            foreach (var day in employeeDTO.Contracts.First().workDays)
                            {
                                using (SqlCommand cmdWeekDay = new SqlCommand())
                                {
                                    cmdWeekDay.Connection = conn;

                                    // Get the WeekDayID
                                    cmdWeekDay.CommandText = @"SELECT TOP (1) WeekDayID FROM [WeekDay] WHERE Days = @WorkDays";
                                    cmdWeekDay.Parameters.AddWithValue("@WorkDays", day.ToString());

                                    var weekDayID = cmdWeekDay.ExecuteScalar();

                                    // Check if weekDayID is null or invalid
                                    if (weekDayID != null)
                                    {
                                        cmdWeekDay.Parameters.Clear();
                                        cmdWeekDay.CommandText = @"INSERT INTO [ContractWeekDay] (ContractID, WeekDayID)
                                       VALUES (@ContractID, @WeekDayID)";
                                        cmdWeekDay.Parameters.AddWithValue("@ContractID", newContractId);
                                        cmdWeekDay.Parameters.AddWithValue("@WeekDayID", weekDayID);

                                        cmdWeekDay.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        // Handle the case when weekDayID is not found
                                        throw new Exception("WeekDayID not found for day: " + day.ToString());
                                    }
                                }
                            }

                            return new Result { Success = true, Message = "Employee and contract inserted successfully." };
                        }
                    }

                    return new Result { Success = false, Message = "Failed to retrieve new employee ID or contract ID." };
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

        public int GetEmployeeByEmail(EmployeeDTO employeeDTO)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT EmployeeID FROM [Employee] WHERE Name = @Name AND Email = @Email AND Active = 1";

            cmd.Parameters.AddWithValue("@Name", employeeDTO.Name);
            cmd.Parameters.AddWithValue("@Email", employeeDTO.Email);

            try
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                int newEmployeeID;
                if (int.TryParse(result?.ToString(), out newEmployeeID))
                {
                    return newEmployeeID;
                }
                return 0;
            }
            catch(SqlException ex)
            {
                throw new Exception("Problem with the database", ex);
            }
        }

        public List<EmployeeDTO> GetRecentEmployee(int count)
        {
            List<EmployeeDTO> recentEmployees = new List<EmployeeDTO>();

            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP (@Count) E.EmployeeID, E.Username, E.Password, E.Name, E.Email, E.PhoneNumber, E.Address, E.Notes, E.Birthday, " +
                              " C.ContractID, C.StartDate, C.EndDate, C.Salary, C.Role, C.BSN, C.BankNumber, C.HoursPerWeek, C.ChangeShifts, C.Overtime, C.DayShifts, C.NightShifts, C.PaidLeaveDays, C.UnPaidLeaveDays, C.Notes AS ContractNotes, C.ContractType, " +
                              " CWD.WeekDayID, " +
                              " WD.Days " +
                              " FROM Employee E " +
                              " LEFT JOIN Contract C ON E.EmployeeID = C.EmployeeID " +
                              " LEFT JOIN ContractWeekDay CWD ON C.ContractID = CWD.ContractID " +
                              " LEFT JOIN WeekDay WD ON CWD.WeekDayID = WD.WeekDayID " +
                              " WHERE E.Active = 1 ORDER BY EmployeeID DESC";

            cmd.Parameters.AddWithValue("@Count", count);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                EmployeeDTO? currentEmployee = null;
                Dictionary<int, ContractDTO> contractMap = new();

                while (reader.Read())
                {
                    if (reader.IsDBNull(0))// If Employee Id is null there are no employees, return an empty list
                    {
                        return new();
                    }
                    // Identifying Ids for easy of legibility
                    int employeeId = reader.GetInt32("EmployeeID");
                    int contractId = 0;
                    if (!reader.IsDBNull("ContractID")) contractId = reader.GetInt32("ContractID");// If Contract is null dont attempt to read, this could crash the program

                    // If Current Employee is null (first time) or has a different Id than before
                    // Save Current employee and create a new one
                    if (currentEmployee == null || currentEmployee.EmployeeID != employeeId)
                    {
                        // If this is not the first employee and therefore we have to save Current Employee
                        if (currentEmployee != null)
                        {
                            currentEmployee.Contracts = contractMap.Values.ToList();
                            recentEmployees.Add(currentEmployee);
                            contractMap.Clear();
                        }

                        currentEmployee = new EmployeeDTO();
                        currentEmployee.EmployeeID = reader.GetInt32("EmployeeID");
                        currentEmployee.Username = reader.GetString("Username");
                        currentEmployee.Password = reader.GetString("Password");
                        currentEmployee.Name = reader.GetString("Name");
                        currentEmployee.Email = reader.GetString("Email");
                        currentEmployee.PhoneNumber = reader.GetString("PhoneNumber");
                        currentEmployee.Address = reader.GetString("Address");
                        currentEmployee.Notes = reader.GetString("Notes");
                        currentEmployee.Birthday = reader.GetDateTime("Birthday");

                    }

                    // If the Contract ID is not in contract Map and its not null, create a new one 
                    if (!contractMap.TryGetValue(contractId, out var currentContract) && contractId != 0)
                    {
                        // Creates contract
                        currentContract = new ContractDTO();
                        currentContract.ContractID = reader.GetInt32("ContractID");
                        currentContract.EmployeeID = reader.GetInt32("EmployeeID");
                        currentContract.startDate = reader.GetDateTime("StartDate");
                        currentContract.endDate = reader.GetDateTime("EndDate");
                        currentContract.salary = reader.GetDouble("Salary");
                        currentContract.role = reader.GetString("Role");
                        currentContract.bsn = reader.GetInt32("BSN");
                        currentContract.bankNumber = reader.GetInt32("BankNumber");
                        currentContract.hoursPerWeek = reader.GetInt32("HoursPerWeek");
                        currentContract.changeShifts = reader.GetBoolean("ChangeShifts");
                        currentContract.overtime = reader.GetBoolean("Overtime");
                        currentContract.dayShifts = reader.GetBoolean("DayShifts");
                        currentContract.nightShifts = reader.GetBoolean("NightShifts");
                        currentContract.workDays = new List<DayOfWeek>();
                        currentContract.paidLeaveDays = reader.GetInt32("PaidLeaveDays");
                        currentContract.unpaidLeaveDays = reader.GetInt32("UnPaidLeaveDays");
                        currentContract.Notes = reader.GetString("ContractNotes");
                        currentContract.contractType = reader.GetString("ContractType");

                        // Adds contract to list of employee contracts
                        contractMap.Add(contractId, currentContract);
                    }

                    // Adds Weekdays if they are not null
                    if (!reader.IsDBNull("Days") && contractId != 0)
                    {
                        DayOfWeek workday = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), reader.GetString("Days"));
                        contractMap[contractId].workDays.Add(workday);
                    }
                }

                // Save contracts to employees, useful for the last employee which doesn't restart the while loop
                if (currentEmployee != null)
                {
                    currentEmployee.Contracts = contractMap.Values.ToList();
                    recentEmployees.Add(currentEmployee);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Problem with the database", ex);
            }

            return recentEmployees;
        }

        public Result UpdateEmployee(EmployeeDTO employeeDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @" UPDATE [Employee] SET Username = @Username, Password = @Password, Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, Notes = @Notes, Birthday = @Birthday
                                            WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.AddWithValue("@EmployeeID", employeeDTO.EmployeeID);
                cmd.Parameters.AddWithValue("@Username", employeeDTO.Username);
                cmd.Parameters.AddWithValue("@Password", employeeDTO.Password);
                cmd.Parameters.AddWithValue("@Name", employeeDTO.Name);
                cmd.Parameters.AddWithValue("@Email", employeeDTO.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employeeDTO.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employeeDTO.Address);
                cmd.Parameters.AddWithValue("@Notes", employeeDTO.Notes);

                string BirthdayString = employeeDTO.Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@Birthday", BirthdayString);


                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "Employee updated successfully." };
                    }

                    return new Result { Success = false, Message = "Failed to retrieve employee ID." };
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


        public Result DeleteEmployee(EmployeeDTO employeeDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Employee] SET Active = 0 
                                    WHERE EmployeeID = @EmployeeID AND Username = @Username AND Email = @Email ";

                cmd.Parameters.AddWithValue("@EmployeeID", employeeDTO.EmployeeID);
                cmd.Parameters.AddWithValue("@Username", employeeDTO.Username);
                cmd.Parameters.AddWithValue("@Email", employeeDTO.Email);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newEmployeeId;
                    if (int.TryParse(result?.ToString(), out newEmployeeId))
                    {
                        return new Result { Success = true, Message = "Employee deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve employee ID." };
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

        ///WebApp

        public EmployeeDTO GetUser(string username)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM Employee WHERE Username = @Username", conn);
                    command.Parameters.AddWithValue("@Username", username);

                    using SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            EmployeeDTO employeeDTO = new EmployeeDTO();
                            employeeDTO.EmployeeID = reader.GetInt32("EmployeeID");
                            employeeDTO.Username = reader.GetString("Username");
                            employeeDTO.Password = reader.GetString("Password");
                            employeeDTO.Name = reader.GetString("Name");
                            employeeDTO.Email = reader.GetString("Email");
                            employeeDTO.PhoneNumber = reader.GetString("PhoneNumber");
                            employeeDTO.Address = reader.GetString("Address");
                            employeeDTO.Notes = reader.GetString("Notes");
                            employeeDTO.Birthday = reader.GetDateTime("Birthday");

                            return employeeDTO;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Problem with the database", ex);
                }
            }
        }


        public bool CheckPassword(EmployeeDTO employeeDTO, string oldPassword)
        {
            if (employeeDTO == null)
            {
                return false;
            }
            return employeeDTO.Password == oldPassword;
        }

        public Result ChangePassword(EmployeeDTO employeeDTO, string oldPassword, string newPassword)
        {
            if (!CheckPassword(employeeDTO, oldPassword))
            {
                return new Result { Success = false, Message = "Invalid old password" };
            }

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Employee SET Password = @NewPassword WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", employeeDTO.EmployeeID);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "Password changed successfully" };
                    }

                    return new Result { Success = false, Message = "Failed to change password" };
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
}

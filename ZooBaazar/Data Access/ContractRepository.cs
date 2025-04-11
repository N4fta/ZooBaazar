using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Access
{
    public class ContractRepository : DatabaseConnection, IContractRepository
    {
        public Result InsertContract(ContractDTO contractDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (contractDTO.ContractID != 0)
                {
                    cmd.CommandText = @"SET IDENTITY_INSERT [Contract] ON
                                    INSERT INTO [Contract] (ContractID, EmployeeID, StartDate, EndDate, Salary, Role, BSN, BankNumber, HoursPerWeek, ChangeShifts, Overtime, PaidLeaveDays, UnPaidLeaveDays, Notes, ContractType) 
                                    VALUES (@ContractID, @EmployeeID, @StartDate, @EndDate, @Salary, @Role, @BSN, @BankNumber, @HoursPerWeek, @ChangeShifts, @Overtime, @PaidLeaveDays, @UnPaidLeaveDays, @Notes, @ContractType)
                                    SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@ContractID", contractDTO.ContractID);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO [Contract] (EmployeeID, StartDate, EndDate, Salary, Role, BSN, BankNumber, HoursPerWeek, ChangeShifts, Overtime, PaidLeaveDays, UnPaidLeaveDays, Notes, ContractType) 
                                    VALUES (@EmployeeID, @StartDate, @EndDate, @Salary, @Role, @BSN, @BankNumber, @HoursPerWeek, @ChangeShifts, @Overtime, @PaidLeaveDays, @UnPaidLeaveDays, @Notes, @ContractType)
                                     INSERT INTO [ContractWeekDay]
				                            (ContractID, WeekDayID)
	                                VALUES ((SELECT TOP (1) WeekDayID from [WeekDay] where Days = @WorkDays))
	                                SELECT SCOPE_IDENTITY()";
                }


                cmd.Parameters.AddWithValue("@EmployeeID", contractDTO.EmployeeID);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string startString = contractDTO.startDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@StartDate", startString);

                string endString = contractDTO.endDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@EndDate", endString);

                cmd.Parameters.AddWithValue("@Salary", contractDTO.salary);

                //This converts the employee enum to a string to pass it to the databse
                string contractString = contractDTO.role.ToString();
                cmd.Parameters.AddWithValue("@Role", contractString);

                cmd.Parameters.AddWithValue("@BSN", contractDTO.bsn);

                cmd.Parameters.AddWithValue("@BankNumber", contractDTO.bankNumber);

                cmd.Parameters.AddWithValue("@HoursPerWeek", contractDTO.hoursPerWeek);

                cmd.Parameters.AddWithValue("@ChangeShifts", contractDTO.changeShifts);

                cmd.Parameters.AddWithValue("@Overtime", contractDTO.overtime);

                cmd.Parameters.AddWithValue("@WorkDays", contractDTO.workDays[0].ToString());

                cmd.Parameters.AddWithValue("@PaidLeaveDays", contractDTO.paidLeaveDays);

                cmd.Parameters.AddWithValue("@UnPaidLeaveDays", contractDTO.unpaidLeaveDays);

                cmd.Parameters.AddWithValue("@Notes", contractDTO.Notes);

                cmd.Parameters.AddWithValue("@ContractType", contractDTO.contractType);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newContractID;
                    if (int.TryParse(result?.ToString(), out newContractID))
                    {
                        contractDTO.ContractID = newContractID;
                        return new Result { Success = true, Message = "Contract inserted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve new contract ID." };
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

        public List<ContractDTO> GetEmployeeContract(int employeeID)
        {
            Dictionary<int, ContractDTO> employeeContracts = new();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP (1000) C.ContractID, C.EmployeeID, C.StartDate, C.EndDate, C.Salary, C.Role, C.BSN, C.BankNumber, C.HoursPerWeek, C.ChangeShifts, C.Overtime, C.DayShifts, C.NightShifts, C.PaidLeaveDays, C.UnPaidLeaveDays, C.Notes, C.ContractType, " +
                                  " CWD.WeekDayID, " +
                                  " WD.Days " +
                                  " FROM Contract C " +
                                  " LEFT JOIN ContractWeekDay CWD ON C.ContractID = CWD.ContractID " +
                                  " LEFT JOIN WeekDay WD ON CWD.WeekDayID = WD.WeekDayID " +
                                  " WHERE C.EmployeeID = @EmployeeID AND C.Active = 1 ORDER BY ContractID DESC ";

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();


                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull("ContractID"))// If Contract Id is null there are no contracts, return an empty list
                    {
                        return new();
                    }
                    int contractId = reader.GetInt32("ContractID");

                    // If there are no contracts with this Id in the list, save the current contract and create a new one
                    if (!employeeContracts.TryGetValue(contractId, out var currentContract))
                     {
                        // Create contract DTO
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
                        currentContract.Notes = reader.GetString("Notes");
                        currentContract.contractType = reader.GetString("ContractType");

                        // Add it to list
                        employeeContracts.Add(contractId, currentContract);
                    }


                    // Adds Weekdays if they are not null
                    if (!reader.IsDBNull("Days"))
                    {
                        DayOfWeek workday = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), reader.GetString("Days"));
                        employeeContracts[contractId].workDays.Add(workday);
                    }
                }
            }
            return employeeContracts.Values.ToList();
        }

        public Result UpdateEmployeeContract(ContractDTO contractDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Contract] SET StartDate = @StartDate, EndDate = @EndDate, Salary = @Salary, Role = @Role, BSN = @BSN, BankNumber = @BankNumber, HoursPerWeek = @HoursPerWeek, ChangeShifts = @ChangeShifts, Overtime = @Overtime, DayShifts = @DayShifts, NightShifts = @NightShifts, PaidLeaveDays = @PaidLeaveDays, UnPaidLeaveDays = @UnPaidLeaveDays, Notes = @ContractNotes, ContractType = @ContractType 
                                            WHERE ContractID = @ContractID;

                                    DELETE FROM [ContractWeekDay] WHERE ContractID = @ContractID ";

                cmd.Parameters.AddWithValue("@ContractID", contractDTO.ContractID);
                cmd.Parameters.AddWithValue("@StartDate", contractDTO.startDate);
                cmd.Parameters.AddWithValue("@EndDate", contractDTO.endDate);
                cmd.Parameters.AddWithValue("@Salary", contractDTO.salary);
                cmd.Parameters.AddWithValue("@Role", contractDTO.role);
                cmd.Parameters.AddWithValue("@BSN", contractDTO.bsn);
                cmd.Parameters.AddWithValue("@BankNumber", contractDTO.bankNumber);
                cmd.Parameters.AddWithValue("@HoursPerWeek", contractDTO.hoursPerWeek);
                cmd.Parameters.AddWithValue("@ChangeShifts", contractDTO.changeShifts);
                cmd.Parameters.AddWithValue("@Overtime", contractDTO.overtime);
                cmd.Parameters.AddWithValue("@DayShifts", contractDTO.dayShifts);
                cmd.Parameters.AddWithValue("@NightShifts", contractDTO.nightShifts);
                cmd.Parameters.AddWithValue("@PaidLeaveDays", contractDTO.paidLeaveDays);
                cmd.Parameters.AddWithValue("@UnPaidLeaveDays", contractDTO.unpaidLeaveDays);
                cmd.Parameters.AddWithValue("@ContractNotes", contractDTO.Notes);
                cmd.Parameters.AddWithValue("@ContractType", contractDTO.contractType);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Update ContractWeekDay
                        foreach (var day in contractDTO.workDays)
                        {
                            using (SqlCommand cmdWeekDay = new SqlCommand())
                            {
                                cmdWeekDay.Connection = conn;
                                cmdWeekDay.CommandText = @"
                                    INSERT INTO [ContractWeekDay] (ContractID, WeekDayID)
                                    VALUES (@ContractID, (SELECT WeekDayID FROM [WeekDay] WHERE Days = @Day))";
                                cmdWeekDay.Parameters.AddWithValue("@ContractID", contractDTO.ContractID);
                                cmdWeekDay.Parameters.AddWithValue("@Day", day.ToString());
                                cmdWeekDay.ExecuteNonQuery();
                            }
                        }

                        return new Result { Success = true, Message = "Contract updated successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to update contract" };
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

        public Result DeleteEmployeeContract(ContractDTO contractDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Contract] SET Active = 0 
                                    WHERE EmployeeID = @EmployeeID";


                cmd.Parameters.AddWithValue("@EmployeeID", contractDTO.EmployeeID);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return new Result { Success = true, Message = "Contract deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to delete contract" };
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

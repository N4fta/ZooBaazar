using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class MedicalRecordRepository : DatabaseConnection, IMedicalRecordRepository
    {
        public Result InsertMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (medicalRecordDTO.MedicalRecordID != 0)
                {
                    cmd.CommandText = @"SET IDENTITY_INSERT [MedicalRecord] ON
                                    INSERT INTO [MedicalRecord] (MedicalRecordID, AnimalID, RecordDate, Notes, Allergies, Diseases, Injuries, Operations, Medication, Dangerlevel ) 
                                    VALUES (@MedicalRecordID, @AnimalID, @RecordDate, @Notes, @Allergies, @Diseases, @Injuries, @Operations, @Medication, @Dangerlevel )
                                    SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@MedicalRecordID", medicalRecordDTO.MedicalRecordID);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO [MedicalRecord] (AnimalID, RecordDate, Notes, Allergies, Diseases, Injuries, Operations, Medication, Dangerlevel ) 
                                    VALUES (@AnimalID, @RecordDate, @Notes, @Allergies, @Diseases, @Injuries, @Operations, @Medication, @Dangerlevel )
                                    SELECT SCOPE_IDENTITY()";
                }


                cmd.Parameters.AddWithValue("@AnimalID", medicalRecordDTO.AnimalID);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string RecordString = medicalRecordDTO.RecordDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@RecordDate", RecordString);

                cmd.Parameters.AddWithValue("@Notes", medicalRecordDTO.Notes);

                cmd.Parameters.AddWithValue("@Allergies", medicalRecordDTO.Allergies);

                cmd.Parameters.AddWithValue("@Diseases", medicalRecordDTO.Diseases);

                cmd.Parameters.AddWithValue("@Injuries", medicalRecordDTO.Injuries);

                cmd.Parameters.AddWithValue("@Operations", medicalRecordDTO.Operations);

                cmd.Parameters.AddWithValue("@Medication", medicalRecordDTO.Medication);

                //This converts the employee enum to a string to pass it to the databse
                string dangerString = medicalRecordDTO.Dangerlevel.ToString();
                cmd.Parameters.AddWithValue("@Dangerlevel", dangerString);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newMedicalRecordID;
                    if (int.TryParse(result?.ToString(), out newMedicalRecordID))
                    {
                        medicalRecordDTO.MedicalRecordID = newMedicalRecordID;
                        return new Result { Success = true, Message = "MedicalRecord inserted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve new MedicalRecord ID." };
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

        public List<MedicalRecordDTO> GetAnimalMedicalRecord(int animalID)
        {
            List<MedicalRecordDTO> recentAnimalMedicalRecord = new List<MedicalRecordDTO>();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP (1000) MedicalRecordID, AnimalID, RecordDate, Notes, Allergies, Diseases, Injuries, Operations, Medication, Dangerlevel " +
                                  "FROM [MedicalRecord] WHERE Active = 1 AND MedicalRecordID = @MedicalRecordID ORDER BY MedicalRecordID DESC";

                cmd.Parameters.AddWithValue("@AnimalID", animalID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(
                                reader.GetInt32("MedicalRecordID"),
                                reader.GetInt32("AnimalID"),
                                reader.GetDateTime("RecordDate"),
                                reader.GetString("Notes"),
                                reader.GetString("Allergies"),
                                reader.GetString("Diseases"),
                                reader.GetString("Injuries"),
                                reader.GetString("Operations"),
                                reader.GetString("Medication"),
                                reader.GetString("Dangerlevel")
                            );

                        recentAnimalMedicalRecord.Add(medicalRecordDTO);
                    }
                }
            }
            return recentAnimalMedicalRecord;
        }

        public Result UpdateAnimalMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [MedicalRecord] 
                                    SET AnimalID = @AnimalID,
                                        RecordDate = @RecordDate,
                                        Notes = @Notes,
                                        Allergies = @Allergies,
                                        Diseases = @Diseases,
                                        Injuries = @Injuries,
                                        Operations = @Operations,
                                        Medication = @Medication,
                                        Dangerlevel = @Dangerlevel
                                    WHERE MedicalRecordID = @MedicalRecordID";


                cmd.Parameters.AddWithValue("@MedicalRecordID", medicalRecordDTO.MedicalRecordID);

                cmd.Parameters.AddWithValue("@AnimalID", medicalRecordDTO.AnimalID);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string RecordString = medicalRecordDTO.RecordDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@RecordDate", RecordString);

                cmd.Parameters.AddWithValue("@Notes", medicalRecordDTO.Notes);

                cmd.Parameters.AddWithValue("@Allergies", medicalRecordDTO.Allergies);

                cmd.Parameters.AddWithValue("@Diseases", medicalRecordDTO.Diseases);

                cmd.Parameters.AddWithValue("@Injuries", medicalRecordDTO.Injuries);

                cmd.Parameters.AddWithValue("@Operations", medicalRecordDTO.Operations);

                cmd.Parameters.AddWithValue("@Medication", medicalRecordDTO.Medication);

                //This converts the employee enum to a string to pass it to the databse
                string dangerString = medicalRecordDTO.Dangerlevel.ToString();
                cmd.Parameters.AddWithValue("@Dangerlevel", dangerString);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "MedicalRecord updated successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to update MedicalRecord" };
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

        public Result DeleteAnimalMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [MedicalRecord] SET Active = 0 
                                    WHERE MedicalRecordID = @MedicalRecordID";


                cmd.Parameters.AddWithValue("@MedicalRecordID", medicalRecordDTO.MedicalRecordID);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return new Result { Success = true, Message = "MedicalRecord deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to delete MedicalRecord" };
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

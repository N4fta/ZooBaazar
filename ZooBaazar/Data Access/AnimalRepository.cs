using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Security;
using System.Security.Cryptography;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace Data_Access
{
    public class AnimalRepository : DatabaseConnection, IAnimalRepository
    {
        public Result InsertAnimal(AnimalDTO animalDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                try
                {
                    conn.Open();

                    // Insert Animal
                    cmd.CommandText = @"INSERT INTO [Animal] 
                            (Name, Species, BloodType, Origin, Birthday, EntryZoo, ExitZoo, LocationID) 
                            VALUES (@Name, @Species, @BloodType, @Origin, @Birthday, @EntryZoo, @ExitZoo, @LocationID);
                            SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("@Name", animalDTO.Name);
                    cmd.Parameters.AddWithValue("@Species", animalDTO.Species);
                    string bloodString = animalDTO.BloodType.ToString();
                    cmd.Parameters.AddWithValue("@BloodType", bloodString);
                    cmd.Parameters.AddWithValue("@Origin", animalDTO.Origin);
                    string BirthdayString = animalDTO.Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters.AddWithValue("@Birthday", BirthdayString);
                    string EntryString = animalDTO.EntryZoo.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters.AddWithValue("@EntryZoo", EntryString);
                    string ExitString = animalDTO.ExitZoo.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters.AddWithValue("@ExitZoo", ExitString);
                    cmd.Parameters.AddWithValue("@LocationID", animalDTO.LocationID.HasValue ? (object)animalDTO.LocationID.Value : DBNull.Value);

                    var result = cmd.ExecuteScalar();
                    int newAnimalID = Convert.ToInt32(result);
                    animalDTO.AnimalID = newAnimalID;

                    // Insert FeedingPlan
                    cmd.CommandText = @"INSERT INTO [FeedingPlan] 
                                (AnimalID, FavoriteFood, DislikedFood, Diet, Notes, CaloriesIntake, Weight, IdealWeight) 
                                VALUES (@AnimalID, @FavoriteFood, @DislikedFood, @Diet, @Notes, @CaloriesIntake, @Weight, @IdealWeight);
                                SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@AnimalID", newAnimalID);
                    cmd.Parameters.AddWithValue("@FavoriteFood", animalDTO.FeedingPlans.First().FavoriteFood);
                    cmd.Parameters.AddWithValue("@DislikedFood", animalDTO.FeedingPlans.First().DislikedFood);
                    string DietString = animalDTO.FeedingPlans.First().Diet.ToString();
                    cmd.Parameters.AddWithValue("@Diet", DietString);
                    cmd.Parameters.AddWithValue("@Notes", animalDTO.FeedingPlans.First().Notes);
                    cmd.Parameters.AddWithValue("@CaloriesIntake", animalDTO.FeedingPlans.First().CaloriesIntake);
                    cmd.Parameters.AddWithValue("@Weight", animalDTO.FeedingPlans.First().Weight);
                    cmd.Parameters.AddWithValue("@IdealWeight", animalDTO.FeedingPlans.First().IdealWeight);

                    result = cmd.ExecuteScalar();
                    int newFeedingPlanID = Convert.ToInt32(result);

                    // Insert FoodAllergies
                    foreach (string allergy in animalDTO.FeedingPlans.First().FoodAllergies)
                    {
                        using (SqlCommand cmdFoodAllergies = new SqlCommand())
                        {
                            cmdFoodAllergies.Connection = conn;
                            cmdFoodAllergies.CommandText = @"INSERT INTO [FoodAllergies] (FeedingPlanID, FoodAllergyID) 
                                                    VALUES (@FeedingPlanID, @FoodAllergyID)";

                            cmdFoodAllergies.Parameters.AddWithValue("@FeedingPlanID", newFeedingPlanID);
                            int FoodAllergyID = GetFoodAllergyID(allergy);
                            cmdFoodAllergies.Parameters.AddWithValue("@FoodAllergyID", FoodAllergyID);
                            cmdFoodAllergies.ExecuteNonQuery();
                        }
                    }

                    // Insert MedicalRecord
                    cmd.CommandText = @"INSERT INTO [MedicalRecord] 
                                (AnimalID, RecordDate, Notes, Allergies, Diseases, Injuries, Operations, Medication, Dangerlevel ) 
                                VALUES (@AnimalID, @RecordDate, @Notes, @Allergies, @Diseases, @Injuries, @Operations, @Medication, @Dangerlevel );
                                SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@AnimalID", newAnimalID);
                    string RecordString = animalDTO.MedicalRecords.First().RecordDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters.AddWithValue("@RecordDate", RecordString);
                    cmd.Parameters.AddWithValue("@Notes", animalDTO.MedicalRecords.First().Notes);
                    cmd.Parameters.AddWithValue("@Allergies", animalDTO.MedicalRecords.First().Allergies);
                    cmd.Parameters.AddWithValue("@Diseases", animalDTO.MedicalRecords.First().Diseases);
                    cmd.Parameters.AddWithValue("@Injuries", animalDTO.MedicalRecords.First().Injuries);
                    cmd.Parameters.AddWithValue("@Operations", animalDTO.MedicalRecords.First().Operations);
                    cmd.Parameters.AddWithValue("@Medication", animalDTO.MedicalRecords.First().Medication);
                    string dangerString = animalDTO.MedicalRecords.First().Dangerlevel.ToString();
                    cmd.Parameters.AddWithValue("@Dangerlevel", dangerString);

                    result = cmd.ExecuteScalar();
                    int newMedicalRecordID = Convert.ToInt32(result);

                    return new Result { Success = true, Message = "Animal with feeding plan and medical record inserted successfully." };
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


        public int GetFoodAllergyID(string allergy)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT FoodAllergyID FROM [FoodAllergy] WHERE Name = @Name";

                cmd.Parameters.AddWithValue("@Name", allergy);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
                return 0;
            }
        }

        public int GetAnimalByNameAndBloodType(AnimalDTO animalDTO)
        {
            using SqlConnection conn = GetSqlConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT AnimalID FROM [Animal] WHERE Name = @Name AND Species = @Species AND Active = 1";

            cmd.Parameters.AddWithValue("@Name", animalDTO.Name);
            cmd.Parameters.AddWithValue("@Species", animalDTO.Species);

            try
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                int newAnimalID;
                if (int.TryParse(result?.ToString(), out newAnimalID))
                {
                    return newAnimalID;
                }
                return 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Problem with the database", ex);
            }
        }

        public List<AnimalDTO> GetRecentAnimal(int count)
        {
            List<AnimalDTO> recentAnimals = new();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP(@Count) a.AnimalID, a.Name, a.Species, a.BloodType, a.Origin, a.Birthday, a.EntryZoo, a.ExitZoo, a.LocationID, " +
                                " mr.MedicalRecordID, mr.RecordDate, mr.Notes, mr.Allergies, mr.Diseases, mr.Injuries, mr.Operations, mr.Medication, mr.Dangerlevel, " +
                                " fp.FeedingPlanID, FavoriteFood, DislikedFood, Diet, fp.Notes as fpNotes, CaloriesIntake, Weight, IdealWeight, " +
                                " fas.FoodAllergyID, " +
                                " fa.Name AS FoodAllergyName " +
                                " FROM Animal a " +
                                " LEFT JOIN MedicalRecord mr ON a.AnimalID = mr.AnimalID " +
                                " LEFT JOIN FeedingPlan fp ON a.AnimalID = fp.AnimalID " +
                                " LEFT JOIN FoodAllergies fas ON fp.FeedingPlanID = fas.FeedingPlanID " +
                                " LEFT JOIN FoodAllergy fa ON fas.FoodAllergyID = fa.FoodAllergyID " +
                                " WHERE a.Active = 1 ORDER BY AnimalID DESC";

                cmd.Parameters.AddWithValue("@Count", count);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();

                    AnimalDTO? currentAnimal = null;
                    Dictionary<int, MedicalRecordDTO> medicalRecordMap = new();
                    Dictionary<int, FeedingPlanDTO> FeedingPlandMap = new();

                    while (reader.Read())
                    {
                        if (reader.IsDBNull("AnimalID"))
                        {
                            return new();
                        }

                        int animalId = reader.GetInt32("AnimalID");
                        int medicalRecordId = 0;
                        int feedingPlanId = 0;
                        // if med and feed are null do not attempt the read. Could crash the program.
                        if (!reader.IsDBNull("MedicalRecordID")) medicalRecordId = reader.GetInt32("MedicalRecordID");
                        if (!reader.IsDBNull("FeedingPlanID")) feedingPlanId = reader.GetInt32("FeedingPlanID");

                        if (currentAnimal == null || currentAnimal.AnimalID != animalId)
                        {
                            if (currentAnimal != null)
                            {
                                currentAnimal.MedicalRecords = medicalRecordMap.Values.ToList();
                                currentAnimal.FeedingPlans = FeedingPlandMap.Values.ToList();
                                recentAnimals.Add(currentAnimal);
                                medicalRecordMap.Clear();
                                FeedingPlandMap.Clear();
                            }

                            currentAnimal = new AnimalDTO(
                                 reader.GetInt32("AnimalID"),
                                  reader.GetString("Name"),
                                  reader.GetString("Species"),
                                  reader.GetString("BloodType"),
                                  reader.GetString("Origin"),
                                 reader.GetDateTime("Birthday"),
                                  reader.GetDateTime("EntryZoo"),
                                  reader.GetDateTime("ExitZoo"),
                                  reader.GetInt32("LocationID")
                            );

                        }

                        if (!medicalRecordMap.TryGetValue(medicalRecordId, out var currentMedicalRecord) && medicalRecordId != 0)
                        {
                            currentMedicalRecord = new MedicalRecordDTO(
                                                reader.GetInt32("MedicalRecordID"),
                                                reader.GetInt32("AnimalID"),
                                                reader.GetDateTime("RecordDate"),
                                                reader.GetString("Notes"),
                                                reader.GetString("Allergies"),
                                                reader.GetString("Diseases"),
                                                reader.GetString("Injuries"),
                                                reader.GetString("Operations"),
                                                reader.GetString("Medication"),
                                                reader.GetString("DangerLevel")
                                            );
                            medicalRecordMap.Add(medicalRecordId, currentMedicalRecord);

                        }

                        if (!FeedingPlandMap.TryGetValue(feedingPlanId, out var currentFeedingPlan) && feedingPlanId != 0)
                        {
                            currentFeedingPlan = new FeedingPlanDTO(
                                  reader.GetInt32("FeedingPlanID"),
                                  reader.GetInt32("AnimalID"),
                                  reader.GetString("FavoriteFood"),
                                  reader.GetString("DislikedFood"),
                                  reader.GetString("Diet"),
                                  new List<string>(),
                                  reader.GetString("fpNotes"),
                                  reader.GetDouble("CaloriesIntake"),
                                  reader.GetDouble("Weight"),
                                  reader.GetDouble("IdealWeight")
                            );

                            FeedingPlandMap.Add(feedingPlanId, currentFeedingPlan);
                        }

                        // Adds Weekdays if they are not null
                        if (!reader.IsDBNull("FoodAllergyName") && feedingPlanId != 0)
                        {
                            var allergy = (reader.GetString("FoodAllergyName"));
                            FeedingPlandMap[feedingPlanId].FoodAllergies.Add(allergy);
                        }

                    }
                    if (currentAnimal != null)
                    {
                        currentAnimal.MedicalRecords = medicalRecordMap.Values.ToList();
                        currentAnimal.FeedingPlans = FeedingPlandMap.Values.ToList();
                        recentAnimals.Add(currentAnimal);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Something went wrong with the Database", ex);
                }
            }

            return recentAnimals;
        }

        private AnimalDTO GetAnimalDTO(SqlDataReader reader)
        {
            int animalID = reader.GetInt32(0);
            string name = reader.GetString(1);
            string species = reader.GetString(2);
            string bloodType = reader.GetString(3);
            string origin = reader.GetString(4);
            DateTime birthday = reader.GetDateTime(5);
            DateTime entryZoo = reader.GetDateTime(6);
            DateTime exitZoo = reader.GetDateTime(7);
            int locationID = reader.GetInt32(8);

            return new AnimalDTO(animalID, name, species, bloodType, origin, birthday, entryZoo, exitZoo, locationID);
        }
        private MedicalRecordDTO GetMedicalRecordDTO(SqlDataReader reader)
        {
            return new MedicalRecordDTO(
                                //MedicalrecordID
                                reader.GetInt32(9),
                                //AnimalID
                                reader.GetInt32(0),
                                //RecordDate
                                reader.GetDateTime(10),
                                //Notes
                                reader.GetString(11),
                                //Allergies
                                reader.GetString(12),
                                //Diseases
                                reader.GetString(13),
                                //Injuries
                                reader.GetString(14),
                                //Operations
                                reader.GetString(15),
                                //Medication
                                reader.GetString(16),
                                //Dangerlevel
                                reader.GetString(17)
                            );
        }

        private FeedingPlanDTO GetFeedingPlanDTO(SqlDataReader reader)
        {
            return new FeedingPlanDTO(
                                //FeedingplanID
                                reader.GetInt32(18),
                                //AnimalID
                                reader.GetInt32(0),
                                //Favfood
                                reader.GetString(19),
                                //DislikedFood
                                reader.GetString(20),
                                //Diet
                                reader.GetString(21),
                                //Notes
                                reader.GetString(22),
                                //CaloriesIntake
                                reader.GetDouble(23),
                                //Weight
                                reader.GetDouble(24),
                                //IdealWeight
                                reader.GetDouble(25)
                            );
        }
        private string GetFeedingAllergy(SqlDataReader reader)
        {
            //Allergies Name aka fp.Name
            return reader.GetString(26);
        }
        // Hello :)
        public Result UpdateAnimal(AnimalDTO animalDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Animal] SET Name = @Name, Species = @Species, BloodType = @BloodType, Origin = @Origin, Birthday = @Birthday, EntryZoo = @EntryZoo, ExitZoo = @ExitZoo, LocationID = @LocationID
                                WHERE AnimalID = @AnimalID";


                cmd.Parameters.AddWithValue("@AnimalID", animalDTO.AnimalID);

                cmd.Parameters.AddWithValue("@Name", animalDTO.Name);

                cmd.Parameters.AddWithValue("@Species", animalDTO.Species);

                //This converts the employee enum to a string to pass it to the databse
                string bloodString = animalDTO.BloodType.ToString();
                cmd.Parameters.AddWithValue("@BloodType", bloodString);

                cmd.Parameters.AddWithValue("@Origin", animalDTO.Origin);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string BirthdayString = animalDTO.Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@Birthday", BirthdayString);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string EntryString = animalDTO.EntryZoo.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@EntryZoo", EntryString);

                // This converts DateTime to string in the format "yyyy-MM-dd HH:mm:ss.fff" so that it works with the database
                string ExitString = animalDTO.ExitZoo.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cmd.Parameters.AddWithValue("@ExitZoo", ExitString);

                if (animalDTO.LocationID.HasValue)
                {
                    cmd.Parameters.AddWithValue("@LocationID", animalDTO.LocationID.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LocationID", DBNull.Value);
                }

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "Animal updated successfully." };
                    }

                    return new Result { Success = false, Message = "Failed to retrieve animal ID." };
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

        public List<string> GetAllSpecies()
        {
            List<string> species = new List<string>();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT Species FROM Animal WHERE Active = 1
                                    UNION
                                    Select Species From Location WHERE Active = 1";
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            species.Add(reader.GetString(0));
                        }
                        return species;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Problem with the database", ex);
                }


            }
        }

        public AnimalDTO GetAnimalByAnimalID(int animalID)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT a.AnimalID, a.Name, Species, BloodType, Origin, Birthday, EntryZoo, ExitZoo, LocationID, 
                            MedicalRecordID, RecordDate, mr.Notes, Allergies, Diseases, Injuries, Operations, Medication, DangerLevel, 
                            fp.FeedingPlanID, FavoriteFood, DislikedFood, Diet, fp.Notes, CaloriesIntake, Weight, IdealWeight, fp.Name AS FPNAME 
                            FROM Animal a 
                            LEFT JOIN MedicalRecord mr ON a.AnimalID = mr.AnimalID 
                            LEFT JOIN (
                                SELECT FeedingPlan.FeedingPlanID, AnimalID, FavoriteFood, DislikedFood, Diet, Notes, CaloriesIntake, Weight, IdealWeight, Name 
                                FROM FeedingPlan 
                                LEFT JOIN FoodAllergies ON FeedingPlan.FeedingPlanID = FoodAllergies.FeedingPlanID 
                                INNER JOIN FoodAllergy ON FoodAllergies.FoodAllergyID = FoodAllergy.FoodAllergyID 
                            ) fp ON a.AnimalID = fp.AnimalID 
                            WHERE a.AnimalID = @AnimalID";

                cmd.Parameters.AddWithValue("@AnimalID", animalID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    AnimalDTO? currentAnimal = null;
                    List<MedicalRecordDTO> currentMedicalRecord = new();
                    List<FeedingPlanDTO> currentFeedingPlan = new();
                    List<string> currentfeedingPlanAllergies = new();

                    while (reader.Read())
                    {
                        if (reader.IsDBNull("AnimalID"))
                        {
                            return null;
                        }

                        if (currentAnimal == null)
                        {
                            currentAnimal = new AnimalDTO(
                                reader.GetInt32("AnimalID"),
                                reader.GetString("Name"),
                                reader.GetString("Species"),
                                reader.GetString("BloodType"),
                                reader.GetString("Origin"),
                                reader.GetDateTime("Birthday"),
                                reader.GetDateTime("EntryZoo"),
                                reader.GetDateTime("ExitZoo"),
                                reader.GetInt32("LocationID")
                            );
                        }

                        if (!reader.IsDBNull("MedicalRecordID") && currentMedicalRecord.Find(record => record.MedicalRecordID == reader.GetInt32("MedicalRecordID")) == null)
                        {
                            currentMedicalRecord.Add(new MedicalRecordDTO(
                                reader.GetInt32("MedicalRecordID"),
                                reader.GetInt32("AnimalID"),
                                reader.GetDateTime("RecordDate"),
                                reader.GetString("Notes"),
                                reader.GetString("Allergies"),
                                reader.GetString("Diseases"),
                                reader.GetString("Injuries"),
                                reader.GetString("Operations"),
                                reader.GetString("Medication"),
                                reader.GetString("DangerLevel")
                            ));
                        }

                        if (!reader.IsDBNull("FeedingPlanID") && currentFeedingPlan.Find(plan => plan.FeedingPlanID == reader.GetInt32("FeedingPlanID")) == null)
                        {
                            List<string> foodAllergies = new List<string>();

                            if (!reader.IsDBNull("FPNAME"))
                            {
                                foodAllergies.Add(reader.GetString("FPNAME"));
                            }

                            currentFeedingPlan.Add(new FeedingPlanDTO(
                                reader.GetInt32("FeedingPlanID"),
                                reader.GetInt32("AnimalID"),
                                reader.GetString("FavoriteFood"),
                                reader.GetString("DislikedFood"),
                                reader.GetString("Diet"),
                                foodAllergies,
                                reader.GetString("Notes"),
                                reader.GetDouble("CaloriesIntake"),
                                reader.GetDouble("Weight"),
                                reader.GetDouble("IdealWeight")
                            ));

                            if (!reader.IsDBNull(reader.GetOrdinal("FPNAME")))
                            {
                                currentfeedingPlanAllergies.Add(reader.GetString("FPNAME"));
                            }
                        }
                    }

                    if (currentAnimal != null)
                    {
                        currentAnimal.MedicalRecords = currentMedicalRecord.ToList();
                        currentAnimal.FeedingPlans = currentFeedingPlan.ToList();
                    }

                    return currentAnimal;
                }
            }
        }



        public int GetAnimalID(AnimalDTO animalDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT AnimalID FROM [Animal] WHERE Name = @Name AND Species = @Species AND Active = 1";

                cmd.Parameters.AddWithValue("@Name", animalDTO.Name);
                cmd.Parameters.AddWithValue("@Species", animalDTO.Species);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newAnimalID;
                    if (int.TryParse(result?.ToString(), out newAnimalID))
                    {
                        return newAnimalID;
                    }
                    return 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Problem with the database", ex);
                }
            }
        }

        public Result DeleteAnimal(AnimalDTO animalDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Animal] SET Active = 0 
                                    WHERE AnimalID = @AnimalID AND Name = @Name ";

                cmd.Parameters.AddWithValue("@AnimalID", animalDTO.AnimalID);
                cmd.Parameters.AddWithValue("@Name", animalDTO.Name);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newAnimalID;
                    if (int.TryParse(result?.ToString(), out newAnimalID))
                    {
                        return new Result { Success = true, Message = "Animal deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve animal ID." };
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

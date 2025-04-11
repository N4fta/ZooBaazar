using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Access
{
    public class FeedingPlanRepository : DatabaseConnection, IFeedingPlanRepository
    {
        public Result InsertFeedingPlan(FeedingPlanDTO feedingPlanDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (feedingPlanDTO.FeedingPlanID != 0)
                {
                    cmd.CommandText = @"SET IDENTITY_INSERT [FeedingPlan] ON
                                INSERT INTO [FeedingPlan] (FeedingPlanID, AnimalID, FavoriteFood, DislikedFood, Diet, Notes, CaloriesIntake, Weight, IdealWeight) 
                                VALUES (@FeedingPlanID, @AnimalID, @FavoriteFood, @DislikedFood, @Diet, @Notes, @CaloriesIntake, @Weight, @IdealWeight)
                                SET IDENTITY_INSERT [FeedingPlan] OFF
                                SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@FeedingPlanID", feedingPlanDTO.FeedingPlanID);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO [FeedingPlan] (AnimalID, FavoriteFood, DislikedFood, Diet, Notes, CaloriesIntake, Weight, IdealWeight) 
                                VALUES (@AnimalID, @FavoriteFood, @DislikedFood, @Diet, @Notes, @CaloriesIntake, @Weight, @IdealWeight)
                                SELECT SCOPE_IDENTITY()";
                }

                cmd.Parameters.AddWithValue("@AnimalID", feedingPlanDTO.AnimalID);
                cmd.Parameters.AddWithValue("@FavoriteFood", feedingPlanDTO.FavoriteFood);
                cmd.Parameters.AddWithValue("@DislikedFood", feedingPlanDTO.DislikedFood);
                string DietString = feedingPlanDTO.Diet.ToString();
                cmd.Parameters.AddWithValue("@Diet", DietString);
                cmd.Parameters.AddWithValue("@Notes", feedingPlanDTO.Notes);
                cmd.Parameters.AddWithValue("@CaloriesIntake", feedingPlanDTO.CaloriesIntake);
                cmd.Parameters.AddWithValue("@Weight", feedingPlanDTO.Weight);
                cmd.Parameters.AddWithValue("@IdealWeight", feedingPlanDTO.IdealWeight);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return new Result { Success = true, Message = "FeedingPlan inserted successfully." };
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


        public List<FeedingPlanDTO> GetAnimalFeedingPlan(int animalID)
        {
            List<FeedingPlanDTO> recentFeedingPlan = new List<FeedingPlanDTO>();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP (1000) FeedingPlanID, AnimalID, FavoriteFood, DislikedFood, Diet, Notes, CaloriesIntake, Weight, IdealWeight " +
                                  "FROM [FeedingPlan] WHERE Active = 1 AND AnimalID = @AnimalID ORDER BY FeedingPlanID DESC";

                cmd.Parameters.AddWithValue("@AnimalID", animalID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<string> FoodAllergies = GetFoodAllergies(reader.GetInt32(0));

                        FeedingPlanDTO feedingPlanDTO = new FeedingPlanDTO(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetDouble(6),
                                reader.GetDouble(7),
                                reader.GetDouble(8)
                            );

                        recentFeedingPlan.Add(feedingPlanDTO);
                    }
                }
            }
            return recentFeedingPlan;
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
        public Result InsertFoodAllergies(FeedingPlanDTO feedingPlanDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"INSERT INTO [FoodAllergies] (FeedingPlanID, FoodAllergyID) 
                            VALUES (@FeedingPlanID, @FoodAllergyID)";

                // Add the parameters once and set their values in the loop
                cmd.Parameters.Add("@FeedingPlanID", SqlDbType.Int);
                cmd.Parameters.Add("@FoodAllergyID", SqlDbType.Int);

                try
                {
                    conn.Open();

                    foreach (string allergy in feedingPlanDTO.FoodAllergies)
                    {
                        int FoodAllergyID = GetFoodAllergyID(allergy);

                        cmd.Parameters["@FeedingPlanID"].Value = feedingPlanDTO.FeedingPlanID;
                        cmd.Parameters["@FoodAllergyID"].Value = FoodAllergyID;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    return new Result { Success = false, Message = $"SQL Exception: {ex.Message}" };
                }
                catch (Exception ex)
                {
                    return new Result { Success = false, Message = $"Exception: {ex.Message}" };
                }
                finally
                {
                    conn.Close();
                }

                return new Result { Success = true, Message = "FoodAllergies inserted successfully." };
            }
        }


        public List<string> GetFoodAllergies(int feedingPlanID)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Name FROM [FoodAllergy] " +
                                "INNER JOIN FoodAllergies ON FoodAllergy.FoodAllergyID = FoodAllergies.FoodAllergyID " +
                                "INNER JOIN FeedingPlan ON FeedingPlan.FeedingPlanID = FoodAllergies.FeedingPlanID " +
                                "WHERE FoodAllergies.FeedingPlanID=@FeedingPlanID";

                cmd.Parameters.AddWithValue("@FeedingPlanID", feedingPlanID);
                conn.Open();

                List<string> Allergies = new List<string>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Allergies.Add(reader.GetString(0));
                    }
                }

                return Allergies;
            }

        }

        public Result UpdateAnimalFeedingPlan(FeedingPlanDTO feedingPlanDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [FeedingPlan] SET FavoriteFood = @FavoriteFood, DislikedFood = @DislikedFood, Diet = @Diet, Notes = @Notes, CaloriesIntake = @CaloriesIntake, Weight = @Weight, IdealWeight = @IdealWeight 
                                    WHERE FeedingPlanID = @FeedingPlanID";

                cmd.Parameters.AddWithValue("@FeedingPlanID", feedingPlanDTO.FeedingPlanID);

                cmd.Parameters.AddWithValue("@FavoriteFood", feedingPlanDTO.FavoriteFood);

                cmd.Parameters.AddWithValue("@DislikedFood", feedingPlanDTO.DislikedFood);

                //This converts the employee enum to a string to pass it to the databse
                string DietString = feedingPlanDTO.Diet.ToString();
                cmd.Parameters.AddWithValue("@Diet", DietString);

                cmd.Parameters.AddWithValue("@Notes", feedingPlanDTO.Notes);

                cmd.Parameters.AddWithValue("@CaloriesIntake", feedingPlanDTO.CaloriesIntake);

                cmd.Parameters.AddWithValue("@Weight", feedingPlanDTO.Weight);

                cmd.Parameters.AddWithValue("@IdealWeight", feedingPlanDTO.IdealWeight);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "FeedingPlan updated successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to update FeedingPlan" };
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

        public Result DeleteAnimalFeedingPlan(FeedingPlanDTO feedingPlanDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [FeedingPlan] SET Active = 0 
                                    WHERE FeedingPlanID = @FeedingPlanID";


                cmd.Parameters.AddWithValue("@FeedingPlanID", feedingPlanDTO.FeedingPlanID);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return new Result { Success = true, Message = "FeedingPlanID deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to delete FeedingPlanID" };
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

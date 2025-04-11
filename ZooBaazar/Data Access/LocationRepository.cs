using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class LocationRepository : DatabaseConnection, ILocationRepository
    {
        public Result InsertLocation(LocationDTO locationDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (locationDTO.Id != 0)
                {
                    cmd.CommandText = @"SET IDENTITY_INSERT [Location] ON
                                    INSERT INTO [Location] (LocationID, Name, Capacity, Description, Danger, Species) 
                                    VALUES (@LocationID, @Name, @Capacity, @Description, @Danger, @Species)
                                    SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@LocationID", locationDTO.Id);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO [Location] (Name, Capacity, Description, Danger, Species) 
                                    VALUES (@Name, @Capacity, @Description, @Danger, @Species)
                                    SELECT SCOPE_IDENTITY() ";
                }

                cmd.Parameters.AddWithValue("@Name", locationDTO.Name);
                cmd.Parameters.AddWithValue("@Capacity", locationDTO.Capacity);
                cmd.Parameters.AddWithValue("@Description", locationDTO.Description);
                cmd.Parameters.AddWithValue("@Danger", locationDTO.Danger);
                cmd.Parameters.AddWithValue("@Species", locationDTO.Species);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return new Result { Success = true, Message = "Location inserted successfully." };
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

        public List<LocationDTO> LoadLocations(int count)
        {
            List<LocationDTO> LocationDTOs = new();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT TOP (@Count) L.LocationID, L.Name as LocationName, L.Capacity, 
                                    (Select Count(Animal.LocationID) from Animal where Animal.LocationID = L.LocationID ) as AnimalCount, 
                                    L.Description, L.Danger, L.Species,
                                    A.AnimalID, A.Name as AnimalName, A.Species, A.BloodType, A.Origin, A.Birthday, A.EntryZoo, A.ExitZoo,
                                    mr.MedicalRecordID, mr.RecordDate, mr.Notes, mr.Allergies, mr.Diseases, mr.Injuries, mr.Operations, mr.Medication, mr.Dangerlevel,
                                    fp.FeedingPlanID, FavoriteFood, DislikedFood, Diet, fp.Notes, CaloriesIntake, Weight, IdealWeight,
                                    fas.FoodAllergyID, 
                                    fa.Name AS FoodAllergyName 
                                    FROM [Location] L                                    
                                    LEFT JOIN Animal A on A.LocationID = L.LocationID
                                    LEFT JOIN MedicalRecord mr ON A.AnimalID = mr.AnimalID
                                    LEFT JOIN FeedingPlan fp ON A.AnimalID = fp.AnimalID
                                    LEFT JOIN FoodAllergies fas ON fp.FeedingPlanID = fas.FeedingPlanID
                                    LEFT JOIN FoodAllergy fa ON fas.FoodAllergyID = fa.FoodAllergyID
                                    WHERE L.Active = 1
                                    ORDER BY L.LocationID DESC";
                
                cmd.Parameters.AddWithValue("@Count", count);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();

                    //  We must keep track of the current location and animal
                    LocationDTO? currentLocation = null;
                    List<AnimalDTO> currentAnimals = new();

                    while (reader.Read())
                    {
                        if (reader.IsDBNull("LocationID"))// If Location Id is null there are no locations, return an empty list
                        {
                            return new();
                        }

                        // If Current Location is null (first time) or has a different Id than before
                        // Save Current Location and create a new one
                        if (currentLocation == null || currentLocation.Id != reader.GetInt32("LocationID"))
                        {
                            // If this is not the first location, save the previous locations animals
                            if (currentLocation != null)
                            {
                                currentLocation.Animals = currentAnimals.ToList();
                                LocationDTOs.Add(currentLocation);
                                currentAnimals.Clear();
                            }

                            currentLocation = new LocationDTO(
                                reader.GetInt32("LocationID"),
                                reader.GetString("LocationName"),
                                reader.GetInt32("Capacity"),
                                reader.GetInt32("AnimalCount"),
                                reader.GetString("Description"),
                                reader.GetString("Danger"),
                                reader.GetString("Species"),
                                new()
                                );
                        }

                        // If the AnimalID is not null and not in currentAnimals, create one
                        if (!reader.IsDBNull("AnimalID") && currentAnimals.Find(animal=>animal.AnimalID == reader.GetInt32("AnimalID")) == null)
                        {
                            // Create Animal and Add it to List
                            currentAnimals.Add(new(
                                reader.GetInt32("AnimalID"),
                                reader.GetString("AnimalName"),
                                reader.GetString("Species"),
                                reader.GetString("BloodType"),
                                reader.GetString("Origin"),
                                reader.GetDateTime("Birthday"),
                                reader.GetDateTime("EntryZoo"),
                                reader.GetDateTime("ExitZoo"),
                                reader.GetInt32("LocationID"),
                                new List<FeedingPlanDTO>(),
                                new List<MedicalRecordDTO>()
                                ));
                        }

                    }
                    // Save last Location
                    currentLocation.Animals = currentAnimals.ToList();
                    LocationDTOs.Add(currentLocation);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Something went wrong with the Database", ex);
                }
            }
            return LocationDTOs;
        }

        public List<int> GetAnimals(int locationID)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT AnimalID FROM [Animal] " +
                                "INNER JOIN Location ON Animal.LocationID = Location.LocationID " +
                                "WHERE Location.LocationID = @LocationID AND Active = 1 ";

                cmd.Parameters.AddWithValue("@LocationID", locationID);

                conn.Open();

                List<int> animalIDs = new List<int>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animalIDs.Add(reader.GetInt32(0));
                    }
                }

                return animalIDs;
            }

        }

        public Result UpdateLocation(LocationDTO locationDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Location] SET Name = @Name, Capacity = @Capacity, Description = @Description, Danger = @Danger, Species = @Species 
                                    WHERE LocationID = @LocationID 
                                    UPDATE [Animal] SET Animal.LocationID = @LocationID
                                    WHERE AnimalID = @AnimalID ";

                cmd.Parameters.AddWithValue("@LocationID", locationDTO.Id);
                cmd.Parameters.AddWithValue("@Name", locationDTO.Name);
                cmd.Parameters.AddWithValue("@Capacity", locationDTO.Capacity);
                cmd.Parameters.AddWithValue("@Description", locationDTO.Description);
                cmd.Parameters.AddWithValue("@Danger", locationDTO.Danger);
                cmd.Parameters.AddWithValue("@Species", locationDTO.Species);

                cmd.Parameters.AddWithValue("@AnimalID", locationDTO.Animals.Select(animal => animal.AnimalID).FirstOrDefault());

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "Location updated successfully." };
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

        public Result DeleteLocation(LocationDTO locationDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [Location] SET Active = 0 
                                    WHERE LocationID = @LocationID AND Name = Name";

                cmd.Parameters.AddWithValue("@LocationID", locationDTO.Id);
                cmd.Parameters.AddWithValue("@Name", locationDTO.Name);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Result { Success = true, Message = "Location deleted successfully." };
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
}

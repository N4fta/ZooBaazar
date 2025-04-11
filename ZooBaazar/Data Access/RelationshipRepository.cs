using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Access
{
    public class RelationshipRepository : DatabaseConnection, IRelationshipRepository
    {
        public Result InsertRelationship(RelationshipDTO relationshipDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (relationshipDTO.RelationshipID != 0)
                {
                    cmd.CommandText = @"SET IDENTITY_INSERT [AnimalRelationship] ON
                                    INSERT INTO [AnimalRelationship] (RelationshipID, PrimaryAnimalID, RelationType, SecondaryAnimalID) 
                                    VALUES (@RelationshipID, @PrimaryAnimalID, @RelationType, @SecondaryAnimalID)
                                    SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@RelationshipID", relationshipDTO.RelationshipID);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO [AnimalRelationship] (PrimaryAnimalID, RelationType, SecondaryAnimalID) 
                                    VALUES (@PrimaryAnimalID, @RelationType, @SecondaryAnimalID)
                                    SELECT SCOPE_IDENTITY()";
                }

                cmd.Parameters.AddWithValue("@PrimaryAnimalID", relationshipDTO.PrimaryAnimalID);

                cmd.Parameters.AddWithValue("@RelationType", relationshipDTO.RelationType);

                cmd.Parameters.AddWithValue("@SecondaryAnimalID", relationshipDTO.SecondaryAnimalID);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newRelationshipID;
                    if (int.TryParse(result?.ToString(), out newRelationshipID))
                    {
                        relationshipDTO.RelationshipID = newRelationshipID;
                        return new Result { Success = true, Message = "Relationship inserted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve new Relationship ID." };
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

        public List<RelationshipDTO> GetRecentRelationships(int count)
        {
            List<RelationshipDTO> recentRelationship = new List<RelationshipDTO>();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP (@Count) RelationshipID, PrimaryAnimalID, RelationType, SecondaryAnimalID " +
                                  "FROM [AnimalRelationship] WHERE Active = 1 ORDER BY RelationshipID DESC";

                cmd.Parameters.AddWithValue("@Count", count);


                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int relationshipID = reader.GetInt32("RelationshipID");
                        int primaryAnimalID = reader.GetInt32("PrimaryAnimalID");
                        string relationTyp = reader.GetString("RelationType");
                        int secondaryAnimalID = reader.GetInt32("SecondaryAnimalID");

                        RelationshipDTO relationship = new RelationshipDTO(relationshipID, primaryAnimalID, relationTyp, secondaryAnimalID);
                        recentRelationship.Add(relationship);
                    }
                }
            }
            return recentRelationship;
        }

        public List<RelationshipDTO> GetRelationshipByAnimalID(int animalID)
        {
            List<RelationshipDTO> recentRelationShip = new List<RelationshipDTO>();

            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RelationshipID, PrimaryAnimalID, RelationType, SecondaryAnimalID " +
                                  "FROM [AnimalRelationship] WHERE Active = 1 AND PrimaryAnimalID = @PrimaryAnimalID";

                cmd.Parameters.AddWithValue("@PrimaryAnimalID", animalID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RelationshipDTO relationshipDTO = new RelationshipDTO(
                                 reader.GetInt32(0),
                                 reader.GetInt32(1),
                                 reader.GetString(2),
                                 reader.GetInt32(3)
                            );

                        recentRelationShip.Add(relationshipDTO);
                    }
                }
            }
            return null;
        }

        public Result UpdateAnimalRelationship(RelationshipDTO relationshipDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT COUNT (*) FROM AnimalRelationship 
                                    WHERE RelationshipID = @RelationshipID AND PrimaryAnimalID = @PrimaryAnimalID AND RelationType = @RelationType AND SecondaryAnimalID = @SecondaryAnimalID";

                cmd.Parameters.AddWithValue("@RelationshipID", relationshipDTO.RelationshipID);

                cmd.Parameters.AddWithValue("@PrimaryAnimalID", relationshipDTO.PrimaryAnimalID);

                cmd.Parameters.AddWithValue("@RelationType", relationshipDTO.RelationType);

                cmd.Parameters.AddWithValue("@SecondaryAnimalID", relationshipDTO.SecondaryAnimalID);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int count = Convert.ToInt32(result);
                    if (count == 0)
                    {
                        return InsertRelationship(relationshipDTO);
                    }

                    return new Result { Success = false, Message = "RelationShip already exists" };
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

        public Result DeleteAnimalRelationship(RelationshipDTO relationshipDTO)
        {
            using (SqlConnection conn = GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [AnimalRelationship] SET Active = 0 
                                    WHERE RelationshipID = @RelationshipID AND PrimaryAnimalID = @PrimaryAnimalID AND RelationType = @OldRelationType AND SecondaryAnimalID = @OldSecondaryAnimalID ";

                cmd.Parameters.AddWithValue("@RelationshipID", relationshipDTO.RelationshipID);
                cmd.Parameters.AddWithValue("@PrimaryAnimalID", relationshipDTO.PrimaryAnimalID);
                cmd.Parameters.AddWithValue("@RelationType", relationshipDTO.RelationType);
                cmd.Parameters.AddWithValue("@SecondaryAnimalID", relationshipDTO.SecondaryAnimalID);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    int newRelationshipID;
                    if (int.TryParse(result?.ToString(), out newRelationshipID))
                    {
                        return new Result { Success = true, Message = "Relationship deleted successfully." };
                    }
                    return new Result { Success = false, Message = "Failed to retrieve Relationship ID." };
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

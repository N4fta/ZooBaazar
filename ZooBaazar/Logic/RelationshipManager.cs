using Data_Access;
using Microsoft.IdentityModel.Tokens;

namespace Logic
{
    public class RelationshipManager
    {
        private readonly IRelationshipRepository relationshipRepository;

        public RelationshipManager(IRelationshipRepository irelRep)
        {
            relationshipRepository = irelRep;
        }

        public List<Relationship> LoadAnimalRelationshipsFromDatabase()
        {
            List<Relationship> relationships = new();
            List<RelationshipDTO> relationshipsDTOs = new();

            try
            {
                relationshipsDTOs = relationshipRepository.GetRecentRelationships(int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var  relationshipDTO in relationshipsDTOs)
            {
                relationships.Add(ConvertToRelationship(relationshipDTO));
            }
            return relationships;
        }

        public Result Add(Relationship relationship)
        {
            RelationshipDTO relationshipDTO = ConvertToRelationshipDTO(relationship);
            
            Result resultADD = SendRelationshipToTheDb(relationshipDTO);

            return resultADD;
        }

        public Result Remove(Relationship relationship)
        {
            Result resultRemoveRelationship = relationshipRepository.DeleteAnimalRelationship(ConvertToRelationshipDTO(relationship));

            return resultRemoveRelationship;
        }

        public Result Update(Relationship newRelationship)
        {
            RelationshipDTO relationshipDTO = ConvertToRelationshipDTO(newRelationship);

            if (newRelationship.RelationShipID == 0)
            {
                return Add(newRelationship);
            }

            Result resultUpdated = relationshipRepository.UpdateAnimalRelationship(relationshipDTO);

            return resultUpdated;
        }

        public Result SendRelationshipToTheDb(RelationshipDTO newRelationShip)
        {
            return relationshipRepository.InsertRelationship(newRelationShip);
        }

        public static RelationshipDTO ConvertToRelationshipDTO(Relationship relationship)
        {
            if (relationship == null)
            {
                return null;
            }
            return new RelationshipDTO(
                relationship.RelationShipID,
                relationship.PrimaryAnimalID,
                relationship.RelationType,
                relationship.SecondaryAnimalID
            );
        }

        public static Relationship ConvertToRelationship(RelationshipDTO relationshipDTO)
        {
            if (relationshipDTO == null)
            {
                return null;
            }
            return new Relationship(
                relationshipDTO.RelationshipID,
                relationshipDTO.PrimaryAnimalID,
                relationshipDTO.RelationType,
                relationshipDTO.SecondaryAnimalID
            );
        }
    }
}

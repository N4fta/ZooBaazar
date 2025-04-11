namespace Data_Access
{
    public interface IRelationshipRepository
    {
        Result InsertRelationship(RelationshipDTO relationShipDTO);

        List<RelationshipDTO> GetRecentRelationships(int count);

        List<RelationshipDTO> GetRelationshipByAnimalID(int animalID);

        Result UpdateAnimalRelationship(RelationshipDTO relationshipDTO);

        Result DeleteAnimalRelationship(RelationshipDTO relationshipDTO);
    }
}
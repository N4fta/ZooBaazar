namespace Logic
{
    public class Relationship
    {
        public int RelationShipID;

        public int PrimaryAnimalID;

        public string RelationType;

        public int SecondaryAnimalID;

        public Relationship(int relationShipID, int primaryAnimalID, string relationType, int secondaryAnimalID)
        {
            RelationShipID = relationShipID;
            PrimaryAnimalID = primaryAnimalID;
            RelationType = relationType;
            SecondaryAnimalID = secondaryAnimalID;
        }

        public Relationship(int primaryAnimalID, string relationType, int secondaryAnimalID)
        {
            RelationShipID = 0;
            PrimaryAnimalID = primaryAnimalID;
            RelationType = relationType;
            SecondaryAnimalID = secondaryAnimalID;
        }
    }
}

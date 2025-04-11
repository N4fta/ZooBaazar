namespace Data_Access
{
    public class RelationshipDTO
    {
        public int RelationshipID { get; set; }

        public int PrimaryAnimalID { get; set; }

        public string RelationType { get; set; }

        public int SecondaryAnimalID { get; set; }
        public RelationshipDTO(int relationShipID, int primaryAnimalID, string relationType, int secondaryAnimalID)
        {
            RelationshipID = relationShipID;
            PrimaryAnimalID = primaryAnimalID;
            RelationType = relationType;
            SecondaryAnimalID = secondaryAnimalID;
        }
    }
}

namespace Logic
{
    public class Animal
    {
        public int AnimalID { get; }

        public string Name { get; set; }

        public string Species { get; set; }
        
        public BloodTypes BloodType { get; set; }
        
        public string Origin { get; set; }

        public Relationship Relationship { get; set; }
        
        public DateTime Birthday { get; set; }

        public DateTime EntryZoo {get; set;}

        public DateTime ExitZoo {get ; set; }

        public FeedingPlan FeedingPlan { get; set; }

        public MedicalRecord MedicalRecord { get; set; }

        public Animal(int animalID, string name, string species, BloodTypes bloodType, string origin, Relationship relationship, DateTime birthday, DateTime entryZoo, DateTime exitZoo, FeedingPlan feedingPlan, MedicalRecord medicalRecord)
        {
            AnimalID = animalID;
            Name = name;
            Species = species;
            BloodType = bloodType;
            Origin = origin;
            Relationship = relationship;
            Birthday = birthday;
            EntryZoo = entryZoo;
            ExitZoo = exitZoo;
            FeedingPlan = feedingPlan;
            MedicalRecord = medicalRecord;
        }

        public Animal(string name, string species, BloodTypes bloodType, string origin, Relationship relationship, DateTime birthday, DateTime entryZoo, DateTime exitZoo, FeedingPlan feedingPlan, MedicalRecord medicalRecord)
        {
            Name = name;
            Species = species;
            BloodType = bloodType;
            Origin = origin;
            Relationship = relationship;
            Birthday = birthday;
            EntryZoo = entryZoo;
            ExitZoo = exitZoo;
            FeedingPlan = feedingPlan;
            MedicalRecord = medicalRecord;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

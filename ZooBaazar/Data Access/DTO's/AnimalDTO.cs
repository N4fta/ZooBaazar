
namespace Data_Access
{
    public class AnimalDTO
    {
        public int AnimalID { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public string BloodType { get; set; }

        public string Origin { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime EntryZoo { get; set; }

        public DateTime ExitZoo { get; set; }

        public int? LocationID { get; set; }
        public List<FeedingPlanDTO> FeedingPlans { get; set; }
        public List<MedicalRecordDTO> MedicalRecords { get; set; }

        public AnimalDTO(int animalID, string name, string species, string bloodType, string origin, DateTime birthday, DateTime entryZoo, DateTime exitZoo, int locationID)
        {
            AnimalID = animalID;
            Name = name;
            Species = species;
            BloodType = bloodType;
            Origin = origin;
            Birthday = birthday;
            EntryZoo = entryZoo;
            ExitZoo = exitZoo;
            LocationID = locationID;
        }

        public AnimalDTO(int animalID, string name, string species, string bloodType, string origin, DateTime birthday, DateTime entryZoo, DateTime exitZoo, int? locationID, List<FeedingPlanDTO> feedingPlans, List<MedicalRecordDTO> medicalRecords)
        {
            AnimalID = animalID;
            Name = name;
            Species = species;
            BloodType = bloodType;
            Origin = origin;
            Birthday = birthday;
            EntryZoo = entryZoo;
            ExitZoo = exitZoo;
            LocationID = locationID;
            FeedingPlans = feedingPlans;
            MedicalRecords = medicalRecords;
        }
    }
}

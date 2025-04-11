namespace Logic
{
    public class MedicalRecord
    {
        public int MedicalRecordID;
        public int AnimalID;
        public DateTime RecordDate;
        public string Notes { get; set; }
        public string Allergies;
        public string Diseases;
        public string Injuries;
        public string Operations;
        public string Medication;
        public DangerEnum Dangerlevel;

        public MedicalRecord(int medicalRecordID, DateTime recordDate, string notes, string allergies, string diseases, string injuries, string operations, string medication, DangerEnum dangerlevel)
        {
            MedicalRecordID = medicalRecordID;
            RecordDate = recordDate;
            Notes = notes;
            Allergies = allergies;
            Diseases = diseases;
            Injuries = injuries;
            Operations = operations;
            Medication = medication;
            Dangerlevel = dangerlevel;
        }

        public MedicalRecord(DateTime recordDate, string notes, string allergies, string diseases, string injuries, string operations, string medication, DangerEnum dangerlevel)
        {
            MedicalRecordID = 0;
            RecordDate = recordDate;
            Notes = notes;
            Allergies = allergies;
            Diseases = diseases;
            Injuries = injuries;
            Operations = operations;
            Medication = medication;
            Dangerlevel = dangerlevel;
        }

        public MedicalRecord(int medicalRecordID, int animalID, DateTime recordDate, string notes, string allergies, string diseases, string injuries, string operations, string medication, DangerEnum dangerlevel)
        {
            MedicalRecordID = medicalRecordID;
            AnimalID = animalID;
            RecordDate = recordDate;
            Notes = notes;
            Allergies = allergies;
            Diseases = diseases;
            Injuries = injuries;
            Operations = operations;
            Medication = medication;
            Dangerlevel = dangerlevel;
        }
    }
}

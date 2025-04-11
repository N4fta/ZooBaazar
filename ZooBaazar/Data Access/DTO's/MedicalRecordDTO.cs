namespace Data_Access
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordID{ get; set; } 
        public int AnimalID{ get; set; } 
        public DateTime RecordDate{ get; set; } 
        public string Notes{ get; set; } 
        public string Allergies{ get; set; } 
        public string Diseases{ get; set; } 
        public string Injuries{ get; set; } 
        public string Operations{ get; set; } 
        public string Medication{ get; set; } 
        public string Dangerlevel{ get; set; }

        public MedicalRecordDTO(int medicalRecordID, int animalID, DateTime recordDate, string notes, string allergies, string diseases, string injuries, string operations, string medication, string dangerlevel)
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

namespace Data_Access
{
    public interface IMedicalRecordRepository
    {
        Result InsertMedicalRecord(MedicalRecordDTO medicalRecordDTO);

        List<MedicalRecordDTO> GetAnimalMedicalRecord(int animalID);

        Result UpdateAnimalMedicalRecord(MedicalRecordDTO medicalRecordDTO);

        Result DeleteAnimalMedicalRecord(MedicalRecordDTO medicalRecordDTO);

    }
}
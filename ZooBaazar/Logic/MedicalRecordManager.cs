using Data_Access;

namespace Logic
{
    public class MedicalRecordManager
    {
        public List<MedicalRecord> medicalRecords;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IAnimalRepository _animalRepository;


        public MedicalRecordManager(IMedicalRecordRepository medicalRecordRepository, IAnimalRepository animalRepository)
        {
            medicalRecords = new List<MedicalRecord>();
            _medicalRecordRepository = medicalRecordRepository;
            _animalRepository = animalRepository;
        }

        public MedicalRecord LoadAnimalMedicalRecord(AnimalDTO animalDTO)
        {
            List<MedicalRecordDTO> MedicalRecordDTOs = _medicalRecordRepository.GetAnimalMedicalRecord(animalDTO.AnimalID);

            if (MedicalRecordDTOs.Count == 0)
            {
                return null;
            }
            return ConvertToMedicalRecord(MedicalRecordDTOs[0]);
        }

        public Result Add(MedicalRecord medicalRecord)
        {
            MedicalRecordDTO medicalRecordDTO = ConvertToMedicalRecordDTO(medicalRecord);

            Result resulADD = SendToTheDb(medicalRecordDTO);
            
            return resulADD;

        }

        public Result Remove(MedicalRecord medicalRecord)
        {
            Result resultRemoveMedicalRecord = _medicalRecordRepository.DeleteAnimalMedicalRecord(ConvertToMedicalRecordDTO(medicalRecord));

            return resultRemoveMedicalRecord;
        }

        public Result Update(MedicalRecord newMedicalRecord)
        {
            MedicalRecordDTO medicalRecordDTO = ConvertToMedicalRecordDTO(newMedicalRecord);

            if (newMedicalRecord.MedicalRecordID == 0)
            {
                return Add(newMedicalRecord);
            }

            Result resultUpdate = _medicalRecordRepository.UpdateAnimalMedicalRecord(medicalRecordDTO);

            return resultUpdate;
        }

        public Result SendToTheDb(MedicalRecordDTO newMedicalRecord)
        {
            return _medicalRecordRepository.InsertMedicalRecord(newMedicalRecord);
        }

        public MedicalRecordDTO ConvertToMedicalRecordDTO(MedicalRecord medicalRecord)
        {
            // this converts contract to ContractDTO
            return new MedicalRecordDTO(
                    medicalRecord.MedicalRecordID,
                    medicalRecord.AnimalID,
                    medicalRecord.RecordDate,
                    medicalRecord.Notes,
                    medicalRecord.Allergies,
                    medicalRecord.Diseases,
                    medicalRecord.Injuries,
                    medicalRecord.Operations,
                    medicalRecord.Medication,
                    medicalRecord.Dangerlevel.ToString()
                );
        }

        public MedicalRecord ConvertToMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            //Converts the role enum to a string 
            DangerEnum danger = (DangerEnum)Enum.Parse(typeof(DangerEnum), medicalRecordDTO.Dangerlevel);

            // this converts contract to ContractDTO
            return new MedicalRecord(
                    medicalRecordDTO.MedicalRecordID,
                    medicalRecordDTO.RecordDate,
                    medicalRecordDTO.Notes,
                    medicalRecordDTO.Allergies,
                    medicalRecordDTO.Diseases,
                    medicalRecordDTO.Injuries,
                    medicalRecordDTO.Operations,
                    medicalRecordDTO.Medication,
                    danger
                );
        }

    }
}

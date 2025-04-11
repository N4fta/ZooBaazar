using Data_Access;
using Logic;
using System;
using System.Collections.Generic;


namespace Logic
{
    public class AnimalManager
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly RelationshipManager _relationshipManager;
        private readonly IFeedingPlanRepository _feedingPlanRepository;
        private readonly FeedingPlanManager _feedingPlanManager;
        private readonly IMedicalRecordRepository _mediicalRecordRepository;
        private readonly MedicalRecordManager _medicalRecordManager;

        public AnimalManager(IAnimalRepository animalRepository, IRelationshipRepository relationshipRepository, IFeedingPlanRepository feedingPlanRepository, IMedicalRecordRepository mediicalRecordRepository)
        {
            _animalRepository = animalRepository;
            _relationshipRepository = relationshipRepository;
            _feedingPlanRepository = feedingPlanRepository;
            _mediicalRecordRepository = mediicalRecordRepository;

            _relationshipManager = new RelationshipManager(relationshipRepository);
            _feedingPlanManager = new FeedingPlanManager(feedingPlanRepository, animalRepository);
            _medicalRecordManager = new MedicalRecordManager(mediicalRecordRepository, animalRepository);
        }

        public List<Animal> LoadAnimalFromDataBase()
        {
            List<Animal> animals = new();
            List<AnimalDTO> animalDTOs = new();

            try
            {
                animalDTOs = _animalRepository.GetRecentAnimal(int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var animalDTO in animalDTOs)
            {
                animals.Add(ConvertToAnimal(animalDTO));
            }

            return animals;
        }

        public Animal GetAnimalByAnimalID(int animalID)
        {
            AnimalDTO animalDTO = _animalRepository.GetAnimalByAnimalID(animalID);
            return ConvertToAnimal(animalDTO);
        }

        public Result Add(Animal animal, int locationID, FeedingPlan feedingPlan, MedicalRecord medicalRecord)
        {
            AnimalDTO animalDTO = ConvertToAnimalDTO(animal, locationID);

            if (animal.Relationship != null)
            {
                Result resultrelationShip = _relationshipRepository.InsertRelationship(RelationshipManager.ConvertToRelationshipDTO(animal.Relationship));

                if (!resultrelationShip.Success)
                {
                    return resultrelationShip;
                }
            }

            if (animalDTO.FeedingPlans == null)
            {
                animalDTO.FeedingPlans = new List<FeedingPlanDTO>();
            }

            FeedingPlanDTO feedingPlanDTO = _feedingPlanManager.ConvertToFeedingPlanDTO(feedingPlan);

            animalDTO.FeedingPlans.Add(feedingPlanDTO);

            if (animalDTO.MedicalRecords == null)
            {
                animalDTO.MedicalRecords = new List<MedicalRecordDTO>();
            }
            MedicalRecordDTO medicalRecordDTO = _medicalRecordManager.ConvertToMedicalRecordDTO(medicalRecord);

            animalDTO.MedicalRecords.Add(medicalRecordDTO);

            Result resultanimal = _animalRepository.InsertAnimal(animalDTO);

            if (!resultanimal.Success)
            {
                return resultanimal;
            }
            return resultanimal;

            #region Old
            //int animalID = _animalRepository.GetAnimalID(animalDTO);

            //Result resultFeedingPlan = _feedingPlanRepository.InsertFeedingPlan(FeedingPlanManager.ConvertToFeedingPlanDTO(animal.FeedingPlan));

            //animal.FeedingPlan.FeedingPlanID = _feedingPlanRepository.GetAnimalFeedingPlan(animalID)[0].FeedingPlanID;
            //Result resultFoodAllergies = _feedingPlanRepository.InsertFoodAllergies(FeedingPlanManager.ConvertToFeedingPlanDTOWithFoodAllergy(animal.FeedingPlan));

            //Result resultMedicalRecord = _mediicalRecordRepository.InsertMedicalRecord(MedicalRecordManager.ConvertToMedicalRecordDTO(animal.MedicalRecord));

            //if (!resultFeedingPlan.Success)
            //{
            //    return resultFeedingPlan;
            //}
            //if (!resultFoodAllergies.Success)
            //{
            //    return resultFoodAllergies;
            //}
            //if (!resultMedicalRecord.Success)
            //{
            //    return resultMedicalRecord;
            //}
            //return resultanimal;
            #endregion
        }

        public Result Remove(Animal animal, int locationId = 0)
        {
            if (animal.Relationship != null)
            {
                _relationshipRepository.DeleteAnimalRelationship(RelationshipManager.ConvertToRelationshipDTO(animal.Relationship));
            }
            //if (animal.FeedingPlan != null)
            //{
            //   _feedingPlanRepository.DeleteAnimalFeedingPlan(_feedingPlanManager.ConvertToFeedingPlanDTO(animal.FeedingPlan));
            //}
            //if (animal.MedicalRecord != null)
            //{
            //    _mediicalRecordRepository.DeleteAnimalMedicalRecord(_medicalRecordManager.ConvertToMedicalRecordDTO(animal.MedicalRecord));
            //}

            Result resultAnimal = _animalRepository.DeleteAnimal(ConvertToAnimalDTO(animal, locationId));

            return resultAnimal;
        }

        public Result Update(Animal newAnimal, int locationID, FeedingPlan newFeedingPlan, MedicalRecord newMedicalRecord)
        {
            AnimalDTO animalDTO = ConvertToAnimalDTO(newAnimal, locationID);

            if (newAnimal.AnimalID == 0)
            {
                return Add(newAnimal, locationID, newFeedingPlan, newMedicalRecord);
            }

            //if (newAnimal.Relationship != null)
            //{
            //    Result resultRelationship = _relationshipRepository.UpdateAnimalRelationship(RelationshipManager.ConvertToRelationshipDTO(newAnimal.Relationship));

            //    if (!resultRelationship.Success && resultRelationship.Message != "Relationship already exists")
            //    {
            //        return resultRelationship;
            //    }
            //}

            return _animalRepository.UpdateAnimal(animalDTO);
        }

        public List<string> GetAllSpecies()
        {
            return _animalRepository.GetAllSpecies();
        }

        public Animal ConvertToAnimal(AnimalDTO newAnimalDTO)
        {
            BloodTypes bloodType = (BloodTypes)Enum.Parse(typeof(BloodTypes), newAnimalDTO.BloodType);

            List<Relationship> relationships = _relationshipManager.LoadAnimalRelationshipsFromDatabase();

            // Assuming an animal has one relationship
            Relationship relationship = relationships.FirstOrDefault(r => r.PrimaryAnimalID == newAnimalDTO.AnimalID);

            FeedingPlan feedingPlan = null;
            if (newAnimalDTO.FeedingPlans.Count != 0)
            {
                feedingPlan = _feedingPlanManager.ConvertToFeedingPlan(newAnimalDTO.FeedingPlans[0]);
            }

            MedicalRecord medicalRecord = null;
            if (newAnimalDTO.MedicalRecords.Count != 0)
            {
                medicalRecord = _medicalRecordManager.ConvertToMedicalRecord(newAnimalDTO.MedicalRecords[0]);
            }

            return new Animal(
                newAnimalDTO.AnimalID,
                newAnimalDTO.Name,
                newAnimalDTO.Species,
                bloodType,
                newAnimalDTO.Origin,
                relationship,
                newAnimalDTO.Birthday,
                newAnimalDTO.EntryZoo,
                newAnimalDTO.ExitZoo,
                feedingPlan,
                medicalRecord
            );
        }


        public AnimalDTO ConvertToAnimalDTO(Animal animal, int locationID)
        {
            return new AnimalDTO(
                animal.AnimalID,
                animal.Name,
                animal.Species,
                animal.BloodType.ToString(),
                animal.Origin,
                animal.Birthday,
                animal.EntryZoo,
                animal.ExitZoo,
                locationID
            );
        }
    }
}

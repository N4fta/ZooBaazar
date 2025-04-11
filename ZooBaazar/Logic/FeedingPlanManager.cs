using Data_Access;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;

namespace Logic
{
    public class FeedingPlanManager
    {
        public List<FeedingPlan> Plans;
        private readonly IFeedingPlanRepository _feedingPlanRepository;
        private readonly IAnimalRepository _animalRepository;


        public FeedingPlanManager(IFeedingPlanRepository ifdpRep, IAnimalRepository animalRepository)
        {
            Plans = new List<FeedingPlan>();
            _feedingPlanRepository = ifdpRep;
            _animalRepository = animalRepository;
        }

        public FeedingPlan LoadAnimalFeedingPlan(AnimalDTO animalDTO)
        {
            List<FeedingPlanDTO> feedingDTOs = _feedingPlanRepository.GetAnimalFeedingPlan(animalDTO.AnimalID);

            if (feedingDTOs.Count == 0)
            {
                return null;
            }
            return ConvertToFeedingPlan(feedingDTOs[0]);

        }

        public Result Add(FeedingPlan feedingPlan)
        {
            FeedingPlanDTO feedingPlanDTO = ConvertToFeedingPlanDTO(feedingPlan);

            Result reasultADD = SendFeedingPlanToTheDb(feedingPlanDTO);

            return reasultADD;
        }

        public Result Remove(FeedingPlan feedingPlan)
        {
            Result resultRemoveFeedingPlan = _feedingPlanRepository.DeleteAnimalFeedingPlan(ConvertToFeedingPlanDTO(feedingPlan));

            return resultRemoveFeedingPlan;
        }

        public Result Update(FeedingPlan newFeedingPlan)
        {
            FeedingPlanDTO feedingPlanDTO = ConvertToFeedingPlanDTO(newFeedingPlan);

            if (newFeedingPlan.FeedingPlanID == 0)
            {
                return Add(newFeedingPlan);
            }

            Result resultupdated = _feedingPlanRepository.UpdateAnimalFeedingPlan(feedingPlanDTO);

            return resultupdated;
        }

        public Result SendFeedingPlanToTheDb(FeedingPlanDTO newFeedingPlan)
        {
            return _feedingPlanRepository.InsertFeedingPlan(newFeedingPlan);
        }

        public FeedingPlanDTO ConvertToFeedingPlanDTO(FeedingPlan feedingPlan)
        {
            List<string> allergyNames = feedingPlan.FoodAllergies.Select(a => a.ToString()).ToList();

            return new FeedingPlanDTO(
                feedingPlan.FeedingPlanID,
                feedingPlan.AnimalID,
                feedingPlan.FavoriteFood,
                feedingPlan.DislikedFood,
                feedingPlan.Diet.ToString(),
                allergyNames, 
                feedingPlan.Notes,
                feedingPlan.CaloriesIntake,
                feedingPlan.Weight,
                feedingPlan.IdealWeight
            );
        }


        //public FeedingPlanDTO ConvertToFeedingPlanDTOWithFoodAllergy(FeedingPlan feedingPlan)
        //{
        //    // Check if foodAllergies is null or empty
        //    if (feedingPlan.FoodAllergies == null || feedingPlan.FoodAllergies.Count == 0)
        //    {
        //        return ConvertToFeedingPlanDTO(feedingPlan); // Fallback to the basic conversion
        //    }

        //    return new FeedingPlanDTO(
        //            feedingPlan.FeedingPlanID,
        //            feedingPlan.AnimalID,
        //            feedingPlan.FeedsPerDay,
        //            feedingPlan.FavoriteFood,
        //            feedingPlan.DislikedFood,
        //            feedingPlan.Diet.ToString(),
        //            feedingPlan.FoodAllergies.Select(fa => fa.ToString()).ToList(),
        //            feedingPlan.Notes,
        //            feedingPlan.CaloriesIntake,
        //            feedingPlan.Weight,
        //            feedingPlan.IdealWeight
        //        );
        //}


        public FeedingPlan ConvertToFeedingPlan(FeedingPlanDTO feedingPlanDTO)
        {
            DietEnum diet = (DietEnum)Enum.Parse(typeof(DietEnum), feedingPlanDTO.Diet);

            List<FoodAllergies> foodAllergies = new();
            foreach (string allergy in feedingPlanDTO.FoodAllergies)
            {
                foodAllergies.Add((FoodAllergies)Enum.Parse(typeof(FoodAllergies), allergy));
            }

            return new FeedingPlan(
                    feedingPlanDTO.FeedingPlanID,
                    feedingPlanDTO.FavoriteFood,
                    feedingPlanDTO.DislikedFood,
                    diet,
                    foodAllergies,
                    feedingPlanDTO.Notes,
                    feedingPlanDTO.CaloriesIntake,
                    feedingPlanDTO.Weight,
                    feedingPlanDTO.IdealWeight
                );
        }

    }
}

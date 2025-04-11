namespace Data_Access
{
    public interface IFeedingPlanRepository
    {
        Result InsertFeedingPlan(FeedingPlanDTO feedingPlanDTO);

        List<FeedingPlanDTO> GetAnimalFeedingPlan(int animalID);

        Result InsertFoodAllergies(FeedingPlanDTO feedingPlanDTO);

        List<string> GetFoodAllergies(int feedingPlanID);

        Result UpdateAnimalFeedingPlan(FeedingPlanDTO feedingPlanDTO);

        Result DeleteAnimalFeedingPlan(FeedingPlanDTO feedingPlanDTO);
    }
}
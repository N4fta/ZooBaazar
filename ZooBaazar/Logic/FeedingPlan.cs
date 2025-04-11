namespace Logic
{
    public class FeedingPlan
    {
        public int FeedingPlanID;
        public int AnimalID;
        public string FavoriteFood;
        public string DislikedFood;
        public DietEnum Diet;
        public List<FoodAllergies> FoodAllergies;
        public string Notes {get; set;}
        public double CaloriesIntake;
        public double Weight;
        public double IdealWeight;

        public FeedingPlan(int feedingPlanID, string favoriteFood, string dislikedFood, DietEnum diet, List<FoodAllergies> foodAllergies, string notes, double caloriesIntake, double weight, double idealWeight)
        {
            FeedingPlanID = feedingPlanID;
            FavoriteFood = favoriteFood;
            DislikedFood = dislikedFood;
            Diet = diet;
            FoodAllergies = foodAllergies;
            Notes = notes;
            CaloriesIntake = caloriesIntake;
            Weight = weight;
            IdealWeight = idealWeight;
        }

        public FeedingPlan(string favoriteFood, string dislikedFood, DietEnum diet, List<FoodAllergies> foodAllergies, string notes, double caloriesIntake, double weight, double idealWeight)
        {
            FeedingPlanID = 0;
            FavoriteFood = favoriteFood;
            DislikedFood = dislikedFood;
            Diet = diet;
            FoodAllergies = foodAllergies;
            Notes = notes;
            CaloriesIntake = caloriesIntake;
            Weight = weight;
            IdealWeight = idealWeight;
        }

        public FeedingPlan(int feedingPlanID, int animalID, string favoriteFood, string dislikedFood, DietEnum diet, List<FoodAllergies> foodAllergies, string notes, double caloriesIntake, double weight, double idealWeight)
        {
            FeedingPlanID = feedingPlanID;
            AnimalID = animalID;
            FavoriteFood = favoriteFood;
            DislikedFood = dislikedFood;
            Diet = diet;
            FoodAllergies = foodAllergies;
            Notes = notes;
            CaloriesIntake = caloriesIntake;
            Weight = weight;
            IdealWeight = idealWeight;
        }
    }
}

namespace Data_Access
{
    public class FeedingPlanDTO
    {
        public int FeedingPlanID { get; set; }
        public int AnimalID { get; set; }
        public string FavoriteFood { get; set; }
        public string DislikedFood { get; set; }
        public string Diet {  get; set; }
        public List<string> FoodAllergies {  get; set; }
        public string Notes {  get; set; }
        public double CaloriesIntake { get; set; }
        public double Weight {  get; set; }
        public double IdealWeight { get; set; }

        public FeedingPlanDTO(int feedingPlanID, int animalID, string favoriteFood, string dislikedFood, string diet, List<string> foodAllergies, string notes, double caloriesIntake, double weight, double idealWeight)
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
        public FeedingPlanDTO(int feedingPlanID, int animalID, string favoriteFood, string dislikedFood, string diet, string notes, double caloriesIntake, double weight, double idealWeight)
        {
            FeedingPlanID = feedingPlanID;
            AnimalID = animalID;
            FavoriteFood = favoriteFood;
            DislikedFood = dislikedFood;
            Diet = diet;
            Notes = notes;
            CaloriesIntake = caloriesIntake;
            Weight = weight;
            IdealWeight = idealWeight;
        }
    }
}

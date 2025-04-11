namespace Logic
{
    public enum WorkType
    {
        None,
        Caretaker,
        Trainer,
        Biologist,
        Veterinarian,
        Guide,
        Cook,
        Retail,
        Cleaner,
        Entertainer,
        Security,
        Maintenance,
        Administrator,
        Curator,
        Other
    }
    public enum ContractType
    {
        FixedTerm,
        Freelance,
        Temporary,
        Internship,
        Other
    }

    public enum DietEnum
    {
        None,
        Vegetarian,
        Meat_Eater,
        Fish_Only,
        Less_Food,
        More_Food
    }
    public enum DangerEnum
    {
        Safe,
        LowDanger,
        MediumDanger,
        HighDanger,
        ExtremeDanger
    }

    public enum FoodAllergies
    {
        Beef,
        Chicken,
        Dairy,
        Wheat,
        Soy,
        Eggs,
        Fish,
        Grain,
        Grass,
        Seeds,
        Nuts,
        Fruits,
        Hay,
        Feathers,
        Other
    }

    public enum HealtStateEnum
    {
        Healthy,
        Sick,
        Injured,
        Dying,
        Dead,
        Pregant,
        Unknown
    }
    public enum RepeatEnum
    {
        Never,
        Daily,
        Weekly,
        Monthly
        //Add numberfield for repeats per the choses repeatenum
    }

    public enum BloodTypes
    {
        A_POS,
        A_NEG,
        B_POS,
        B_NEG,
        AB_POS,
        AB_NEG,
        O_POS,
        O_NEG
    }
}

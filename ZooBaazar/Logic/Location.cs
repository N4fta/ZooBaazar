
namespace Logic
{
    public class Location
    {
        public int Id { get; }

        public string Name { get;  set; }

        public int Capacity { get;  set; }

        public int AnimalCount { get; set; }

        public string Description { get; set; }

        public DangerEnum Danger { get; set; }

        public string Species { get; set; }
        
        public List<Animal> Animals { get; set; }

        public Location(string name, int capacity, int animalCount, string description, DangerEnum? danger, string species, List<Animal> animals)
        {
            this.Id = 0;
            this.Name = name;
            this.Capacity = capacity;
            this.AnimalCount = animalCount;
            this.Description = description;
            this.Danger = (DangerEnum)danger;
            this.Species = species;
            this.Animals = animals;
        }
        public Location(int id, string name, int capacity, int animalCount, string description, DangerEnum? danger, string species, List<Animal> animals)
        {
            this.Id = id;
            this.Name = name;
            this.Capacity = capacity;
            this.AnimalCount = animalCount;
            this.Description = description;
            this.Danger = (DangerEnum)danger;
            this.Species = species;
            this.Animals = animals;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

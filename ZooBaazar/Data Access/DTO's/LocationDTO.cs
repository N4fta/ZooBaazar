namespace Data_Access
{
    public class LocationDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int AnimalCount { get; set; }

        public string Description { get; set; }

        public string Danger { get; set; }

        public string Species { get; set; }

        public List<AnimalDTO> Animals { get; set; }

        public LocationDTO(int id, string name, int capacity, int animalCount, string description, string danger, string species, List<AnimalDTO> animals)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            AnimalCount = animalCount;
            Description = description;
            Danger = danger;
            Species = species;
            Animals = animals;
        }
        public LocationDTO(string name, int capacity, int animalCount, string description, string danger, string species, List<AnimalDTO> animals)
        {
            Id = 0;
            Name = name;
            Capacity = capacity;
            AnimalCount = animalCount;
            Description = description;
            Danger = danger;
            Species = species;
            Animals = animals;
        }
    }
}

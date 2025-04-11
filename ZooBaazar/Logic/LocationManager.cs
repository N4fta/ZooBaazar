using Data_Access;
using System.Threading.Tasks;

namespace Logic
{
    public class LocationManager
    {
        private readonly ILocationRepository _locationRepository;
        private readonly AnimalManager _animalManager;
        private readonly IAnimalRepository _animalRepository;
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IFeedingPlanRepository _feedingPlanRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public LocationManager(ILocationRepository locationRepository, IAnimalRepository animalRepository, IRelationshipRepository relationshipRepository, IFeedingPlanRepository feedingPlanRepository, IMedicalRecordRepository medicalRecordRepository)
        {
            _locationRepository = locationRepository;
            _animalRepository = animalRepository;
            _relationshipRepository = relationshipRepository;
            _feedingPlanRepository = feedingPlanRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _animalManager = new AnimalManager(animalRepository, relationshipRepository, feedingPlanRepository, medicalRecordRepository);
        }
        public List<Location> LoadLocationFromDatabse()
        {
            List<Location> locations = new();
            List<LocationDTO> locationDTOs = new();

            try
            {
                locationDTOs = _locationRepository.LoadLocations(int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var locationDTO in locationDTOs)
            {
                locations.Add(ConvertToLocation(locationDTO));
            }

            return locations;
        }

        public int GetLocationID(string location)
        {
            return LoadLocationFromDatabse().Find(l => l.Name == location).Id;
        }

        public Result Add(Location location)
        {
            LocationDTO lcoationDTO = ConvertToLocationDTO(location);

            //AnimalDTO animalDTO = _animalManager.ConvertToAnimalDTO(animal, location.Id);

            Result resultLocation = SendLocationToTheDb(lcoationDTO);

            return resultLocation;
        }

        public Location? Find(int locationID)
        {
            return LoadLocationFromDatabse().Find(l => l.Id == locationID);
        }

        public Result Remove(Location location)
        {
            Result resultlocation = _locationRepository.DeleteLocation(ConvertToLocationDTO(location));

            return resultlocation;
        }

        public Result Update(Location location)
        {
            LocationDTO locationDTO = ConvertToLocationDTO(location);

            //AnimalDTO animalDTO = _animalManager.ConvertToAnimalDTO(animal, location.Id);

            if (location.Id == 0)
            {
                return Add(location);
            }

            Result resultupdate = _locationRepository.UpdateLocation(locationDTO);
            // Location is already updated
            return resultupdate;
        }

        public Result SendLocationToTheDb(LocationDTO newLocation)
        {
            return _locationRepository.InsertLocation(newLocation);
        }

        public Location ConvertToLocation(LocationDTO locationDTO)
        {
            // Find Animals with an ID in locations.Animals list
            List<Animal> animals = new();
            foreach (var animalDTO in locationDTO.Animals)
            {
                animals.Add(_animalManager.ConvertToAnimal(animalDTO));
            }

            // Converts string to enums
            DangerEnum dangerEnum = (DangerEnum)Enum.Parse(typeof(DangerEnum), locationDTO.Danger);

            return new Location(
                    locationDTO.Id,
                    locationDTO.Name,
                    locationDTO.Capacity,
                    locationDTO.AnimalCount,
                    locationDTO.Description,
                    dangerEnum,
                    locationDTO.Species,
                    animals
                );
        }
        public List<Location> ConvertToLocation(List<LocationDTO> locationDTOs)
        {
            List<Location> locations = new();
            foreach (var locationDTO in locationDTOs)
            {
                locations.Add(ConvertToLocation(locationDTO));
            }
            return locations;
        }

        public LocationDTO ConvertToLocationDTO(Location location)
        {
            if (location == null)
            {
                return null;
            }

            // Find Animals IDs and make a list
            List<AnimalDTO> animalDTOs = new();
            foreach (var animalDTO in location.Animals)
            {
                animalDTOs.Add(_animalManager.ConvertToAnimalDTO(animalDTO, location.Id));
            }

            // This converts a Location to LocationDTO
            return new LocationDTO(
                    location.Id,
                    location.Name,
                    location.Capacity,
                    location.AnimalCount,
                    location.Description,
                    location.Danger.ToString(),
                    location.Species,
                    animalDTOs
                );
        }
        public List<LocationDTO> ConvertToLocationDTO(List<Location> locations)
        {
            List<LocationDTO> locationDTOs = new();
            foreach (var location in locations)
            {
                locationDTOs.Add(ConvertToLocationDTO(location));
            }
            return locationDTOs;
        }
    }
}

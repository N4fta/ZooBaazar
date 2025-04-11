namespace Data_Access
{
    public interface IAnimalRepository
    {
        Result InsertAnimal(AnimalDTO animalDTO);

        List<AnimalDTO> GetRecentAnimal(int count);

        Result UpdateAnimal(AnimalDTO animalDTO);

        Result DeleteAnimal(AnimalDTO animalDTO);

        AnimalDTO GetAnimalByAnimalID(int animalID);

        int GetAnimalID(AnimalDTO animalDTO);

        List <string> GetAllSpecies();
    }
}

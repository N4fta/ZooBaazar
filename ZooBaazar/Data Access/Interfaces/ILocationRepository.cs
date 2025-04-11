namespace Data_Access
{
    public interface ILocationRepository
    {
        Result InsertLocation(LocationDTO locationDTO);

        List<LocationDTO> LoadLocations(int count);

        Result UpdateLocation(LocationDTO locationDTO);

        Result DeleteLocation(LocationDTO locationDTO);
    }
}

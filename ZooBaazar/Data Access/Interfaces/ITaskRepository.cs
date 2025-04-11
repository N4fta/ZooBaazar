using Data_Access;

namespace Data_Access
{
    public interface ITaskRepository
    {
        Result InsertTask(TaskDTO taskDTO);

        List<TaskDTO> LoadTasks();

        Result UpdateTask(TaskDTO taskDTO);

        Result DeleteTask(TaskDTO taskDTO);

    }
}

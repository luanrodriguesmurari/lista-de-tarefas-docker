using CRUD.Api.Models;

namespace CRUD.Api.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem> GetById(int id);
        Task Create(TaskItemDto taskItem);
        Task Update(int id, TaskItemDto taskItem);
        Task<bool> Delete(int id);
    }
}

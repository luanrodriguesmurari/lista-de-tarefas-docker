using CRUD.Api.Models;

namespace CRUD.Api.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem> GetById(int id);
        Task Add(TaskItem task);
        Task Update(TaskItem task);
        Task Delete(int id);
    }
}

using AutoMapper;
using CRUD.Api.Interfaces;
using CRUD.Api.Models;

namespace CRUD.Api.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskItemService> _logger;

        public TaskItemService(ITaskItemRepository repository, IMapper mapper, ILogger<TaskItemService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as tarefas.");
                return new List<TaskItem>();
            }
        }

        public async Task<TaskItem> GetById(int id)
        {
            try
            {
                return await _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar a tarefa com o ID {id}.");
                return null;
            }
        }

        public async Task Create(TaskItemDto taskItem)
        {
            try
            {
                var task = _mapper.Map<TaskItem>(taskItem);
                await _repository.Add(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar uma nova tarefa.");
            }
        }

        public async Task Update(int id, TaskItemDto taskItemDto)
        {
            try
            {
                var taskItem = await _repository.GetById(id);
                if (taskItem != null)
                {
                    taskItem.Name = taskItemDto.Name ?? taskItem.Name;
                    taskItem.Description = taskItemDto.Description ?? taskItem.Description;
                    taskItem.Completed = taskItemDto.Completed ?? taskItem.Completed;

                    await _repository.Update(taskItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar a tarefa com o ID {id}.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var task = await _repository.GetById(id);
                if (task == null)
                {
                    _logger.LogWarning($"Tarefa com o ID {id} não encontrada para exclusão.");
                    return false;
                }

                await _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir a tarefa com o ID {id}.");
                return false;
            }
        }
    }
}

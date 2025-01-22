using AutoMapper;
using CRUD.Api.Models;

namespace CRUD.Api.Mappers
{
    public class TaskItemMapperProfile : Profile
    {
        public TaskItemMapperProfile()
        {
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        }
    }
}

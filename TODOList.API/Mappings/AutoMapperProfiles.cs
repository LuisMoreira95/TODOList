using AutoMapper;
using TODOList.API.Models.DTO;
using TODOList.API.Models.Domain;

namespace TODOList.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<AddTodoRequestDto, Todo>().ReverseMap();
            CreateMap<UpdateTodoRequestDto, Todo>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}

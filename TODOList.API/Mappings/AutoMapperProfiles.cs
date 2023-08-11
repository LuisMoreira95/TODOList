using AutoMapper;
using TODOList.API.Models.DTO;
using TODOList.API.Models.Domain;

namespace TODOList.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            var toTodoDTO = CreateMap<Todo, TodoDto>()
                 .ForMember(dto => dto.Categories, todo => todo.MapFrom(t => t.Category_Todos.Select(ct => ct.Category)))
                 .ReverseMap();
            CreateMap<AddTodoRequestDto, Todo>().ReverseMap();
            CreateMap<UpdateTodoRequestDto, Todo>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}

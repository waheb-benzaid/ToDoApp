using AutoMapper;
using ToDoApp.TodoItems.Dtos;
using ToDoApp.TodoItems;

namespace ToDoApp;

public class ToDoAppApplicationAutoMapperProfile : Profile
{
    public ToDoAppApplicationAutoMapperProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
        CreateMap<CreateTodoItemDto, TodoItem>();
        CreateMap<UpdateTodoItemDto, TodoItem>();
    }
}

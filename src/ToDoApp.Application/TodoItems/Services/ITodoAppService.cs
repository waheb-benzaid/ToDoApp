using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.TodoItems.Dtos;
using Volo.Abp.Application.Services;

namespace ToDoApp.TodoItems.Services
{
    public interface ITodoAppService : IApplicationService
    {
        Task<List<TodoItemDto>> GetListAsync();
        Task<TodoItemDto> GetAsync(Guid id);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto input);
        Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input);
        Task DeleteAsync(Guid id);
    }
}
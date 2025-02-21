using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ToDoApp.TodoItems
{
    public interface ITodoAppService : IApplicationService
    {
        Task<List<TodoItemDto>> GetListAsync();
        Task<TodoItemDto> GetAsync(Guid id);
        Task<TodoItemDto> CreateAsync(CreateUpdateTodoItemDto input);
        Task<TodoItemDto> UpdateAsync(Guid id, CreateUpdateTodoItemDto input);
        Task DeleteAsync(Guid id);
    }
}
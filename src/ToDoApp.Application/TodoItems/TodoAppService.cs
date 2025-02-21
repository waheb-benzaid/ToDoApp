using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ToDoApp.TodoItems
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;

        public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _todoItemRepository.GetListAsync();
            return items
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted
                })
                .ToList();
        }

        public async Task<TodoItemDto> GetAsync(Guid id)
        {
            var item = await _todoItemRepository.GetAsync(id);
            return new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<TodoItemDto> CreateAsync(CreateUpdateTodoItemDto input)
        {
            var item = new TodoItem(GuidGenerator.Create(), input.Title, input.Description);
            item = await _todoItemRepository.InsertAsync(item);
            return new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<TodoItemDto> UpdateAsync(Guid id, CreateUpdateTodoItemDto input)
        {
            var item = await _todoItemRepository.GetAsync(id);

            if (input.Title != null)
            {
                item.Title = input.Title;
            }

            if (input.Description != null)
            {
                item.Description = input.Description;
            }

            if (input.IsCompleted.HasValue)
            {
                item.IsCompleted = input.IsCompleted.Value;
            }

            item = await _todoItemRepository.UpdateAsync(item);

            return new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }
    }
}
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.TodoItems.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace ToDoApp.TodoItems.Services
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;
        private readonly IDistributedCache<List<TodoItemDto>> _cache;

        public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository, IDistributedCache<List<TodoItemDto>> cache)
        {
            _todoItemRepository = todoItemRepository;
            _cache = cache;
        }

        public async Task<List<TodoItemDto>> GetListAsync()
        {
            // Define a unique cache key
            var cacheKey = "AllTodoItems";

            // Try to get the cached data
            return await _cache.GetOrAddAsync(
                cacheKey,
                async () => await GetTodoItemsFromDatabaseAsync(),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                }
            );
        }

        private async Task<List<TodoItemDto>> GetTodoItemsFromDatabaseAsync()
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

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
        {
            var item = new TodoItem(GuidGenerator.Create(), input.Title, input.Description);
           
            item = await _todoItemRepository.InsertAsync(item);

            await _cache.RemoveAsync("AllTodoItems");

            return new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input)
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

            await _cache.RemoveAsync("AllTodoItems");

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

            await _cache.RemoveAsync("AllTodoItems");
        }
    }
}
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.TodoItems.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace ToDoApp.TodoItems.Services
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;
        private readonly IDistributedCache<List<TodoItemDto>> _cache;
        private readonly ILogger<TodoAppService> _logger;

        public TodoAppService(
            IRepository<TodoItem, Guid> todoItemRepository,
            IDistributedCache<List<TodoItemDto>> cache,
            ILogger<TodoAppService> logger)
        {
            _todoItemRepository = todoItemRepository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<TodoItemDto>> GetListAsync()
        {
            _logger.LogInformation("Retrieving all to-do items from the database or cache.");
            var cacheKey = "AllTodoItems";
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
            try
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
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("To-do item with ID {Id} was not found.", id);

                throw new UserFriendlyException("The requested to-do item was not found.");
            }
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
        {
            _logger.LogInformation("Creating a new to-do item with title: {Title}", input.Title);

            var item = new TodoItem(GuidGenerator.Create(), input.Title, input.Description);

            item = await _todoItemRepository.InsertAsync(item);

            await _cache.RemoveAsync("AllTodoItems");

            _logger.LogInformation("Successfully created a new to-do item with ID: {Id}", item.Id);

            return ObjectMapper.Map<TodoItem, TodoItemDto>(item);
        }

        public async Task<TodoItemDto> UpdateAsync(Guid id, UpdateTodoItemDto input)
        {
            _logger.LogInformation("Updating to-do item with ID: {Id}", id);

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

            _logger.LogInformation("Successfully updated to-do item with ID: {Id}", item.Id);

            return ObjectMapper.Map<TodoItem, TodoItemDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting to-do item with ID: {Id}", id);

            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                _logger.LogWarning("To-do item with ID {Id} was not found.", id);
                throw new UserFriendlyException("The to-do item with the specified ID was not found.");
            }

            await _todoItemRepository.DeleteAsync(id);

            await _cache.RemoveAsync("AllTodoItems");

            _logger.LogInformation("Successfully deleted to-do item with ID: {Id}", id);
        }
    }
}
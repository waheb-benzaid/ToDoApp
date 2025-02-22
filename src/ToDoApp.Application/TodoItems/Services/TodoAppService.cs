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
using Volo.Abp.Validation;

namespace ToDoApp.TodoItems.Services
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;
        private readonly IDistributedCache<List<TodoItemDto>> _cache;
        private readonly ILogger<TodoAppService> _logger;
        private readonly IObjectValidator _objectValidator;

        public TodoAppService(
            IRepository<TodoItem, Guid> todoItemRepository,
            IDistributedCache<List<TodoItemDto>> cache,
            ILogger<TodoAppService> logger,
            IObjectValidator objectValidator)
        {
            _todoItemRepository = todoItemRepository;
            _cache = cache;
            _logger = logger;
            _objectValidator = objectValidator;
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
            var item = await _todoItemRepository.GetAsync(id);

            if (item == null)
            {
                throw new EntityNotFoundException(typeof(TodoItem), id);
            }

            return ObjectMapper.Map<TodoItem, TodoItemDto>(item);
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
        {
            _logger.LogInformation("Creating a new to-do item with title: {Title}", input.Title);

            await _objectValidator.ValidateAsync(input);

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

            if (item == null)
            {
                throw new EntityNotFoundException(typeof(TodoItem), id);
            }

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
                throw new EntityNotFoundException(typeof(TodoItem), id);
            }

            await _todoItemRepository.DeleteAsync(id);

            await _cache.RemoveAsync("AllTodoItems");
        }
    }
}
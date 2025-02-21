using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace ToDoApp.TodoItems
{
    public class TodoItem : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        protected TodoItem() { }

        public TodoItem(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = false;
        }
    }
}
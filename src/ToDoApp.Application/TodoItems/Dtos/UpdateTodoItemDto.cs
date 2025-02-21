namespace ToDoApp.TodoItems.Dtos
{
    public class UpdateTodoItemDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

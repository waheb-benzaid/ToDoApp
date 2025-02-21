using FluentValidation;
using ToDoApp.TodoItems.Dtos;

public class CreateTodoItemDtoValidator : AbstractValidator<CreateTodoItemDto>
{
    public CreateTodoItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(128).WithMessage("Title must be 128 characters or less.");

        RuleFor(x => x.Description)
            .MaximumLength(512).WithMessage("Description must be 512 characters or less.");
    }
}
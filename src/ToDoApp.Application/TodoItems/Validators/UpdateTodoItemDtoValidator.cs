using FluentValidation;
using ToDoApp.TodoItems.Dtos;

public class UpdateTodoItemDtoValidator : AbstractValidator<UpdateTodoItemDto>
{
    public UpdateTodoItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(128).WithMessage("Title must be 128 characters or less.")
            .When(x => x.Title != null);

        RuleFor(x => x.Description)
            .MaximumLength(512).WithMessage("Description must be 512 characters or less.")
            .When(x => x.Description != null);

        RuleFor(x => x.IsCompleted)
            .NotNull().WithMessage("IsCompleted is required.")
            .When(x => x.IsCompleted.HasValue);
    }
}
namespace Crud.DDD.Application.Features.User.Dtos
{
    public record UserDto
    {
        public required Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
    }
}

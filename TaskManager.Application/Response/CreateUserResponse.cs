namespace TaskManager.Application.Response
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}

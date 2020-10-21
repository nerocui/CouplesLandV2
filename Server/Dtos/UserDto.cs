namespace Server.Dtos
{
    public record UserDto
    {
        public string UserName { get; init; }
        public string Token { get; init; }
    }
}
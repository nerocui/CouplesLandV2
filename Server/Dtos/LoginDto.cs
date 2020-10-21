namespace Server.Dtos
{
    public record LoginDto
    {
        public string UserName { get; }
        public string Password { get; }
    }
}
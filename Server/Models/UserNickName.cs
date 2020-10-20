namespace Server.Models
{
    public record UserNickName
    {
        public AppUser AppUser { get; init; }
        public string AppUserId { get; init; }
        public AppUser NickNameUser { get; init; }
        public string NickNameUserId { get; set; }
        public string NickName { get; set; }
    }
}
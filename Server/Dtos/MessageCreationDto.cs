using Server.Models;

namespace Server.Dtos
{
    public record MessageCreationDto
    {
        public string RecipientUserName { get; init; }
        public MessageType MessageType { get; init; }
        public string Content { get; init; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public record Message
    {
        public string Id { get; init; }

        [Required]
        [DefaultValue(MessageType.Text)]
        public MessageType MessageType { get; init; }
        public string Content { get; init; }
        public AppUser Sender { get; init; }
        public string SenderId { get; init; }

        [Required]
        public string SenderUserName { get; init; }
        public AppUser Recipient { get; init; }
        public string RecipientId { get; init; }

        [Required]
        public string RecipientUserName { get; init; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; init; } = DateTime.UtcNow;
    }
}
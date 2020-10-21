using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserNickName> UserNickNames { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserNickName>()
                .HasOne(x => x.AppUser)
                .WithMany(appuser => appuser.UserNickNames)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserNickName>()
                .HasKey(x => new {x.AppUserId, x.NickNameUserId});
            builder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(sender => sender.MessagesSent)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(sender => sender.MessagesReceived)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
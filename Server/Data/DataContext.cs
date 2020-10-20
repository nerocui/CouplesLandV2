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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserNickName>()
                .HasOne(x => x.AppUser)
                .WithMany(appuser => appuser.UserNickNames)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserNickName>()
                .HasKey(x => new {x.AppUserId, x.NickNameUserId});
        }
    }
}
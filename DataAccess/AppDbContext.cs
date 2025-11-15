using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.PrivateMessage)
                .WithOne(pm => pm.Message)
                .HasForeignKey<PrivateMessage>(pm => pm.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.GroupMessage)
                .WithOne(gm => gm.Message)
                .HasForeignKey<GroupMessage>(gm => gm.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Messages)
                .WithOne(gm => gm.Group)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

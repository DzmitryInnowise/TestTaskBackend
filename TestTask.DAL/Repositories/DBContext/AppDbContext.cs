using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Repositories.Entities;

namespace TestTask.DAL.Repositories.DBContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Email property to be unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedBy);

            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.Updater)
                .WithMany(u => u.UpdatedTasks)
                .HasForeignKey(t => t.UpdatedBy);
        }
    }
}

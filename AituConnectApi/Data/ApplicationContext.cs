using AituConnectApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<University> Universities { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasMany(p => p.Subjects)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PostSubjects",
                    j => j
                        .HasOne<Subject>()
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade));

            builder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasOne(p => p.University)
                .WithMany(u => u.RelatedPosts)
                .HasForeignKey(p => p.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(u => u.University)
                .WithMany(u => u.Students)
                .HasForeignKey(u => u.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(u => u.Major)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.MajorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Application.Identitity;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<TestQuestion> TestsQuestion { get; set; }

        public DbSet<StudentTestAttempt> StudentTestAttempts { get; set; }

        public DbSet<StudentTestResult> StudentTestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new DomainUserConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTestResultConfiguration());
            //SeedIdentityData(modelBuilder);
        }

        private static void SeedIdentityData(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var adminId = Guid.NewGuid();

            modelBuilder.Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole
                {
                    Id = adminRoleId,
                    Name = ApplicationUserRole.Admin.ToString(),
                    ConcurrencyStamp = "1",
                    NormalizedName = ApplicationUserRole.Admin.ToString(),
                },
                    new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = ApplicationUserRole.Teacher.ToString(),
                    ConcurrencyStamp = "2",
                    NormalizedName = ApplicationUserRole.Teacher.ToString(),
                },
                    new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = ApplicationUserRole.Student.ToString(),
                    ConcurrencyStamp = "3",
                    NormalizedName = ApplicationUserRole.Student.ToString(),
                });

            var hasher = new PasswordHasher<ApplicationUser>();

            var appUser = new ApplicationUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedEmail = "admin",
                Email = "admin@gmail.com",
            };
            appUser.PasswordHash = hasher.HashPassword(appUser, "admin12345");

            modelBuilder.Entity<ApplicationUser>()
                .HasData(appUser);

            modelBuilder.Entity<IdentityUserRole<Guid>>()
               .HasData(new IdentityUserRole<Guid>
               {
                   RoleId = adminRoleId,
                   UserId = adminId,
               });
        }
    }
}

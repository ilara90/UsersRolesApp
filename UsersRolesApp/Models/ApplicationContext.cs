using Microsoft.EntityFrameworkCore;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Класс для взаимодействовать с базой данных 
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Связь с таблицей пользователей в базе данных
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Связь с таблицей ролей в базе данных
        /// </summary>
        public DbSet<Role> Roles { get; set; } = null!;

        /// <summary>
        /// Связь с таблицей пользователей и ролей, которые имеют пользователи в базе данных
        /// </summary>
        public DbSet<UsersRoles> UsersRoles { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
           .Entity<Role>()
           .HasMany(r => r.Users)
           .WithMany(u => u.Roles)
           .UsingEntity<UsersRoles>(
              j => j
               .HasOne(pt => pt.User)
               .WithMany(t => t.UsersRoles)
               .HasForeignKey(pt => pt.UserId),
           j => j
               .HasOne(pt => pt.Role)
               .WithMany(p => p.UsersRoles)
               .HasForeignKey(pt => pt.RoleId)
               );
        }
    }
}

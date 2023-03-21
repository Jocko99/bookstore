using Domain.Entities;
using EfDataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfDataAccess
{
    public class BookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-F0CU76S;Initial Catalog=BookStore;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                UserName = "test",
                Password = "test123",
                Role = Role.Admin
            };

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId,x.AuthorId});
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.Now;
                            e.ModifiedAt = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

    }
}

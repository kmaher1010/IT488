using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.WebApi.Services.LibraryRepository {
    public class LibraryDbContext : DbContext {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice" },
                new User { Id = 2, Name = "Bob" }

            );

            modelBuilder.Entity<Book>()
                .HasOne(b => b.User)
                .WithMany(u => u.CheckedOutBooks)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby" },
                new Book { Id = 2, Title = "To Kill a Mockingbird" }
            );
        }
    }

    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> CheckedOutBooks { get; set; } = new List<Book>();
    }

    public class Book {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? CheckedOutByUserId { get; set; }


        public int? UserId { get; set; } // indicates a book is checked out by user
        public User? User { get; set; } 

    }

}

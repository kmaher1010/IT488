using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.WebApi.Services.LibraryRepository {
    public interface ILibraryRepository {

        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User?> AddUser(User user);
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task<Book?> AddBook(Book book);
        Task<Book?> CheckOut(int userId, int bookId);
        Task<Book?> CheckIn(int bookId);
    }

    public class LibraryRepository: ILibraryRepository {
        private readonly LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext context) {
            _dbContext = context;
        }

        public async Task<List<User>> GetAllUsers() {
            var users =  await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserById(int id) {
            return await _dbContext.Users.Where(u=>u.Id ==id).FirstOrDefaultAsync();
        }

        public async Task<User?> AddUser(User user) {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<Book>> GetAllBooks() {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id) {
            return await _dbContext.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book?> AddBook(Book book) {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> CheckOut(int userId, int bookId) {
            var user = await _dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null) {
                return null;
            }
            var book = await _dbContext.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
            if (book == null) {
                return null;
            }
            book.CheckedOutByUserId = userId;
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> CheckIn(int bookId) {

            var book = await _dbContext.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
            if (book == null) {
                return null;
            }
            book.CheckedOutByUserId = null;
            await _dbContext.SaveChangesAsync();
            return book;
        }

    }

}

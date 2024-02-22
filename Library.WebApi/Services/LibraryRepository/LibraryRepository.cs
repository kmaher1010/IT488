using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.WebApi.Services.LibraryRepository {
    public class LibraryRepository {
        private readonly LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext context) {
            _dbContext = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers() {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id) {
            return await _dbContext.Users.Where(u=>u.Id ==id).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user) {
            await _dbContext.Users.AddAsync(user);
        }

    }

}

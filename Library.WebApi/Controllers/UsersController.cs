using Library.WebApi.Services.LibraryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.WebApi.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly ILibraryRepository _libraryRepository;
        public UsersController(ILibraryRepository libraryRepository) {
            _libraryRepository = libraryRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> GetAll() {
            return Ok( await _libraryRepository.GetAllUsers());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(User user) {
            return Ok(await _libraryRepository.AddUser(user));
        }
    }
}

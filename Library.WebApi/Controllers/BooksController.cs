using Library.WebApi.Services.LibraryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers {
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase {
        private readonly ILibraryRepository _libraryRepository;
        public BooksController(ILibraryRepository libraryRepository) {
            _libraryRepository = libraryRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Book>>> GetAll() {
            return Ok(await _libraryRepository.GetAllBooks());
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Add(Book book) {
            return Ok(await _libraryRepository.AddBook(book));
        }


        [HttpPost("checkout")]
        public async Task<ActionResult<Book>> Checkout(BookCheckoutRequest request) {
            return Ok(await _libraryRepository.CheckOut(request.UserId, request.BookId));
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<Book>> Checkin(BookCheckinRequest request) {
            return Ok(await _libraryRepository.CheckIn(request.UserId, request.BookId));
        }
    }

    
}

namespace Library.WebApi.Services.LibraryRepository {
    public class BookCheckoutRequest {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }

    public class BookCheckinRequest {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }


}

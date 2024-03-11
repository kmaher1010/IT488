using Library.UI.AppFiles;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI.Services.LibraryService {

    public class User {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Book {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? CheckedOutByUserId { get; set; }

    }

    public interface ILibraryService {
        Task<List<User>> GetUsers();
        Task<User> AddNewUser(string name);

        Task<List<Book>> GetBooks();
        Task<Book> AddNewBook(string title);
    }
    public class LibraryService : ILibraryService {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly IOptions<AppSettings> _appSettings;

        public LibraryService(HttpClient httpClient, IOptions<AppSettings> appSettings) {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _baseAddress = appSettings.Value.LibraryApiUrl;
        }

        public async Task<List<User>> GetUsers() {
            var response = await _httpClient.GetAsync($"{_baseAddress}/api/users/all");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<User>>(data);
        }

        public async Task<User> AddNewUser(string name) {
            var newUser = new User { Name = name };
            var content = new StringContent(JsonConvert.SerializeObject(newUser), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/users", content);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(data);
        }

        public async Task<List<Book>> GetBooks() {
            var response = await _httpClient.GetAsync($"{_baseAddress}/api/books/all");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Book>>(data);
        }

        public async Task<Book> AddNewBook(string title) {
            var newBook = new Book { Title = title };
            var content = new StringContent(JsonConvert.SerializeObject(newBook), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/books", content);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Book>(data);
        }
    }
}

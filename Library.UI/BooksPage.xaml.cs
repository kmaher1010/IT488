using Library.UI.Services.LibraryService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Library.UI {
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page {

        public ObservableCollection<Book> Books { get; set; }
        private ILibraryService _libraryService;
        public BooksPage(ILibraryService libraryService) {
            InitializeComponent();
            _libraryService = libraryService;
            List<Book> books = new List<Book>();
            var task = Task.Run(async () => { books = await _libraryService.GetBooks(); });
            task.Wait();
            Books = new ObservableCollection<Book>(books);
            BooksList.ItemsSource = Books;
        }

        private async void AddBook_Click(object sender, RoutedEventArgs e) {
            var inputDialog = new AddBookDialog();
            inputDialog.Owner = Window.GetWindow(this);
            if (inputDialog.ShowDialog() == true) {
                var newBook = await _libraryService.AddNewBook(inputDialog.Answer);
                Books.Add(newBook);
                BooksList.SelectedItem = newBook;
            }

        }
    }
}

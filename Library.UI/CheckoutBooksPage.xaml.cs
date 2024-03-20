using Library.UI.Services.LibraryService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.UI {
    /// <summary>
    /// Interaction logic for CheckoutBooksPage.xaml
    /// </summary>
    public partial class CheckoutBooksPage : Page {
        public ObservableCollection<Book> Books { get; set; }
        private ILibraryService _libraryService;
        private Book? _selectedBook;
        public List<User> _users = new List<User>();


        public CheckoutBooksPage(ILibraryService libraryService) {
            InitializeComponent();
            _libraryService = libraryService;

            var task = Task.Run(async () => { _users = await _libraryService.GetUsers(); });
            task.Wait();

            List<Book> books = new List<Book>();
            task = Task.Run(async () => { books = await _libraryService.GetBooks(); });
            task.Wait();
            books.ForEach(b => {
                if (b.CheckedOutByUserId.HasValue) {
                    b.CheckedOutByUserName = _users.FirstOrDefault(u => u.Id == b.CheckedOutByUserId)?.Name;
                    b.IsCheckedOut = true;
                }
            });
            Books = new ObservableCollection<Book>(books);
            BooksList.ItemsSource = Books;
        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _selectedBook = Books[BooksList.SelectedIndex];
        }

        private async void btnCheckout_Click(object sender, RoutedEventArgs e) {

            if (_selectedBook != null) {
                if (_selectedBook.IsCheckedOut) {
                    MessageBox.Show("Book is already checked out.");
                    return;
                }

                var selectUserDialog = new SelectUserDialog( _libraryService);
                selectUserDialog.Owner = Window.GetWindow(this);
                if (selectUserDialog.ShowDialog() == true) {
                    var newBook = await _libraryService.CheckoutBook(_selectedBook.Id, selectUserDialog._selectedUser.Id);
                    var update = Books.FirstOrDefault(b => b.Id == newBook.Id);
                    if (update != null) {
                        update.CheckedOutByUserId = newBook.CheckedOutByUserId;
                        if (update.CheckedOutByUserId.HasValue) {
                            update.CheckedOutByUserName = _users.FirstOrDefault(u => u.Id == update.CheckedOutByUserId)?.Name;
                            update.IsCheckedOut = true;
                        }
                    }
                    BooksList.Items.Refresh();

                }


            } else {
                MessageBox.Show("Please select a book to checkout.");
            }


        }
    }
}

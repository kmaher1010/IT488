
using Library.UI.Services.LibraryService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Library.UI {
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page {

        public ObservableCollection<User> Users { get; set; }
        private ILibraryService _libraryService;
        public UsersPage(ILibraryService libraryService) {
            InitializeComponent();
            _libraryService = libraryService;
            List<User> users = new List<User>();
            var task = Task.Run( async () => { users = await _libraryService.GetUsers(); });
            task.Wait();
            Users = new ObservableCollection<User>(users);
            UsersList.ItemsSource = Users;
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e) {
            var inputDialog = new AddUserDialog();
            if (inputDialog.ShowDialog() == true) {
                var newUser = await _libraryService.AddNewUser(inputDialog.Answer);
                Users.Add(newUser);
                UsersList.SelectedItem = newUser;
            }
        }
    }


}

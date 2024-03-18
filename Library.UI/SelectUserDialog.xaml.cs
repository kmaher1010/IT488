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
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.UI
{
    /// <summary>
    /// Interaction logic for SelectUserDialog.xaml
    /// </summary>
    public partial class SelectUserDialog : Window
    {
        public ObservableCollection<User> Users { get; set; }
        private ILibraryService _libraryService;
        public User? _selectedUser;

        public SelectUserDialog(ILibraryService libraryService) {
            InitializeComponent();
            _libraryService = libraryService;
            List<User> users = new List<User>();
            var task = Task.Run(async () => { users = await _libraryService.GetUsers(); });
            task.Wait();
            Users = new ObservableCollection<User>(users);
            UserListBox.ItemsSource = Users;
        }

        private void SelectUser_Click(object sender, RoutedEventArgs e) {
            _selectedUser = (User)UserListBox.SelectedItem;
            if (_selectedUser != null) {
                this.DialogResult = true;
            } else {
                MessageBox.Show("No user selected.");
            }
        }


    }
}

using System.Windows;

namespace Library.UI {
    /// <summary>
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window {
        public string Answer { get; private set; }

        public AddUserDialog() {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
            Answer = txtUserName.Text;
            this.DialogResult = true;
        }
    }
}

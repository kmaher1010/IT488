using System.Windows;

namespace Library.UI {
    /// <summary>
    /// Interaction logic for AddBookDialog.xaml
    /// </summary>
    public partial class AddBookDialog : Window {
        public string Answer { get; private set; }

        public AddBookDialog() {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
            Answer = txtBookTitle.Text;
            this.DialogResult = true;
        }
    }
}

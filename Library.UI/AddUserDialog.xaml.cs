using System;
using System.Collections.Generic;
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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    public interface IMainWindow {
        void Close(Page page);
        void UpdateStatus(string text);
        void Navigate(Page page);
        void Show();
    }
    public partial class MainWindow : Window, IMainWindow {
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider) {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        public void Close(Page page) {
            mainFrame.Content = null;
        }

        public void Navigate(Page page) {
            UpdateStatus(string.Empty);
            mainFrame.Content = page;
        }

        public void UpdateStatus(string text) {
            lbStatus.Content = text;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e) {

            var page = _serviceProvider.GetRequiredService<UsersPage>();
            Navigate(page);
        }

        private void Window_Closed(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e) {
            var page = _serviceProvider.GetRequiredService<BooksPage>();
            Navigate(page);
        }
    }
}

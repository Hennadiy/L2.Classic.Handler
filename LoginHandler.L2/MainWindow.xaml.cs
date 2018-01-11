using LoginHandler.L2.Engine;
using LoginHandler.L2.Models;
using System.Windows;
using System.Windows.Controls;

namespace LoginHandler.L2
{
    public partial class MainWindow : Window
    {
        private readonly IEngine _engine;

        public MainWindow(IEngine engine)
        {
            _engine = engine;

            InitializeComponent();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            AccountsList.ItemsSource = _engine.LoadAccounts();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var account = GetContextAccount(sender);

            _engine.Login(account);
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Length < 4 || PasswordTextBox.Text.Length < 4)
            {
                MessageBox.Show("Login and password should have at least 4 symbols.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AccountsList.ItemsSource = _engine.AddAccount(LoginTextBox.Text, PasswordTextBox.Text);

            LoginTextBox.Text = "";
            PasswordTextBox.Text = "";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var account = GetContextAccount(sender);

            AccountsList.ItemsSource = _engine.DeleteAccount(account);
        }

        private static Account GetContextAccount(object sender)
        {
            return (sender as Button).DataContext as Account;
        }
    }
}

using LoginHandler.L2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace LoginHandler.L2.Engine
{
    public interface IEngine
    {
        ObservableCollection<Account> AddAccount(string login, string password);
        ObservableCollection<Account> DeleteAccount(Account account);
        ObservableCollection<Account> LoadAccounts();
        void Login(Account account);
    }

    public class LoginEngine : IEngine
    {
        private const string FILE_NAME = "accounts.json";

        private ObservableCollection<Account> _accounts;

        public LoginEngine()
        {
            _accounts = new ObservableCollection<Account>();
        }

        public ObservableCollection<Account> AddAccount(string login, string password)
        {
            _accounts.Add(new Account
            {
                Login = login,
                Password = password
            });

            SaveData();

            return _accounts;
        }

        public ObservableCollection<Account> DeleteAccount(Account account)
        {
            _accounts.Remove(account);

            SaveData();

            return _accounts;
        }

        public ObservableCollection<Account> LoadAccounts()
        {
            if (!File.Exists(FILE_NAME))
            {
                return new ObservableCollection<Account>();
            }

            var json = File.ReadAllText(FILE_NAME);
            _accounts = JsonConvert.DeserializeObject<ObservableCollection<Account>>(json);

            return _accounts;
        }

        public void Login(Account account)
        {
            throw new NotImplementedException();
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(_accounts);
            File.WriteAllText(FILE_NAME, json);
        }
    }
}

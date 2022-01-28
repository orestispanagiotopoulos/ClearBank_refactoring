using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services.Factory
{
    public class AccountRepositoryFactory : IAccountRepositoryFactory
    {
        private const string Backup = "Backup";

        public IAccountRepository GetAccountRepository(string storeType)
        {
            if (storeType == Backup)
            {
                return new BackupAccountRepository();
            }
            return new AccountRepository();
        }
    }
}

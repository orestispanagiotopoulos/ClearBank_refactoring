//using ClearBank.DeveloperTest.Data;
//using ClearBank.DeveloperTest.Services.Factory;
//using ClearBank.DeveloperTest.Types;

//namespace ClearBank.DeveloperTest.Services
//{
//    public class AccountService : IAccountService
//    {
//        public readonly IConfigService _configService;
//        public readonly IAccountRepositoryFactory _accountRepositoryFactory;

//        public AccountService(IConfigService configService, IAccountRepositoryFactory accountDataStoreFactory)
//        {
//            _configService = configService;
//            _accountRepositoryFactory = accountDataStoreFactory;
//        }
//        public Account GetAccount(string accountNumber)
//        {
//            return GetAccountRepositoryFactory().GetAccount(accountNumber);
//        }

//        public void UpdateAccount(Account account)
//        {
//            GetAccountRepositoryFactory().UpdateAccount(account);
//        }

//        private IAccountRepository GetAccountRepositoryFactory()
//        {
//            return _accountRepositoryFactory.GetAccountRepository(_configService.DataStoreType);
//        }
//    }
//}

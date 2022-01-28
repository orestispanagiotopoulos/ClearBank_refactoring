using ClearBank.DeveloperTest.Services.Factory;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IAccountRepositoryFactory _accountRepositoryFactory;
        public readonly IConfigService _config;
        
        public PaymentService(IAccountRepositoryFactory accountRepositoryFactory, IConfigService config)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
            _config = config;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountRepository = _accountRepositoryFactory.GetAccountRepository(_config.DataStoreType);
            var account = accountRepository.GetAccount(request.DebtorAccountNumber);

            if(account == null)
            {
                return new MakePaymentResult { Success = false};
            }

            if (account.Pay(request.Amount, request.PaymentScheme))
            {
                accountRepository.UpdateAccount(account);
                return new MakePaymentResult { Success = true };
            }

            return new MakePaymentResult { Success = false };
        }
    }
}

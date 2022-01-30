using ClearBank.DeveloperTest.Services.Factory;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IAccountRepositoryFactory _accountRepositoryFactory;
        public readonly IPaymentSchemeValidator _paymentSchemeValidator;
        public readonly IConfigService _config;
        
        public PaymentService(IAccountRepositoryFactory accountRepositoryFactory, 
                              IPaymentSchemeValidator paymentSchemeValidator, 
                              IConfigService config)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
            _paymentSchemeValidator = paymentSchemeValidator;
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

            var isValidPayment = _paymentSchemeValidator.Validate(request.PaymentScheme, request.Amount, account);

            if (isValidPayment)
            {
                account.Debit(request.Amount);
                // account.Balance -= request.Amount;

                accountRepository.UpdateAccount(account);
            }

            return new MakePaymentResult { Success = isValidPayment };
        }
    }
}

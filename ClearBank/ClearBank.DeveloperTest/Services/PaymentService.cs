using ClearBank.DeveloperTest.Services.Factory;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        // public readonly IAccountService _accountService;
        // public readonly IPaymentValidatorFactory _paymentValidatorFactory;
        public readonly IAccountRepositoryFactory _accountRepositoryFactory;
        public readonly IConfigService _config;
        
        public PaymentService( // IAccountService accountService, 
                              // IPaymentValidatorFactory paymentValidatorFactory, 
                              IAccountRepositoryFactory accountRepositoryFactory,
                              IConfigService config
                                )
        {
            // _accountService = accountService;
            // _paymentValidatorFactory = paymentValidatorFactory;
            _accountRepositoryFactory = accountRepositoryFactory;
            _config = config;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            //var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            //Account account = null;

            //if (dataStoreType == "Backup")
            //{
            //    var accountDataStore = new BackupAccountDataStore();
            //    account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            //}
            //else
            //{
            //    var accountDataStore = new AccountDataStore();
            //    account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            //}

            // var account = _accountService.GetAccount(request.DebtorAccountNumber);

            var accountRepository = _accountRepositoryFactory.GetAccountRepository(_config.DataStoreType); // .GetAccount(request.DebtorAccountNumber) // ()_configService.DataStoreType
            var account = accountRepository.GetAccount(request.DebtorAccountNumber);

            if(account == null)
            {
                return new MakePaymentResult { Success = false};
            }

            //var paymentValidator = _paymentValidatorFactory.Create(request.PaymentScheme);
            //var result = paymentValidator.Validate(account, request.Amount);


            //var result = new MakePaymentResult();

            //switch (request.PaymentScheme)
            //{
            //    case PaymentScheme.Bacs:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            //        {
            //            result.Success = false;
            //        }
            //        break;

            //    case PaymentScheme.FasterPayments:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            //        {
            //            result.Success = false;
            //        }
            //        else if (account.Balance < request.Amount)
            //        {
            //            result.Success = false;
            //        }
            //        break;

            //    case PaymentScheme.Chaps:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            //        {
            //            result.Success = false;
            //        }
            //        else if (account.Status != AccountStatus.Live)
            //        {
            //            result.Success = false;
            //        }
            //        break;
            //}

            if (account.Pay(request.Amount, request.PaymentScheme))
            {
                accountRepository.UpdateAccount(account);
                return new MakePaymentResult { Success = true };
            }

            return new MakePaymentResult { Success = false };
        }
    }
}

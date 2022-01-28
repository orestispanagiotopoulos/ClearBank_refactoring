//using ClearBank.DeveloperTest.Types;

//namespace ClearBank.DeveloperTest.PaymentValidation
//{
//    class ChapsPaymentSchemeValidator : IPaymentSchemeValidator
//    {
//        public MakePaymentResult Validate(Account account, decimal requestAmount)
//        {
//            var result = new MakePaymentResult();
//            if (account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
//                account.Status == AccountStatus.Live)
//            {
//                result.Success = true;
//            }
//            return result;
//        }
//    }
//}

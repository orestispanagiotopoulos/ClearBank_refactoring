//using ClearBank.DeveloperTest.Types;

//namespace ClearBank.DeveloperTest.PaymentValidation
//{
//    public class FasterPaymentValidator : IPaymentSchemeValidator
//    {
//        public MakePaymentResult Validate(Account account, decimal requestAmount)
//        {
//            var result = new MakePaymentResult();
//            if (account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
//                requestAmount != 0 && account.Balance > requestAmount)
//            {
//                result.Success = true;
//            }
//            return result;
//        }
//    }
//}

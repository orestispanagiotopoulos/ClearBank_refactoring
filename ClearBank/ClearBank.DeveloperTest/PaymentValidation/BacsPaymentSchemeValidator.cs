//using ClearBank.DeveloperTest.Types;

//namespace ClearBank.DeveloperTest.PaymentValidation
//{
//    public class BacsPaymentSchemeValidator : IPaymentSchemeValidator
//    {
//        public MakePaymentResult Validate(Account account, decimal requestAmount)
//        {
//            var result = new MakePaymentResult();
//            if (account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
//            {
//                result.Success = true;
//            }
//            return result;
//        }
//    }
//}

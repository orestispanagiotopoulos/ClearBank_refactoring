//using ClearBank.DeveloperTest.PaymentValidation;
//using ClearBank.DeveloperTest.Types;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ClearBank.DeveloperTest.Services.Factory
//{
//    public class PaymentValidatorFactory : IPaymentValidatorFactory
//    {
//        public IPaymentSchemeValidator Create(PaymentScheme paymentScheme)
//        {
//            switch (paymentScheme)
//            {
//                case PaymentScheme.Bacs:
//                    return new BacsPaymentSchemeValidator();

//                case PaymentScheme.FasterPayments:
//                    return new FasterPaymentValidator();

//                case PaymentScheme.Chaps:
//                    return new ChapsPaymentSchemeValidator();

//                default: 
//                    throw new ArgumentException("Invalid payment scheme");
//            }
//        }
//    }
//}

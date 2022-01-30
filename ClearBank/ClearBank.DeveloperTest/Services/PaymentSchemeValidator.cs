using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentSchemeValidator : IPaymentSchemeValidator
    {
        public bool Validate(PaymentScheme paymentScheme, decimal amount, Account account)
        {
            if (account == null)
                return false;

            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
                case PaymentScheme.FasterPayments:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && account.Balance >= amount;
                case PaymentScheme.Chaps:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && account.Status == AccountStatus.Live;
                default:
                    throw new ArgumentException("Unknown payment scheme");
            }
        }
    }
}

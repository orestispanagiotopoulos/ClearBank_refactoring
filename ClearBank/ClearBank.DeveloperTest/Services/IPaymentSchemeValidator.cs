using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentSchemeValidator
    {
        bool Validate(PaymentScheme paymentScheme, decimal requestAmount, Account account);
    }
}

namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        public bool Pay(decimal amount, PaymentScheme scheme)
        {
            if(CanPay(amount, scheme))
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        private bool CanPay(decimal amount, PaymentScheme scheme)
        {
            switch (scheme)
            {
                case PaymentScheme.Bacs:
                    return this.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);

                case PaymentScheme.FasterPayments:
                    return this.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && this.Balance >= amount;

                case PaymentScheme.Chaps:
                    return this.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && this.Status == AccountStatus.Live;

                default: return false;
            }
        }
    }
}

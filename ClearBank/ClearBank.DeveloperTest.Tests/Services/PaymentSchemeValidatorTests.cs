using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentSchemeValidatorTests
    {
        
        private PaymentSchemeValidator _paymentSchemeValidator;

        [SetUp]
        public void SetUp()
        {
            _paymentSchemeValidator = new PaymentSchemeValidator();
        }

        [Test]
        public void Validate_WhenAccountIsNull_ThenReturnFalse()
        {
            // Arrange - Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Bacs, 0, null);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validate_WhenSchemeIsBacsAndAccountSupportsIt_ThenReturnTrue()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Bacs, 0, account);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validate_WhenSchemeIsBacsAndAccountDoesNotSupportIt_ThenReturnFalse()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Bacs, 0, account);

            // Assert
            Assert.AreEqual(false, result);
        }


        [Test]
        public void Validate_WhenSchemeIsFasterPaymetsAndAccountSupportsItAndEnoughMoney1_ThenReturnTrue()
        {
            // Arrange
            decimal amount = 100.5m;
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100.5m
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.FasterPayments, amount, account);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validate_WhenSchemeIsFasterPaymetsAndAccountSupportsItAndEnoughMoney2_ThenReturnTrue()
        {
            // Arrange
            decimal amount = 100.5m;
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments,
                Balance = 101m
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.FasterPayments, amount, account);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validate_WhenSchemeIsFasterPaymetsAndAccountSupportsItButNotEnoughMoney_ThenReturnFalse()
        {
            // Arrange
            decimal amount = 100.5m;
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100.4m
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.FasterPayments, amount, account);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validate_WhenSchemeIsFasterPaymetsAndAccountDoesNotSupportIt_ThenReturnFalse()
        {
            // Arrange
            decimal amount = 100;
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                Balance = 100
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.FasterPayments, amount, account);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validate_WhenSchemeIsChampAndAccountSupportsItAndStatusLive_ThenReturnTrue()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Chaps, 0, account);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validate_WhenSchemeIsChampAndAccountSupportsItButStatusIsNotLive_ThenReturnTrue()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.InboundPaymentsOnly
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Chaps, 0, account);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validate_WhenSchemeIsChampAndAccountDoesNotSupportIt_ThenReturnTrue()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs,
                Status = AccountStatus.Live
            };

            // Act
            var result = _paymentSchemeValidator.Validate(PaymentScheme.Chaps, 0, account);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}

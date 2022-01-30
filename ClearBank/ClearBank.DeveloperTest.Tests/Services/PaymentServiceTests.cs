using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Factory;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private Mock<IAccountRepositoryFactory> _accountRepositoryFactory;
        private Mock<IPaymentSchemeValidator> _paymentSchemeValidator;
        private Mock<IConfigService> _config;
        private Mock<IAccountRepository> _accountRepository;

        [SetUp]
        public void SetUp()
        {
            _accountRepositoryFactory = new Mock<IAccountRepositoryFactory>();
            _paymentSchemeValidator = new Mock<IPaymentSchemeValidator>();
            _config = new Mock<IConfigService>();
            _accountRepository = new Mock<IAccountRepository>();
        }

        [Test]
        public void MakePayment_WhenReturnedAccountIsNull_ThenPaymentFail()
        {
            // Arrange
            var paymentService = new PaymentService(_accountRepositoryFactory.Object, _paymentSchemeValidator.Object, _config.Object);

            _accountRepositoryFactory.Setup(x => x.GetAccountRepository(It.IsAny<string>())).Returns(_accountRepository.Object);
            _accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns((Account)null);

            // Act
            var result = paymentService.MakePayment(new MakePaymentRequest());

            // Assert
            Assert.IsFalse(result.Success);

            _accountRepositoryFactory.Verify(x => x.GetAccountRepository(It.IsAny<string>()), Times.Once);
            _accountRepository.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void MakePayment_WhenPaymentIsValid_ThenPaymentSucceed()
        {
            // Arrange
            var account = new Account { Balance = 10 };
            decimal paymentAmount = 8m;
            var paymentService = new PaymentService(_accountRepositoryFactory.Object, _paymentSchemeValidator.Object, _config.Object);

            _accountRepositoryFactory.Setup(x => x.GetAccountRepository(It.IsAny<string>())).Returns(_accountRepository.Object);
            _accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _accountRepository.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
            _paymentSchemeValidator.Setup(x => x.Validate(It.IsAny<PaymentScheme>(), It.IsAny<decimal>(), It.IsAny<Account>())).Returns(true);

            // Act
            var result = paymentService.MakePayment(new MakePaymentRequest { Amount = paymentAmount });

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, account.Balance);

            _accountRepositoryFactory.Verify(x => x.GetAccountRepository(It.IsAny<string>()), Times.Once);
            _accountRepository.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Once);
            _accountRepository.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Once);
            _paymentSchemeValidator.Verify(x => x.Validate(It.IsAny<PaymentScheme>(), It.IsAny<decimal>(), It.IsAny<Account>()), Times.Once);
        }

        [Test]
        public void MakePayment_WhenPaymentIsNotValid_ThenPaymentFail()
        {
            // Arrange
            var account = new Account { Balance = 10 };
            decimal paymentAmount = 8m;
            var paymentService = new PaymentService(_accountRepositoryFactory.Object, _paymentSchemeValidator.Object, _config.Object);

            _accountRepositoryFactory.Setup(x => x.GetAccountRepository(It.IsAny<string>())).Returns(_accountRepository.Object);
            _accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _accountRepository.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
            _paymentSchemeValidator.Setup(x => x.Validate(It.IsAny<PaymentScheme>(), It.IsAny<decimal>(), It.IsAny<Account>())).Returns(false);

            // Act
            var result = paymentService.MakePayment(new MakePaymentRequest { Amount = paymentAmount });

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(10, account.Balance);

            _accountRepositoryFactory.Verify(x => x.GetAccountRepository(It.IsAny<string>()), Times.Once);
            _accountRepository.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Once);
            _accountRepository.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
            _paymentSchemeValidator.Verify(x => x.Validate(It.IsAny<PaymentScheme>(), It.IsAny<decimal>(), It.IsAny<Account>()), Times.Once);
        }
    }
}

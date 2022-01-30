using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Factory;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void MakePayment_WhenReturnedAccountIsNull_ThenPaymentFails()
        {
            // Arrange
            var paymentService = new PaymentService(_accountRepositoryFactory.Object, _paymentSchemeValidator.Object, _config.Object);

            _accountRepositoryFactory.Setup(x => x.GetAccountRepository(It.IsAny<string>())).Returns(_accountRepository.Object);
            _accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns((Account)null);

            // Act
            var result = paymentService.MakePayment(new MakePaymentRequest());

            // Assert
            Assert.IsFalse(result.Success);
        }
    }
}

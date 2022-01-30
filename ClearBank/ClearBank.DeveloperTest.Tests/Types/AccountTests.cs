using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Types
{
    public class AccountTests
    {
        [Test]
        public void Debit_WhenTryToDebitAccount_ThenCorrectBalance()
        {
            // Arrange
            var account = new Account
            {
                Balance = 10.5m
            };

            // Act
            account.Debit(5.5m);

            // Assert
            Assert.AreEqual(5, account.Balance);
        }

        [Test]
        public void Debit_WhenTryToDebitAccountWithNoMoney_ThenCorrectBalance()
        {
            // Arrange
            var account = new Account
            {
                Balance = 0m
            };

            // Act
            account.Debit(5m);

            // Assert
            Assert.AreEqual(-5, account.Balance);
        }
    }
}

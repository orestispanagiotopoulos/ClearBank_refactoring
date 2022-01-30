using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.Factory;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services.Factory
{
    public class AccountRepositoryFactoryTests
    {
        [Test]
        public void GetAccountRepository_WhenStoreTypeIsBackUp_ThenReturnBackupAccountRepository()
        {
            // Arrange
            var accountRepositoryFactory = new AccountRepositoryFactory();

            // Act
            var result = accountRepositoryFactory.GetAccountRepository("Backup");

            // Assert
            Assert.IsTrue(result is BackupAccountRepository);
        }

        [Test]
        public void GetAccountRepository_WhenStoreTypeIsNotBackUp_ThenReturnAccountRepository()
        {
            // Arrange
            var accountRepositoryFactory = new AccountRepositoryFactory();

            // Act
            var result = accountRepositoryFactory.GetAccountRepository("");

            // Assert
            Assert.IsTrue(result is AccountRepository);
        }

        [Test]
        public void GetAccountRepository_WhenStoreTypeIsNotBackupCaseSensitive_ThenReturnAccountRepository()
        {
            // Arrange
            var accountRepositoryFactory = new AccountRepositoryFactory();

            // Act
            var result = accountRepositoryFactory.GetAccountRepository("BACKUP");

            // Assert
            Assert.IsTrue(result is AccountRepository);
        }
    }
}

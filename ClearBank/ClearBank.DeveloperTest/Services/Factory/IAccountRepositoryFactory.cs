using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services.Factory
{
    public interface IAccountRepositoryFactory
    {
        IAccountRepository GetAccountRepository(string storeType);
    }
}

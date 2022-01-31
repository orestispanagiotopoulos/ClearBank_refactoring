using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigService : IConfigService
    {
        // Ideally this should be done at the composition root of the application
        public string DataStoreType => ConfigurationManager.AppSettings["DataStoreType"];
    }
}

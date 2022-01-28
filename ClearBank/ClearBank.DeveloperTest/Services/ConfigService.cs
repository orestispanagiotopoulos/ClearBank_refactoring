using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigService : IConfigService
    {
        public string DataStoreType => ConfigurationManager.AppSettings["DataStoreType"];
    }
}

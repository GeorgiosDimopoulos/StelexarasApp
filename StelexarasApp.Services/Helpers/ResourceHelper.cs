using System.Resources;

namespace StelexarasApp.Services.Helpers
{
    public class ResourceHelper
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("StelexarasApp.DataAccess.AppSettings", typeof(ResourceHelper).Assembly);

        public static string GetValue(string key)
        {
            return _resourceManager.GetString(key);
        }
    }
}

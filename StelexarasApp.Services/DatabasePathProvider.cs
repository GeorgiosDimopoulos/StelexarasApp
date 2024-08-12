using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetDatabasePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folderPath, "app.db");
        }
    }
}

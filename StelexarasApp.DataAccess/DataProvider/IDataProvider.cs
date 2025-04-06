namespace StelexarasApp.DataAccess.DataProvider;

public interface IDataProvider
{
    bool LoadSqlServerDbEntities();
    bool ConfigureDatabaseForCrossPlatform();
}

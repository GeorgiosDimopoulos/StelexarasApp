namespace StelexarasApp.DataAccess.DataProvider;

public interface IDataProvider
{
    void LoadSqlServerDbEntities();
    void ConfigureDatabaseForCrossPlatform();
}

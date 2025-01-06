using Microsoft.Extensions.DependencyInjection;

namespace StelexarasApp.DataAccess.DataProvider;

public interface IDataProvider
{
    bool CheckDbConnection(IServiceCollection services);
    void LoadDbEntities();
}

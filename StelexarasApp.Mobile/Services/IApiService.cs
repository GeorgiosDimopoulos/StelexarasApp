using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Services;

public interface IApiService
{
    Task<List<IStelexos>> GetStelexi();
    Task CreateStelexos(IStelexos stelexos);
    Task DeleteStelexos(int id);
    Task UpdateStelexos(IStelexos stelexos);
}

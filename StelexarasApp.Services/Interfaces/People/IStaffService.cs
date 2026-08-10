namespace StelexarasApp.Services.Interfaces.People;

public interface IStaffService<TCreate, TUpdate, TResponse>
{
    Task<bool> CreateStelexos(TCreate entity);
    Task<bool> UpdateStelexos(int id, TUpdate entity);
    Task<bool> DeleteStelexos(int id);
    Task<IEnumerable<TResponse>> GetStelexi(string? xwros, StelexosQueryParameters stelexosQueryParameters);
    Task<TResponse> GetStelexosByName(string n, StelexosQueryParameters stelexosQueryParameters);
    Task<TResponse> GetStelexosById(int id, StelexosQueryParameters stelexosQueryParameters);
    Task<bool> MoveOmadarxisToAnotherSkiniInService(int id, string skiniName);
}
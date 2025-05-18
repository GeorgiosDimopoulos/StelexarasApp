namespace StelexarasApp.Services.Interfaces.People;

public interface IStaffService<TCreate, TUpdate, TDelete, TResponse>
{
    Task<bool> CreateStelexos(TCreate entity, Thesi thesi);
    Task<bool> UpdateStelexos(int id, TUpdate entity);
    Task<bool> DeleteStelexos(TDelete entity);
    Task<IEnumerable<TResponse>> GetStelexi(Thesi thesi, string? xwros, StelexosQueryParameters stelexosQueryParameters);
    Task<TResponse> GetStelexosByName(Thesi thesi, string n, StelexosQueryParameters stelexosQueryParameters);
    Task<TResponse> GetStelexosById(int id, StelexosQueryParameters stelexosQueryParameters);
    Task<bool> MoveOmadarxisToAnotherSkiniInService(int id, string skiniName);
}
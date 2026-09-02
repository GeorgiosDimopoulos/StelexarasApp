using FluentResults;

namespace StelexarasApp.Services.Interfaces.People;

public interface IStaffService
{
    Task<IEnumerable<StelexosResponse>> GetStelexi(Thesi? thesi, StelexosQueryParameters stelexosQueryParameters);
    Task<IEnumerable<StelexosResponse>> GetStelexoiAnaXwro(string xwros, StelexosQueryParameters stelexosQueryParameters);
    Task<Result<StelexosResponse>> GetStelexosByName(Thesi thesi, string n, StelexosQueryParameters stelexosQueryParameters);
    Task<Result<StelexosResponse>> GetStelexosById(Thesi thesi, int id, StelexosQueryParameters stelexosQueryParameters);

    Task<Result> CreateStelexos(CreateStelexosRequest entity);
    Task<Result> UpdateStelexos(int id, UpdateStelexosRequest entity);
    Task<Result> DeleteStelexos(int id);
}
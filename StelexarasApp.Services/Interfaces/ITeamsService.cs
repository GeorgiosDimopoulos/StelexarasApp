using FluentResults;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Services.IServices;

public interface ITeamsService
{
    Task<Result> AddSkiniInService(CreateSkiniRequest skini);
    Task<Result> AddKoinotitaInService(CreateKoinotitaRequest koinotita);
    Task<Result> AddTomeasInService(CreateTomeasRequest tomeas);

    Task<Result> DeleteSkiniInService(int skiniId);
    Task<Result> DeleteKoinotitaInService(int koinotitaId);
    Task<Result> DeleteTomeasInService(string n);

    Task<Result> UpdateSkiniInService(int id, UpdateSkiniRequest skini);
    Task<Result> UpdateKoinotitaInService(int id, UpdateKoinotitaRequest koinotita);
    Task<Result> UpdateTomeaInService(string id, UpdateTomeasRequest tomeas);

    Task<IEnumerable<SkiniResponse>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters);
    Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaIdInService(SkiniQueryParameters? skiniQueryParameters, int id);
    Task<IEnumerable<SkiniResponse>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters? skiniQueryParameters);
    Task<Result<SkiniResponse>> GetSkiniByNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<Result<SkiniResponse>> GetSkiniByIdInService(SkiniQueryParameters? skiniQueryParameters, int id);

    Task<Result<KoinotitaResponse>> GetKoinotitaByIdInService(int id, KoinotitaQueryParameters koinotitaQueryParameters);
    Task<Result<KoinotitaResponse>> GetKoinotitaByNameInService(KoinotitaQueryParameters? koinotitaQueryParameters, string name);
    Task<IEnumerable<KoinotitaResponse>> GetAllKoinotitesInService(KoinotitaQueryParameters? koinotitaQueryParameters);
    Task<IEnumerable<KoinotitaResponse>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters? koinotitaQueryParameters, int id);    
    
    Task<IEnumerable<string>> GetAnwtatoiXwroi();
    Task<IEnumerable<TomeasResponse>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters);
    Task<Result<TomeasResponse>> GetTomeaByNameInService(TomeasQueryParameters? tomeasQueryParameters, string name);    
}

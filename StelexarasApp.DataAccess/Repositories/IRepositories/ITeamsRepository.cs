using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Library.QueryParameters;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface ITeamsRepository
    {
        Task<bool> HasData();

        Task<bool> AddSkiniInDb(Skini skini);
        Task<bool> AddKoinotitaInDb(Koinotita koinotita);
        Task<bool> AddTomeasInDb(Tomeas koinotita);

        Task<IEnumerable<Skini>> GetSkinesInDb(SkiniQueryParameters? parameters);
        Task<IEnumerable<Koinotita>> GetKoinotitesInDb(KoinotitaQueryParameters? parameters);
        Task<IEnumerable<Tomeas>> GetTomeisInDb(TomeasQueryParameters? parameters);

        Task<Skini> GetSkiniByNameInDb(SkiniQueryParameters? parameters, string name);
        Task<Tomeas> GetTomeaByNameInDb(TomeasQueryParameters? parameters, string name);
        Task<Koinotita> GetKoinotitaByNameInDb(KoinotitaQueryParameters? parameters, string name);

        Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInDb(SkiniQueryParameters? parameters, string Koinotitaname);
        Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(KoinotitaQueryParameters? parameters, int tomeaId);
        Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb(SkiniQueryParameters? parameters);

        Task<bool> UpdateKoinotitaInDb(int id, Koinotita koinotita);
        Task<bool> UpdateSkiniInDb(int id, Skini skini);
        Task<bool> UpdateTomeasInDb(int id, Tomeas tomeas);

        Task<bool> DeleteSkiniInDb(int id);
        Task<bool> DeleteKoinotitaInDb(int id);
        Task<bool> DeleteTomeasInDb(string id);
    }
}

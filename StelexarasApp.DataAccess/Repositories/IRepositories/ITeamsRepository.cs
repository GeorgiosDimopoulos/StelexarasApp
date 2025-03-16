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

        Task<IEnumerable<Skini>> GetSkinesInDb(SkiniQueryParameters skiniQueryParameters);
        Task<IEnumerable<Koinotita>> GetKoinotitesInDb();
        Task<IEnumerable<Tomeas>> GetTomeisInDb();

        Task<Skini> GetSkiniByNameInDb(string name);
        Task<Tomeas> GetTomeaByNameInDb(string name);
        Task<Koinotita> GetKoinotitaByNameInDb(string name);

        Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInDb(string Koinotitaname);
        Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(int tomeaId);
        Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb();

        Task<bool> UpdateKoinotitaInDb(Koinotita koinotita);
        Task<bool> UpdateSkiniInDb(Skini skini);
        Task<bool> UpdateTomeasInDb(Tomeas tomeas);

        Task<bool> DeleteSkiniInDb(int id);
        Task<bool> DeleteKoinotitaInDb(int id);
        Task<bool> DeleteTomeasInDb(string id);
    }
}

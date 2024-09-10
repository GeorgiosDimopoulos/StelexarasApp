using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface ITeamsRepository
    {
        Task<bool> AddSkiniInDb(Skini skini);

        Task<bool> AddKoinotitaInDb(Koinotita koinotita);

        Task<IEnumerable<Skini>> GetSkinesInDb();

        Task<Skini> GetSkiniByNameInDb(string name);

        Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInDb(string Koinotitaname);

        Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb();

        Task<IEnumerable<Koinotita>> GetKoinotitesInDb();

        Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(int tomeaId);

        Task<IEnumerable<Tomeas>> GetTomeisInDb();

        Task<bool> UpdateKoinotitaInDb(Koinotita koinotita);

        Task<bool> UpdateSkiniInDb(Skini skini);

        Task<bool> UpdateTomeasInDb(Tomeas tomeas);

        Task<bool> DeleteSkiniInDb(int id);
        
        Task<bool> DeleteKoinotitaInDb(int id);

        Task<bool> DeleteTomeasInDb(int id);
    }
}

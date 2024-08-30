using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface ITeamsRepository
    {
        Task<IEnumerable<Skini>> GetSkines();

        Task<Skini> GetSkiniByName(string name);

        Task<IEnumerable<Skini>> GetSkinesAnaKoinotita(int KoinotitaId);

        Task<IEnumerable<Skini>> GetSkinesEkpaideuomenon();

        Task<IEnumerable<Koinotita>> GetKoinotites();

        Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomea(int tomeaId);

        Task<IEnumerable<Tomeas>> GetTomeis();
    }
}

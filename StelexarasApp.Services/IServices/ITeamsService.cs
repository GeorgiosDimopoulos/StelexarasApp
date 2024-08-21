using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddPaidiInDbAsync(Paidi paidi);
        // Task<bool> AddEkpaideuomenosInDb(Ekpaideuomenos ekpaideuomenos);
        // Task<bool> AddKataskinotisInDb(Kataskinotis kataskinotis);

        Task<bool> DeletePaidiInDb(Paidi paidi);
        Task<bool> UpdatePaidiInDb(Paidi paidi);
        Task<IEnumerable<Skini>> GetSkines();

        Task<IEnumerable<Paidi>> GetPaidia(PaidiType type);

        Task<Skini> GetSkiniByName(string name);

        Task<Paidi> GetPaidiById(int id, PaidiType type);

        // Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);
    }
}

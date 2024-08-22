using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddPaidiInDbAsync(Paidi paidi);

        Task<bool> DeletePaidiInDb(Paidi paidi);
        Task<bool> UpdatePaidiInDb(Paidi paidi);
        Task<IEnumerable<Skini>> GetSkines();

        Task<IEnumerable<Paidi>> GetPaidia(PaidiType type);

        Task<Skini> GetSkiniByName(string name);

        Task<Paidi> GetPaidiById(int id);

        Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);
    }
}

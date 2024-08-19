using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddPaidiInDbAsync(Paidi paidi);

        Task<bool> DeletePaidiInDbAsync(Paidi paidi);
        Task<bool> UpdatePaidiInDbAsync(Paidi paidi);
        Task<IEnumerable<Skini>> GetSkinesAsync();

        Task<IEnumerable<Paidi>> GetPaidiaAsync(PaidiType type);

        Task<Skini> GetSkiniByNameAsync(string name);

        Task<Paidi> GetPaidiByIdAsync(int id, PaidiType type);

        Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);
    }
}

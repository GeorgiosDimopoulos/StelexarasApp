using StelexarasApp.Services.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.IServices
{
    public interface IPaidiaService
    {
        Task<bool> AddPaidiInDbAsync(PaidiDto paidiDto);

        Task<bool> DeletePaidiInDb(int id);

        Task<bool> UpdatePaidiInDb(PaidiDto paidiDto);

        Task<IEnumerable<Paidi>> GetPaidia(PaidiType type);

        Task<Paidi> GetPaidiById(int id);

        Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);
    }
}

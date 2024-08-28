using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface IPaidiaService
    {
        Task<bool> AddPaidiInDbAsync(PaidiDto paidiDto);

        Task<bool> DeletePaidiInDb(int id);
        Task<bool> UpdatePaidiInDb(PaidiDto paidiDto);
        Task<IEnumerable<Skini>> GetSkines();

        Task<IEnumerable<Paidi>> GetPaidia(PaidiType type);

        Task<Skini> GetSkiniByName(string name);

        Task<Paidi> GetPaidiById(int id);

        Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);
    }
}

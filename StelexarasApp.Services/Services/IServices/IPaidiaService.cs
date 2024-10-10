using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Services.Services.IServices
{
    public interface IPaidiaService
    {
        Task<bool> AddPaidiInService(PaidiDto paidiDto);

        Task<bool> DeletePaidiInService(int id);

        Task<bool> UpdatePaidiInService(PaidiDto paidiDto);

        Task<IEnumerable<Paidi>> GetPaidiaInService(PaidiType type);

        Task<Paidi> GetPaidiByIdInService(int id);

        Task<bool> MovePaidiToNewSkiniInService(int paidiId, int newSkiniId);
    }
}

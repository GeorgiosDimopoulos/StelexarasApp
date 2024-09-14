using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface IStaffService
    {
        Task<bool> AddStelexosInService(StelexosDto stelexosDto, Thesi thesi);

        Task<bool> DeleteStelexosInService(int id, Thesi thesi);
        Task<bool> UpdateStelexosInService(StelexosDto stelexosDto);
        Task<IEnumerable<StelexosDto>> GetStelexoiAnaXwroInService(Thesi thesi, string? xwrosName);

        Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(Koinotita koinotita);

        Task<Stelexos> GetStelexosByIdInService(int id, Thesi thesi);

        Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesInService();

        Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, int newSkiniId);
    }
}

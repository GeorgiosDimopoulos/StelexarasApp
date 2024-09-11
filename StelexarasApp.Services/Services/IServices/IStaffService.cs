using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.IServices
{
    public interface IStaffService
    {
        Task<bool> AddStelexosInService(StelexosDto stelexosDto, Thesi thesi);

        Task<bool> DeleteStelexosInService(int id, Thesi thesi);
        Task<bool> UpdateStelexosInService(StelexosDto stelexosDto);
        Task<IEnumerable<StelexosDto>> GetStelexoiAnaThesiInService(Thesi thesi);

        Task<Stelexos> GetStelexosByIdInService(int id, Thesi thesi);

        Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, int newSkiniId);
    }
}

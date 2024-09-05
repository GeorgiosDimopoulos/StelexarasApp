using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.Services.IServices
{
    public interface IStelexiService
    {
        Task<bool> AddStelexosInService(StelexosDto stelexosDto, Thesi thesi);

        Task<bool> DeleteStelexosInService(int id, Thesi thesi);
        Task<bool> UpdateStelexosInService(StelexosDto stelexosDto, Thesi thesi);
        Task<IEnumerable<Stelexos>> GetStelexoiAnaThesiInService(Thesi thesi);

        Task<Stelexos> GetStelexosByIdInService(int id, Thesi thesi);

        Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, int newSkiniId);
    }
}

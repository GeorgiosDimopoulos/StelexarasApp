using StelexarasApp.DataAccess.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.Services.IServices
{
    public interface IStelexiService
    {
        Task<bool> AddStelexosInDbAsync(StelexosDto stelexosDto, Thesi thesi);

        Task<bool> DeleteStelexosInDb(int id, Thesi thesi);
        Task<bool> UpdateStelexosInDb(int id, StelexosDto stelexosDto, Thesi thesi);
        Task<IEnumerable<Stelexos>> GetStelexoiAnaThesi(Thesi thesi);

        Task<Stelexos> GetStelexosById(int id, Thesi thesi);

        Task<bool> MoveOmadarxisToAnotherSkini(int Id, int newSkiniId);
    }
}

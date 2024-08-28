using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.Services.IServices
{
    public interface IStelexiService
    {
        Task<bool> AddStelexosInDbAsync(StelexosDto stelexosDto, Thesi thesi);

        Task<bool> DeleteStelexosInDb(int id, Thesi thesi);
        Task<bool> UpdateStelexosInDb(int id, StelexosDto stelexosDto, Thesi thesi);
        Task<IEnumerable<Stelexos>> GetStelexoi(Thesi thesi);

        Task<Stelexos> GetStelexosById(int id, Thesi thesi);

        Task<bool> MoveStelexosToNewSkini(int Id, int newSkiniId);
    }
}

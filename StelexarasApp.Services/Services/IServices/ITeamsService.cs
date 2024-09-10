using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Domi;
using System.Collections.ObjectModel;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddSkiniInService(SkiniDto skini);
        Task<bool> AddKoinotitaInService(KoinotitaDto skini);
        Task<bool> DeleteSkiniInService(int skiniId);
        Task<bool> DeleteKoinotitaInService(int koinotitaId);
        Task<bool> UpdateSkiniInService(SkiniDto skini);
        Task<IEnumerable<Skini>> GetSkines();
        Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInService(string name);
        Task<Skini> GetSkiniByName(string name);
    }
}

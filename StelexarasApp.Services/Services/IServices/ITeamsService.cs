using StelexarasApp.Services.DtosModels.Domi;
using System.Collections.ObjectModel;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddSkini(SkiniDto skini);

        Task<bool> DeleteSkiniInDb(SkiniDto skini);
        Task<bool> UpdateSkiniInDb(SkiniDto skini);
        Task<ObservableCollection<SkiniDto>> GetSkines();
        Task<SkiniDto> GetSkini();
        Task<bool> AddKoinotita(KoinotitaDto skini);
    }
}

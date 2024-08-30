using StelexarasApp.DataAccess.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.DtosModels.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface ITeamsService
    {
        Task<bool> AddSkini(SkiniDto skini);

        Task<bool> DeleteSkiniInDb(SkiniDto skini);
        Task<bool> UpdateSkiniInDb(SkiniDto skini);

        // ToDo: implement the rest of the methods for Koinotites and Tomeis
    }
}

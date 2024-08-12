using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.IServices
{
    public interface IPeopleService
    {
        Task<bool> AddPaidiInDbAsync(Paidi paidi, string skiniName);

        Task<bool> DeletePaidiInDbAsync(Paidi paidi);
        Task<bool> UpdatePaidiInDbAsync(Paidi paidi, string skiniName);
        Task<IEnumerable<Skini>> GetSkinesAsync();

        Task<IEnumerable<Paidi>> GetKataskinotesAsync();

        Task<IEnumerable<Paidi>> GetEkpaideuomenoiAsync();
    }
}

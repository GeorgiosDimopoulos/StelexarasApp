using StelexarasApp.DataAccess.Models.Atoma.Paidia;

namespace StelexarasApp.Services.IServices
{
    public interface IKoinotitaService
    {
        Task AddPaidiAsync(Kataskinotis paidi);
        Task DeletePaidiAsync(int paidiId);
        Task UpdatePaidiAsync(int paidiId);
        Task<IEnumerable<Kataskinotis>> GetPaidiaAsync();
    }
}

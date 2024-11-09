using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Mobile.ViewModels.TeamsViewModels
{
    public class TomeasViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _teamsService;
        private IPaidiaService _paidiaService;

        public List<KoinotitaDto>? Koinotites { get; set; }
        public string TomeasNumber { get; set; }

        public TomeasViewModel(int tomeasNumber, ITeamsService teamsService, IPaidiaService paidiaService)
        {
            _teamsService = teamsService;
            _paidiaService = paidiaService;
            TomeasNumber  = tomeasNumber.ToString();
            _ = LoadKoinotites(tomeasNumber);
        }

        private async Task LoadKoinotites(int tomeas)
        {
            var koinotites = await GetKoinotitesForTomea(tomeas);
            Koinotites = koinotites.ToList();
            OnPropertyChanged(nameof(Koinotites));
        }

        private async Task<IEnumerable<KoinotitaDto>> GetKoinotitesForTomea(int tomeasId)
        {
            return await _teamsService.GetKoinotitesAnaTomeaInService(tomeasId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

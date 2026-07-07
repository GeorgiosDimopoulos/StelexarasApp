using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Mobile.ViewModels.TeamsViewModels
{
    public class TomeasViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _teamsService;
        private IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;

        public List<KoinotitaDtoBase>? Koinotites { get; set; }
        public string TomeasNumber { get; set; }

        public TomeasViewModel(int tomeasNumber, ITeamsService teamsService, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService)
        {
            _teamsService = teamsService;
            _paidiaService = paidiaService;
            TomeasNumber = tomeasNumber.ToString();
            _ = LoadKoinotites(tomeasNumber);
        }

        private async Task LoadKoinotites(int tomeas)
        {
            var koinotites = await GetKoinotitesForTomea(tomeas);
            Koinotites = koinotites.ToList();
            OnPropertyChanged(nameof(Koinotites));
        }

        private async Task<IEnumerable<KoinotitaDtoBase>> GetKoinotitesForTomea(int tomeasId)
        {
            return await _teamsService.GetKoinotitesAnaTomeaInService(new(), tomeasId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

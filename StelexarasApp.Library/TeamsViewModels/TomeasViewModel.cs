using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels.TeamsViewModels
{
    public class TomeasViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _teamsService;
        private IPaidiaService _paidiaService;

        public TomeasDto TomeasDto { get; set; }
        public List<KoinotitaDto>? Koinotites { get; set; }

        public TomeasViewModel(TomeasDto tomeasDto, ITeamsService teamsService, IPaidiaService paidiaService)
        {
            _teamsService = teamsService;
            _paidiaService = paidiaService;
            TomeasDto = tomeasDto;
            _ = LoadKoinotites(tomeasDto);
        }

        private async Task LoadKoinotites(TomeasDto tomeasDto)
        {
            var koinotites = await GetKoinotitesForTomea(tomeasDto.Id);
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

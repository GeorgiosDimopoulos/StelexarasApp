using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Mobile.ViewModels.TeamsViewModels
{
    public class SxoliViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _teamsService;
        private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;
        public List<string> SkinesNames { get; set; }

        public SxoliViewModel(ITeamsService teamsService, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService)
        {
            _teamsService = teamsService;
            _paidiaService = paidiaService;
            SkinesNames = [];
            LoadSkinesKoinotitas();
        }

        private async void LoadSkinesKoinotitas()
        {
            var skines = await _teamsService.GetSkinesAnaKoinotitaInService(new(), "Sxoli");
            foreach (var skini in skines)
                SkinesNames.Add(skini.Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> AddEkpaideuomenos(CreatePaidiRequest request)
        {
            var result = await _paidiaService.CreatePaidiInService(request);

            if (result)
            {
                OnPropertyChanged(nameof(SkinesNames));
                return true;
            }

            return false;
        }
    }
}

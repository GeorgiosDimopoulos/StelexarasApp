using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Mobile.ViewModels.TeamsViewModels
{
    public class KoinotitaViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _paidiaService;
        private readonly ITeamsService _teamsService;
        public ObservableCollection<string> Skines { get; set; }
        public KoinotitaResponse? Koinotita { get; set; }

        public KoinotitaViewModel(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> paidiaService, ITeamsService teamsService)
        {
            _paidiaService = paidiaService;
            _teamsService = teamsService;

            Skines = [];
            Koinotita = new KoinotitaResponse();
            LoadSkinesKoinotitas();
        }

        public async Task<bool> AddPaidiAsync(string fullName, string skiniName, int age, Sex sex, PaidiType paidiType)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(skiniName))
            {
                return false;
            }

            var paidi = new CreatePaidiRequest
            {
                FullName = fullName,
                SkiniName = skiniName,
                Age = age,
                SeAdeia = false,
                Sex = sex,
            };

            if (paidiType == PaidiType.Ekpaideuomenos)
            {
                paidi.PaidiType = PaidiType.Ekpaideuomenos;
            }

            var result = await _paidiaService.CreatePaidiInService(paidi);

            if (result)
            {
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePaidiAsync(string paidiId)
        {
            if (paidiId == null)
                return false;

            var result = await _paidiaService.DeletePaidiInService(int.Parse(paidiId));
            if (result)
            {
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }

        private async void LoadSkinesKoinotitas()
        {
            var skines = await _teamsService.GetSkinesAnaKoinotitaNameInService(null, Koinotita!.Name);
            foreach (var skini in skines)
                Skines.Add(skini.Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> AddKoinotita(CreateKoinotitaRequest koinotita)
        {
            bool result = await _teamsService.AddKoinotitaInService(koinotita);
            if (result)
            {
                OnPropertyChanged(nameof(Skines));
                return true;
            }
            return false;
        }
    }
}

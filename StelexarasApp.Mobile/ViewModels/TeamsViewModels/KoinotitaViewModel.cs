using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.ViewModels.TeamsViewModels
{
    public class KoinotitaViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private readonly ITeamsService _teamsService;
        public ObservableCollection<string> Skines { get; set; }
        public Koinotita? Koinotita { get; set; }
        
        public KoinotitaViewModel(IPaidiaService paidiaService, ITeamsService teamsService)
        {
            _paidiaService = paidiaService;
            _teamsService = teamsService;

            Skines = [];
            Koinotita = new Koinotita();
            LoadSkinesKoinotitas();
        }

        public async Task<bool> AddPaidiAsync(string fullName, string skiniName, PaidiType paidiType)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(skiniName))
            {
                return false;
            }

            var paidi = new PaidiDto
            {
                FullName = fullName,
                PaidiType = PaidiType.Ekpaideuomenos,
            };

            if (paidiType == PaidiType.Ekpaideuomenos)
            {
                paidi.PaidiType = PaidiType.Ekpaideuomenos;
            }

            var result = await _paidiaService.AddPaidiInService(paidi);

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
            var skines = await _teamsService.GetSkinesAnaKoinotitaInService(Koinotita.Name);
            foreach (var skini in skines)
                Skines.Add(skini.Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> AddKoinotita(KoinotitaDto koinotita)
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

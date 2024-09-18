using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels.TeamsViewModels
{
    public class SxoliViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _teamsService;
        private readonly IPaidiaService _paidiaService;
        public List<string> Skines { get; set; }

        public SxoliViewModel(ITeamsService teamsService, IPaidiaService paidiaService)
        {
            _teamsService = teamsService;
            _paidiaService = paidiaService;
            Skines = [];
            LoadSkinesKoinotitas();
        }

        private async void LoadSkinesKoinotitas()
        {
            var skines = await _teamsService.GetSkinesAnaKoinotitaInService("Sxoli");
            foreach (var skini in skines)
                Skines.Add(skini.Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> AddEkpaideuomenos(PaidiDto paidiDto)
        {
            var result = await _paidiaService.AddPaidiInDbAsync(paidiDto);

            if (result)
            {
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }
    }
}

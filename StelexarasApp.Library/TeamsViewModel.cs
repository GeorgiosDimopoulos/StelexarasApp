using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.Services.Services;

namespace StelexarasApp.ViewModels
{
    public class TeamsViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private readonly ITeamsService _teamsService;
        public ObservableCollection<string> Skines { get; set; }
        public Koinotita? Koinotita { get; set; }
        public string Title { get; set; }

        private async void LoadSkines()
        {
            var skines = await _teamsService.GetSkines();
            foreach (var skini in skines)
                Skines.Add(skini.Name);
        }

        public TeamsViewModel(IPaidiaService paidiaService, ITeamsService teamsService, EidosXwrou eidosXwrou)
        {
            _paidiaService = paidiaService;
            _teamsService = teamsService;
            Skines = [];
            LoadSkines();
            Koinotita = new Koinotita();
            Title = GetTitle(eidosXwrou);
        }

        private static string? GetTitle(EidosXwrou eidosXwrou)
        {
            return eidosXwrou switch
            {
                EidosXwrou.Koinotita => "Koinotita",
                EidosXwrou.Skini => "Skini",
                EidosXwrou.Tomeas => "Tomeas",
                _ => "Unknown Title",
            };
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

            var result = await _paidiaService.AddPaidiInDbAsync(paidi);

            if (result)
            {
                // await GetAllSkinesAsync();
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePaidiAsync(string paidiId)
        {
            if (paidiId == null)
            {
                return false;
            }

            var result = await _paidiaService.DeletePaidiInDb(int.Parse(paidiId));
            if (result)
            {
                // await GetAllSkinesAsync();
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

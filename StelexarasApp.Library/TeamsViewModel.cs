using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels
{
    public class TeamsViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        public ObservableCollection<SkiniDto>? Skines { get; set; }
        public Koinotita? Koinotita { get; set; }

        private EidosXwrou _eidosXwrou;
        public EidosXwrou EidosXwrou
        {
            get => _eidosXwrou;
            set
            {
                _eidosXwrou = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Title => EidosXwrou switch
        {
            EidosXwrou.Koinotita => "Koinotita",
            EidosXwrou.Skini => "Skini",
            EidosXwrou.Tomeas => "Tomeas",
            _ => "Unknown Title"
        };

        public TeamsViewModel(IPaidiaService paidiaService, EidosXwrou eidosXwrou)
        {
            _paidiaService = paidiaService;
            Skines = [];
            Koinotita = new Koinotita();
            EidosXwrou = eidosXwrou;
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

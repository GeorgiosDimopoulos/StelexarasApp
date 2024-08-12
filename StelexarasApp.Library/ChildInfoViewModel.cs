using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class ChildInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPeopleService _peopleService;
        public Paidi Paidi { get; set; }
        public Skini Skini { get; set; }

        public ObservableCollection<Skini> Skines { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        public ChildInfoViewModel(IPeopleService peopleService, Paidi paidi, ObservableCollection<Skini> skines)
        {
            _peopleService = peopleService;

            Paidi = paidi;
            FullName = paidi.FullName;
            Age = paidi.Age;
            Skini = paidi.Skini;
            Skines = skines;
        }

        public async Task DeleteEkpaideuomenosAsync()
        {
            bool isDeleted = await _peopleService.DeletePaidiInDbAsync(Paidi);

            if (isDeleted)
            {
                await _peopleService.GetSkinesAsync();
                OnPropertyChanged(nameof(Skines));
            }
        }

        public async Task UpdateEkpaideuomenosAsync(string fullName, string age, string skiniName)
        {
            var result = await _peopleService.UpdatePaidiInDbAsync(Paidi, skiniName);
            if (result)
            {
                await _peopleService.GetSkinesAsync();
                OnPropertyChanged(nameof(Skines));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

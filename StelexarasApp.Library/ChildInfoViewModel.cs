using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class ChildInfoViewModel : INotifyPropertyChanged
    {
        private readonly ITeamsService _peopleService;
        public Paidi Paidi { get; set; }
        public Skini Skini { get; set; }

        public ObservableCollection<Skini> Skines { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        public ChildInfoViewModel(ITeamsService peopleService, Paidi paidi, ObservableCollection<Skini> skines)
        {
            _peopleService = peopleService;

            Paidi = paidi;
            FullName = paidi.FullName;
            Age = paidi.Age;
            Skini = paidi.Skini;
            Skines = skines;
        }

        public async Task DeletePaidiAsync(Paidi _paidi)
        {
            bool isDeleted = await _peopleService.DeletePaidiInDb(_paidi);

            if (isDeleted)
            {
                await _peopleService.GetSkines();
                OnPropertyChanged(nameof(Skines));
            }
        }

        public async Task<bool> UpdatePaidiAsync(string id, string fullName, Skini skini, int age)
        {
            if (skini is null)
            {
                return false;
            }

            Paidi.Age = age;
            Paidi.FullName = fullName;
            Paidi.Skini = skini;

            var result = await _peopleService.UpdatePaidiInDb(Paidi);
            
            if (result)
            {
                await _peopleService.GetSkines();
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

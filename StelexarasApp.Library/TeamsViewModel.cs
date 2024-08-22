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
        private readonly ITeamsService _peopleService;
        public ObservableCollection<Skini>? Skines { get; set; }
        public Koinotita? Koinotita { get; set; }
        public TeamsViewModel(ITeamsService peopleService)
        {
            _peopleService = peopleService;
            
            // InitializeSkines();
            GetAllSkinesAsync();
        }
        
        public async Task<bool> AddPaidiAsync(string fullName, string skiniName, PaidiType paidiType)
        {
            if(string.IsNullOrEmpty(fullName)|| string.IsNullOrEmpty(skiniName))
            {
                return false;
            }

            Koinotita koinotita = Skines.FirstOrDefault(s => s.Name == skiniName)?.Koinotita ?? new Koinotita { Name = "Ήπειρος" };
            bool result;

            var paidi = new Paidi
            {
                FullName = fullName,
                PaidiType = PaidiType.Ekpaideuomenos,
                Skini = new Skini
                {
                    Name = skiniName,
                    Koinotita = Koinotita
                }
            };

            result = await _peopleService.AddPaidiInDbAsync(paidi);

            if (result)
            {
                await GetAllSkinesAsync();
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

            var paidi = await _peopleService.GetPaidiById(int.Parse(paidiId));

            var result = await _peopleService.DeletePaidiInDb(paidi);
            if (result)
            {
                await GetAllSkinesAsync();
                OnPropertyChanged(nameof(Skines));
                return true;
            }
            
            return false;
        }
        
        private async Task GetAllSkinesAsync()
        {
            Skines = new ObservableCollection<Skini>(await _peopleService.GetSkines());
            OnPropertyChanged(nameof(Skines));
        }

        private void InitializeSkines()
        {
            var skini1 = new Skini
            {
                Name = "Πίνδος",
                Koinotita = new Koinotita { Name = "Ήπειρος" },
                Paidia = new ObservableCollection<Paidi>
                {
                    new Paidi
                    {
                        FullName = "Βασιλης Λαμπαδιτης",
                        PaidiType = PaidiType.Ekpaideuomenos,
                        Sex = DataAccess.Models.Atoma.Sex.Male,
                        Age = 16,
                        SeAdeia = false
                    },
                    new Paidi
                    {
                        FullName = "Άγγελος Γεωργόπουλος",
                        Age = 16,
                        PaidiType = PaidiType.Ekpaideuomenos,
                        Sex = DataAccess.Models.Atoma.Sex.Female,
                        SeAdeia = false
                    },
                    new Paidi
                    {
                        FullName = "Δημήτρης Στεφάς",
                        Age = 16,
                        Sex = DataAccess.Models.Atoma.Sex.Male,
                        PaidiType = PaidiType.Ekpaideuomenos,
                        SeAdeia = false
                    },
                }
            };

            foreach (var paidi in skini1.Paidia)
            {
                paidi.Skini = skini1;
            }

            var skini2 = new Skini
            {
                Name = "Κορυτσά",
                Koinotita = new Koinotita { Name = "Ήπειρος" },
                Paidia = new ObservableCollection<Paidi>
                {
                    new Paidi
                    {
                        FullName = "Φίλιππος Σταφυλάς",
                        Sex = Sex.Male,
                        Age = 16,
                        PaidiType = PaidiType.Ekpaideuomenos,
                        SeAdeia = true
                    },
                    new Paidi
                    {
                        FullName = "Διαουρτας Βασιλης",
                        Age = 16,
                        PaidiType = PaidiType.Ekpaideuomenos,
                        Sex = DataAccess.Models.Atoma.Sex.Male,
                        SeAdeia = false
                    },
                }
            };

            foreach (var paidi in skini2.Paidia)
            {
                paidi.Skini = skini2;
            }

            Skines = new ObservableCollection<Skini>
            {
                skini1, skini2
            };

            Koinotita = skini1.Koinotita;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

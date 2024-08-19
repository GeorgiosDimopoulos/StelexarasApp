using StelexarasApp.DataAccess.Models.Atoma.Paidia;
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

            var paidi = new Kataskinotis
            {
                FullName = fullName,
                Skini = new Skini 
                {
                    Name = skiniName,
                    Koinotita = Koinotita
                }
            };

            var result = await _peopleService.AddPaidiInDbAsync(paidi);
            if (result)
            {
                await GetAllSkinesAsync();
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }        

        public async Task<bool> DeletePaidiAsync(string paidiId, PaidiType paidiType)
        {
            if (paidiId == null)
            {
                return false;
            }

            var paidi = await _peopleService.GetPaidiByIdAsync(int.Parse(paidiId), paidiType);

            var result = await _peopleService.DeletePaidiInDbAsync(paidi);
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
            Skines = new ObservableCollection<Skini>(await _peopleService.GetSkinesAsync());
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
                    new Ekpaideuomenos
                    {
                        FullName = "Βασιλης Λαμπαδιτης",
                        Sex = DataAccess.Models.Atoma.Sex.Male,
                        Age = 16,
                        SeAdeia = false
                    },
                    new Ekpaideuomenos
                    {
                        FullName = "Άγγελος Γεωργόπουλος",
                        Age = 16,
                        Sex = DataAccess.Models.Atoma.Sex.Female,
                        SeAdeia = false
                    },
                    new Ekpaideuomenos
                    {
                        FullName = "Δημήτρης Στεφάς",
                        Age = 16,
                        Sex = DataAccess.Models.Atoma.Sex.Male,
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
                    new Ekpaideuomenos
                    {
                        FullName = "Φίλιππος Σταφυλάς",
                        Sex = DataAccess.Models.Atoma.Sex.Male,
                        Age = 16,
                        SeAdeia = true
                    },
                    new Ekpaideuomenos
                    {
                        FullName = "Διαουρτας Βασιλης",
                        Age = 16,
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

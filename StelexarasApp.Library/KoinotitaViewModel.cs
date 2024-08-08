using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels
{
    public class KoinotitaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Skini> Skines { get; set; }

        public Koinotita Koinotita { get; set; }

        public KoinotitaViewModel()
        {
            InitializeSkines();
        }

        public int AddNewEkpaideuomenos(string fullName, string skiniName)
        {
            var skini = Skines.FirstOrDefault(s => s.Name == skiniName);
            if (skini == null)
                return -1;

            var newEkpaideuomenos = new Ekpaideuomenos
            {
                FullName = fullName,
                Age = 16,
                Skini = skini
            };
            OnPropertyChanged(nameof(Skines));
            return 1;
        }

        public void DeleteEkpaideuomenos(string fullName)
        {
            foreach (var skini in Skines)
            {
                foreach (var ekpaide in skini.Paidia)
                {
                    if (ekpaide.FullName.Equals(fullName))
                    {
                        skini.Paidia.Remove(ekpaide);
                    }
                }
            }

            OnPropertyChanged(nameof(Skines));
        }

        public void UpdateEkpaideuomenos(string fullName, string age, string skiniName)
        {
            var skini = Skines.FirstOrDefault(s => s.Name == skiniName);
            if (skini == null)
                return;

            foreach (var skini1 in Skines)
            {
                foreach (var ekpaide in skini1.Paidia)
                {
                    if (ekpaide.FullName.Equals(fullName))
                    {
                        ekpaide.FullName = fullName;
                        ekpaide.Age = int.Parse(age);
                        ekpaide.Skini = skini;
                    }
                }
            }

            OnPropertyChanged(nameof(Skines));
        }

        private void InitializeSkines()
        {
            var skini1 = new Skini
            {
                Name = "Πίνδος",
                Koinotita = new Koinotita { Name = "Ήπειρος" },
                Paidia = new ObservableCollection<Ekpaideuomenos>
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
                Paidia = new ObservableCollection<Ekpaideuomenos>
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

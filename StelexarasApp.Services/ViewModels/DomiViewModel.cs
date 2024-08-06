using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Domi;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Services.ViewModels
{
    public class DomiViewModel
    {
        public ObservableCollection<Skini> Skines { get; set; }

        public DomiViewModel()
        {
            Skines = new ObservableCollection<Skini>
            {
                new Skini
                {
                     //Omadarxis = new Library.Models.Atoma.Stelexi.Omadarxis
                     //{
                     //    Skini = new Skini { Name = "Πίνδος" },
                     //    FullName = "Georg Dimopoulos",
                     //},
                    Name = "Πίνδος",
                    Koinotita = new Koinotita { Name = "Ήπειρος" },
                    Paidia = new ObservableCollection<Ekpaideuomenos>
                    {
                        new Ekpaideuomenos {

                            FullName = "ΒασιληςΛαμπαδιτης",
                            Sex = Library.Models.Atoma.Sex.Male,
                            Age = 16,
                            // Skini = new Skini { Name = "Πίνδος"}
                        },
                        new Ekpaideuomenos {
                            FullName = "Άγγελος Γεωργόπουλος",
                            Age = 16,
                            Sex = Library.Models.Atoma.Sex.Female ,
                        },
                        new Ekpaideuomenos {
                            FullName = "Δημήτρης Στεφάς",
                            Age = 16,
                            Sex = Library.Models.Atoma.Sex.Male,
                        },
                    },
                },
                new Skini 
                {
                    Name = "Πίνδος 2",
                    Koinotita = new Koinotita { Name = "Ήπειρος" },
                    Paidia = new ObservableCollection<Ekpaideuomenos>
                    {
                        new Ekpaideuomenos {

                            FullName = "Φίλιππος Σταφυλάς",
                            Sex = Library.Models.Atoma.Sex.Male,
                            Age = 16,
                            // Skini = new Skini { Name = "Πίνδος"}
                        },
                        new Ekpaideuomenos {
                            FullName = "Διαουρτας Βασιλης",
                            Age = 16,
                            Sex = Library.Models.Atoma.Sex.Male,
                        },
                    },
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

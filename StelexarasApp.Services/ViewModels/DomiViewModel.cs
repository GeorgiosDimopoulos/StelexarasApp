using StelexarasApp.Library.Models.Atoma.Paidia;
using System.Collections.ObjectModel;
using StelexarasApp.Library.Models.Domi;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Services.ViewModels
{
    public class DomiViewModel
    {
        public ObservableCollection<Ekpaideuomenos> Ekpaideuomenoi { get; set; }

        public DomiViewModel()
        {
            Ekpaideuomenoi =
            [
                new Ekpaideuomenos {
                    FullName = "Georg Dimopoulos",
                    Sex = Library.Models.Atoma.Sex.Male,
                    // Skini = new Skini { Name = "Πίνδος"}
                },
                new Ekpaideuomenos {
                    FullName = "Entzi Kurti",
                    Sex = Library.Models.Atoma.Sex.Female ,
                    // Skini = new Skini { Name = "Πίνδος" }
                },
                new Ekpaideuomenos {
                    FullName = "Kostis Alimpertis",
                    Sex = Library.Models.Atoma.Sex.Male,
                    // Skini = new Skini { Name = "Πίνδος" }
                },
            ];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

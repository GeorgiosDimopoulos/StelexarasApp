using StelexarasApp.Services.ViewModels;
using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Domi;
using System.Collections.ObjectModel;

namespace StelexarasApp.Presentation.Views
{
    public partial class ChildInfoPage : ContentPage
    {
        public ChildInfoPage(Ekpaideuomenos paidi, ObservableCollection<Skini> skines)
        {
            InitializeComponent();

            // BindingContext = new ChildInfoViewModel();
            BindingContext = new ChildInfoViewModel(paidi, skines);
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            // ToDo: Remove object from DB
            // await Navigation.PopAsync();
        }
    }
}

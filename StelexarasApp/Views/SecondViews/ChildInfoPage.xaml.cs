using StelexarasApp.ViewModels;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class ChildInfoPage : ContentPage
    {
        private ChildInfoViewModel? _viewModel;
        private Ekpaideuomenos _paidi;

        public ChildInfoPage(Ekpaideuomenos paidi, ObservableCollection<Skini> allSkines)
        {
            InitializeComponent();
            _paidi = paidi;
            BindingContext = new ChildInfoViewModel(paidi, allSkines);
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            new KoinotitaViewModel().DeleteEkpaideuomenos(_paidi.FullName);
            await Navigation.PopAsync();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            Skini sk = (Skini) SkiniPicker.SelectedItem;
            new KoinotitaViewModel().UpdateEkpaideuomenos(ChildName.Text, ChildAge.Text, sk.Name);
            SaveButton.IsEnabled = false;
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }
    }
}

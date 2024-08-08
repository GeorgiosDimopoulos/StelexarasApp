using StelexarasApp.Services.ViewModels;
using StelexarasApp.Library.Models.Atoma.Paidia;
using System.Collections.ObjectModel;
using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Presentation.Views
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

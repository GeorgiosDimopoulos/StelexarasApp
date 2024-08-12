using StelexarasApp.ViewModels;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class ChildInfoPage : ContentPage
    {
        private ChildInfoViewModel? _viewModel;
        private Ekpaideuomenos _paidi;

        public ChildInfoPage(IPeopleService peopleService,Ekpaideuomenos paidi, ObservableCollection<Skini> allSkines)
        {
            InitializeComponent();
            _paidi = paidi;
            _viewModel= new ChildInfoViewModel(peopleService, paidi, allSkines);
            BindingContext = _viewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            await _viewModel.DeleteEkpaideuomenosAsync();
            await Navigation.PopAsync();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            Skini sk = (Skini) SkiniPicker.SelectedItem;
            await _viewModel.UpdateEkpaideuomenosAsync(ChildName.Text, ChildAge.Text, sk.Name);
            SaveButton.IsEnabled = false;
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }
    }
}

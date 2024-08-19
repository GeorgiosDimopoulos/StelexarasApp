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
        private Skini? _skini;
        private Paidi _paidi;

        public ChildInfoPage(ITeamsService peopleService, Paidi paidi, ObservableCollection<Skini> allSkines)
        {
            InitializeComponent();
            _paidi = paidi;
            _viewModel = new ChildInfoViewModel(peopleService, paidi, allSkines);
            BindingContext = _viewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            await _viewModel.DeletePaidiAsync(_paidi);
            await Navigation.PopAsync();
        }

        private void OnChildEntryFocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsVisible = true;
        }

        private void OnSkiniEntryFocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsEnabled = true;
            SaveButton.IsEnabled = true;
        }

        private async void OnAdeiaEntryFocused(object sender, FocusEventArgs e)
        {
            // ToDo: fill it
            SaveButton.IsEnabled = true;
        }

        private async void OnAgeEntryFocused(object sender, FocusEventArgs e)
        {
            // ToDo: fill it
            SaveButton.IsEnabled = true;
        }

        private async void OnSkiniPickerUnfocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsVisible = false;
            _skini = (Skini)SkiniPicker.SelectedItem;
            SaveButton.IsEnabled = true;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _viewModel.UpdatePaidiAsync(ChildName.Text, ChildAge.Text, _skini, int.Parse(ChildAge.Text));
            SaveButton.IsEnabled = false;
        }
    }
}

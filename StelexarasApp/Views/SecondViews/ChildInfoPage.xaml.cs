using StelexarasApp.ViewModels;
using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.DtosModels;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class ChildInfoPage : ContentPage
    {
        private ChildInfoViewModel? _viewModel;
        private Skini? _skini;
        private PaidiDto _paidiDto;

        public ChildInfoPage(IPaidiaService peopleService, PaidiDto paidi, ObservableCollection<Skini> allSkines)
        {
            InitializeComponent();
            _paidiDto = paidi;
            _viewModel = new ChildInfoViewModel(peopleService, paidi, allSkines);
            BindingContext = _viewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            await _viewModel.DeletePaidiAsync(_paidiDto);
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

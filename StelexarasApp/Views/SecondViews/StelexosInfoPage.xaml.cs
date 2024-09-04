using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class StelexosInfoPage : ContentPage
    {
        private StelexiInfoViewModel? infoViewModel;
        private StelexosDto stelexosDto1;

        public StelexosInfoPage(IStelexiService stelexiService, StelexosDto stelexosDto)
        {
            InitializeComponent();
            infoViewModel = new StelexiInfoViewModel(stelexiService);
            stelexosDto1 = stelexosDto;
            BindingContext = infoViewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (infoViewModel is null)
            {
                return;
            }

            await infoViewModel.DeleteStelexos(stelexosDto1.Id ?? 0, Thesi.Omadarxis);
            await Navigation.PopAsync();
        }

        //private void OnChildNameEntryFocused(object sender, FocusEventArgs e)
        //{
        //    SkiniPicker.IsVisible = true;
        //    SkiniPicker.Focus();
        //}

        //private void OnChildNameEntryUnfocused (object sender, FocusEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ChildName.Text))
        //    {
        //        isChildNameFilled = false;
        //        SkiniPicker.IsEnabled = false;
        //        SaveButton.IsEnabled = false;
        //    }
        //    else
        //    {
        //        _paidiDto.Age = int.Parse(ChildAge.Text);
        //        isChildNameFilled = true;
        //    }
        //}

        //private void OnSkiniEntryFocused(object sender, FocusEventArgs e)
        //{
        //    isChildNameFilled = true;
        //    SkiniPicker.IsEnabled = true;
        //    SaveButton.IsEnabled = true;
        //}

        //private void OnAdeiaEntryFocused(object sender, FocusEventArgs e)
        //{
        //    isChildAdeiaFieldFilled = true;
        //    SaveButton.IsEnabled = true;
        //}

        //private void OnAgeEntryFocused(object sender, FocusEventArgs e)
        //{
        //    isChildAdeiaFieldFilled = true;
        //    var age = int.Parse(ChildAge.Text);
        //    SaveButton.IsEnabled = true;
        //}

        //private async void OnSkiniPickerUnfocused(object sender, FocusEventArgs e)
        //{
        //    SkiniPicker.IsVisible = false;

        //    try
        //    {
        //        _skini = (SkiniDto)SkiniPicker.SelectedItem;
        //        SaveButton.IsEnabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Σφάλμα", ex.Message, "OK");
        //    }
        //}

        //private async void OnSaveClicked(object sender, EventArgs e)
        //{
        //    if (_childviewModel is null)
        //    {
        //        return;
        //    }

        //    if (isChildNameFilled || isChildAdeiaFieldFilled || isChildAgeFilled && _skini is not null)
        //    {
        //        _paidiDto.FullName = ChildName.Text;
        //        _paidiDto.Age = int.Parse(ChildAge.Text);
        //        _paidiDto.SkiniName = _skini.Name;

        //        await _childviewModel.UpdatePaidiAsync(_paidiDto, _skini);
        //        SaveButton.IsEnabled = false;
        //    }
        //    else
        //    {
        //        await DisplayAlert("Σφάλμα", "Παρακαλώ συμπληρώστε όλα τα πεδία", "OK");
        //    }
        //}
    }
}
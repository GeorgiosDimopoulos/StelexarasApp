using StelexarasApp.ViewModels;
using System.Collections.ObjectModel;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class ChildInfoPage : ContentPage
    {
        private ChildInfoViewModel? _childviewModel;
        private bool isSkiniPickerFilled = false;
        private bool isChildAdeiaFieldFilled = false;
        private PaidiDto _paidiDto;
        private SkiniDto _skini;
        private bool isChildNameFilled = false;
        private bool isChildAgeFilled = false;

        public ChildInfoPage(IPaidiaService peopleService, PaidiDto paidi, ObservableCollection<SkiniDto> allSkines)
        {
            InitializeComponent();
            _childviewModel = new ChildInfoViewModel(peopleService, allSkines);
            _paidiDto = paidi;
            _skini = new SkiniDto();
            BindingContext = _childviewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if(_childviewModel is null)
            {
                return;
            }

            await _childviewModel.DeletePaidiAsync(_paidiDto.Id);
            await Navigation.PopAsync();
        }

        private void OnChildNameEntryFocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsVisible = true;
            SkiniPicker.Focus();
        }

        private void OnChildNameEntryUnfocused (object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(ChildName.Text))
            {
                isChildNameFilled = false;
                SkiniPicker.IsEnabled = false;
                SaveButton.IsEnabled = false;
            }
            else
            {
                _paidiDto.Age = int.Parse(ChildAge.Text);
                isChildNameFilled = true;
            }
        }

        private void OnSkiniEntryFocused(object sender, FocusEventArgs e)
        {
            isChildNameFilled = true;
            SkiniPicker.IsEnabled = true;
            SaveButton.IsEnabled = true;
        }

        private void OnAdeiaEntryFocused(object sender, FocusEventArgs e)
        {
            isChildAdeiaFieldFilled = true;
            SaveButton.IsEnabled = true;
        }

        private void OnAgeEntryFocused(object sender, FocusEventArgs e)
        {
            isChildAdeiaFieldFilled = true;
            var age = int.Parse(ChildAge.Text);
            SaveButton.IsEnabled = true;
        }

        private async void OnSkiniPickerUnfocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsVisible = false;

            try
            {
                _skini = (SkiniDto)SkiniPicker.SelectedItem;
                SaveButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Σφάλμα", ex.Message, "OK");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_childviewModel is null)
            {
                return;
            }

            if (isChildNameFilled || isChildAdeiaFieldFilled || isChildAgeFilled && _skini is not null)
            {
                _paidiDto.FullName = ChildName.Text;
                _paidiDto.Age = int.Parse(ChildAge.Text);
                _paidiDto.SkiniName = _skini.Name;

                if (await _childviewModel.UpdatePaidiAsync(_paidiDto, _skini))
                {
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Σφάλμα", "Παρακαλώ συμπληρώστε όλα τα πεδία", "OK");
                }
                
            }
            else
            {
                await DisplayAlert("Σφάλμα", "Παρακαλώ συμπληρώστε όλα τα πεδία", "OK");
            }
        }
    }
}
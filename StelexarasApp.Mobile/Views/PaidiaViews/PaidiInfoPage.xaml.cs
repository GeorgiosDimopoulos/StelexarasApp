﻿using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.Mobile.Views.PaidiaViews
{
    public partial class PaidiInfoPage : ContentPage
    {
        private PaidiInfoViewModel? _paidiviewModel;
        private bool isSkiniPickerFilled = false;
        private bool isPaidiAdeiaFieldFilled = false;
        private PaidiDto _paidiDto;
        private SkiniDto _skini;
        private bool isPaidiNameFilled = false;
        private bool isPaidiAgeFilled = false;

        public PaidiInfoPage(IPaidiaService peopleService, PaidiDto paidi)
        {
            InitializeComponent();
            _paidiDto = paidi;
            _paidiviewModel = new PaidiInfoViewModel(_paidiDto, peopleService, _paidiDto.SkiniName);
            _skini = new SkiniDto();
            BindingContext = _paidiviewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_paidiviewModel is null)
            {
                return;
            }

            await _paidiviewModel.DeletePaidiAsync(_paidiDto.Id);
            await Navigation.PopAsync();
        }

        private void OnPaidiNameEntryFocused(object sender, FocusEventArgs e)
        {
            SkiniPicker.IsVisible = true;
            SkiniPicker.Focus();
        }

        private void OnPaidiNameEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(PaidiName.Text))
            {
                isPaidiNameFilled = false;
                SkiniPicker.IsEnabled = false;
                SaveButton.IsEnabled = false;
            }
            else
            {
                _paidiDto.Age = int.Parse(PaidiAge.Text);
                isPaidiNameFilled = true;
            }
        }

        private void OnSkiniEntryFocused(object sender, FocusEventArgs e)
        {
            isPaidiNameFilled = true;
            SkiniPicker.IsEnabled = true;
            SaveButton.IsEnabled = true;
        }

        private void OnAdeiaEntryFocused(object sender, FocusEventArgs e)
        {
            isPaidiAdeiaFieldFilled = true;
            SaveButton.IsEnabled = true;
        }

        private void OnAgeEntryFocused(object sender, FocusEventArgs e)
        {
            isPaidiAgeFilled = true;
            var age = int.Parse(PaidiAge.Text);
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

        // ToDo: should be removed!
        //private async void OnSaveClicked(object sender, EventArgs e)
        //{
        //    if (isPaidiNameFilled || isPaidiAdeiaFieldFilled || isPaidiAgeFilled && _skini is not null)
        //    {
        //        _paidiDto.FullName = PaidiName.Text;
        //        _paidiDto.Age = int.Parse(PaidiAge.Text);
        //        _paidiDto.SkiniName = _skini.Name;

        //        if (await _paidiviewModel.UpdatePaidiAsync(_paidiDto, _skini))
        //        {
        //            SaveButton.IsEnabled = false;
        //        }
        //        else
        //        {
        //            await DisplayAlert("Σφάλμα", "Παρακαλώ συμπληρώστε όλα τα πεδία", "OK");
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Σφάλμα", "Παρακαλώ συμπληρώστε όλα τα πεδία", "OK");
        //    }
        //}
    }
}
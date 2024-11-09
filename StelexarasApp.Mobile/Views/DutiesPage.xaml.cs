using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels;

namespace StelexarasApp.Mobile.Views;

public partial class DutiesPage : ContentPage
{
    private readonly IDutyService _dutyService;
    private readonly DutyViewModel _viewModel;

    public DutiesPage(IDutyService dutyService)
    {
        _dutyService = dutyService;
        InitializeComponent();
        _viewModel = new DutyViewModel(_dutyService);
        BindingContext = _viewModel;
    }

    private async void AddDuty_Clicked(object sender, EventArgs e)
    {
        try
        {
            string dutyName = await DisplayPromptAsync("Νέα Υποχρεωση", "Όνομα Υποχρεωσης:", "OK");
            if (string.IsNullOrEmpty(dutyName))
                return;
            await _viewModel.AddDuty(dutyName);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Λάθος Στοιχεία", ex.Message, "OK");
            return;
        }        
    }

    private async void OnDutyTapped(object sender, SelectionChangedEventArgs e)
    {
        var selectedDuty = e.CurrentSelection.FirstOrDefault() as Duty;
        if (selectedDuty == null)
            return;

        string action = await DisplayActionSheet("Επιλογές", "Ακύρωση", null, "Διαγραφή", "Μετονομασία");
        switch (action)
        {
            case "Διαγραφή":
                bool confirmDelete = await DisplayAlert("Διαγραφή", $"Σίγουρος για τη διαγραφή: '{selectedDuty.Name}';", "Ναι", "Όχι");
                if (confirmDelete)
                {
                    try
                    {
                        await _viewModel.DeleteDuty(selectedDuty.Id);
                        await DisplayAlert("Επιτυχής Διαγραφή", "Διαγραφή υποχρέωσης!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                    }
                }
                break;

            case "Μετονομασία":
                string newName = await DisplayPromptAsync("Μετονομασία", "Νέο όνομα υποχρέωσης:", "OK", "Ακύρωση", initialValue: selectedDuty.Name);
                if (!string.IsNullOrEmpty(newName) && newName != selectedDuty.Name)
                {
                    try
                    {
                        await _viewModel.UpdateDuty(selectedDuty, newName);
                        await DisplayAlert("Επιτυχής Μετονομασία", "Η υποχρέωση μετονομάστηκε!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                    }
                }
                break;
        }

    ((CollectionView)sender).SelectedItem = null;
    }
}

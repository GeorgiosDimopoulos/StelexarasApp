using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class PaidiaViewModel : INotifyPropertyChanged
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> kataskinotisService;

    public ObservableCollection<PaidiDtoBase> PaidiaList { get; set; }

    public PaidiaViewModel(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> peopleService)
    {
        kataskinotisService = peopleService;
        PaidiaList = [];
        LoadPaidia();
    }

    private void LoadPaidia()
    {
        // ToDo: Load your data into PaidiaList
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels.PeopleViewModels;

public class PaidiaViewModel : INotifyPropertyChanged
{
    private readonly IPaidiaService _peopleService;

    public ObservableCollection<PaidiDto> PaidiaList { get; set; }

    public PaidiaViewModel(IPaidiaService peopleService)
    {
        _peopleService = peopleService;
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

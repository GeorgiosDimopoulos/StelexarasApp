using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class PaidiaViewModel : INotifyPropertyChanged
{
    private readonly IPaidiaService _paidiService;

    public ObservableCollection<PaidiDtoBase> PaidiaList { get; set; }

    public PaidiaViewModel(IPaidiaService paidiService)
    {
        _paidiService = paidiService;
        PaidiaList = new ObservableCollection<PaidiDtoBase>();
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

using StelexarasApp.Services.DtosModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels.PeopleViewModels;

public class PaidiaViewModel : INotifyPropertyChanged
{
    public ObservableCollection<PaidiDto> PaidiaList { get; set; }

    public PaidiaViewModel()
    {
        PaidiaList = new ObservableCollection<PaidiDto>();
        LoadPaidia();
    }

    private void LoadPaidia()
    {
        // ToDo: Load your data into PaidiaList
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

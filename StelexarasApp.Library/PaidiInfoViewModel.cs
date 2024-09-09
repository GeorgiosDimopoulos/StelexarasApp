using Microsoft.Maui.Platform;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace StelexarasApp.ViewModels
{
    public class PaidiInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private ICommand SavePaidiCommand { get; }

        public PaidiDto PaidiDto { get; set; } = new PaidiDto();
        public ObservableCollection<SkiniDto> Skines { get; set; }
        public string StatusMessage { get; set; } = string.Empty;

        public PaidiInfoViewModel(PaidiDto paidiDto,IPaidiaService peopleService, ObservableCollection<SkiniDto> skines)
        {
            PaidiDto = paidiDto;
            _paidiaService = peopleService;
            SavePaidiCommand = new Command(async () => await OnSavePaidi());
            Skines = skines;
        }

        public async Task<bool> DeletePaidiAsync(int id)
        {
            if (await _paidiaService.DeletePaidiInDb(id))
            {
                StatusMessage = "Delete successful";
                return true;
            }
            else
            {
                StatusMessage = "Delete failed";
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<bool> OnSavePaidi()
        {
            var skini = Skines.FirstOrDefault(s => s.Name == PaidiDto.SkiniName);
            if (skini is null || string.IsNullOrEmpty(skini.Name))
                return false;

            // ToDo: maybe it is not needed to set the Skini here, it could be already be set via XAML
            PaidiDto.SkiniName = skini.Name;
            var result = await _paidiaService.UpdatePaidiInDb(PaidiDto);
            StatusMessage = result ? "Save successful" : "Save failed";

            if (result)
            {
                OnPropertyChanged(nameof(Skines));
                OnPropertyChanged(nameof(PaidiDto));
                return true;
            }

            return false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

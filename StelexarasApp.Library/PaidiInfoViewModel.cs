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
            return await _paidiaService.DeletePaidiInDb(id);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task OnSavePaidi()
        {
            var skini = Skines.FirstOrDefault(s => s.Name == PaidiDto.SkiniName);

            if (skini?.Name is null)
                return;

            // ToDo: maybe it is not needed to set the Skini here, it could be already be set via XAML
            PaidiDto.SkiniName = skini.Name;
            var result = await _paidiaService.UpdatePaidiInDb(PaidiDto);

            if (result)
            {
                StatusMessage = result ? "Save successful" : "Save failed";

                OnPropertyChanged(nameof(Skines));
                OnPropertyChanged(nameof(PaidiDto));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

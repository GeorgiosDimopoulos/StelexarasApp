using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.ViewModels
{
    public class StelexosInfoViewModel : INotifyPropertyChanged
    {
        private readonly IStaffService _stelexiService;

        public event PropertyChangedEventHandler? PropertyChanged;
        public StelexosDto StelexosDto { get; set; }
        public ICommand SaveStelexosCommand { get; }
        public string StatusMessage { get; set; } = string.Empty;

        public StelexosInfoViewModel(StelexosDto stelexosDto, IStaffService stelexiService)
        {
            _stelexiService = stelexiService;
            StelexosDto = stelexosDto;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos(StelexosDto s)
        {
            return await _stelexiService.DeleteStelexosInService(s.Id ?? 1, s.Thesi);
        }

        public async Task OnSaveStelexos()
        {
            var result = await _stelexiService.UpdateStelexosInService(StelexosDto);
            StatusMessage = result ? "Save successful" : "Save failed";
            OnPropertyChanged(nameof(StelexosDto));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.ViewModels
{
    public class StelexiInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public StelexosDto StelexosDto { get; set; }
        public ICommand SaveStelexosCommand { get; }

        private readonly IStelexiService _stelexiService;

        public StelexiInfoViewModel(StelexosDto stelexosDto, IStelexiService stelexiService)
        {
            _stelexiService = stelexiService;
            StelexosDto = stelexosDto;
            SaveStelexosCommand = new Command(OnSaveStelexos);
        }

        public async Task<bool> DeleteStelexos(StelexosDto s, Thesi thesi)
        {
            return await _stelexiService.DeleteStelexosInService(s.Id ?? 0, thesi);
        }

        private async void OnSaveStelexos()
        {
            var result = await _stelexiService.UpdateStelexosInService(StelexosDto, StelexosDto.Thesi);
            if (result)
            {
                OnPropertyChanged(nameof(StelexosDto));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

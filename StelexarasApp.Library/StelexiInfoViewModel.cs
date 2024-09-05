using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.ViewModels
{
    public class StelexiInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public StelexosDto StelexosDto { get; set; }
        public ICommand SaveCommand { get; }


        private readonly IStelexiService _stelexiService;

        public StelexiInfoViewModel(StelexosDto stelexosDto, IStelexiService stelexiService)
        {
            _stelexiService = stelexiService;
            StelexosDto = stelexosDto;
            SaveCommand = new Command(OnSave);
        }

        public async Task<bool> DeleteStelexos(StelexosDto s, Thesi thesi)
        {
            return await _stelexiService.DeleteStelexosInService(s.Id ?? 0, thesi);
        }

        private async void OnSave()
        {
            // ToDo: add better Save logic goes here, e.g., calling the service to update the entity in the database.
            await _stelexiService.AddStelexosInService(StelexosDto, StelexosDto.Thesi);
        }

        public async Task<bool> UpdateStelexos(StelexosDto stelexosDto, Thesi thesi)
        {
            return await _stelexiService.UpdateStelexosInService(stelexosDto, thesi);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

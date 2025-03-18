using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels
{
    public class StelexosInfoViewModel : INotifyPropertyChanged
    {
        private readonly IStaffService _stelexiService;
        private readonly bool skiniIsChanged;
        private IStelexosDto _stelexosDto;

        public IStelexosDto StelexosDto
        {
            get => _stelexosDto;
            set
            {
                if (_stelexosDto != value)
                {
                    _stelexosDto = value;
                    OnPropertyChanged(nameof(StelexosDto));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand SaveStelexosCommand { get; }
        public string StatusMessage { get; set; } = string.Empty;

        public StelexosInfoViewModel(IStelexosDto stelexosDto, IStaffService stelexiService)
        {
            _stelexosDto = stelexosDto ?? throw new ArgumentNullException(nameof(stelexosDto));
            _stelexiService = stelexiService;
            skiniIsChanged = false;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos(IStelexosDto s)
        {
            return await _stelexiService.DeleteStelexosByIdInService(s.Id, s.Thesi);
        }

        public async Task OnSaveStelexos()
        {
            if(skiniIsChanged)
                await MoveOmadarxisToAnotherSkini();
            
            var result = await _stelexiService.UpdateStelexosInService(StelexosDto);
            StatusMessage = result ? "Save successful" : "Save failed";
            OnPropertyChanged(nameof(StelexosDto));
        }

        public async Task MoveOmadarxisToAnotherSkini()
        {
            var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(StelexosDto.Id, StelexosDto.XwrosName);
            StatusMessage = result ? "Move successful" : "Move failed";
            OnPropertyChanged(nameof(StelexosDto));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

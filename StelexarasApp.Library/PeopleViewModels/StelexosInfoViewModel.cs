using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.ViewModels.PeopleViewModels
{
    public class StelexosInfoViewModel : INotifyPropertyChanged
    {
        private readonly IStaffService _stelexiService;
        private readonly bool skiniIsChanged;
        private StelexosDto _stelexosDto;

        public StelexosDto StelexosDto
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

        public StelexosInfoViewModel(StelexosDto stelexosDto, IStaffService stelexiService)
        {
            _stelexiService = stelexiService;
            _stelexosDto = stelexosDto ?? throw new ArgumentNullException(nameof(stelexosDto)); ;
            skiniIsChanged = false;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos(StelexosDto s)
        {
            return await _stelexiService.DeleteStelexosByIdInService(s.Id ?? 1, s.Thesi);
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
            var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(StelexosDto.Id ?? 0, StelexosDto.XwrosName);
            StatusMessage = result ? "Move successful" : "Move failed";
            OnPropertyChanged(nameof(StelexosDto));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

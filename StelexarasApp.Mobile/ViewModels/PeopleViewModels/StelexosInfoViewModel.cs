using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels
{
    public class StelexosInfoViewModel : INotifyPropertyChanged
    {
        private readonly int Id;
        private readonly IStaffService _stelexiService;
        private readonly bool skiniIsChanged;
        private IStelexosDto _stelexos;
        
        public IStelexosDto Stelexos
        {
            get => _stelexos;
            set
            {
                if (_stelexos != value)
                {
                    _stelexos = value;
                    OnPropertyChanged(nameof(Stelexos));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand SaveStelexosCommand { get; }
        public string StatusMessage { get; set; } = string.Empty;

        public StelexosInfoViewModel(IStelexosDto stelexos, int id, IStaffService stelexiService)
        {
            _stelexiService = stelexiService;
            skiniIsChanged = false;
            Id = id;
            _stelexos = stelexos;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos()
        {
            return await _stelexiService.DeleteStelexosByIdInService(Id);
        }

        public async Task OnSaveStelexos()
        {
            if (skiniIsChanged)
                await MoveOmadarxisToAnotherSkini();

            var result = await _stelexiService.UpdateStelexosInService(_stelexos);
            StatusMessage = result ? "Save successful" : "Save failed";
            OnPropertyChanged(nameof(Stelexos));
        }

        public async Task MoveOmadarxisToAnotherSkini()
        {
            var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(Id, Stelexos.XwrosName);
            StatusMessage = result ? "Move successful" : "Move failed";
            OnPropertyChanged(nameof(Stelexos));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

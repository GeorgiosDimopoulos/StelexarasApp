using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels
{
    public class StelexosInfoViewModel : INotifyPropertyChanged
    {
        private readonly int Id;
        private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> _stelexiService;
        private readonly bool skiniIsChanged;
        private StelexosDtoBase _stelexos;

        public StelexosDtoBase Stelexos
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

        public StelexosInfoViewModel(StelexosDtoBase stelexos, int id, IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> stelexiService)
        {
            _stelexiService = stelexiService;
            skiniIsChanged = false;
            Id = id;
            _stelexos = stelexos;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos()
        {
            var request = new DeleteStelexosRequest { Id = Id };
            return await _stelexiService.DeleteStelexos(request);
        }

        public async Task OnSaveStelexos()
        {
            if (skiniIsChanged)
                await MoveOmadarxisToAnotherSkini();

            var request = new UpdateStelexosRequest() { Id = Id };
            var result = await _stelexiService.UpdateStelexos(Id, request);
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

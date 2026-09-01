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

        public StelexosInfoViewModel(StelexosDtoBase stelexos, int id, IStaffService stelexiService)
        {
            _stelexiService = stelexiService;
            skiniIsChanged = false;
            Id = id;
            _stelexos = stelexos;
            SaveStelexosCommand = new Command(async () => await OnSaveStelexos());
        }

        public async Task<bool> DeleteStelexos()
        {
            var res = await _stelexiService.DeleteStelexos(Id);
            if (res.IsSuccess)
            {
                StatusMessage = "Delete successful";
                return true;
            }
            else
            {
                StatusMessage = $"Delete failed: {res.Errors.FirstOrDefault()?.Message}";
                return false;
            }
        }

        public async Task OnSaveStelexos()
        {
            //if (skiniIsChanged)
            //    await MoveOmadarxisToAnotherSkini();

            var request = new UpdateStelexosRequest() { Id = Id };
            var result = await _stelexiService.UpdateStelexos(Id, request);
            StatusMessage = result.IsSuccess ? "Save successful" : $"Save failed: {result.Errors.FirstOrDefault()?.Message}";
            OnPropertyChanged(nameof(Stelexos));
        }

        public async Task MoveOmadarxisToAnotherSkini()
        {
            //var result = await _stelexiService.UpdateStelexos(Thesi.Omadarxis, Id, Stelexos.XwrosName);
            //if (result.IsSuccess)
            //{
            //    StatusMessage = "Move successful";
            //}
            //else
            //{
            //    StatusMessage = $"Move failed: {result.Errors.FirstOrDefault()?.Message}";
            //}

            OnPropertyChanged(nameof(Stelexos));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

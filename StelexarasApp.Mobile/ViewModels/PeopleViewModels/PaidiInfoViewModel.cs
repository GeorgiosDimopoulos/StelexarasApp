using System.ComponentModel;
using System.Windows.Input;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels
{
    public class PaidiInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private ICommand SavePaidiCommand { get; }

        public PaidiResponse PaidiDto { get; set; } = new PaidiResponse();
        public string SkiniName { get; set; }
        public string StatusMessage { get; set; } = string.Empty;

        public PaidiInfoViewModel(PaidiResponse paidiDto, IPaidiaService peopleService, string skini)
        {
            PaidiDto = paidiDto;
            _paidiaService = peopleService;
            SavePaidiCommand = new Command(async () => await OnSavePaidi());
            SkiniName = skini;
        }

        public async Task<bool> DeletePaidiAsync(int id)
        {
            var result = await _paidiaService.DeletePaidiInService(id);
            if (result.IsSuccess)
            {
                StatusMessage = "Delete successful";
                return true;
            }
            else
            {
                StatusMessage = result.Errors.FirstOrDefault()?.Message ?? "Delete failed";
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<bool> OnSavePaidi()
        {
            var paidiToUpdate = new UpdatePaidiRequest
            {
                Id = PaidiDto.Id,
                FirstName = PaidiDto.FirstName,
                Sex = PaidiDto.Sex,
                LastName = PaidiDto.LastName,
                Age = PaidiDto.Age,
                SkiniName = SkiniName
            };
            var result = await _paidiaService.UpdatePaidiInService(paidiToUpdate);
            if (!result.IsSuccess)
            {
                StatusMessage = result.Errors.FirstOrDefault()?.Message ?? "Save failed";
                return false;
            }
            StatusMessage = "Save successful";
            OnPropertyChanged(nameof(SkiniName));
            OnPropertyChanged(nameof(PaidiDto));
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

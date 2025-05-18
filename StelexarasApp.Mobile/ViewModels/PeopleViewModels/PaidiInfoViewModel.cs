using System.ComponentModel;
using System.Windows.Input;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels
{
    public class PaidiInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;
        private ICommand SavePaidiCommand { get; }

        public PaidiResponse PaidiDto { get; set; } = new PaidiResponse();
        public string SkiniName { get; set; }
        public string StatusMessage { get; set; } = string.Empty;

        public PaidiInfoViewModel(PaidiResponse paidiDto, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> peopleService, string skini)
        {
            PaidiDto = paidiDto;
            _paidiaService = peopleService;
            SavePaidiCommand = new Command(async () => await OnSavePaidi());
            SkiniName = skini;
        }

        public async Task<bool> DeletePaidiAsync(int id)
        {
            var deletePaidiDto = new DeletePaidiRequest
            {
                Id = id
            };
            if (await _paidiaService.DeletePaidiInService(deletePaidiDto))
            {
                StatusMessage = "Delete successful";
                return true;
            }
            else
            {
                StatusMessage = "Delete failed";
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<bool> OnSavePaidi()
        {
            var paidiToUpdate = new UpdatePaidiRequest
            {
                Id = PaidiDto.Id,
                FullName = PaidiDto.FullName,
                Age = PaidiDto.Age,
                SkiniName = SkiniName
            };
            var result = await _paidiaService.UpdatePaidiInService(paidiToUpdate);
            StatusMessage = result ? "Save successful" : "Save failed";

            if (result)
            {
                OnPropertyChanged(nameof(SkiniName));
                OnPropertyChanged(nameof(PaidiDto));
                return true;
            }

            return false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

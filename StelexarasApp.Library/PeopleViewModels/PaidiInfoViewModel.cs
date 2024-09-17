using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Windows.Input;

namespace StelexarasApp.ViewModels.PeopleViewModels
{
    public class PaidiInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private ICommand SavePaidiCommand { get; }

        public PaidiDto PaidiDto { get; set; } = new PaidiDto();
        public string SkiniName { get; set; }
        public string StatusMessage { get; set; } = string.Empty;

        public PaidiInfoViewModel(PaidiDto paidiDto,IPaidiaService peopleService, string skini)
        {
            PaidiDto = paidiDto;
            _paidiaService = peopleService;
            SavePaidiCommand = new Command(async () => await OnSavePaidi());
            SkiniName = skini;
        }

        public async Task<bool> DeletePaidiAsync(int id)
        {
            if (await _paidiaService.DeletePaidiInDb(id))
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
            var result = await _paidiaService.UpdatePaidiInDb(PaidiDto);
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

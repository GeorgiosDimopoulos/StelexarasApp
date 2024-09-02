using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class ChildInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        
        public PaidiDto PaidiDto { get; set; }
        public ObservableCollection<SkiniDto> Skines { get; set; }

        public ChildInfoViewModel(IPaidiaService peopleService, ObservableCollection<SkiniDto> skines)
        {
            _paidiaService = peopleService;
            PaidiDto = new PaidiDto();
            Skines = skines;
        }

        public async Task DeletePaidiAsync(PaidiDto paidi)
        {
            bool isDeleted = await _paidiaService.DeletePaidiInDb(paidi.Id);

            if (isDeleted)
            {
                // await _paidiaService.GetSkines();
                OnPropertyChanged(nameof(Skines));
            }
        }

        public async Task<bool> UpdatePaidiAsync(PaidiDto paidiDto, SkiniDto skini)
        {
            if (skini is null)
            {
                return false;
            }
            PaidiDto = paidiDto;
            PaidiDto.SkiniName = skini.Name;

            var result = await _paidiaService.UpdatePaidiInDb(PaidiDto);
            
            if (result)
            {
                // await _paidiaService.GetSkines();
                OnPropertyChanged(nameof(Skines));
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

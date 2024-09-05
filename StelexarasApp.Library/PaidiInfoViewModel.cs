using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class PaidiInfoViewModel(IPaidiaService peopleService, ObservableCollection<SkiniDto> skines) : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService = peopleService;

        public PaidiDto PaidiDto { get; set; } = new PaidiDto();
        public ObservableCollection<SkiniDto> Skines { get; set; } = skines;

        public async Task<bool> DeletePaidiAsync(int id)
        {
            return await _paidiaService.DeletePaidiInDb(id);
        }

        public async Task<bool> UpdatePaidiAsync(PaidiDto paidiDto, SkiniDto skini)
        {
            if (skini?.Name is null)
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

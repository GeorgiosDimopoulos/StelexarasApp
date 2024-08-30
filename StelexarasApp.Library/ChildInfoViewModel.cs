using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class ChildInfoViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        public PaidiDto PaidiDto { get; set; }
        public Skini Skini { get; set; }

        public ObservableCollection<Skini> Skines { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        public ChildInfoViewModel(IPaidiaService peopleService, PaidiDto paidiDto, ObservableCollection<Skini> skines)
        {
            _paidiaService = peopleService;

            PaidiDto = paidiDto;
            FullName = paidiDto.FullName;
            Age = paidiDto.Age;
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

        public async Task<bool> UpdatePaidiAsync(string id, string fullName, Skini skini, int age)
        {
            if (skini is null)
            {
                return false;
            }

            PaidiDto.SkiniName = skini.Name;
            PaidiDto.Age = age;
            PaidiDto.FullName = fullName;

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

using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.Services.IServices;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class StelexiInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IStelexiService _stelexiService;

        public StelexiInfoViewModel(IStelexiService stelexiService)
        {
            _stelexiService = stelexiService;
        }

        public async Task<bool> DeleteStelexos(int id, Thesi thesi)
        {
            return await _stelexiService.DeleteStelexosInDb(id, thesi);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

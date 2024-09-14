using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class StaffViewModel(IStaffService personalService, string thesiStr) : INotifyPropertyChanged
    {
        private readonly IStaffService personalService1 = personalService;

        public string Title = thesiStr;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<IEnumerable<StelexosDto>> GetStelexoiAnaThesi(Thesi thesi)
        {
            return await personalService1.GetStelexoiAnaThesiInService(thesi);
        }

        public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotita(Koinotita koinotita)
        {
            return await personalService1.GetOmadarxesSeKoinotitaInService(koinotita);
        }

        public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxes()
        {
            return await personalService1.GetKoinotarxesInService();
        }
    }
}

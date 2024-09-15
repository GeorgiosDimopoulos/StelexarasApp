using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
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

        public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotita(KoinotitaDto koinotitaDto)
        {
            return await personalService1.GetOmadarxesSeKoinotitaInService(koinotitaDto);
        }

        public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomea(TomeasDto tomeasDto)
        {
            return await personalService1.GetOmadarxesSeTomeaInService(tomeasDto);
        }

        public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomeasDto)
        {
            return await personalService1.GetKoinotarxesSeTomeaInService(tomeasDto);
        }

        public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxes()
        {
            return await personalService1.GetAllKoinotarxesInService();
        }
    }
}

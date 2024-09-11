using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class StaffViewModel(IStaffService personalService, Thesi thesi) : INotifyPropertyChanged
    {
        private readonly IStaffService personalService1 = personalService;

        private Thesi thesi;
        public Thesi Thesi1
        {
            get => thesi;
            set
            {
                thesi = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Title => Thesi1 switch
        {
            Thesi.Tomearxis => "Tomearxis",
            Thesi.Ekpaideutis => "Ekpaideutis",
            Thesi.Omadarxis => "Omadarxis",
            Thesi.Koinotarxis => "Koinotarxis",
            _ => "Unknown Thesi Title"
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<IEnumerable<StelexosDto>> GetStelexoi(Thesi thesi)
        {
            return await personalService1.GetStelexoiAnaThesiInService(thesi);
        }
    }
}

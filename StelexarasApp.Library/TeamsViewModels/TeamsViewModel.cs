using StelexarasApp.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels.TeamsViewModels
{
    public class TeamsViewModel : INotifyPropertyChanged
    {
        private readonly IPaidiaService _paidiaService;
        private readonly ITeamsService _teamsService;
        
        public string Title { get; set; }

        public TeamsViewModel(IPaidiaService paidiaService, ITeamsService teamsService)
        {
            _paidiaService = paidiaService;
            _teamsService = teamsService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

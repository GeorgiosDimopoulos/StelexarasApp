using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Services.Services
{
    public class TeamsService
    {
        private readonly ITeamsRepository _teamsRepository;
        public TeamsService(ITeamsRepository teamsRepository) 
        {
            _teamsRepository = teamsRepository;
        }

        public Task<IEnumerable<Skini>> GetSkines()
        {
            return _teamsRepository.GetSkines();
        }

        //public Task<Skini> GetSkiniByName(string name)
        //{
        //    return _teamsRepository.GetSkiniByName(name);
        //}
    }
}

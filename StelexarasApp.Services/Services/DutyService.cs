using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class DutyService : IDutyService
    {
        private readonly IDutyRepository? _dutyRepository;

        public DutyService(IDutyRepository dutyRepository)
        {
            try
            {
                _dutyRepository = dutyRepository;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> AddDutyInService(Duty duty)
        {
            try 
            {
                if (string.IsNullOrEmpty(duty.Name) || _dutyRepository is null)
                    throw new ArgumentException("Duty name or duty Repository cannot be null");

                return await _dutyRepository.AddDutyInDb(duty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteDutyInService(int dutyId)
        {
            if (_dutyRepository is null)
                throw new ArgumentException("Duty name or duty Repository cannot be null");
            return await _dutyRepository.DeleteDutyInDb(dutyId);
        }

        public async Task<bool> UpdateDutyInService(string dutyName, Duty updatedDuty)
        {
            if (string.IsNullOrEmpty(dutyName) || updatedDuty is null || _dutyRepository is null)
                throw new ArgumentException("Duty name or updated duty or duty Repository cannot be null");

            return await _dutyRepository.UpdateDutyInDb(dutyName, updatedDuty);
        }

        public async Task<IEnumerable<Duty>> GetDutiesInService()
        {
            if (_dutyRepository is null)
                throw new ArgumentException("Duty Repository cannot be null");
            return await _dutyRepository.GetDutiesFromDb();
        }

        public Task<bool> HasData()
        {
            if (_dutyRepository is null)
                throw new ArgumentException("Duty Repository cannot be null");
            return Task.FromResult(_dutyRepository.GetDutiesFromDb().Result.Any());
        }

        public Task<Duty> GetDutyByIdInService(int id)
        {
            if (_dutyRepository is null)
                throw new ArgumentException("Duty Repository cannot be null");
            return _dutyRepository.GetDutyFromDb(id.ToString());
        }
    }
}
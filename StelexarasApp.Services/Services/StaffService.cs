using AutoMapper;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.IServices;
using StelexarasApp.DataAccess.Helpers;

namespace StelexarasApp.Services.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository? _stelexiRepository;
        private readonly IMapper? _mapper;

        public StaffService(IMapper mapper, IStaffRepository stelexiRepository)
        {
            try
            {
                _mapper = mapper;
                _stelexiRepository = stelexiRepository;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> AddStelexosInService(StelexosDto stelexosDto, Thesi thesi)
        {
            var stelexos = _mapper!.Map<Stelexos>(stelexosDto);
            if (stelexos == null) 
            { 
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, Mapping Stelexos with stelexosDto failed", TypeOfOutput.DbErroMessager);
                throw new ArgumentNullException(nameof(stelexos), "Mapping failed");
            }

            var result = await _stelexiRepository!.AddStelexosInDb(stelexos);
            if (!result)
                return false;
            return true;
        }

        public async Task<bool> DeleteStelexosInService(int id, Thesi thesi)
        {
            return await _stelexiRepository!.DeleteStelexosInDb(id, thesi);
        }

        public async Task<IEnumerable<StelexosDto>> GetStelexoiAnaThesiInService(Thesi thesi)
        {
            try
            {
                var stelexi = await _stelexiRepository!.GetStelexoiAnaThesiFromDb(thesi);
                if (stelexi == null)
                    return null!;
                var stelexiDtos = _mapper?.Map<IEnumerable<StelexosDto>>(stelexi);
                return stelexiDtos;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null;
            }
        }

        public async Task<Stelexos> GetStelexosByIdInService(int id, Thesi thesi)
        {
            try
            {
                return await _stelexiRepository.FindStelexosByIdInDb(id, thesi);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, int newSkiniId)
        {
            var result = await _stelexiRepository.MoveOmadarxisToAnotherSkiniInDb(Id, newSkiniId);

            if (!result)
                return false;
            return true;
        }

        public async Task<bool> UpdateStelexosInService(StelexosDto stelexosDto)
        {
            var stelexos = MapDtoToEntity(stelexosDto);
            if (stelexos == null)
                return false;
            return await _stelexiRepository.UpdateStelexosInDb(stelexos);
        }

        private Stelexos MapDtoToEntity(StelexosDto stelexosDto)
        {
            return stelexosDto.Thesi switch
            {
                Thesi.Omadarxis => _mapper.Map<Omadarxis>(stelexosDto),
                Thesi.Koinotarxis => _mapper.Map<Koinotarxis>(stelexosDto),
                Thesi.Tomearxis => _mapper.Map<Tomearxis>(stelexosDto),
                _ => _mapper.Map<Stelexos>(stelexosDto),
            };
        }
    }
}

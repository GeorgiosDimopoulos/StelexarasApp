using AutoMapper;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.DataAccess.Models.Domi;

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

        public async Task<bool> DeleteStelexosByIdInService(int id, Thesi thesi)
        {
            return await _stelexiRepository!.DeleteStelexosInDb(id, thesi);
        }

        public async Task<IEnumerable<OmadarxisDto>> GetAllOmadarxesInService()
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                return (IEnumerable<OmadarxisDto>)await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxesInService()
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                return (IEnumerable<KoinotarxisDto>)await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<StelexosDto> GetStelexosByNameInService(string name, Thesi? thesi)
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null || thesi is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var stelexosInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb((Thesi)thesi, name);
                var stelexosInService = _mapper.Map<StelexosDto>(stelexosInDb);
                return stelexosInService;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<TomearxisDto>> GetAllTomearxesInService()
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var stelexosInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Tomearxis, string.Empty);
                var stelexosInService = _mapper.Map<StelexosDto>(stelexosInDb);
                return (IEnumerable<TomearxisDto>)stelexosInService;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(KoinotitaDto koinotita)
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name);
                if (stelexoiInDb is null || !stelexoiInDb.Any())
                    return [];

                var omadarxesInService = _mapper.Map<IEnumerable<OmadarxisDto>>(stelexoiInDb);
                return omadarxesInService;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<StelexosDto> GetStelexosByIdInService(int id, Thesi? thesi)
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null || thesi is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id, thesi);
                if (stelexosInDb is null)
                    return null!;

                var stelexosInService = _mapper.Map<StelexosDto>(stelexosInDb);
                return stelexosInService;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, int newSkiniId)
        {
            if (_stelexiRepository is null || _mapper is null || Id is <= 0 || newSkiniId <= 0)
                throw new ArgumentException("StaffRepository, Ids or _mapper cannot be null");

            var result = await _stelexiRepository.MoveOmadarxisToAnotherSkiniInDb(Id, newSkiniId);

            if (!result)
                return false;
            return true;
        }

        public async Task<bool> UpdateStelexosInService(StelexosDto stelexosDto)
        {
            if (_stelexiRepository is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexos = MapDtoToEntity(stelexosDto);
            if (stelexos == null)
                return false;

            return await _stelexiRepository.UpdateStelexosInDb(stelexos);
        }

        public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomeaInService(TomeasDto tomeaDto)
        {
            try
            {
                if (tomeaDto is null || string.IsNullOrWhiteSpace(tomeaDto.Name))
                    throw new ArgumentException("Invalid tomea information.");

                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var omadarxes = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, tomeaDto.Name);

                if (omadarxes is null || !omadarxes.Any())
                    return [];

                var omadarxesDto = _mapper.Map<IEnumerable<OmadarxisDto>>(omadarxes);
                return omadarxesDto;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomea)
        {
            try
            {
                if (_stelexiRepository is null || _mapper is null)
                    throw new ArgumentException("StaffRepository or _mapper cannot be null");

                var koinotarxes = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);

                if (koinotarxes is null || !koinotarxes.Any())
                    return [];

                var koinotarxesDto = _mapper.Map<IEnumerable<KoinotarxisDto>>(koinotarxes);
                return koinotarxesDto;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        private Stelexos MapDtoToEntity(StelexosDto stelexosDto)
        {
            return stelexosDto.Thesi switch
            {
                Thesi.Omadarxis => _mapper!.Map<Omadarxis>(stelexosDto),
                Thesi.Koinotarxis => _mapper!.Map<Koinotarxis>(stelexosDto),
                Thesi.Tomearxis => _mapper!.Map<Tomearxis>(stelexosDto),
                _ => _mapper!.Map<Stelexos>(stelexosDto),
            };
        }
    }
}

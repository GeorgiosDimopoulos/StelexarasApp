using AutoMapper;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class StelexiService : IStelexiService
    {
        private readonly IStelexiRepository? _stelexiRepository;
        private readonly IMapper? _mapper;

        public StelexiService(IMapper mapper, IStelexiRepository stelexiRepository)
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
                throw new ArgumentNullException(nameof(stelexos), "Mapping failed");

            var result = await _stelexiRepository!.AddStelexosInDb(stelexos);
            if (!result)
                return false;
            return true;
        }

        public async Task<bool> DeleteStelexosInService(int id, Thesi thesi)
        {
            return await _stelexiRepository.DeleteStelexosInDb(id, thesi);
        }

        public async Task<IEnumerable<Stelexos>> GetStelexoiAnaThesiInService(Thesi thesi)
        {
            try
            {
                return await _stelexiRepository.GetStelexoiAnaThesiFromDb(thesi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
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

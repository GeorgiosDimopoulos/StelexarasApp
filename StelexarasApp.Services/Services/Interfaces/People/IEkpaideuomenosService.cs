namespace StelexarasApp.Services.Services.IServices
{
    public interface IEkpaideuomenosService : IPaidiServiceBase<CreateEkpaideuomenosRequest, UpdateEkpaideuomenosRequest, EkpaideuomenosResponse>
    {
        Task<bool> AddEkpaideuomenosInService(CreateEkpaideuomenosRequest paidiDto);

        Task<bool> DeleteEkpaideuomenosInService(int id);

        Task<bool> UpdateEkpaideuomenosInService(UpdateEkpaideuomenosRequest paidiDto);

        Task<IEnumerable<EkpaideuomenosResponse>> GetEkpaideuomenoiInService(PaidiType type);

        Task<EkpaideuomenosResponse> GetEkpaideuomenosByIdInService(int id);

        Task<bool> MoveEkpaideuomenosToNewSkiniInService(int paidiId, int newSkiniId);
    }
}

namespace StelexarasApp.Services.Interfaces.People.Children;

public interface IEkpaideuomenosService : IPaidiServiceBase<CreateEkpaideuomenosRequest, UpdateEkpaideuomenosRequest, DeleteEkpaideuomenosRequest, EkpaideuomenosResponse>
{
    Task<bool> CreateEkpaideuomenosInService(CreateEkpaideuomenosRequest paidiDto);

    Task<bool> DeleteEkpaideuomenosInService(DeleteEkpaideuomenosRequest deleteEkpaideuomenosRequest);

    Task<bool> UpdateEkpaideuomenosInService(UpdateEkpaideuomenosRequest paidiDto);

    Task<IEnumerable<EkpaideuomenosResponse>> GetEkpaideuomenoiInService();

    Task<EkpaideuomenosResponse> GetEkpaideuomenosByIdInService(int id);

    Task<bool> MoveEkpaideuomenosToNewSkiniInService(int paidiId, int newSkiniId);
}

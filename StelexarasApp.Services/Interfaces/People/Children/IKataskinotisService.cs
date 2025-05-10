namespace StelexarasApp.Services.Interfaces.People.Children;

public interface IKataskinotisService : IPaidiServiceBase<CreateKataskinotisRequest, UpdateKataskinotisRequest, DeleteKataskinotisRequest, KataskinotisResponse>
{
    Task<bool> CreateKataskinotisInService(CreateKataskinotisRequest paidiDto);

    Task<bool> DeleteKataskinotisInService(DeleteKataskinotisRequest request);

    Task<bool> UpdateKataskinotisInService(UpdateKataskinotisRequest paidiDto);

    Task<IEnumerable<KataskinotisResponse>> GetKataskinotesInService();

    Task<KataskinotisResponse> GetKataskinotisByIdInService(int id);

    Task<bool> MoveKataskinotisToNewSkiniInService(int paidiId, int newSkiniId);
}

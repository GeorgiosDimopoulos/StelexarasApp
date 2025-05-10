namespace StelexarasApp.Services.Services.IServices.People;

public interface IKataskinotisService : IPaidiServiceBase<CreateKataskinotisRequest, UpdateKataskinotisRequest, KataskinotisResponse>
{
    Task<bool> AddKataskinotisInService(CreateKataskinotisRequest paidiDto);

    Task<bool> DeleteKataskinotisInService(int id);

    Task<bool> UpdateKataskinotisInService(UpdateKataskinotisRequest paidiDto);

    Task<IEnumerable<KataskinotisResponse>> GetKataskiknotesInService();

    Task<KataskinotisResponse> GetKataskinotisByIdInService(int id);

    Task<bool> MoveKataskinotisToNewSkiniInService(int paidiId, int newSkiniId);
}

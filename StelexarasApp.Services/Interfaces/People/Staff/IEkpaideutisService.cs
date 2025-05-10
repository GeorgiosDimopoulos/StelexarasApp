namespace StelexarasApp.Services.Interfaces.People.Staff;

public interface IEkpaideutisService : IStaffService<CreateEkpaideutisRequest, UpdateEkpaideutisRequest, DeleteEkpaideutisRequest, EkpaideutisResponse>
{
    Task<IEnumerable<EkpaideutisResponse>> GetAllEkpaideutesInService();
    Task<EkpaideutisResponse> GetEkpaideutis(string tomeaName);
    Task<bool> CreateEkpaideutisInService(CreateEkpaideutisRequest request);
    Task<bool> UpdateEkpaideutisInService(int id, UpdateEkpaideutisRequest request);
    Task<bool> DeleteEkpaideutisInService(DeleteEkpaideutisRequest request);
}
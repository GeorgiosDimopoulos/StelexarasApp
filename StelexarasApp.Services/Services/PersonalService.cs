using StelexarasApp.DataAccess;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly AppDbContext _dbContext;

        public PersonalService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

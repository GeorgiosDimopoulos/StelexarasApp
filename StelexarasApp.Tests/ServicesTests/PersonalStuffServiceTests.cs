using StelexarasApp.DataAccess;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Tests.ServicesTests
{
    public class PersonalStuffServiceTests
    {
        private readonly PersonalService personalStuffService;
        private readonly AppDbContext _dbContext;

        public PersonalStuffServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            personalStuffService = new PersonalService(_dbContext);
        }
    }
}

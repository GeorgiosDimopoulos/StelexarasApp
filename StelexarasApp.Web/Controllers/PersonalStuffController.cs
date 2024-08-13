using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess;

namespace StelexarasApp.Web.Controllers
{
    public class PersonalStuffController : ControllerBase
    {
        private AppDbContext _appDbContext;
        public PersonalStuffController(AppDbContext context)
        {
            _appDbContext = context;
        }
    }
}

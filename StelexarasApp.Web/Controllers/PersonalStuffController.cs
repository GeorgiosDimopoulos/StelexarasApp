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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PersonalStuff>>> GetPersonalStuff()
        //{
        //    return await _appDbContext.Mythings.ToListAsync();
        //}
    }
}

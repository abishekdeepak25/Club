using Microsoft.AspNetCore.Mvc;

namespace NetClubApi.CourtModule
{
    public class CourtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

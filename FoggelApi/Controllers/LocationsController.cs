using Microsoft.AspNetCore.Mvc;

namespace FoggelApi.Controllers
{
    public class LocationsController : Controller
    {
        
        public IActionResult Locations()
        {
            return View();
        }

    }
}

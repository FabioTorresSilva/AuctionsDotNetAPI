using Microsoft.AspNetCore.Mvc;

namespace AuctionProject.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

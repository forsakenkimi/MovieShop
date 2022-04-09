using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        //localhost//movies/detail/Id
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}

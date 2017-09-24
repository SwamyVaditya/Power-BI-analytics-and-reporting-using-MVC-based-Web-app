using System.Web.Mvc;

namespace BuilderWebApp3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return null;
        }

        public void GoToHome() {
            //return View("MyHomePage");
            Response.Redirect("/Default.aspx");
        }

        public void GoToRegisterNewUser()
        {
            //return View("RegisterUser");
            Response.Redirect("/AdminPage.aspx");
        }

    }
}
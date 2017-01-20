using System;
using System.Web.Mvc;

namespace Novi.WebAppTest.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return Content("home page");
		}

		public ActionResult Throw()
		{
			throw new Exception("This is an exception.");
		}
	}
}
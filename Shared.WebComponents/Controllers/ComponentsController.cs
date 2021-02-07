using Microsoft.AspNetCore.Mvc;
using Shared.WebComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.WebComponents.Controllers
{
    public class ComponentsController : Controller
    {
        public ActionResult HeaderMenu()
        {
            HeaderMenuViewModel model = new HeaderMenuViewModel {
                FirstName = "Test",
                LastName = "User",
                LastLoginTimestamp = DateTime.Now.AddHours(-3)
            };
            return PartialView(model);
        }

        public ActionResult Footer()
        {           
            return PartialView();
        }

        public ActionResult BlueBox()
        {
            return PartialView();
        }

    }
}

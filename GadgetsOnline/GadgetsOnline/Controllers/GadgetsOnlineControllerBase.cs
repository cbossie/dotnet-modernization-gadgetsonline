using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetsOnline.Controllers
{
    public abstract class GadgetsOnlineControllerBase : Controller
    {

        public const string CartSessionKey = "CartId";

        public string GetCartId()
        {
            if (HttpContext.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    HttpContext.Session[CartSessionKey] = HttpContext.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    HttpContext.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return HttpContext.Session[CartSessionKey].ToString();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetsOnline.Models;
using GadgetsOnline.Services;
using GadgetsOnline.ViewModel;

namespace GadgetsOnline.Controllers
{
    public class ShoppingCartController : GadgetsOnlineControllerBase
    {

        Inventory inventory;

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = new ShoppingCart();
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(GetCartId()),
                CartTotal = cart.GetTotal(GetCartId())
            };
            // Return the view
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var cart = new ShoppingCart();
            
            cart.AddToCart(GetCartId(), id);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = new ShoppingCart();
            int itemCount = cart.RemoveFromCart(GetCartId(), id);
            inventory = new Inventory();
            var productName = inventory.GetProductNameById(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(GetCartId()),
                CartCount = cart.GetCount(GetCartId()),
                ItemCount = itemCount,
                DeleteId = id
            };
            return RedirectToAction("Index");            
        }

    }
}
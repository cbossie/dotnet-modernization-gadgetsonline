using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GadgetsOnline.Models;

namespace GadgetsOnline.Services
{
    public class OrderProcessing
    {
        GadgetsOnlineEntities store = new GadgetsOnlineEntities();
        internal bool ProcessOrder(Order order, string shoppingCartId)
        {
            store.Orders.Add(order);
            store.SaveChanges();

            //Process the order
            var cart = new ShoppingCart();
            cart.CreateOrder(shoppingCartId, order);

            return true;
        }
    }
}
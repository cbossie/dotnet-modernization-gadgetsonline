using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GadgetsOnline.Models;

namespace GadgetsOnline.Services
{
    public class ShoppingCart
    {
        GadgetsOnlineEntities store = new GadgetsOnlineEntities();

        internal int CreateOrder(string shoppingCartId, Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems(shoppingCartId);

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.Price);

                store.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            store.SaveChanges();

            // Empty the shopping cart
            EmptyCart(shoppingCartId);

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        private void EmptyCart(string shoppingCartId)
        {
            var cartItems = store.Carts.Where(cart => cart.CartId == shoppingCartId);

            foreach (var cartItem in cartItems)
            {
                store.Carts.Remove(cartItem);
            }

            // Save changes
            store.SaveChanges();
        }

        public void AddToCart(string shoppingCartId, int id)
        {            
            var cartItem = store.Carts.SingleOrDefault(
                        c => c.CartId == shoppingCartId
                        && c.ProductId == id);


            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = id,
                    CartId = shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                store.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

            // Save changes
            store.SaveChanges();
        }

        public int GetCount(string shoppingCartId)
        {
            int? count = (from cartItems in store.Carts
                          where cartItems.CartId == shoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        internal int RemoveFromCart(string shoppingCartId, int id)
        {
            // Get the cart
            var cartItem = store.Carts.Single(
                            cart => cart.CartId == shoppingCartId
                            && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    store.Carts.Remove(cartItem);
                }

                // Save changes
                store.SaveChanges();
            }

            return itemCount;
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return store.Carts.Where(cart => cart.CartId == shoppingCartId).ToList();
        }

        public decimal GetTotal(string shoppingCartId)
        {
            decimal? total = (from cartItems in store.Carts
                              where cartItems.CartId == shoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;

        }
    }

}
 using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using GadgetsOnline.Models;

namespace GadgetsOnline.DTO
{
    public static class DTOHelper
    {
        public static List<DTO_Product> GetDTOProductList(List<Product> products)
        {
            var dto_products = new List<DTO_Product>();

            foreach (var product in products)
            {
                var categoryDTO = new DTO_Category
                {
                    Name = product.Category.Name,
                    CategoryId = product.Category.CategoryId,
                    Description = product.Category.Description,
                };

                dto_products.Add(new DTO_Product
                {
                    Category = categoryDTO,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    ProductId = product.ProductId,
                    ProductArtUrl = product.ProductArtUrl,
                });
            }

            return dto_products;
        }

        /// <summary>
        /// Converts EF cart list to DTO cart list
        /// </summary>
        /// <param name="carts"></param>
        /// <returns></returns>
        public static List<DTO_Cart> GetGetDTOCartList(List<Cart> carts)
        {
            var dto_carts = new List<DTO_Cart>();

            foreach(var cart in carts)
            {
                var dto_cart = GetDTOCart(cart);
                dto_carts.Add(dto_cart);
            }
            return dto_carts;
        }

        /// <summary>
        /// Converts a single EF cart to DTO Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static DTO_Cart GetDTOCart(Cart cart)
        {
            var dto_cart = new DTO_Cart
            {
                CartId = cart.CartId,
                Count = cart.Count,
                DateCreated = cart.DateCreated,
                ProductId = cart.ProductId,
                RecordId = cart.RecordId,
                Product = GetDTOProduct(cart.Product)
            };
            return dto_cart;
        }

        public static DTO_Product GetDTOProduct(Product product)
        {
            var categoryDTO = new DTO_Category
            {
                Name = product.Category.Name,
                CategoryId = product.Category.CategoryId,
                Description = product.Category.Description,
            };

            var dto_product = new DTO_Product
            {
                Category = categoryDTO,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                ProductArtUrl = product.ProductArtUrl,
                ProductId = product.ProductId,
            };
            return dto_product;
        }
    }
}

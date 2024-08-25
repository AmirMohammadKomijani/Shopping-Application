﻿using System.Collections.Generic;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }

    }
}

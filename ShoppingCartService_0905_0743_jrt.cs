// 代码生成时间: 2025-09-05 07:43:10
 * Features:
 * - Add items to the cart
 * - Remove items from the cart
 * - Update quantities of items in the cart
 * - Get the cart's content
 * - Clear the cart
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApp
{
    // Entity representing a product in the database
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Entity representing an item in the shopping cart
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    // Entity representing the shopping cart
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

    // Service class for shopping cart operations
    public class ShoppingCartService
    {
        private readonly DbContext _context;

        public ShoppingCartService(DbContext context)
        {
            _context = context;
        }

        // Adds a product to the cart
        public ShoppingCart AddItemToCart(int cartId, int productId, int quantity)
        {
            var cart = _context.Set<ShoppingCart>()
                .Include(c => c.Items)
                .FirstOrDefault(c => c.ShoppingCartId == cartId);

            if (cart == null)
            {
                // Handle the error: Cart not found
                throw new Exception("Shopping cart not found.");
            }

            var product = _context.Set<Product>()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                // Handle the error: Product not found
                throw new Exception("Product not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                // Add a new item to the cart
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                });
            }
            else
            {
                // Update the quantity of the existing item in the cart
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
            return cart;
        }

        // Removes an item from the cart
        public ShoppingCart RemoveItemFromCart(int cartId, int productId)
        {
            var cart = _context.Set<ShoppingCart>()
                .Include(c => c.Items)
                .FirstOrDefault(c => c.ShoppingCartId == cartId);

            if (cart == null)
            {
                // Handle the error: Cart not found
                throw new Exception("Shopping cart not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                // Handle the error: Item not found
                throw new Exception("Item not found in the cart.");
            }

            cart.Items.Remove(cartItem);
            _context.SaveChanges();
            return cart;
        }

        // Updates the quantity of an item in the cart
        public ShoppingCart UpdateItemQuantity(int cartId, int productId, int quantity)
        {
            var cart = _context.Set<ShoppingCart>()
                .Include(c => c.Items)
                .FirstOrDefault(c => c.ShoppingCartId == cartId);

            if (cart == null)
            {
                // Handle the error: Cart not found
                throw new Exception("Shopping cart not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                // Handle the error: Item not found
                throw new Exception("Item not found in the cart.");
            }

            cartItem.Quantity = quantity;
            _context.SaveChanges();
            return cart;
        }

        // Gets the cart's content
        public ShoppingCart GetCart(int cartId)
        {
            var cart = _context.Set<ShoppingCart>()
                .Include(c => c.Items)
                .Include(c => c.Items).ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.ShoppingCartId == cartId);

            if (cart == null)
            {
                // Handle the error: Cart not found
                throw new Exception("Shopping cart not found.");
            }

            return cart;
        }

        // Clears the cart
        public void ClearCart(int cartId)
        {
            var cart = _context.Set<ShoppingCart>()
                .Include(c => c.Items)
                .FirstOrDefault(c => c.ShoppingCartId == cartId);

            if (cart == null)
            {
                // Handle the error: Cart not found
                throw new Exception("Shopping cart not found.");
            }

            cart.Items.Clear();
            _context.SaveChanges();
        }
    }
}

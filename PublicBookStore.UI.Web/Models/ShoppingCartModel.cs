using PublicBookStore.API.Models;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicBookStore.UI.Web.Models
{
    public class ShoppingCartModel
    {
        StoreRepository store = new StoreRepository();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCartModel GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCartModel();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCartModel GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(BookModel book)
        {
            // Get the matching cart and album instances
            var cartItem = store.GetCarts().FirstOrDefault(s => s.CartId.Equals(ShoppingCartId) && s.BookId.Equals(book.BookId)); //storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new API.Models.Cart
                {
                    BookId = book.BookId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                store.AddorUpdate(cartItem);

                //storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

            // Save changes
            store.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = store.GetCarts(ShoppingCartId).Single(c => c.RecordId == id);

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
                    store.Delete(cartItem.RecordId);
                }

                // Save changes
                store.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = store.GetCarts(ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                store.Delete(cartItem.RecordId);
            }

            // Save changes
            store.SaveChanges();
        }

        public List<CartModel> GetCartItems()
        {
            return store.GetCarts(ShoppingCartId).Select(c => new CartModel()
            {
                BookId = c.BookId,
                CartId = c.CartId,
                Count = c.Count,
                DateCreated = c.DateCreated,
                RecordId = c.RecordId
            }).ToList();
        }

        public int GetCount()
        {
            var carts = store.GetCarts();
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            var carts = store.GetCarts();
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Book.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(OrderModel order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                store.AddOrUpdateOrderDetail(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            store.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = store.GetCarts(ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            store.SaveChanges();
        }
    }
}

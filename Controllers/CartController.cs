using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaptopShopWebsite.Data;
using LaptopShopWebsite.Models;
using System.Text.Json;

namespace LaptopShopWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int quantity = 1)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.InStock < quantity)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại hoặc không đủ hàng" });
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Price = product.Price,
                    Image = product.Image,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return Json(new { success = true, message = "Đã thêm vào giỏ hàng", cartCount = cart.Sum(c => c.Quantity) });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(string productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.Id == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCart(cart);
            }

            return Json(new { success = true, cartCount = cart.Sum(c => c.Quantity), total = cart.Sum(c => c.Price * c.Quantity) });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.Id == productId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return Json(new { success = true, cartCount = cart.Sum(c => c.Quantity) });
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }
            
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Vui lòng đăng nhập để thanh toán!";
                return RedirectToAction("Login", "Auth");
            }
            
            ViewBag.Cart = cart;
            ViewBag.Total = cart.Sum(c => c.Price * c.Quantity);
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string customerName, string customerEmail, string customerPhone, string customerAddress, string paymentMethod)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }
            
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Vui lòng đăng nhập để đặt hàng!";
                return RedirectToAction("Login", "Auth");
            }
            
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(customerEmail) || 
                string.IsNullOrEmpty(customerPhone) || string.IsNullOrEmpty(customerAddress) || 
                string.IsNullOrEmpty(paymentMethod))
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin!";
                return RedirectToAction("Checkout");
            }
            
            var order = new Order
            {
                UserId = userId,
                Items = cart,
                Total = cart.Sum(c => c.Price * c.Quantity),
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                CustomerPhone = customerPhone,
                CustomerAddress = customerAddress,
                PaymentMethod = paymentMethod,
                Status = "pending"
            };
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            // Clear cart after successful order
            HttpContext.Session.Remove("Cart");
            
            TempData["Success"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }
        
        public async Task<IActionResult> OrderSuccess(string orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        public int GetCartCount()
        {
            var cart = GetCart();
            return cart.Sum(c => c.Quantity);
        }
        
        [HttpGet]
        public IActionResult GetCartCountApi()
        {
            var count = GetCartCount();
            return Json(new { count = count });
        }
    }
}
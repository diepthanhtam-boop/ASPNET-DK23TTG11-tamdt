using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaptopShopWebsite.Data;
using LaptopShopWebsite.Models;

namespace LaptopShopWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private bool IsAdmin()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            return userRole == "admin" || userEmail == "tamdt@gmail.com";
        }
        
        private IActionResult? CheckAdminAccess()
        {
            if (!IsAdmin())
            {
                TempData["Error"] = "Bạn không có quyền truy cập trang này!";
                return RedirectToAction("Login", "Auth");
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalContacts = 0,
                TotalUsers = await _context.Users.CountAsync(),
                TotalRevenue = 0m,
                RecentOrders = new List<Order>(),
                RecentContacts = new List<object>()
            };

            return View(stats);
        }

        // Products Management
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.OrderBy(p => p.Name).ToListAsync();
            return View(products);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sản phẩm đã được tạo thành công!";
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public async Task<IActionResult> EditProduct(string id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(string id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Sản phẩm đã được cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Products");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sản phẩm đã được xóa thành công!";
            }
            return RedirectToAction("Products");
        }

        // Orders Management
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(string id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(string id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Trạng thái đơn hàng đã được cập nhật!";
            }
            return RedirectToAction("Orders");
        }

        // Contacts Management
        public async Task<IActionResult> Contacts()
        {
            return View(new List<ContactRequest>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactStatus(string id, string status)
        {
            var contact = await _context.ContactRequests.FindAsync(id);
            if (contact != null)
            {
                contact.Status = status;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Trạng thái liên hệ đã được cập nhật!";
            }
            return RedirectToAction("Contacts");
        }

        // Users Management
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string id, string role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vai trò người dùng đã được cập nhật!";
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null && user.Role != "admin")
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Người dùng đã được xóa thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể xóa tài khoản admin!";
            }
            return RedirectToAction("Users");
        }

        // Action test đơn giản
        public IActionResult Test()
        {
            return Content("Admin controller hoạt động bình thường!");
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
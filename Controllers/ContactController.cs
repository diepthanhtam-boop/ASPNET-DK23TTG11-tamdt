using Microsoft.AspNetCore.Mvc;
using LaptopShopWebsite.Data;
using LaptopShopWebsite.Models;

namespace LaptopShopWebsite.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactRequest model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactRequests.Add(model);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi trong thời gian sớm nhất.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
using LaptopShopWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopWebsite.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Seed admin user
            if (!context.Users.Any(u => u.Role == "admin"))
            {
                var adminUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    Email = "admin@laptopstore.com",
                    PasswordHash = HashPassword("admin123"),
                    Role = "admin",
                    Phone = "0123456789",
                    Address = "123 Admin Street"
                };
                
                context.Users.Add(adminUser);
                context.SaveChanges();
            }
            
            if (context.Products.Any())
                return;

            var products = new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Name = "MacBook Pro 14\" M3 Pro",
                    Brand = "Apple",
                    Price = 2499,
                    OriginalPrice = 2699,
                    Image = "https://images.pexels.com/photos/18105/pexels-photo.jpg",
                    ImagesJson = "[\"https://images.pexels.com/photos/18105/pexels-photo.jpg\",\"https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg\",\"https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg\"]",
                    Category = "Professional",
                    Processor = "Apple M3 Pro 11-core CPU",
                    Ram = "18GB Unified Memory",
                    Storage = "512GB SSD",
                    Graphics = "14-core GPU",
                    Display = "14.2\" Liquid Retina XDR (3024×1964)",
                    Description = "Siêu phẩm MacBook Pro với chip M3 Pro mạnh mẽ, màn hình Liquid Retina XDR tuyệt đẹp và thời lượng pin ấn tượng. Hoàn hảo cho các chuyên gia sáng tạo và lập trình viên.",
                    SpecificationsJson = "{\"Processor\":\"Apple M3 Pro 11-core CPU\",\"Memory\":\"18GB Unified Memory\",\"Storage\":\"512GB SSD\",\"Graphics\":\"14-core GPU\",\"Display\":\"14.2\\\" Liquid Retina XDR (3024×1964)\",\"Battery\":\"Up to 18 hours\",\"Weight\":\"1.6kg\",\"OS\":\"macOS Sonoma\",\"Warranty\":\"1 year\"}",
                    InStock = 15,
                    Rating = 4.9,
                    Reviews = 247,
                    Featured = true
                },
                new Product
                {
                    Id = "2",
                    Name = "Dell XPS 13 Plus",
                    Brand = "Dell",
                    Price = 1699,
                    OriginalPrice = 1899,
                    Image = "https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg",
                    ImagesJson = "[\"https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg\",\"https://images.pexels.com/photos/18105/pexels-photo.jpg\",\"https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg\"]",
                    Category = "Ultrabook",
                    Processor = "Intel Core i7-1360P",
                    Ram = "16GB LPDDR5",
                    Storage = "512GB NVMe SSD",
                    Graphics = "Intel Iris Xe Graphics",
                    Display = "13.4\" 3.5K OLED Touch (3456×2160)",
                    Description = "Dell XPS 13 Plus với thiết kế tinh tế, màn hình OLED sống động và hiệu suất mạnh mẽ. Lựa chọn hoàn hảo cho doanh nhân và sinh viên.",
                    SpecificationsJson = "{\"Processor\":\"Intel Core i7-1360P (up to 5.0GHz)\",\"Memory\":\"16GB LPDDR5-5200MHz\",\"Storage\":\"512GB M.2 PCIe NVMe SSD\",\"Graphics\":\"Intel Iris Xe Graphics\",\"Display\":\"13.4\\\" 3.5K OLED Touch (3456×2160)\",\"Battery\":\"Up to 12 hours\",\"Weight\":\"1.24kg\",\"OS\":\"Windows 11 Pro\",\"Warranty\":\"1 year Premium Support\"}",
                    InStock = 23,
                    Rating = 4.7,
                    Reviews = 189,
                    Featured = true
                },
                new Product
                {
                    Id = "3",
                    Name = "ASUS ROG Strix G16",
                    Brand = "ASUS",
                    Price = 1999,
                    Image = "https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg",
                    ImagesJson = "[\"https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg\",\"https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg\",\"https://images.pexels.com/photos/18105/pexels-photo.jpg\"]",
                    Category = "Gaming",
                    Processor = "Intel Core i7-13650HX",
                    Ram = "16GB DDR5",
                    Storage = "1TB NVMe SSD",
                    Graphics = "NVIDIA RTX 4060 8GB",
                    Display = "16\" FHD 165Hz IPS",
                    Description = "Laptop gaming mạnh mẽ với RTX 4060, màn hình 165Hz mượt mà và hệ thống tản nhiệt hiệu quả. Chiến game và làm việc đồ họa cực đỉnh.",
                    SpecificationsJson = "{\"Processor\":\"Intel Core i7-13650HX (up to 4.9GHz)\",\"Memory\":\"16GB DDR5-4800MHz\",\"Storage\":\"1TB M.2 NVMe PCIe 4.0 SSD\",\"Graphics\":\"NVIDIA GeForce RTX 4060 8GB GDDR6\",\"Display\":\"16\\\" FHD (1920×1080) 165Hz IPS\",\"Battery\":\"Up to 8 hours\",\"Weight\":\"2.5kg\",\"OS\":\"Windows 11 Home\",\"Warranty\":\"2 years International\"}",
                    InStock = 18,
                    Rating = 4.6,
                    Reviews = 156
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
        
        private static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
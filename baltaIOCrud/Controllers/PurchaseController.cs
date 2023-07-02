using baltaIOCrud.Data;
using baltaIOCrud.Models;
using baltaIOCrud.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace baltaIOCrud.Controllers
{
    public class PurchaseController : Controller
    {
        //get the context
        public readonly ApplicationDbContext _context;
        //get the user id
        public readonly UserManager<IdentityUser> _userManager;

        public PurchaseController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            ViewBag.UserId = userId;

            var products = _context.Products.Where(p => p.StockQuantity > 0).ToList();
            var viewModel = new ProductViewModel { Products = products };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Buy(Guid productId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            ViewBag.UserId = userId;

            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                // Product not found, handle the error
                return NotFound();
            }
            var viewModel = new UpdateProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                Available = product.Available,
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Buy(UpdateStockQuantity model)
        {
            var product = await _context.Products.FindAsync(model.Id);
            if (product != null)
            {
   

                product.StockQuantity = model.StockQuantity;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}


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
            if (user == null) 
            {
                return NotFound();
            }
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

            var viewModel = new UpdateStockQuantity
            {
                Id = productId,//get product id to update correctly
                StockQuantity = product.StockQuantity
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(UpdateStockQuantity model, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var product = await _context.Products.FindAsync(Id);
            if (product != null)
            {
                if (model.StockQuantity > product.StockQuantity)
                {
                    ModelState.AddModelError("StockQuantity", "Invalid stock quantity");
                    return View(model);
                }
                product.StockQuantity -= model.StockQuantity;
                if (product.StockQuantity == 0) 
                {
                    product.Available = false;
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}


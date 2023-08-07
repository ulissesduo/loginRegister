using baltaIOCrud.Data;
using baltaIOCrud.Models;
using baltaIOCrud.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace baltaIOCrud.Controllers
{
    public class CartController : Controller
    {
        public readonly ApplicationDbContext _context;
        //get the user id
        public readonly UserManager<IdentityUser> _userManager;
        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> DisplayItems(Guid Id)
        {
            var product = await _context.Products.FindAsync(Id);

            if (product == null)
            {
                // Product not found, handle the error
                return NotFound();
            }

            var viewModel = new CartViewModel
            {
                Id = Id,
                Items = new List<Products> // Initialize the Items property
                {
                    product // Add the retrieved product to the Items list
                },
                TotalQuantity = product.StockQuantity, // Set the total quantity
                TotalPrice = product.Price // Set the total price
                                           // Add other properties to the view model as needed
            };

            return View(viewModel);
        }
        //[HttpPost]
        //public async Task<IActionResult> AddToCart(Guid productId)
        //{
        //    var product = await _context.Products.FindAsync(productId);

        //    if (product == null)
        //    {
        //        // Product not found, handle the error
        //        return NotFound();
        //    }

        //    // Retrieve the current user ID or any other identifier for associating the cart items with the user
           
        //    // Check if the product already exists in the cart for the user
        //    var existingCartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

        //    if (existingCartItem != null)
        //    {
        //        // Product already exists in the cart, you can update the quantity or handle it as needed
        //        existingCartItem.Quantity += 1;
        //    }
        //    else
        //    {
        //        // Product does not exist in the cart, create a new cart item
        //        var cartItem = new Cart
        //        {
        //            Items = product.Id,
        //            Quantity = 1,
        //            UserId = userId
        //        };

        //        // Add the cart item to the Cart table
        //        _context.Cart.Add(cartItem);
        //    }

        //    await _context.SaveChangesAsync();

        //    // Optionally, you can provide feedback or redirect the user to the cart page after adding the product

        //    return RedirectToAction("Cart"); // Redirect to the cart page
        //}

        public IActionResult AddToCart(Products product)
        {
            return RedirectToAction();
        }
        public IActionResult RemoveToCart(Guid Id)
        {
            return RedirectToAction();
        }


    }
}

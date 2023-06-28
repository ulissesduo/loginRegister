using baltaIOCrud.Data;
using baltaIOCrud.Helper;
using baltaIOCrud.Models;
using baltaIOCrud.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace baltaIOCrud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var x = await _context.Products.ToListAsync();
            return View(x);
        }



        public IActionResult Add()
        {
            var typeProducts = TypeProducts.GetAll();
            var model = new AddProductsViewModel();
            model.TypeProductList = new List<SelectListItem>();
            foreach (var typeProduct in typeProducts)
            {
                model.TypeProductList.Add(new SelectListItem { Text = typeProduct.Name, Value = typeProduct.Name });
            }
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductsViewModel model) 
        {
            var selectCategory = model.SelectedCategory;//this is to call the selected category from dropdown
            Products products = new Products() 
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Category = selectCategory,
                StockQuantity = model.StockQuantity,
            };

            await _context.AddAsync(products);//add object to my database
            await _context.SaveChangesAsync();//save the changes
            return RedirectToAction("Index");//return to index page after insert
        }
    }
}

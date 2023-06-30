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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id) 
        {
            var products = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);//find the first record that matches with the id parameter

            if (products != null)//check if the data retrieved has been found 
            {
                var typeProducts = TypeProducts.GetAll();//get all the types of products generated dinamically
                var viewModel = new UpdateProductViewModel()//initialize the productViewModel with its properties values as retrieved from 'products' object
                {
                    Id = Guid.NewGuid(),//set values to properties to update
                    Name = products.Name,
                    Price = products.Price,
                    Description = products.Description,
                    Category = products.Category,
                    StockQuantity = products.StockQuantity,
                    TypeProductList = typeProducts.Select(typeProducts => new SelectListItem 
                    {
                        Text = typeProducts.Name,
                        Value = typeProducts.Name,
                        Selected = typeProducts.Name == products.Category
                    }).ToList(),//transform the object into a list
                };
                return View("Edit", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductViewModel model) 
        {
            var selectedCategory = model.SelectedCategory;
            var product = await _context.Products.FindAsync(model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Category = selectedCategory;
                product.StockQuantity = model.StockQuantity;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? Id) 
        {
            if (Id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel model) 
        {
            var products = await _context.Products.FindAsync(model.Id);

            if (products != null)
            {

                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}

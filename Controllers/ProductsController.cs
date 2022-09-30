using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestNTT.Models;

namespace TestNTT.Controllers
{
    public class ProductsController : Controller
    {
        ProductContext db;
        public ProductsController(ProductContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string? sortOrder)
        {
            ViewCreateProduct viewCreateProduct = new ViewCreateProduct();

            var productList = db.Products.Include(c => c.Category);

            viewCreateProduct.Products = productList;

            ////параметры сортировки
            ViewBag.ProductNameSort = string.IsNullOrEmpty(sortOrder) ? "product_desc" : "";
            ViewBag.CategoryNameSort = sortOrder == "category_asc" ? "category_desc" : "category_asc";
            switch (sortOrder)
            {
                case "category_desc":
                    viewCreateProduct.Products = productList.OrderByDescending(c => c.Category.CategoryName);
                    break;
                case "category_asc":
                    viewCreateProduct.Products = productList.OrderBy(c => c.Category.CategoryName);
                    break;
                case "product_desc":
                    viewCreateProduct.Products = productList.OrderByDescending(p => p.ProductName);
                    break;
                default:
                    viewCreateProduct.Products = productList.OrderBy(p => p.ProductName);
                    break;

            }
            return View(viewCreateProduct);
        }

        //Сreate добавление данных продуктов
        [HttpGet]
        public IActionResult Create()
        {
            var viewCreateProduct = new ViewCreateProduct();
            viewCreateProduct.Categories = db.Categories;
            return View(viewCreateProduct);

        }
        //Post метод добавления данных продукта из выбранных и веденных значений из представления Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update обновление уже имеющих данных
        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            Product product = db.Products.FirstOrDefault(p => p.ProductId == id);

            ViewCreateProduct viewCreateProduct = new ViewCreateProduct();

            viewCreateProduct.Product = product;

            viewCreateProduct.Categories = db.Categories;

            return View(viewCreateProduct);

        }
        //Post Update метод для обновления существующей записи
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //Delete метод удаления продукта из таблицы product
        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            if (id != null)
            {
                Product? product = db.Products.FirstOrDefault(p => p.ProductId == id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        //Метод на добавление категории
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewCreateProduct viewCreateProduct = new ViewCreateProduct();
            var category = db.Categories.ToList();
            viewCreateProduct.Categories = category;
            return View(viewCreateProduct);

        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("CreateCategory");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bloomify.Models;
using Bloomify.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Bloomify.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProviderService _providerService;

        public ProductsController(IProductService productService, ICategoryService categoryService, IProviderService providerService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _providerService = providerService;

        }

        // GET: Products
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName");
            ViewData["ProviderID"] = new SelectList(_providerService.GetAllProviders(), "ProviderID", "ProviderName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("ProductID,ProductName,ProductDescription,Price,ImagePath,CategoryID,ProviderID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            ViewData["ProviderID"] = new SelectList(_providerService.GetAllProviders(), "ProviderID", "ProviderName", product.ProviderID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id) {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            ViewData["ProviderID"] = new SelectList(_providerService.GetAllProviders(), "ProviderID", "ProviderName", product.ProviderID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("ProductID,ProductName,ProductDescription,Price,ImagePath,CategoryID,ProviderID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            ViewData["ProviderID"] = new SelectList(_providerService.GetAllProviders(), "ProviderID", "ProviderName", product.ProviderID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("SearchResults", new List<Product>());
            }

            var results = _productService.GetAllProducts()
                                         .Where(p => p.ProductName.ToLower().Contains(query.ToLower()))
                                         .ToList();

            return View("SearchResults", results);
        }
    }
}

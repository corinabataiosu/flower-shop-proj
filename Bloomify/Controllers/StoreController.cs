using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bloomify.Data;
using Bloomify.Models;
using Bloomify.Services.Interfaces;

namespace Bloomify.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        public StoreController(IProductService productService, ICategoryService categoryService, IReviewService reviewService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _reviewService = reviewService;
        }

        // GET: Store
        public IActionResult Index(string category, string provider, string sort)
        {
            var products = _productService.GetAllProducts();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Categories != null && p.Categories.CategoryName == category).ToList();
            }

            if (!string.IsNullOrEmpty(provider))
            {
                products = products.Where(p => p.Providers != null && p.Providers.ProviderName == provider).ToList();
            }

            products = sort switch
            {
                "price_asc" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                "name_asc" => products.OrderBy(p => p.ProductName).ToList(),
                "name_desc" => products.OrderByDescending(p => p.ProductName).ToList(),
                _ => products
            };

            products = products.ToList();

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.Manufacturers = products.Select(p => p.Providers).Distinct().ToList();

            return View(products);
        }


        // GET: /Store/Category/3
        [Route("Store/Category/{categoryId}")]
        public IActionResult Category(int categoryId)
        {
            var products = _productService.GetProductsByCategoryId(categoryId);
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.CategoryName = category.CategoryName;
            return View("Index", products);
        }

        // GET: /Store/Product/5
        [Route("Store/Product/{id}")]
        public IActionResult Product(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var reviews = _reviewService.GetReviewsByProductId(id);
            ViewData["Reviews"] = reviews;
            //ViewBag.Reviews = reviews;

            return View(product); 
        }
    }
}

using FrontToBack.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _productCount;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _productCount = _dbContext.Products.Count();
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _productCount;
            var products = _dbContext.Products.Take(4).Include(x => x.Category).ToList();

            return View(products);
        }

        public IActionResult Load(int skip)
        {
            #region Old way

            //var products = _dbContext.Products.Skip(4).Take(4).Include(x => x.Category).ToList();

            //return Json(products);

            #endregion

            if (skip >= _productCount)
            {
                return Content("Error");
            }

            var products = _dbContext.Products.Skip(skip).Take(4).Include(x => x.Category).ToList();

            return PartialView("_ProductPartial", products);
        }
    }
}

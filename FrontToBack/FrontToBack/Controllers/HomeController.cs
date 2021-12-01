using FrontToBack.DataAccessLayer;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sliderImages = _dbContext.SliderImages.ToList();
            //var slider = _dbContext.Sliders.FirstOrDefault();
            var slider = _dbContext.Sliders.SingleOrDefault();
            var categories = _dbContext.Categories.ToList();
            //var products = _dbContext.Products.Include(x => x.Category).OrderBy(x => x.Price).ToList();
            //var products = _dbContext.Products.Include(x => x.Category).Where(x => x.Price > 200).ToList();
            var products = _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.Price).ToList();
            //var products = _dbContext.Products.Where(x => x.Price > 200).ToList();

            return View(new HomeViewModel 
            {
                Slider = slider,
                SliderImages = sliderImages,
                Categories = categories,
                Products = products
            });
        }

        public async Task<IActionResult> Index2()
        {
            var sliderImages = _dbContext.SliderImages.ToListAsync();
            var slider = _dbContext.Sliders.SingleOrDefaultAsync();
            var categories = _dbContext.Categories.ToListAsync();
            var products = _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.Price).ToListAsync();

            return Json(new HomeViewModel
            {
                Slider = await slider,
                SliderImages = await sliderImages,
                Categories = await categories,
                Products = await products
            });
        }
    }
}

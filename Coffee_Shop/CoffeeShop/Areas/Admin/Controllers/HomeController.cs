using CoffeeShop.Areas.Admin.Models;
using CoffeeShop.Areas.Identity.Data;
using CoffeeShop.Models;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing.Printing;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private OderDrinkingContext _ctx;

        private IProductRepository _productRepository;
        private IGenreRepository _genreRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(IProductRepository productRepository, 
                                IGenreRepository genreRepository, 
                                OderDrinkingContext ctx,
                                SignInManager<ApplicationUser> signInManager)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _ctx = ctx;
            _signInManager = signInManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/identity/account/login");
        }
        [HttpPost]
        public IActionResult FillProductById(int page = 1, int pageSize = 4)
        {

            string id = Request.Form["GenreId"].ToString();
            int id1 = Convert.ToInt32(id);

            /*==================== Pagination ====================*/
            var genreList = _genreRepository.GetAll();
            ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");

            var allData = _productRepository.FindAllProductById(id1);
            int itemsPerPage = 5; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = allData.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            var pagedData = allData.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.TotalPages = totalPages;

            // Tính toán vị trí bắt đầu và kết thúc của mục cho trang hiện tại
            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage;

            ViewBag.CurrentPage = page;

            List<Product> products = _productRepository.FindAllProductById(id1).OrderBy(x => x.ProductId).Skip(startIndex).Take(itemsPerPage).ToList();
            return View(products);
        }
        public IActionResult Products(int page = 1, int pageSize= 4)
        {

            /*==================== Pagination ====================*/

            var allData = _ctx.Products;
            int itemsPerPage = 10; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = _ctx.Products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            var pagedData = allData.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.TotalPages = totalPages;

            // Tính toán vị trí bắt đầu và kết thúc của mục cho trang hiện tại
            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage;

            ViewBag.CurrentPage = page;

            /*================== Get All Data Genre ==================*/

            var genreList = _genreRepository.GetAll();
            ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");

            /*==================== Passing Data ====================*/

            //1. Get data
            List<Product> lst = _productRepository.GetAll().OrderBy(x=>x.ProductId).Skip(startIndex).Take(itemsPerPage).ToList();
            //2. Passing data to view

            return View("Products", lst);
        }
        public IActionResult CreateProduct()
        {
            //var q = from g in _genreRepository.GetAll()
            //        select new SelectListItem()
            //        {
            //            Text = g.GenreName,
            //            Value = Convert.ToString(g.GenreId)
            //        };
            //ViewBag.GenreList = q.ToList();
            var genreList = _genreRepository.GetAll(); // Truy vấn cơ sở dữ liệu để lấy danh sách các mục
            ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName"); // Gán danh sách các mục vào ViewBag
            return View("CreateProduct", new Product());
        }
        public IActionResult EditProduct(string Id)
        {
            var genreList = _genreRepository.GetAll(); 
            ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
            return View("EditProduct", _productRepository.FindById(Id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                bool isProductNameExist = _productRepository.CheckNameProduct(product.ProductName);
                if (isProductNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Product name is exist!!!");
                    var genreList = _genreRepository.GetAll();
                    ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
                    return View("EditProduct");
                }
                else
                {
                    _productRepository.Update(product);
                    return RedirectToAction("Products", "Home");
                }
            }
            else
            {
                var genreList = _genreRepository.GetAll();
                ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
                return View("EditProduct");
            }
                
        }
        public IActionResult DeleteProduct(string id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Products", "Home");
        }
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            
            if (ModelState.IsValid)
            {
                bool isProductIdExist = _productRepository.CheckId(product.ProductId);
                bool isProductNameExist = _productRepository.CheckNameProduct(product.ProductName);

                if (isProductIdExist)
                {
                    ModelState.AddModelError(string.Empty, "Product Id is exist!!!");
                    var genreList = _genreRepository.GetAll();
                    ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
                    return View("CreateProduct");
                }
                else
                {
                    if (isProductNameExist)
                    {
                        ModelState.AddModelError(string.Empty, "Product name is exist!!!");
                        var genreList = _genreRepository.GetAll();
                        ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
                        return View("CreateProduct");
                    }
                    else
                    {
                        _productRepository.Create(product);
                        return RedirectToAction("Products", "Home");
                    }
                }
            }
            else
            {
                var genreList = _genreRepository.GetAll();
                ViewBag.GenreId = new SelectList(genreList, "GenreId", "GenreName");
                return View("CreateProduct");
            }
        }
    }
}

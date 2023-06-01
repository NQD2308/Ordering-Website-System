using CoffeeShop.Models;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private ProductDAO _productDAO = new ProductDAO();
        private ICustomerRepository _customerRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            List<Product> Coffee = _productDAO.GetCoffeeProduct();
            List<Product> Fruit = _productDAO.GetFruitProduct();
            List<Product> Tea = _productDAO.GetTeaProduct();
            List<Product> MilkShake = _productDAO.GetMilkshakeProduct();

            //ViewBag.CoffeeList = Coffee;
            //ViewBag.FruitList = Fruit;
            //ViewBag.TeaList = Tea;

            MenuModel m = new MenuModel();
            m.coffee = Coffee;
            m.fruit = Fruit;
            m.tea = Tea;
            m.MilkShake = MilkShake;

            return View(m);
        }

        public IActionResult Payment()
        {
            List<Product> Coffee = _productDAO.GetCoffeeProduct();
            List<Product> Fruit = _productDAO.GetFruitProduct();
            List<Product> Tea = _productDAO.GetTeaProduct();
            List<Product> MilkShake = _productDAO.GetMilkshakeProduct();

            MenuModel m = new MenuModel();
            m.coffee = Coffee;
            m.fruit = Fruit;
            m.tea = Tea;
            m.MilkShake = MilkShake;

            return View(m);
        }

        public IActionResult Customers()
        {
            //1. Get data
            List<Customer> lst = _customerRepository.GetAll();
            //2. Passing data to view
            return View("Customers", lst);
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }


        public IActionResult SignUp1()
        {
            return View("SignUp", new Customer());
        }

        public IActionResult SaveCustomer(Customer customer)
        {
            _customerRepository.Create(customer);
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
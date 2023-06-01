using CoffeeShop.Models;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            List<Customer> lst = _customerRepository.GetAll();
            return View(lst);
        }
        public IActionResult Customers()
        {
            //1. Get data
            List<Customer> lst = _customerRepository.GetAll();
            //2. Passing data to view
            return View("Customers", lst);
        }
        public IActionResult CreateCustomer()
        {
            return View("CreateCustomer", new Customer());
        }
        public IActionResult EditCustomer(int Id) 
        {
            return View("EditCustomer", _customerRepository.FindById(Id));
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                return RedirectToAction("Index", "Customer");
            }
            else
                return View("EditCustomer");
        }
        public IActionResult DeleteCustomer(int id)
        {
            _customerRepository.Delete(id);
            return RedirectToAction("Index", "Customer");
        }
        [HttpPost]
        public IActionResult SaveCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Create(customer);
                return RedirectToAction("Index", "Customer");
            }
            else
                return View("CreateCustomer");
        }
    }
}

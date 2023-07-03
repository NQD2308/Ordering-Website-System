using CoffeeShop.Areas.Identity.Data;
using CoffeeShop.Models;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace CoffeeShop.Controllers
{
    public class CartController : Controller
    {
        private ProductDAO _productDAO = new ProductDAO();
        private IProductRepository _productRepository;
        private IBillRepository _billRepository;
        private OderDrinkingContext _ctx;
        SignInManager<ApplicationUser> _signInManager;
        public static string note { get; set; }

        /*public string NoteBill;*/
        public CartController(IProductRepository productRepository, 
                                OderDrinkingContext ctx, 
                                IBillRepository billRepository, 
                                SignInManager<ApplicationUser> signInManager)
        {
            _productRepository = productRepository;
            _ctx = ctx;
            _billRepository = billRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var user = ClaimsPrincipal.Current;
            //var user = new ClaimsPrincipal();

            if (!_signInManager.IsSignedIn(user))
            {
                return LocalRedirect("/identity/account/logincustomer");
            }else
            {


           
            ViewBag.sessionId = HttpContext.Session.Id;
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
           
        }
        /*[Authorize(Roles = "Customer")]*/        
        public IActionResult Payment(string _note)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return LocalRedirect("/identity/account/logincustomer");
            }
            else
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

                ViewBag.sessionId = HttpContext.Session.Id;
                CartModel cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                if (HttpContext.Session.Get<List<Item>>("cart") != null)
                {
                    List<Item>? items = HttpContext.Session.Get<List<Item>>("cart");
                    cartModel.setAllItems(items);
                }

                m.Cart = cartModel;

                int? n = HttpContext.Session.GetInt32("nITems") == null ? 0 : HttpContext.Session.GetInt32("nITems");
                ViewBag.n = n;


                ViewBag.Note = _note;//HttpContext.Request.Form["_Note"].ToString();
                cartModel.NoteBill = _note;//HttpContext.Request.Form["_Note"].ToString(); ;
                /*TempData["Parameter"] = _note;*/
                CartController.note = _note;
                /*HttpContext.Session.SetString("Parameter", _note);*/

                return View(m);
            }
        }
        [HttpPost]
        public IActionResult SavePayment()
        {
            return View();
        }

        public IActionResult CheckOut(Customer customer, CartModel cartModel)
        {
            //doc session va luu database
            List<Item>? items = HttpContext.Session.Get<List<Item>>("cart");
            
            Bill bill = new Bill();

            string userEmail = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                userEmail = User.Identity.Name;
            } 
             
            Customer c= _ctx.Customers.OrderByDescending(x => x.Name == userEmail).Take(1).SingleOrDefault();
            bill.CustomerId = c.Id;
            bill.PhoneNumber= c.PhoneNumber;
            bill.Address = c.Address;
            bill.FeeShip = 10;
            bill.Note = CartController.note;
            bill.DiscountTotalBill = 5;
            bill.DateReceive= DateTime.Now;
            bill.Status = true;//?????
            _ctx.Bills.Add(bill);
            _ctx.SaveChanges();
            //2
            var BillId = _ctx.Bills.OrderByDescending(x => x.BillId).Take(1).SingleOrDefault().BillId;
            //db
            //1 ==> insert shopping cart
            //2 ==> insert orders
            double a = 0;
            foreach (var item in items)
            {
                DetailBill d = new DetailBill();
                d.BillId = BillId;
                d.ProductId = item.Id;
                d.Price = (double)item.Price;
                d.Quantity = item.Quantity;
                var total = item.Price * item.Quantity;
                a += (double)total;
                d.Total = (decimal)a;
                _ctx.DetailBills.Add(d);
            }
            decimal div = 5m / 100m;
            bill.TotalBill = a - (a * (double)div) + 10;
            _ctx.SaveChanges();

            ClearAllCartItem();
            return RedirectToAction("Payment");
        }

        //[Authorize(Roles = "Customer")]
        public IActionResult AddToCart(string id)
        {
            //find product by id
            Product product = _productRepository.FindById(id);
            int quantity = 1;
            CartModel cartModel = null;
            if (HttpContext.Session.Get<List<Item>>("cart") != null)
            {
                //1
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<Item>>("cart"));
                //2
                decimal PriceByDiscount = 0;
                decimal dis = Convert.ToDecimal(product.Discount);
                PriceByDiscount = product.Price - (product.Price * (dis / 100));

                Item item = new Item()
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    Price = PriceByDiscount,
                    Quantity = quantity,
                    lineTotal = quantity * PriceByDiscount,
                };
                //3
                cartModel.addItem(item);

                //4 Save to session
                HttpContext.Session.Set<List<Item>>("cart", cartModel.getAllItem());
                HttpContext.Session.SetInt32("nITems", cartModel.GetCountItem());
            }
            else
            {
                //the first time
                //1
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;

                //2
                decimal PriceByDiscount = 0;
                decimal dis = Convert.ToDecimal(product.Discount);
                PriceByDiscount = product.Price - (product.Price * (dis / 100));

                Item item = new Item()
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    Price = PriceByDiscount,
                    Quantity = quantity,
                    lineTotal = quantity * PriceByDiscount,
                };

                //3
                cartModel.addItem(item);

                //4 Save to session
                HttpContext.Session.Set<List<Item>>("cart", cartModel.getAllItem());
                HttpContext.Session.SetInt32("nITems", cartModel.GetCountItem());

            }
            return RedirectToAction("Payment");
        }
        public IActionResult updateQuantity()
        {
            var btn = Request.Form["btnUpdateQuantity"].ToString();
            var id = Request.Form["item.Id"].ToString();
            var qty = Request.Form["item.Quantity"].ToString();
            CartModel cartModel = null;
            if (HttpContext.Session.Get<List<Item>>("cart") != null)
            {
                //1
                cartModel = new CartModel();    
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<Item>>("cart"));
            }
            cartModel.UpdateQuantity(id, 1, btn);

            //4
            HttpContext.Session.Set<List<Item>>("cart", cartModel.getAllItem());
            HttpContext.Session.SetInt32("nITems", cartModel.GetCountItem());

            return RedirectToAction("Payment");
        }

        public IActionResult RemoveItem(string id)
        {
            //1
            List<Item> cartItems = HttpContext.Session.Get<List<Item>>("cart");

            if(cartItems != null)
            {
                Item RemoveToProduct = cartItems.FirstOrDefault(x => x.Id == id);

                cartItems.Remove(RemoveToProduct);
            }
            HttpContext.Session.Set("cart", cartItems);

            return RedirectToAction("Payment");
        }

        public IActionResult ClearAllCartItem()
        {
            HttpContext.Session.Remove("cart");

            return RedirectToAction("Payment", "cart");
        }
    }
}

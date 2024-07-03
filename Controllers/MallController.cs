using MallRAj.Data;
using MallRAj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Controllers
{
    public class MallController : Controller
    {
        protected readonly ApplicationDbContext _context;
        public MallController(ApplicationDbContext context)
        {
            _context = context;
        }
        //show all the list of products along with product type
        public IActionResult Index()
        {
            ViewBag.CartList = _context.Carts.Include(a=>a.Product).ToList();
            return View(_context.ProductItems.Include(a => a.ProductType).ToList());
        }

        public IActionResult AddToCart(int productItemId)
        {
            CartModel newCart = new CartModel
            {
                ProductId = productItemId,
                Quantity = 1
            };

            _context.Carts.Add(newCart);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseItem(int itemId)
        {
            CartModel cart = _context.Carts.Where(a => a.ProductId == itemId).FirstOrDefault();
            cart.Quantity += 1;

            _context.Update<CartModel>(cart);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseItem(int itemId)
        {
            CartModel cart = _context.Carts.Where(a => a.ProductId == itemId).FirstOrDefault();
            cart.Quantity -= 1;

            _context.Update<CartModel>(cart);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        

        public IActionResult CheckCustomerNumber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckCustomerNumber(string phoneNumber)
        {
            if (_context.Customers.Any(a => a.CustomerPhoneNumber.Equals(phoneNumber)))
            {
                CustomerModel customer = _context.Customers.FirstOrDefault(a => a.CustomerPhoneNumber.Equals(phoneNumber));
                return RedirectToAction("AddNewOrder",new { customerId = customer.Id });
            }
            else
            {
                return RedirectToAction("AddNewCustomer");
            }
        }

        public IActionResult AddNewCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCustomer(CustomerModel customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("AddNewOrder",new { customerId = customer.Id });
        }

        

        /// <summary>
        /// Adding new order
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IActionResult AddNewOrder(int customerId)
        {
            OrderModel order = new OrderModel
            {
                CustomerId = customerId,
                PurchaseDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            var cartList = _context.Carts.Include(a=>a.Product).ToList();

            foreach (var item in cartList)
            {
                OrderItemsModel orderItem = new OrderItemsModel
                {
                    OrderId = order.Id,
                    Price = item.Product.Price * item.Quantity, 
                    Quantity = item.Quantity, 
                    ProductItemId = item.Product.Id, 
                };

                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();

                _context.Carts.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Finalize", new { orderId = order.Id});
        }


        public IActionResult Finalize(int orderId)
        {
            return View(_context.Orders.Include(a=>a.OrderItems).Include("OrderItems.ProductItem").Include(a=>a.Customer)
                .FirstOrDefault(a=>a.Id == orderId));
        }
        public IActionResult Show()
        {
            return View();
        }
    }
}

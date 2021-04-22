using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eshop.DataAccess.Repository.IRepository;
using Eshop.Models;
using Eshop.Models.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderDetailsVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            OrderVM = new OrderDetailsVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id,
                                                includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == id, includeProperties: "Product")

            };
            return View(OrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Details")]
        public IActionResult Details(string stripeToken)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id,
                                                includeProperties: "ApplicationUser");
            if(stripeToken!=null)
            {
                //process the payment
                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal*100),
                    Currency = "usd",
                    Description = "Order ID : " + orderHeader.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Id == null)
                {
                    orderHeader.PaymentStatus = StaticDetail.PaymentStatusRejected;
                }
                else
                {
                    orderHeader.TransactionId = charge.Id;
                }
                if (charge.Status.ToLower() == "succeeded")
                {
                    orderHeader.PaymentStatus = StaticDetail.PaymentStatusApproved;
                   
                    orderHeader.PaymentDate = DateTime.Now;
                }

                _unitOfWork.Save();
                
            }
            return RedirectToAction("Details", "Order", new { id = orderHeader.Id });
        }


        [Authorize(Roles =StaticDetail.Role_Admin+","+StaticDetail.Role_Employee)]
        public IActionResult StartProcessing(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            orderHeader.OrderStatus = StaticDetail.StatusInProcess;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = StaticDetail.Role_Admin + "," + StaticDetail.Role_Employee)]
        public IActionResult ShipOrder()
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeader.OrderStatus = StaticDetail.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;
            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = StaticDetail.Role_Admin + "," + StaticDetail.Role_Employee)]
        public IActionResult CancelOrder(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            if (orderHeader.PaymentStatus == StaticDetail.StatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Reason = RefundReasons.RequestedByCustomer,
                    Charge = orderHeader.TransactionId

                };
                var service = new RefundService();
                Refund refund = service.Create(options);

                orderHeader.OrderStatus = StaticDetail.StatusRefunded;
                orderHeader.PaymentStatus = StaticDetail.StatusRefunded;
            }
            else
            {
                orderHeader.OrderStatus = StaticDetail.StatusCancelled;
                orderHeader.PaymentStatus = StaticDetail.StatusCancelled;
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateOrderDetails()
        {
            var orderHEaderFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHEaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHEaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHEaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHEaderFromDb.City = OrderVM.OrderHeader.City;
            orderHEaderFromDb.State = OrderVM.OrderHeader.State;
            orderHEaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if (OrderVM.OrderHeader.Carrier != null)
            {
                orderHEaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (OrderVM.OrderHeader.TrackingNumber != null)
            {
                orderHEaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.Save();
            TempData["Error"] = "Order Details Updated Successfully.";
            return RedirectToAction("Details", "Order", new { id = orderHEaderFromDb.Id });
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetOrderList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim =claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> orderHeaderList;

            if (User.IsInRole(StaticDetail.Role_Admin) || User.IsInRole(StaticDetail.Role_Employee))
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(
                                        u=>u.ApplicationUserId==claim.Value,
                                        includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "pending":
                    orderHeaderList = orderHeaderList.Where(o => o.PaymentStatus == StaticDetail.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus==StaticDetail.StatusApproved ||
                                                            o.OrderStatus==StaticDetail.StatusInProcess||
                                                            o.OrderStatus==StaticDetail.StatusPending);
                    break;
                case "completed":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == StaticDetail.StatusShipped);
                    break;
                case "rejected":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == StaticDetail.StatusCancelled ||
                                                            o.OrderStatus == StaticDetail.StatusRefunded ||
                                                            o.OrderStatus == StaticDetail.PaymentStatusRejected);
                    break;
                default:
                    break;
            }

            return Json(new { data = orderHeaderList });
        }
        #endregion
    }
}
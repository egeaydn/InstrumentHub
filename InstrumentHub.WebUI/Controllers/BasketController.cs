using DocuSign.eSign.Model;
using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using InstrumentHub.WebUI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InstrumentHub.WebUI.Controllers
{
	public class BasketController : Controller
	{
		private ICartServices _cartService;
		private IEProductServices _productService;
		private IOrderServices _orderService;
		private UserManager<AplicationUser> _usermanager;

		public BasketController(ICartServices cartServices,IEProductServices eProductServices,IOrderServices orderServices,UserManager<AplicationUser> usermanager)
		{
			_cartService = cartServices;
			_productService = eProductServices;
			_orderService = orderServices;
			_usermanager = usermanager;
		}

		public IActionResult Home()
		{
			var basket = _cartService.GetCartByUserId(_usermanager.GetUserId(User));

			return View(
				new CartModel()
				{
					CartId = basket.Id,
					CartItems = basket.CartItems.Select(i => new CartItemModel()
					{
						CartItemId = i.Id,
						ProductId = i.EProduct.Id,
						ProductName = i.EProduct.Name,
						Price = i.EProduct.Price,
						Quantity = i.Quantity,
						Image = i.EProduct.Images[0].ImageUrl
					}).ToList()
				}
			);
		}
		public IActionResult AddToBasket(int productId, int quantity)
		{
			_cartService.AddToCart(_usermanager.GetUserId(User), productId, quantity);
			return RedirectToAction("Home");
		}


		[HttpPost]
		public IActionResult DeleteFromBasket(int eProductId)
		{
			_cartService.DeleteFromCart(_usermanager.GetUserId(User), eProductId);
			return RedirectToAction("Home");
		}

		public IActionResult GetBasketItemCount()
		{
			var userId = _usermanager.GetUserId(User);
			if (userId == null)
			{
				return Json(0);
			}

			var basket = _cartService.GetCartByUserId(userId);
			int totalItemCount = basket?.CartItems.Sum(i => i.Quantity) ?? 0;

			return Json(totalItemCount);
		}

		public IActionResult Checkout()
		{
			var basket = _cartService.GetCartByUserId(_usermanager.GetUserId(User));
			var orderModel = new OrderModel();
			orderModel.CartTemplate = new CartModel()
			{
				CartId = basket.Id,
				CartItems = basket.CartItems.Select(i => new CartItemModel()
				{
					CartItemId = i.Id,
					ProductId = i.EProduct.Id,
					ProductName = i.EProduct.Name,
					Price = i.EProduct.Price,
					Quantity = i.Quantity,
					Image = i.EProduct.Images[0].ImageUrl
				}).ToList()
			};
			return View(orderModel);
		}

		[HttpPost]
		public IActionResult Checkout(OrderModel model, string paymentMethod)
		{
			ModelState.Remove("CartModel");

			if (ModelState.IsValid)
			{
				var userId = _usermanager.GetUserId(User);
				var cart = _cartService.GetCartByUserId(userId);

				model.CartTemplate = new CartModel()
				{
					CartId = cart.Id,
					CartItems = cart.CartItems.Select(x => new CartItemModel()
					{
						CartItemId = x.Id,
						ProductId = x.EProduct.Id,
						ProductName = x.EProduct.Name,
						Price = x.EProduct.Price,
						Image = x.EProduct.Images[0].ImageUrl,
						Quantity = x.Quantity
					}).ToList()
				};

				if (paymentMethod == "credit")
				{
					var payment = PaymentProces(model);

					if (payment.Result.Status == "success")
					{
						SaveOrder(model, payment, userId);
						ClearCart(cart.Id.ToString());
						TempData.Put("message", new ResultMessageModel()
						{
							Title = "Order Completed",
							Message = "Your order has been completed successfully",
							Css = "success"
						});
					}
					else
					{
						TempData.Put("message", new ResultMessageModel()
						{
							Title = "Order Uncompleted",
							Message = "Your order could not be completed",
							Css = "danger"
						});
					}
				}
				else
				{

					SaveOrder(model, userId);
					ClearCart(cart.Id.ToString());

					TempData.Put("message", new ResultMessageModel()
					{
						Title = "Order Completed",
						Message = "Your order has been completed successfully",
						Css = "success"
					});

				}

			}

			return View(model);
		}

		private void ClearCart(string id)
		{
			_cartService.ClearCart(id);
		}

		//Burası Eft Kısmı

		private void SaveOrder(OrderModel model, object payment, string userId)
		{
			Order order = new Order()
			{
				OrderNumber = Guid.NewGuid().ToString(),
				OrderEnums = OrderStatus.completed,
				PaymentEnum = PaymentMethod.Eft,
				PaymentToken = Guid.NewGuid().ToString(),
				ConversionId = Guid.NewGuid().ToString(),
				PaymentId = Guid.NewGuid().ToString(),
				OrderNote = model.OrderNote,
				Orderdate = DateTime.Now,
				FirstName = model.FirstName,
				Lastname = model.LastName,
				Adress = model.Adress,
				City = model.City,
				Email = model.Email,
				Phone = model.Phone,
				UserId = userId
			};

			foreach (var cartItem in model.CartTemplate.CartItems)
			{
				var orderItem = new Entites.OrderItem()
				{
					Price = cartItem.Price,
					Quantity = cartItem.Quantity,
					EProductId = cartItem.ProductId,
				};

				order.OrderItems.Add(orderItem);
			}

			_orderService.Create(order);

		}

		private object PaymentProces(OrderModel model)
		{
			var userId = _usermanager.GetUserId(User);
			var orders = _orderService.GetOrders(userId);

			var orderListModel = new List<OrderListeModel>();

			OrderListeModel orderModel;

			foreach (var order in orders)
			{

				orderModel = new OrderListeModel();
				orderModel.id = order.Id;
				orderModel.Adress = order.Adress;
				orderModel.OrderNumber = order.OrderNumber;
				orderModel.OrderDate = order.Orderdate;
				orderModel.OrderState = order.OrderEnums;
				orderModel.PaymentTypes = order.PaymentEnum;
				orderModel.OrderNote = order.OrderNote;
				orderModel.City = order.City;
				orderModel.Email = order.Email;
				orderModel.FirstName = order.FirstName;
				orderModel.LastName = order.Lastname;
				orderModel.Phone = order.Phone;

				orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemCartItemModel()
				{
					OrderItemId = i.Id,
					ProductName = i.Eproduct.Name,
					Price = i.Eproduct.Price,
					Quantity = i.Quantity,
					ImageUrl = i.Eproduct.Images[0].ImageUrl
				}).ToList();

				orderListModel.Add(orderModel);
			}

			return View(orderListModel);
		}
	}
}

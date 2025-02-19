using System.Net;
using DocuSign.eSign.Model;
using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using InstrumentHub.WebUI.Extensions;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
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
		public IActionResult AddToBasket(int productId, int quantity, string action = "addToCart")
		{
			_cartService.AddToCart(_usermanager.GetUserId(User), productId, quantity);

			if (action == "buyNow")// valuesini buyNow yaptım
			{
				return RedirectToAction("Checkout", "Basket");
			}

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
			ModelState.Remove("CartTemplate");

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
					var payment = PaymentProccess(model); // Payment Process Yarın yazılacak

					if (payment.Result.Status == "success")
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

		private async Task<Payment> PaymentProccess(OrderModel model)
		{
			Options options = new Options()
			{
				BaseUrl = "https://sandbox-api.iyzipay.com",
				ApiKey = "sandbox-cNnJEaoyNt0sCREL4nOq8PajTLQwWeXz",
				SecretKey = "sandbox-cmJxJfaGlVarqNV3c5ZQcMTwVNh8qswx"
			};

			string extarnalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();

			var externalIp = IPAddress.Parse(extarnalIpString);

			CreatePaymentRequest request = new CreatePaymentRequest();
			request.Locale = Locale.TR.ToString();
			request.ConversationId = Guid.NewGuid().ToString();
			request.Price = model.CartTemplate.TotalPrice().ToString().Split(',')[0];
			request.PaidPrice = model.CartTemplate.TotalPrice().ToString().Split(',')[0];
			request.Currency = Iyzipay.Model.Currency.TRY.ToString();
			request.Installment = 1;
			request.BasketId = model.CartTemplate.CartId.ToString();
			request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
			request.PaymentChannel = PaymentChannel.WEB.ToString();

			PaymentCard paymentCard = new PaymentCard()
			{
				CardHolderName = model.CardName,
				CardNumber = model.CardNumber,
				ExpireYear = model.ExparationYear,
				ExpireMonth = model.ExparationMonth,
				Cvc = model.CVV,
				RegisterCard = 0
			};

			request.PaymentCard = paymentCard;

			Buyer buyer = new Buyer()
			{
				Id = _usermanager.GetUserId(User),
				Name = model.FirstName,
				Surname = model.LastName,
				GsmNumber = model.Phone,
				Email = model.Email,
				IdentityNumber = "12345678901",
				RegistrationAddress = model.Adress,
				Ip = externalIp.ToString(),
				City = model.City,
				Country = "TURKEY",
				ZipCode = "34736"
			};

			request.Buyer = buyer;

			Address address = new Address()
			{
				ContactName = model.FirstName + " " + model.LastName,
				City = model.City,
				Country = "TURKEY",
				Description = model.Adress,
				ZipCode = "34742"
			};

			request.ShippingAddress = address;
			request.BillingAddress = address;

			List<BasketItem> basketItems = new List<BasketItem>();
			BasketItem basketItem;

			foreach (var cartıtem in model.CartTemplate.CartItems)
			{
				basketItem = new BasketItem()
				{
					Id = cartıtem.ProductId.ToString(),
					Name = cartıtem.ProductName,
					Category1 = _productService.GetEProductDetail(cartıtem.ProductId).ProductDivisions.FirstOrDefault().DivisionId.ToString(),
					ItemType = BasketItemType.PHYSICAL.ToString(),
					Price = (cartıtem.Price * cartıtem.Quantity).ToString().Split(',')[0]
				};
				basketItems.Add(basketItem);
			}

			request.BasketItems = basketItems;

			Payment payment = await Payment.Create(request, options);

			return payment;
		}

		//Burası Eft Kısmı

		private void SaveOrder(OrderModel model,  string userId)
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

		public IActionResult GetOrders()
		{
			var userId = _usermanager.GetUserId(User);
			var orders = _orderService.GetOrders(userId) ?? new List<Order>(); // Null kontrolü eklendi

			var orderListModel = new List<OrderListeModel>();

			foreach (var order in orders)
			{
				var orderModel = new OrderListeModel
				{
					id = order.Id,
					Adress = order.Adress,
					OrderNumber = order.OrderNumber,
					OrderDate = order.Orderdate,
					OrderState = order.OrderEnums,
					PaymentTypes = order.PaymentEnum,
					OrderNote = order.OrderNote,
					City = order.City,
					Email = order.Email,
					FirstName = order.FirstName,
					LastName = order.Lastname,
					Phone = order.Phone,
					OrderItems = order.OrderItems.Select(i => new OrderItemCartItemModel()
					{
						OrderItemId = i.Id,
						ProductName = i.Eproduct.Name,
						Price = i.Eproduct.Price,
						Quantity = i.Quantity,
						ImageUrl = i.Eproduct.Images[0].ImageUrl
					}).ToList()
				};

				orderListModel.Add(orderModel);
			}

			return View(orderListModel);
		}

	}
}

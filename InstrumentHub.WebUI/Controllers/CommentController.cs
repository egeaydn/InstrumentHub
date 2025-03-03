using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class CommentController : Controller
	{
		private ICommentServices _commentServices;
		private IEProductServices _eProductServices;
		private UserManager<AplicationUser> _userManager;

		public CommentController(UserManager<AplicationUser> usermaneger, ICommentServices commnetservices,IEProductServices eProductServices)
		{
			_commentServices = commnetservices;
			_eProductServices = eProductServices;
			_userManager = usermaneger;
		}

		public IActionResult ShowComment(int? id)
		{
			EProduct eProduct = _eProductServices.GetEProductDetail(id.Value);

			if (id == null)
			{
				return NotFound();
			}
			
			if (eProduct == null)
			{
				return NotFound();
			}

			var users = new Dictionary<string, string>();
			foreach (var comment in eProduct.Comments)
			{
				if (!users.ContainsKey(comment.UserId))
				{
					var user = _userManager.FindByIdAsync(comment.UserId).Result;
					users[comment.UserId] = user?.UserName;
				}
			}

			ViewBag.Usernames = users;

			return PartialView("_PartialComments", eProduct.Comments);
		}

		public IActionResult Create(CommentModel model, int? productId)
		{

			ModelState.Remove("UserId");
			if (ModelState.IsValid)
			{
				if (productId is null)
				{
					return BadRequest();
				}

				EProduct product = _eProductServices.GetById(productId.Value);

				if (product is null)
				{
					return NotFound();
				}

				// Burada rating değeri kontrol ediliyorum
				
				Comment comment = new Comment()
				{
					EProductId = productId.Value,
					CommentCreateOn = DateTime.Now,
					UserId = _userManager.GetUserId(User) ?? "0",
					CommentText = model.Text.Trim('\n').Trim(' '),
					Rating = model.Rating
				};

				_commentServices.Create(comment);

				return Json(new { result = true });
			}

			return View(model);
		}

		public IActionResult Delete(int? id)
		{

			if (id is null)
			{
				return BadRequest();
			}

			Comment comment = _commentServices.GetById(id.Value);

			if (comment is null)
			{
				return NotFound();
			}

			_commentServices.Delete(comment);

			return Json(new { result = true });	
		}

		public IActionResult GetComments(int productId)
		{
			var comments = _commentServices.GetCommentsByProductId(productId);

			var users = new Dictionary<string, string>();
			foreach (var comment in comments)
			{
				if (!users.ContainsKey(comment.UserId))
				{
					var user = _userManager.FindByIdAsync(comment.UserId).Result;
					users[comment.UserId] = user?.UserName;
				}
			}

			ViewBag.Usernames = users;
			return PartialView("_PartialComments", comments);
		}



	}
}

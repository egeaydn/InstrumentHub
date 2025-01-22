using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstrumentHub.WebUI.Controllers
{
	public class AdminController : Controller
	{

		private IEProductServices _productService;
		private IDivisonsServices _categoryService;
		private UserManager<AplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public AdminController(IEProductServices productService, IDivisonsServices categoryService, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_productService = productService;
			_categoryService = categoryService;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		
		public IActionResult EProductList()
		{
			return View(
				new EProductListModel()
				{
					EProducts = _productService.GetAll()
				}
			 );
		}

		public IActionResult CreateEProduct()
		{
			var category = _categoryService.GetAll();
			ViewBag.Category = category.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() });
			return View(new EProductModel());
		}

		[HttpPost]
		public async Task<IActionResult> CreateEProduct(EProductModel model, List<IFormFile> files)
		{
			ModelState.Remove("SelectedDivision");

			if (ModelState.IsValid)
			{
				if (int.Parse(model.DivisionId) == -1)
				{
					ModelState.AddModelError("", "Lütfen bir kategori seçiniz.");

					ViewBag.Divisions = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() });

					return View(model);
				}

				var entity = new EProduct()
				{
					Name = model.Name,
					Description = model.Description,
					Price = model.Price,
					Brand = model.Brand,
				};

				if (files.Count > 0 && files != null)
				{
					if (files.Count < 4)
					{
						ModelState.AddModelError("", "Lütfen en az 4 resim yükleyin.");
						ViewBag.Divisions = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() });
						return View(model);
					}
					foreach (var item in files)
					{
						Image image = new Image();
						image.ImageUrl = item.FileName;

						entity.Images.Add(image);

						var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", item.FileName);

						using (var stream = new FileStream(path, FileMode.Create))
						{
							await item.CopyToAsync(stream);
						}
					}
				}

				entity.ProductDivisions = new List<ProductDivision> { new ProductDivision { DivisionId = int.Parse(model.DivisionId), EProductId = entity.Id } };

				_productService.Create(entity);

				return RedirectToAction("EProductList");
			}

			ViewBag.Divisions = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() });


			return View(model);

		}


		public IActionResult EditEProduct(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = _productService.GetEProductDetail(id);

			if (entity == null)
			{
				return NotFound();
			}

			var model = new EProductModel()
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Price = entity.Price,
				Image = entity.Images,
				SelectedDivision = entity.ProductDivisions.Select(i => i.Division).ToList()
			};

			ViewBag.Categories = _categoryService.GetAll();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditEProduct(EProductModel model, List<IFormFile> files, int[] divisionIds)
		{
			var entity = _productService.GetById(model.Id);

			if (entity == null)
			{
				return NotFound();
			}

			entity.Name = model.Name;
			entity.Description = model.Description;
			entity.Price = model.Price;

			if (files != null)
			{
				foreach (var file in files)
				{
					Image image = new Image();
					image.ImageUrl = file.FileName;

					entity.Images.Add(image);

					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

					using (var stream = new FileStream(path, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
				}
			}

			_productService.Update(entity,divisionIds);

			return RedirectToAction("ProductList");
		}

		[HttpPost]
		public IActionResult DeleteProduct(int productId)
		{
			var product = _productService.GetById(productId);

			if (product != null)
			{
				_productService.Delete(product);
			}

			return RedirectToAction("ProductList");
		}

		public IActionResult DivisionList()
		{
			return View(new DivisionListModel() { Divisions = _categoryService.GetAll() });
		}

		public IActionResult EditDivisions(int? id)
		{
			var entity = _categoryService.GetByWithProducts(id.Value);

			return View(
					new DivisionModel()
					{
						Id = entity.Id,
						Name = entity.CategoryName,
						EProducts = entity.ProductDivisions.Select(i => i.EProduct).ToList()
					}
				);
		}


		[HttpPost]
		public IActionResult EditDivisions(DivisionModel model)
		{
			var entity = _categoryService.GetById(model.Id);

			if (entity == null)
			{
				return NotFound();
			}

			entity.CategoryName = model.Name;
			_categoryService.Update(entity);

			return RedirectToAction("CategoryList");
		}

		[HttpPost]
		public IActionResult DeleteCategory(int categoryId)
		{
			var entity = _categoryService.GetById(categoryId);
			_categoryService.Delete(entity);

			return RedirectToAction("CategoryList");
		}

		public IActionResult CreateDivisions()
		{
			return View(new DivisionModel());
		}


		[HttpPost]
		public IActionResult CreateDivisions(DivisionModel model)
		{
			var entity = new Division()
			{
				CategoryName = model.Name
			};

			_categoryService.Create(entity);

			return RedirectToAction("CategoryList");
		}
	}
}

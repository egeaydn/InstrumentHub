using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;
using Microsoft.EntityFrameworkCore;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
	public class EfCoreEProductDal : EfCoreGenericRepository<EProduct, DataContext>, IProductDal
	{
		public int GetCountByDivision(string division)
		{
			using (var context = new DataContext())
			{
				var eproducts = context.EProducts.AsQueryable();

				if (!string.IsNullOrEmpty(division))
				{
					eproducts = eproducts
						.Include(i => i.ProductDivisions)
						.ThenInclude(i => i.Division)
						.Where(i => i.ProductDivisions.Any(a => a.Division.CategoryName.ToLower() == division.ToLower()));

					return eproducts.Count();
				}
				return 0;
			}
		}

		public List<EProduct> GetEProductsCategory(string division, int screen, int screenSize)
		{
			using (var context = new DataContext())
			{
				var eproducts = context.EProducts.Include("Images").AsQueryable();

				if (!string.IsNullOrEmpty(division))
				{
					eproducts = eproducts
						.Include(i => i.ProductDivisions)
						.ThenInclude(i => i.Division)
						.Where(i => i.ProductDivisions.Any(a => a.Division.CategoryName.ToLower() == division.ToLower()));
				}
				return eproducts.Skip((screen - 1) * screenSize).Take(screenSize).ToList();
			}
		}

		public EProduct GetProductDetails(int id)
		{
			using (var context = new DataContext())
			{
				return context.EProducts
					.Where(i => i.Id == id)
					.Include("Images")
					.Include("Comments")
					.Include(i => i.ProductDivisions)
					.ThenInclude(i => i.Division)
					.FirstOrDefault();
			}
		}

		public void Update(EProduct entity , int[] divisionid)
		{
			using (var context = new DataContext())
			{
				var products = context.EProducts.Include(i => i.ProductDivisions).FirstOrDefault(i => i.Id == entity.Id);

				if(products is not null)
				{
					context.Images.RemoveRange(context.Images.Where(i => i.EProductId == entity.Id));
					products.Price = entity.Price;
					products.Name = entity.Name;
					products.Description = entity.Description;
					products.ProductDivisions = divisionid.Select(cartid => new ProductDivision()
					{
						EProductId = entity.Id,
						DivisionId = cartid,
					}).ToList();
					products.Images = entity.Images;
				}
				context.SaveChanges();
			}
		}
		public void Delete(EProduct entity)
		{
			using (var context = new DataContext())
			{
				context.Images.RemoveRange(entity.Images);
				context.EProducts.Remove(entity);
				context.SaveChanges();
			}
		}

		public List<EProduct> GetEProductsDivision(string division, int screen, int screenSize)
		{
			using (var context = new DataContext())
			{
				var products = context.EProducts.Include("Images").AsQueryable();


				if (!string.IsNullOrEmpty(division))
				{
					products = products
							  .Include(i => i.ProductDivisions)
							  .ThenInclude(i => i.Division)
							  .Where(i => i.ProductDivisions.Any(a => a.Division.CategoryName.ToLower() == division.ToLower()));
				}

				return products.Skip((screen - 1) * screenSize).Take(screenSize).ToList();
			}
		}
		public override List<EProduct> GetAll(Expression<Func<EProduct, bool>> filter = null)
		{
			using (var context = new DataContext())
			{
				return filter == null
						? context.EProducts.Include("Images").ToList()
						: context.EProducts.Include("Images").Where(filter).ToList();

			}
		}
	}
}

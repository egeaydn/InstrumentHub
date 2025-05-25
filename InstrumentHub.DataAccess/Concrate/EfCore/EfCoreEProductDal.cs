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
				else
				{
					return eproducts.Include(i => i.ProductDivisions)
								  .ThenInclude(i => i.Division)
								  .Where(i => i.ProductDivisions.Any())
								  .Count();
				}
				return 0;
			}
		}

		public List<EProduct> GetEProductsDivision(string division, int page, int pageSize)
		{
			using (var context = new DataContext())
			{
				var eproducts = context.EProducts
					.Include("Images")
					.Include(i => i.ProductDivisions)
					.ThenInclude(i => i.Division)
					.AsQueryable();

				if (!string.IsNullOrEmpty(division) && division != "all")
				{
					eproducts = eproducts
						.Include(i => i.ProductDivisions)
						.ThenInclude(i => i.Division)
						.Where(i => i.ProductDivisions.Any(a => a.Division.CategoryName.ToLower() == division.ToLower()));
				}
				return eproducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
		public override void Delete(EProduct entity)
		{
			using (var context = new DataContext())
			{
				context.Images.RemoveRange(entity.Images);
				context.EProducts.Remove(entity);
				context.SaveChanges();
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

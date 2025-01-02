using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}

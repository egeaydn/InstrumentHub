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
	public class EfCoreDivisionDal : EfCoreGenericRepository<Division, DataContext>, IDivisionDal
	{
		public void DeleteDivision(int divisionId, int productId)
		{
			using (var context = new DataContext())
			{
				var cmd = @"delete from ProductDivision where ProductId and DivisionId= @p0";
				context.Database.ExecuteSqlRaw(cmd, productId, divisionId);
			}
		}

		public Division GetByıProducts(int id)
		{
			using (var context = new DataContext())
			{
				return context.Divisions
					.Where(i => i.Id == id)
					.Include(i => i.ProductDivisions)
					.ThenInclude(i => i.EProduct)
					.ThenInclude(i => i.Images)
					.FirstOrDefault();
			}
		}

		public override void Update(Division entity)
		{
			using (var context = new DataContext())
			{
				context.Divisions.Remove(entity);
				context.SaveChanges();
			}
		}
	}
}

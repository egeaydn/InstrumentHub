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
	public class EfCoreCartDal : EfCoreGenericRepository<Cart, DataContext>, ICartDal
	{
		public Cart CartByUserId(string userId)
		{
			using (var context = new DataContext())
			{
				return context.Carts
					.Include(i => i.CartItems)
					.ThenInclude(i => i.EProduct)
					.ThenInclude(i => i.Images)
					.FirstOrDefault(i => i.UserId == userId);
			}
		}

		public void ClearCart(string cartId)
		{
			using(var context = new DataContext())
			{
				var cmd = @"delete from CartItem where CartId=@p0";
				context.Database.ExecuteSqlRaw(cmd, cartId);
			}
		}

		public void DeleteCart(int cartId, int eproductId)
		{
			using (var context = new DataContext())
			{
				var cmd = @"delete from CartItem where CartId=@p0 and EProductId=@p1";
				context.Database.ExecuteSqlRaw(cmd,cartId, eproductId);
			}
		}
		public override void Update(Cart entity)
		{
			using (var context = new DataContext())
			{
				context.Carts.Update(entity);
				context.SaveChanges();
			}
		}
	}
}

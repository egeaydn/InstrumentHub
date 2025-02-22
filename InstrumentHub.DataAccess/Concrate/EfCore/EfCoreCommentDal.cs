using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
	public class EfCoreCommentDal : EfCoreGenericRepository<Comment, DataContext>, ICommentDal
	{
		public double GetAverageRating(int eproductId)
		{
			using (var context = new DataContext())
			{
				var reviews = context.Comments.Where(r => r.EProductId == eproductId);
				return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
			}
		}

		public List<Comment> GetCommetsByProductId(int eproductId)
		{
			using (var context = new DataContext())
			{
				return context.Comments.Where(r => r.EProductId == eproductId).ToList();
			}
		}

	}
}

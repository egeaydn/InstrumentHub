using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface ICommentDal : IRepository<Comment>
	{
		List<Comment> GetCommetsByProductId(int productId);
		double GetAverageRating(int productId);
	}
}

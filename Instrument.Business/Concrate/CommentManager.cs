using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instrument.Business.Abstract;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace Instrument.Business.Concrate
{
	public class CommentManager : ICommentServices
	{
		private ICommentDal  _commentDal;

		public CommentManager(ICommentDal commentDal)
		{
			_commentDal = commentDal;
		}
		public void Create(Comment entity)
		{
			_commentDal.Create(entity);
		}

		public void Delete(Comment entity)
		{
			_commentDal.Delete(entity);
		}

		public double GetAverageRating(int eproductId)
		{
			return _commentDal.GetAverageRating(eproductId);
		}

		public Comment GetById(int id)
		{
			return _commentDal.GetbyId(id);
		}

		public void Update(Comment entity)
		{
			_commentDal.Update(entity);
		}
		public List<Comment> GetCommentsByProductId(int eproductId)
		{
			return _commentDal.GetCommetsByProductId(eproductId);
		}

	}
}

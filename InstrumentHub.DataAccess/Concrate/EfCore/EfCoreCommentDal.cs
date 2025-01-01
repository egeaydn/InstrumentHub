using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
	public class EfCoreCommentDal : EfCoreGenericRepository<Comment,DataContext>, ICommentDal
	{
	}
}

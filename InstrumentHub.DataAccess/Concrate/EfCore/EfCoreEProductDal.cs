using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
	public class EfCoreEProductDal : EfCoreGenericRepository<EProduct, DataContext>, IProductDal
	{
		public int GetCountByDivision(string division)
		{
			throw new NotImplementedException();
		}

		public List<EProduct> GetEProductsCategory(string category, int screen, int screenSize)
		{
			throw new NotImplementedException();
		}

		public EProduct GetProductDetails(int id)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface IProductDal : IRepository<EProduct>
	{
		int GetCountByDivision(string division);
		EProduct GetProductDetails(int id);
		List<EProduct> GetEProductsDivision(string division, int screen, int screenSize);
		void Update(EProduct entity, int[] categoryIds);
	}
}

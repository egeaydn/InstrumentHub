using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface IProductDal
	{
		int GetCountByDivision(string division);
		EProduct GetProductDetails(int id);
		List<EProduct> GetEProductsCategory(string category, int screen, int screenSize);
	}
}

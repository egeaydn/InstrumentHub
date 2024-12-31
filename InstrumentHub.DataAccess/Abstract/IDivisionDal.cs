using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface IDivisionDal : IRepository<Division>
	{
		void DeleteDivision(int divisionId, int productId);
		Division GetByıProducts(int id);
	}
}

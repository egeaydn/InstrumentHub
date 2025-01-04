using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace Instrument.Business.Abstract
{
	public interface IDivisonsServices
	{
		Division GetById(int id);
		Division GetByWithProducts(int id);
		List<Division> GetAll();
		void Create(Division entity);
		void Update(Division entity);
		void Delete(Division entity);
		void DeleteFromCategory(int categoryId, int productId);
	}
}

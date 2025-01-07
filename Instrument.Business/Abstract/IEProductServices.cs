using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace Instrument.Business.Abstract
{
	public interface IEProductServices
	{
		EProduct GetById(int id);
		List<EProduct> GetEProductByDivision(string division, int page, int pageSize);
		List<EProduct> GetAll();
		EProduct GetEProductDetail(int id);
		void Create(EProduct entity);
		void Update(EProduct entity);
		void Delete(EProduct entity);
		int GetCountByDivision(string division);
	}
}

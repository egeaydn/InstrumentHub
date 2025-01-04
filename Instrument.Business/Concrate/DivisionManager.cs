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
	internal class DivisionManager : IDivisonsServices
	{
		private IDivisionDal _divisionDal;
		public DivisionManager(IDivisionDal categoryDal)
		{
			_divisionDal = categoryDal;
		}
		public void Create(Division entity)
		{
			_divisionDal.Create(entity);
		}

		public void Delete(Division entity)
		{
			_divisionDal.Delete(entity);
		}

		public void DeleteFromCategory(int categoryId, int productId)
		{
			_divisionDal.DeleteDivision(categoryId, productId);
		}

		public List<Division> GetAll()
		{
			return _divisionDal.GetAll();
		}

		public Division GetById(int id)
		{
			return _divisionDal.GetbyId(id);
		}

		public Division GetByWithProducts(int id)
		{
			return _divisionDal.GetByıProducts(id);
		}

		public void Update(Division entity)
		{
			_divisionDal.Update(entity);
		}
	}
}

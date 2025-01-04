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
	public class ProductManager : IEProductServices
	{
		private IProductDal _eproductDal;

		public ProductManager(IProductDal productDal)
		{
			_eproductDal = productDal;
		}

		public void Create(EProduct entity)
		{
			_eproductDal.Create(entity);
		}

		public void Delete(EProduct entity)
		{
			_eproductDal.Delete(entity);
		}

		public List<EProduct> GetAll()
		{
			return _eproductDal.GetAll();
		}

		public EProduct GetById(int id)
		{
			return _eproductDal.GetbyId(id);
		}

		public int GetCountByCategory(string category)
		{
			return _eproductDal.GetCountByDivision(category);
		}

		public List<EProduct> GetEProductByDivision(string category, int page, int pageSize)
		{
			return _eproductDal.GetEProductsDivision(category, page, pageSize);
		}

		public EProduct GetEProductDetail(int id)
		{
			return _eproductDal.GetProductDetails(id);
		}

		public void Update(EProduct entity)
		{
			_eproductDal.Update(entity);
		}
	}
}

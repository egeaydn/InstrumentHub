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

		public int GetCountByDivision(string division)
		{
			return _eproductDal.GetCountByDivision(division);
		}

		public List<EProduct> GetEProductByDivision(string division, int page, int pageSize)
		{
			return _eproductDal.GetEProductsDivision(division, page, pageSize);
		}

		public EProduct GetEProductDetail(int id)
		{
			return _eproductDal.GetProductDetails(id);
		}

		public void Update(EProduct entity, int[] divisionIds)
		{
			_eproductDal.Update(entity, divisionIds);
		}

		public List<EProduct> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
		{
			return _eproductDal.GetAll(p => p.Price >= minPrice && p.Price <= maxPrice); 
		}
	}
}

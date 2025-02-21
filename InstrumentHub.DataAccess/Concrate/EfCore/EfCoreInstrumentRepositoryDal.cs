using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
    class EfCoreInstrumentRepositoryDal : EfCoreGenericRepository<EProduct, DataContext>, IInstrumentHubRepositoryDal
	{
        public List<EProduct> GetInstrumentsByPriceRange(decimal price, int instrumentId)
		{
			using (var context = new DataContext())
			{
				var lowerPrice = price * 0.8m;
				var upperPrice = price * 1.2m;

				return context.EProducts
				   .Where(i => i.Id != instrumentId && i.Price >= lowerPrice && i.Price <= upperPrice)
				   .Take(4) 
				   .ToList();
			}
		}
	}
}

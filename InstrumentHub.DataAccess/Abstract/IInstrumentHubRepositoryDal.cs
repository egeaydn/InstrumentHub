using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.DataAccess.Concrate.EfCore;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
    public interface IInstrumentHubRepositoryDal : IRepository<EProduct>
    {
		List<EProduct> GetInstrumentsByPriceRange(decimal price, int instrumentId);
	}
}

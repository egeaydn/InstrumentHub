using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface IRepository<T> 
	{
		T GetbyId(int id);
		T GetOne(Expression<Func<T,bool>>filter=null);
		List<T> GetAll(Expression<Func<T, bool>> filter = null);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}

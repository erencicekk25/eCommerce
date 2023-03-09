using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.BL.Repositories
{
    public interface IRepository<T>//IRepository<Admin> IRepository<Slide>
    {
        public IQueryable<T> GetAll();
        public IQueryable<T> GetAll(Expression<Func<T,bool>> expression);
        public T GetBy(Expression<Func<T,bool>> expression);
        public void Add(T entity);
        public void Update(T entity);
        public void Update(T entity, params Expression<Func<T, object>>[] expressions);
        public void Delete(T entity);
    }
}
//IRepository<Slide> repoSlide();
// IEnumerable<Slide> slaytlar=repoSlide.GetAll();
// IEnumerable<Slide> slaytlar2=repoSlide.GetAll(x=>x.Name=='Slayt1');
//return view(slaytlar2)
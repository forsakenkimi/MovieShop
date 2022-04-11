using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //implement IRpository and inject instance of MovieShopDbContext (for display DI, will be use later on)

        protected readonly MovieShopDbContext _dbContext;
        public Repository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

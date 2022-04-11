using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    //This is a base interface with basic CRUD functionalities
    public interface IRepository<T> where T : class
    {
        
        //Get record by id
        Task<T> GetById(int id);
        
        //Getting all the records; usually for small table
        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);
        
        Task<T> Update(T entity);

        Task Delete(T entity);

    }
}

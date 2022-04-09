using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        //This is a base interface with basic CRUD functionalities

        //Get record by id
        T GetById(int id);
        
        //Getting all the records; usually for small table
        IEnumerable<T> GetAll();

        T Add(T entity);
        
        T Update(T entity);

        void Delete(T entity);

    }
}

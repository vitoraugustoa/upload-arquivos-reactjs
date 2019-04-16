using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadArquivosAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();


        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        void AddRange(IEnumerable<T> arquivos);

        void RemoveRange(IEnumerable<T> arquivos);

        void UpdateRange(IEnumerable<T> arquivos);

    }
}

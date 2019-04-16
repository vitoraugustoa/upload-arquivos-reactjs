using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivosAPI.Context;
using UploadArquivosAPI.Interfaces;

namespace UploadArquivosAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ArquivoContext _context;
        internal DbSet<T> _dbSet;

        public Repository(ArquivoContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void AddRange(IEnumerable<T> arquivos)
        {
            _context.AddRange(arquivos);
        }

        public void RemoveRange(IEnumerable<T> arquivos)
        {
            _context.RemoveRange(arquivos);
        }

        public void UpdateRange(IEnumerable<T> arquivos)
        {
            _context.Arquivos.AttachRange();

            foreach (T arquivo in arquivos)
            {
                _context.Entry(arquivo).State = EntityState.Modified;
            }
        }
    }
}

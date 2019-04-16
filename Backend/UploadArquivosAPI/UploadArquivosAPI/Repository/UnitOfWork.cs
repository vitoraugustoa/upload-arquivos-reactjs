using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivosAPI.Context;
using UploadArquivosAPI.Interfaces;

namespace UploadArquivosAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArquivoContext _context;
        public IArquivoRepository Arquivo { get; private set; }

        public UnitOfWork()
        {
        }

        public UnitOfWork(ArquivoContext context)
        {
            _context = context;
            Arquivo = new ArquivoRepository(_context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

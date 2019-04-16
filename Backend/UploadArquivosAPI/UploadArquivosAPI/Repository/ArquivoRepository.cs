using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivosAPI.Context;
using UploadArquivosAPI.Interfaces;
using UploadArquivosAPI.Models;

namespace UploadArquivosAPI.Repository
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(ArquivoContext context) : base(context)
        {

        }

        public List<Arquivo> Get(int id)
        {
            return _context.Arquivos.FromSql($"SELECT * FROM arquivos WHERE id = {id}").ToList(); ;
        }

        public Arquivo Find(Arquivo arquivo)
        {
            return _context.Arquivos.Find(arquivo);
        }
    }
}

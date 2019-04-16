using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivosAPI.Models;

namespace UploadArquivosAPI.Context
{
    public class ArquivoContext : DbContext
    {

        public ArquivoContext(DbContextOptions<ArquivoContext> options): 
        base(options) {   }

        public DbSet<Arquivo> Arquivos { get; set; }
    }
}

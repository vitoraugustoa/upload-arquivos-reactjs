using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivosAPI.Models;

namespace UploadArquivosAPI.Interfaces
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
        List<Arquivo> Get(int id);
        Arquivo Find(Arquivo arquivo);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadArquivosAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArquivoRepository Arquivo { get; }
        int Commit();
    }
}

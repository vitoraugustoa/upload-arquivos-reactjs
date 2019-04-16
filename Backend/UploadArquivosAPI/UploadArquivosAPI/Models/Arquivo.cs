using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UploadArquivosAPI.Models
{
    [Table("Arquivos")]
    public class Arquivo
    {
        [Key]
        public int Id { get; set; }
    
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        public int Size { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

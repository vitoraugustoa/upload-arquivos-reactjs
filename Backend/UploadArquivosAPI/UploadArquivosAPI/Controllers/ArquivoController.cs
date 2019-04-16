using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using UploadArquivosAPI.Interfaces;
using UploadArquivosAPI.Models;

namespace UploadArquivosAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("UploadFiles")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;

        public ArquivoController(IUnitOfWork unitOfWork , IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpGet]
        public IEnumerable<Arquivo> GetArquivos()
        {
            return _unitOfWork.Arquivo.GetAll();
        }

        [HttpPost("{files}"), DisableRequestSizeLimit]
        public IActionResult UploadArquivos(List<IFormFile> files)
        {
            Arquivo arquivo = new Arquivo();

            if (files == null)
            {
                return BadRequest();
            }

            foreach (var file in files)
            {   
                string webRoot = _environment.ContentRootPath;
                string extension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(webRoot,"wwwroot/Arquivos/");
                var nomeArquivo = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                arquivo.Name = file.FileName;
                arquivo.Size = Convert.ToInt32(file.Length);
                arquivo.Url = $"https://localhost:5001/Arquivos/{file.FileName}";
                arquivo.CreatedAt = DateTime.Now;

                _unitOfWork.Arquivo.Add(arquivo);

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                
                using (var stream = new FileStream($"{filePath}{nomeArquivo}", FileMode.Create))
                {
                    file.CopyTo(stream);
                    _unitOfWork.Commit();
                }

            }

            return Ok(arquivo);
        }

        [HttpDelete("{id}", Name = "delete")]
        public IActionResult DeleteArquivo([FromRoute] int id)
        {
            Arquivo arc = new Arquivo();
            List<Arquivo> arquivos = _unitOfWork.Arquivo.Get(id);

            if (arquivos.Count <= 0)
                return NotFound();

            foreach(Arquivo arquivo in arquivos)
            {
               arc = arquivo;
               _unitOfWork.Arquivo.Remove(arquivo);
               string dirPath = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot/Arquivos/{arquivo.Name}");
                System.IO.File.Delete(dirPath);
            }

            _unitOfWork.Commit();
            return Ok(arc);
        }

    }
}

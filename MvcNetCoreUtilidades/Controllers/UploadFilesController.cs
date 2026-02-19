using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SubirFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            //Necesitamos la ruta a wwwroot/uploads
            string rootFolder = this.hostEnvironment.WebRootPath;
            //Vamos a subir el fichero a los elementos temporales
            string fileName=fichero.FileName;
            //Cuando pensamos ne ficheros y sus rutas, estamos pensando en algo parecido a esto:
            //C:\misficheros\carpeta\1.txt
            //NetCore no es windows, y esta ruta es de windows, las rutas de linux pueden ser distintas
            //y MacOS no funcionan
            //Debemos crear rutas siempre con herramientas de Net Core
            string path = Path.Combine(rootFolder, "uploads", fileName);
            //Para subir ficheros utilizamos Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["FILENAME"] = fileName;
                return View();
        }
    }
}

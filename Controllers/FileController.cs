using System.IO;
using FileAccesses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    public class FileController: Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly IConfiguration _config;
        private readonly IFileAccessService _fileAccess;
        public FileController(IFileAccessService fileAccess,
            IConfiguration config,
            ILogger<FileController> logger)
        {
            _logger = logger;
            _config = config;
            _fileAccess = fileAccess;
        }
        [Produces("application/json")]
        [Route("/Files/Paths")]
        public FilePath GetPaths()
        {
            return _fileAccess.GetPaths();
        }
        [Route("/Files/Pdf")]
        public IActionResult GetPdf(string fileName, string directory = "")
        {
            var fullPath = Path.Combine(_config["BasePath"], directory);
            using (var provider = new PhysicalFileProvider(fullPath))
            {
                var stream = provider.GetFileInfo(fileName).CreateReadStream();
                // 第三引数にファイル名を渡すとダウンロード
                return File(stream, "application/pdf");
            }
        }
        [Route("/Files/Movies")]
        public IActionResult GetMovie(string fileName, string directory = "")
        {
            _logger.LogDebug("HelloMovies");
            var fullPath = Path.Combine(_config["BasePath"], directory);
            using (var provider = new PhysicalFileProvider(fullPath))
            {
                var stream = provider.GetFileInfo(fileName).CreateReadStream();
                // 第三引数にファイル名を渡すとダウンロード
                return File(stream, "video/mp4");
            }
        }
        [Route("/Pages/Movies")]
        public IActionResult GetMovePage(string fileName, string directory = "")
        {
            var url = $"/Files/Movies?fileName={fileName}";
            if (string.IsNullOrEmpty(directory) == false)
            {
                url += $"&directory={directory}";
            }
            ViewData["url"] = url;

            return View("./Views/MoviePage.cshtml");
        }
    }
}
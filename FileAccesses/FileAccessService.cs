using Microsoft.Extensions.Configuration;

namespace FileAccesses
{
    public class FileAccessService: IFileAccessService
    {
        private readonly IConfiguration _config;
        public FileAccessService(IConfiguration config)
        {
            _config = config;
        }
        public FilePath GetPaths()
        {
            return FileMapGenerator.GetPaths(_config["BasePath"]);
        }
    }
}
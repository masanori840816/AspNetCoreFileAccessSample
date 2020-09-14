using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace FileAccesses
{
    public static class FileMapGenerator
    {
        public static FilePath GetPaths(string baseDirectory)
        {
            if (Directory.Exists(baseDirectory) == false)
            {
                throw new ArgumentException("Directory was not found");
            }
            using(var provider = new PhysicalFileProvider(baseDirectory))
            {
                var rootInfo = new DirectoryInfo(baseDirectory);   
                var path = new FilePath
                {
                    Name = rootInfo.Name,
                    ParentPath = "",
                    Directory = true,
                    FileType = FileType.Directory,
                    Children = new List<FilePath>(),
                };
                foreach(var content in provider.GetDirectoryContents(string.Empty))
                {
                    path.Children.Add(GetChild(provider, content, ""));
                }
                return path;   
            }
        }
        private static FilePath GetChild(PhysicalFileProvider provider,
            IFileInfo fileInfo, string parentDirectoryName)
        {
            var path = $"{parentDirectoryName}{fileInfo.Name}/";
            var newPath = new FilePath
            {
                Name = fileInfo.Name,
                ParentPath = parentDirectoryName,                
            };
            if (fileInfo.IsDirectory)
            {
                newPath.Directory = true;
                newPath.FileType = FileType.Directory;
                newPath.Children = new List<FilePath>();
                foreach(var content in provider.GetDirectoryContents(path))
                {
                    newPath.Children.Add(GetChild(provider, content, path));
                }
            }
            else
            {
                newPath.FileType = GetFileType(fileInfo);
            }
            return newPath;
        }
        private static FileType GetFileType(IFileInfo fileInfo)
        {
            var extension = Path.GetExtension(fileInfo.PhysicalPath);
            if(string.IsNullOrEmpty(extension))
            {
                return FileType.Others;
            }
            switch(extension)
            {
                case ".pdf":
                    return FileType.Pdf;
                case ".mp4":
                case ".m4a":
                    return FileType.Movie;
                default:
                    return FileType.Others;
            }
        }
    }
}
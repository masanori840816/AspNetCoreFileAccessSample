using System.Collections.Generic;

namespace FileAccesses
{
    public class FilePath
    {
        public string Name { get; set; } = "";
        public string ParentPath { get; set; } = "";
        public bool Directory { get; set; }
        public FileType FileType { get; set; }
        public List<FilePath> Children { get; set; } = new List<FilePath>();
    }
}
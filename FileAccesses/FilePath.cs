using System.Collections.Generic;

namespace FileAccesses
{
    public struct FilePath
    {
        public string Name { get; set; }
        public string ParentPath { get; set; }
        public bool Directory { get; set; }
        public FileType FileType { get; set; }
        public List<FilePath> Children { get; set; }
    }
}
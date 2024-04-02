namespace ReadFolder.Models;

public class FileSystemModel
{
        public string Name { get; set; }
        public bool IsFile { get; set; }
        public List<FileSystemModel> Children { get; set; }
}
namespace Raccoon.Tools.Entities;

public class FileStorage : Entity<long>
{
    
    public string Name { get; set; }

    public string FileType { get; set; }

    public FileState State { get; set; }

    public string Path { get; set; }

    public long Size { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string SessionId { get; set; }
}
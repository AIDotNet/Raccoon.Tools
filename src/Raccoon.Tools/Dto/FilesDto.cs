using Raccoon.Tools.Entities;

namespace Raccoon.Tools.Dto;

public class FilesDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public string FileType { get; set; }

    public FileState State { get; set; }
    
    public string StateText => State switch
    {
        FileState.None => "暂无",
        FileState.Success => "成功",
        FileState.Fail => "失败",
        _ => "未知"
    };

    public string Path { get; set; }

    public long Size { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string SessionId { get; set; }
}
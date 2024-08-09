namespace Raccoon.Tools.Entities;

public class ChatLog : Entity<long>
{
    public string Content { get; set; }

    public string Role { get; set; }
    
    public string UserName { get; set; }

    public string FromModel { get; set; }

    public string ParentId { get; set; }
    
    public Dictionary<string,string> Extend { get; set; }
}
namespace Raccoon.Tools.Dto;

public class ResultDto<T>
{
    public string Message { get; set; }

    public bool Success { get; set; }
    
    public T Data { get; set; }
}
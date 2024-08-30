namespace Raccoon.Tools.Dto;

public class ChatModelsDto
{
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 支持函数调用
    /// </summary>
    public bool FunctionCall { get; set; }

    /// <summary>
    /// 模型Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 最大token数
    /// </summary>
    public int Tokens { get; set; }

    public string TokenName
    {
        get
        {
            // 将token数转换为字符串，如果是10000则显示为10k，如果是1000000则显示为1m
            if (Tokens >= 1000000)
            {
                return $"{Tokens / 1000000}m";
            }

            if (Tokens >= 10000)
            {
                return $"{Tokens / 1000}k";
            }

            if (Tokens >= 1000)
            {
                return $"{Tokens / 1000}k";
            }

            if (Tokens <= 0)
            {
                return "无限制";
            }

            return Tokens.ToString();
        }
    }

    /// <summary>
    /// 支持视觉模型
    /// </summary>
    public bool Vision { get; set; }
}
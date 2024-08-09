namespace Raccoon.Tools.Services;

public class TokenService
{
    private const string Path = "Token.rac";
    private static string Token { get; set; }

    public static void SaveToken(string token)
    {
        using var file = File.CreateText(Path);

        file.Write(token);

        Token = token;
    }

    public static string GetToken()
    {
        if (!File.Exists(Path))
        {
            return null;
        }

        using var file = File.OpenText(Path);

        return (Token = file.ReadToEnd().Trim());
    }
}
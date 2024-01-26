namespace FullCart.Application.Common.Shared;


public class AppSetting
{
    public string ApplicationId { get; set; }
    public string[] allowedCrossOrign { get; set; }
    public string[] acceptedMimeTypes { get; set; }
    public int maxFileSize { get; set; }
    public APISecurity APISecurity { get; set; }
}

public class APISecurity
{
    public string Key { get; set; }
    public int Expiration { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
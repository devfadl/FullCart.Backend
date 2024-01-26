namespace FullCart.Application.Common.Dto;
public class DownloadAttachmentBriefDto
{
    [System.Text.Json.Serialization.JsonPropertyName("mimeType")]
    public string MimeType { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("fileStream")]
    public byte[] FileStream { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("fileName")]
    public string FileName { get; set; }
}

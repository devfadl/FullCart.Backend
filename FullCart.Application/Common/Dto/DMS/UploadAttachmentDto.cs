namespace FullCart.Application.Common.Dto.DMS
{
    public class UploadAttachmentDto
    {
        public byte[] File { get; set; }
        public string CreatedBy { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}

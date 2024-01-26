using FullCart.Application.Common.Shared;

using Microsoft.AspNetCore.Http;

namespace FullCart.Application.Common.Interfaces
{
    public interface IUploadAttachmentProxy
    {
        Task<Result<Guid>> Upload(IFormFile file, string createdBy);
    }
}

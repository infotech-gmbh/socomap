using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Models;

namespace Socomap.Adapter
{
    public interface ITransferAdapter
    {
        IActionResult TransmissionState(string id);
        IActionResult TransmissionsCreate(TransmissionsCreateRequest transmissionsCreateRequest);
        IActionResult TransmissionsUpload(string id, Base64UploadTransmission base64UploadTransmission);
    }
}

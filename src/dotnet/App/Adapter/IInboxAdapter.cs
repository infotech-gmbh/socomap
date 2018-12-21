using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Models;

namespace Socomap.Adapter
{
    public interface IInboxAdapter
    {
        IActionResult InboxesCreate(InboxesCreateRequest inboxesCreateRequest);
        IActionResult InboxesNextTransmission(string id);
        IActionResult ConfirmTransmissionReceipt(string id, string tid);
    }
}

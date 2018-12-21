using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Models;
using SocomapLib;
using System;

namespace Socomap.Adapter
{
    /// <summary>  
    ///  Connects the service endpoint with the application logic.
    ///  As the service stubs are generated but not derived from an interface
    ///  the service logic is implemented here, so the generated sources
    ///  can be ammended in a trivial way. This supports easy regeneration.
    /// </summary>  

    //TODO: Interfaces instead of Dictionary and List?

    public class InboxAdapter : ControllerBase, IInboxAdapter
    {
        private IInboxFunctionsSampleApi inboxApi;
        private ITransmissionApi transmissionApi;

        public InboxAdapter(IInboxFunctionsSampleApi inboxApi, ITransmissionApi transmissionApi)
        {
            this.inboxApi = inboxApi ?? throw new ArgumentNullException("inboxApi");
            this.transmissionApi = transmissionApi ?? throw new ArgumentNullException("transmissionApi");
        }

        IActionResult IInboxAdapter.InboxesCreate(InboxesCreateRequest inboxesCreateRequest)
        {
            var inbox = new Inbox { Id = inboxesCreateRequest.PartyName, Email = inboxesCreateRequest.Email };
            try
            {
                inboxApi.CreateInbox(inbox);
                return StatusCode(200, new InboxesCreateResponse200 { ApiKey = inbox.ApiKey });
            }
            catch (ArgumentException)
            {
                return StatusCode(409);
            }
        }

        IActionResult IInboxAdapter.ConfirmTransmissionReceipt(string id, string tid)
        {
            Guid tidGuid;
            try
            {
                tidGuid = Guid.Parse(tid);
            }
            catch (Exception)
            {
                return StatusCode(404);
            }
            var transmission = transmissionApi.GetTransmission(tidGuid);
            if (transmission == null)
                return StatusCode(404);

            transmissionApi.ConfirmReceived(transmission);
            return StatusCode(200);
        }

        IActionResult IInboxAdapter.InboxesNextTransmission(string id)
        {
            try
            {
                var transmission = transmissionApi.GetNextTransmission(id);
                if (transmission == null)
                    return StatusCode(204);
                else
                    return StatusCode(200, new InboxesTransmissionNextResponse200 { Tid = transmission.Id.ToString(), Message = transmission.BinaryContent });
            }
            catch (ArgumentException)
            {
                return StatusCode(404);
            }

        }
    }
}

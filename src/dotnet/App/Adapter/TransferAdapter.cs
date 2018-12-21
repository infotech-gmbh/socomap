using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Models;
using SocomapLib;
using System;

namespace Socomap.Adapter
{
    public class TransferAdapter : ControllerBase, ITransferAdapter
    {
        private IInboxFunctionsSampleApi inboxApi;
        private ITransmissionApi transmissionApi;

        public TransferAdapter(IInboxFunctionsSampleApi inboxApi, ITransmissionApi transmissionApi)
        {
            this.inboxApi = inboxApi ?? throw new ArgumentNullException("inboxApi");
            this.transmissionApi = transmissionApi ?? throw new ArgumentNullException("transmissionApi");
        }

        public IActionResult TransmissionState(string id)
        {
            Guid tidGuid;
            try
            {
                tidGuid = Guid.Parse(id);
            }
            catch (Exception)
            {
                return StatusCode(404);
            }
            var transmission = transmissionApi.GetTransmission(tidGuid);
            if (transmission == null)
                return StatusCode(404);

            return StatusCode(200, new TransmissionsStateResponse200
            {
                Created = transmission.CreatedOn,
                Transferred = transmission.TransferredOn,
                Delivered = transmission.DeliveredOn
            });
        }

        public IActionResult TransmissionsCreate(TransmissionsCreateRequest transmissionsCreateRequest)
        {
            try
            {
                var transmission = transmissionApi.CreateTransmission(transmissionsCreateRequest.Party);
                return StatusCode(200, new TransmissionsCreateResponse200 { Tid = transmission.Id.ToString() });
            }
            catch (ArgumentException)
            {
                //TODO: Specify exactly what is missing on 400 response. How?
                return StatusCode(400);
            }
        }

        public IActionResult TransmissionsUpload(string id, Base64UploadTransmission base64UploadTransmission)
        {
            Guid tidGuid;
            try
            {
                tidGuid = Guid.Parse(id);
            }
            catch (Exception)
            {
                return StatusCode(404);
            }
            var transmission = transmissionApi.GetTransmission(tidGuid);
            if (transmission == null)
                return StatusCode(404);

            if (transmission.BinaryContent != null)
                return StatusCode(412);

            //TODO: The uploaded data should be byte[] from the generated files, not string.
            transmissionApi.AddBinaryContent(transmission, Convert.FromBase64String(base64UploadTransmission.Message));

            return StatusCode(200);
        }
    }
}

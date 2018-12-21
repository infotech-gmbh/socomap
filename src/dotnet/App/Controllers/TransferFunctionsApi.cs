/*
 * SOCOMAP
 *
 * This is the Api for the new Socomap Protocol
 *
 * OpenAPI spec version: 0.0.1
 * Contact: development@infotech.de
 * Generated by: https://openapi-generator.tech
 */

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.OpenAPITools.Attributes;
using Org.OpenAPITools.Models;
using Socomap.Adapter;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Org.OpenAPITools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TransferFunctionsApiController : ControllerBase
    {
        private ITransferAdapter transferAdapter;

        public TransferFunctionsApiController(ITransferAdapter transferAdapter)
        {
            this.transferAdapter = transferAdapter ?? throw new ArgumentNullException("transferAdapter");
        }

        /// <summary>
        /// get transmission state
        /// </summary>
        /// <remarks>returns the current state of the transmission.</remarks>
        /// <param name="id">gets the right transmission</param>
        /// <response code="200">OK</response>
        /// <response code="404">transmission not exists</response>
        [HttpGet]
        [Route("/v1/transmissions/{id}/state")]
        [ValidateModelState]
        [SwaggerOperation("TransmissionState")]
        [SwaggerResponse(statusCode: 200, type: typeof(TransmissionsStateResponse200), description: "OK")]
        public virtual IActionResult TransmissionState([FromRoute][Required]string id)
        {
            return transferAdapter.TransmissionState(id);
        }

        /// <summary>
        /// create a new transmission
        /// </summary>
        /// <remarks> this function is creating a new transmission. the transmission is referenced in any further operation for the transmission. &lt;br&gt;&lt;br&gt; the creation is split off in this separate function to archieve a reliable messaging. if the response gets lost on the wire, only an empty transmission is created. no transmission is generated in the receiver inbox. &lt;br&gt;&lt;br&gt; after the creation a call to TransmissionState returns the field &#x60;created&#x60;with the timestamp of the creation. &lt;br&gt;&lt;br&gt; the parameter target is provided to reject the creation, if no inbox is found on the broker. no further data upload should be done if the inbox is not available. &lt;br&gt;&lt;br&gt; the actual sender is not known on api layer. the &#x60;response_to&#x60; field can be set if the sender expects a response to the transmission. the &#x60;response_to&#x60; field have to provide a full EDI address. </remarks>
        /// <param name="transmissionsCreateRequest"></param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("/v1/transmissions/create")]
        [ValidateModelState]
        [SwaggerOperation("TransmissionsCreate")]
        [SwaggerResponse(statusCode: 200, type: typeof(TransmissionsCreateResponse200), description: "OK")]
        public virtual IActionResult TransmissionsCreate([FromBody]TransmissionsCreateRequest transmissionsCreateRequest)
        {
            return transferAdapter.TransmissionsCreate(transmissionsCreateRequest);
        }

        /// <summary>
        /// upload message data
        /// </summary>
        /// <remarks> after transmission creation. This function is used to upload the pre encrypted WSR message data. &lt;br&gt;&lt;br&gt; TODO: Specify encryption method. &lt;br&gt;&lt;br&gt; after the upload the transmission state is extended with the field &#x60;transferred&#x60;. </remarks>
        /// <param name="id">selects the transmission id</param>
        /// <param name="base64UploadTransmission"></param>
        /// <response code="200">OK</response>
        /// <response code="404">transmission not exists</response>
        /// <response code="412">if the data was already set.</response>
        [HttpPost]
        [Route("/v1/transmissions/{id}/upload")]
        [ValidateModelState]
        [SwaggerOperation("TransmissionsUpload")]
        public virtual IActionResult TransmissionsUpload([FromRoute][Required]string id, [FromBody]Base64UploadTransmission base64UploadTransmission)
        {
            return transferAdapter.TransmissionsUpload(id, base64UploadTransmission);
        }
    }
}

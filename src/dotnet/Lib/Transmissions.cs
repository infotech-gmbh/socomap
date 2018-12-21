using System;
using System.Linq;

namespace SocomapLib
{
    /// <summary>
    ///  Manages all existing transmissions.
    ///  The implementation is not threadsafe.
    /// </summary>

    // TODO: Introduce and inject a Timeprovider-interface to improve testability.

    public class Transmissions : ITransmissionApi
    {
        private TransmissionContext context;
        private IInboxFunctionsSampleApi inboxApi;

        public Transmissions(TransmissionContext Context, IInboxFunctionsSampleApi InboxApi)
        {
            context = Context;
            inboxApi = InboxApi;
        }

        Transmission ITransmissionApi.CreateTransmission(string targetInboxId)
        {
            if (targetInboxId == null)
                throw new ArgumentNullException("TargetInboxId");

            var targetInbox = inboxApi.GetInbox(targetInboxId);
            if (targetInbox == null)
                throw new ArgumentException("TargetInbox does not exist."); //TODO: Need an exception infrastructure.

            Transmission transmission = new Transmission { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, TargetInbox = targetInbox };
            context.Transmissions.Add(transmission);
            context.SaveChanges();  //TODO: Who handles the exceptions?
            return transmission;
        }

        Transmission ITransmissionApi.GetTransmission(Guid id)
        {
            return context.Transmissions.Find(id);
        }

        void ITransmissionApi.AddBinaryContent(Transmission transmission, byte[] binaryContent)
        {
            if (transmission == null)
                throw new ArgumentNullException("Transmission");
            if (transmission.BinaryContent != null)
                throw new InvalidOperationException("Content already assigned.");

            transmission.BinaryContent = binaryContent;
            transmission.TransferredOn = DateTime.Now;

            context.SaveChanges();  //TODO: Who handles the exceptions?
        }

        Transmission ITransmissionApi.GetNextTransmission(string targetInboxId)
        {
            if (targetInboxId == null)
                throw new ArgumentNullException("TargetInboxId");

            var targetInbox = inboxApi.GetInbox(targetInboxId);
            if (targetInbox == null)
                throw new ArgumentException("TargetInbox"); //TODO: Need an exception infrastructure.

            //TODO: We may need more order than the creation datetime if multiple transmissions are accepted within the same datetime.
            var query = from t in context.Transmissions
                        where t.TargetInbox == targetInbox &&
                            t.TransferredOn != null &&
                            t.DeliveredOn == null
                        orderby t.CreatedOn
                        select t;
            return query.FirstOrDefault();
        }

        void ITransmissionApi.ConfirmReceived(Transmission transmission)
        {
            if (transmission == null)
                throw new ArgumentNullException("Transmission");

            transmission.DeliveredOn = DateTime.Now;

            context.SaveChanges();  //TODO: Who handles the exceptions?
        }
    }
}

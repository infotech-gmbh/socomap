using System;

namespace SocomapLib
{
    /// <summary>
    ///  Manages all existing inboxes.
    ///  The implementation is not threadsafe.
    /// </summary>
    public class Inboxes : IInboxFunctionsSampleApi
    {
        private TransmissionContext context;

        public Inboxes(TransmissionContext Context)
        {
            context = Context;
        }

        void IInboxFunctionsSampleApi.CreateInbox(Inbox inbox)
        {
            if (inbox.Id == null || inbox.Id.Length == 0)
                throw new ArgumentException();  //TODO: Maybe use a different exception type? Need to distinguish it from "inboxAlreadyExists".

            var existingInbox = context.Inboxes.Find(inbox.Id);
            if (existingInbox != null)
                throw new ArgumentException();

            inbox.ApiKey = generateApiKey();
            context.Inboxes.Add(inbox);
            context.SaveChanges();  //TODO: Who handles the exceptions?
        }

        Inbox IInboxFunctionsSampleApi.GetInbox(string partyName)
        {
            Inbox result = context.Inboxes.Find(partyName);
            return result;
        }

        private string generateApiKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

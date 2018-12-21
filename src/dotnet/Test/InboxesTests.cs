using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using SocomapLib;
using System;
using Xunit;

namespace SocomapLibTest
{
    public class InboxesTests
    {
        private IInboxFunctionsSampleApi inboxes;

        public InboxesTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransmissionContext>();
            var mockContext = new DbContextMock<TransmissionContext>(optionsBuilder.Options);
            var inboxSet = mockContext.CreateDbSetMock(x => x.Inboxes);

            inboxes = new Inboxes(mockContext.Object);
        }

        [Fact]
        public void AllowsCreation()
        {
            inboxes.CreateInbox(new Inbox { Id = "PartyA" });
        }

        [Fact]
        public void PreventsDuplicateCreation()
        {
            // The party name is the unique identifier.

            inboxes.CreateInbox(new Inbox { Id = "PartyA" });
            Assert.Throws<ArgumentException>(() => inboxes.CreateInbox(new Inbox { Id = "PartyA" }));
        }

        [Fact]
        public void CreateReturnsApiKey()
        {
            var inbox = new Inbox { Id = "PartyA" };
            inboxes.CreateInbox(inbox);

            Assert.NotEmpty(inbox.ApiKey);
        }

        [Fact]
        public void ApiKeyIsDifferentOnEachCall()
        {
            var inbox1 = new Inbox { Id = "PartyA" };
            var inbox2 = new Inbox { Id = "PartyB" };
            inboxes.CreateInbox(inbox1);
            inboxes.CreateInbox(inbox2);

            Assert.NotEqual(inbox1.ApiKey, inbox2.ApiKey);
        }

        [Fact]
        public void CanGetCreatedInboxes()
        {
            var referenceInbox = new Inbox { Id = "PartyA", Email = "PartyE-Mail", ApiKey = "abc123" };
            inboxes.CreateInbox(referenceInbox);

            Inbox getResult = inboxes.GetInbox("PartyA");

            Assert.NotNull(getResult);
            Assert.Equal(referenceInbox, getResult);
        }

        [Fact]
        public void GetReturnsNullIfInboxNotFound()
        {
            Inbox getResult = inboxes.GetInbox("PartyA");

            Assert.Null(getResult);
        }

    }
}

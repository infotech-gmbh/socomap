using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using SocomapLib;
using System;
using Xunit;

namespace SocomapLibTest
{
    public class TransmissionsTests
    {
        private IInboxFunctionsSampleApi inboxes;
        private ITransmissionApi transmissions;

        private string existingInboxId;

        public TransmissionsTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransmissionContext>();
            var mockContext = new DbContextMock<TransmissionContext>(optionsBuilder.Options);
            var inboxSet = mockContext.CreateDbSetMock(x => x.Inboxes);
            var transmissionsSet = mockContext.CreateDbSetMock(x => x.Transmissions);

            inboxes = new Inboxes(mockContext.Object);
            transmissions = new Transmissions(mockContext.Object, inboxes);

            existingInboxId = @"ValidInbox";
            inboxes.CreateInbox(new Inbox { Id = existingInboxId, Email = @"Box@Provider.com", ApiKey = "TheApiKey" });
        }

        [Fact]
        public void AllowsCreationForExistingInbox()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);
            Assert.NotNull(transmission);
        }

        [Fact]
        public void DeniesCreationForNonExistingInbox()
        {
            var nonExistingInboxId = @"InvalidInbox";
            Assert.Throws<ArgumentException>(() => transmissions.CreateTransmission(nonExistingInboxId));
        }

        [Fact]
        public void NewTransmissionDoesNotHaveTransmissionDates()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);

            Assert.Null(transmission.TransferredOn);
            Assert.Null(transmission.DeliveredOn);
        }

        [Fact]
        public void NewTransmissionIsNotQueuedForRetrieval()
        {
            transmissions.CreateTransmission(existingInboxId);

            Assert.Null(transmissions.GetNextTransmission(existingInboxId));
        }

        [Fact]
        public void NewTransmissionAllowsDataAssignment()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);

            var referenceBinarycontent = new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 };
            transmissions.AddBinaryContent(transmission, referenceBinarycontent);
        }

        [Fact]
        public void AddingBinaryDataSetTransmissionToTransferredState()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);

            var referenceBinarycontent = new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 };
            transmissions.AddBinaryContent(transmission, referenceBinarycontent);
            Assert.NotNull(transmission.TransferredOn);
        }

        [Fact]
        public void TransmissionWithBinaryContentDeniesDataAssignment()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);

            var referenceBinarycontent = new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 };
            transmissions.AddBinaryContent(transmission, referenceBinarycontent);
            Assert.Throws<InvalidOperationException>(() => transmissions.AddBinaryContent(transmission, referenceBinarycontent));
        }

        [Fact]
        public void TransmissionWithBinaryDataIsQueuedForRetrieval()
        {
            var transmission = transmissions.CreateTransmission(existingInboxId);
            transmissions.AddBinaryContent(transmission, new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 });

            var transmissionOut = transmissions.GetNextTransmission(existingInboxId);
            Assert.NotNull(transmissionOut);
            Assert.Equal(transmission.Id, transmissionOut.Id);
        }

        [Fact]
        public void ConfirmingReceiptRemovesTransmissionFromQueue()
        {
            {
                var transmission = transmissions.CreateTransmission(existingInboxId);
                transmissions.AddBinaryContent(transmission, new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 });
            }

            var transmissionOut = transmissions.GetNextTransmission(existingInboxId);
            transmissions.ConfirmReceived(transmissionOut);
            Assert.Null(transmissions.GetNextTransmission(existingInboxId));
        }

        [Fact]
        public void ConfirmingReceiptSetsTransmissionToDeliveredState()
        {
            {
                var transmission = transmissions.CreateTransmission(existingInboxId);
                transmissions.AddBinaryContent(transmission, new byte[] { 0xFF, 0x0, 0x10, 0x20, 0x30, 0x40, 0x50 });
            }

            var transmissionOut = transmissions.GetNextTransmission(existingInboxId);
            transmissions.ConfirmReceived(transmissionOut);
            Assert.NotNull(transmissionOut.DeliveredOn);
        }
    }
}

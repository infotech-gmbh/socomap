using System;

namespace SocomapLib
{
    public interface ITransmissionApi
    {
        Transmission CreateTransmission(string targetInboxId);
        Transmission GetTransmission(Guid id);
        void AddBinaryContent(Transmission transmission, byte[] binaryContent);
        Transmission GetNextTransmission(string targetInboxId);
        void ConfirmReceived(Transmission transmission);
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocomapLib
{
    public class Transmission
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }
        public Inbox TargetInbox { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? TransferredOn { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public byte[] BinaryContent { get; set; }
    }
}

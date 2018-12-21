using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocomapLib
{
    public class Inbox
    {
        [Key, Column(Order = 0)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string ApiKey { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int? UserId { get; set; }
        public int Amount { get; set; }
        public string? TransactionType { get; set; }
        public string? TransactionStatus { get; set; }
        public string? Active { get; set; }

        public virtual User? User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodie_mvc.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderedItems = new HashSet<OrderedItems>();
        }
        [Key]
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? TotalPrice { get; set; }
        public DateTime? OrderedAt { get; set; }
        public string? Active { get; set; }
        [ForeignKey("OrderStatusNavigation")]
        public int? OrderStatus { get; set; }

        public virtual OrderStatus? OrderStatusNavigation { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }
    }
}

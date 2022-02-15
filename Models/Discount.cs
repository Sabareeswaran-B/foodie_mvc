using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Products = new HashSet<Product>();
        }

        public int DiscountId { get; set; }
        public string DiscountName { get; set; } = null!;
        public int DiscountAmount { get; set; }
        public int? MaxDiscount { get; set; }
        public int Validity { get; set; }
        public string? Active { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

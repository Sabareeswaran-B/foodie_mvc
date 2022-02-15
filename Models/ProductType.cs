using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Category { get; set; }
        public string? Active { get; set; }
        public string? ServedAs { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

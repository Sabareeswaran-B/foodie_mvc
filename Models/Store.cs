using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }

        public int StoreId { get; set; }
        public string? OwnerName { get; set; }
        public string? StoreAddress { get; set; }
        public int? AdminId { get; set; }
        public string? Active { get; set; }
        public string? StoreName { get; set; }
        public int? StoreType { get; set; }

        public virtual User? Admin { get; set; }
        public virtual StoreType? StoreTypeNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

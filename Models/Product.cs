using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderedItems = new HashSet<OrderedItems>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? QuantityAvailable { get; set; }
        public int? StoreId { get; set; }
        public int? TypeId { get; set; }
        public int? Mrp { get; set; }
        public int? SpecialPrice { get; set; }
        public string? Active { get; set; }
        public int? DiscountId { get; set; }

        public virtual Discount? Discount { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ProductType? Type { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }
    }
}

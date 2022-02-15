using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class StoreType
    {
        public StoreType()
        {
            Stores = new HashSet<Store>();
        }

        public int StoreTypeId { get; set; }
        public string? StoreType1 { get; set; }
        public string? Active { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}

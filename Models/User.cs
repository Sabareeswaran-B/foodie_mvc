using System;
using System.Collections.Generic;

namespace foodie_mvc.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Stores = new HashSet<Store>();
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? RegNo { get; set; }
        public string? PhoneNo { get; set; }
        public int? Balance { get; set; }
        public string? Active { get; set; }
        public string? UserRole { get; set; }
        public string PassWord { get; set; } = null!;

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}

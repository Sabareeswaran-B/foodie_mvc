using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace foodie_mvc.Models
{
    public partial class foodieContext : DbContext
    {
        public foodieContext()
        {
        }

        public foodieContext(DbContextOptions<foodieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderedItems> OrderedItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreType> StoreTypes { get; set; } = null!;
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=foodie;Trusted_connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__cart__product_id__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__cart__user_id__3D5E1FD2");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("discount");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount");

                entity.Property(e => e.DiscountName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("discount_name");

                entity.Property(e => e.MaxDiscount).HasColumnName("max_discount");

                entity.Property(e => e.Validity).HasColumnName("validity");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.OrderedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("ordered_at");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.OrderStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatus)
                    .HasConstraintName("FK__orders__order_st__59063A47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__orders__user_id__4222D4EF");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__order_st__3683B531C8CCEADC");

                entity.ToTable("order_status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status_name")
                    .HasDefaultValueSql("('Pending')");
            });

            modelBuilder.Entity<OrderedItems>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__orderedI__52020FDD9A218DC7");

                entity.ToTable("OrderedItems");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderedItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__orderedIt__order__45F365D3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderedItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__orderedIt__produ__46E78A0C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.Mrp).HasColumnName("mrp");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.QuantityAvailable)
                    .HasColumnName("quantity_available")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpecialPrice).HasColumnName("special_price");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__products__discou__6754599E");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__products__store___38996AB5");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__products__type_i__398D8EEE");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__product___2C0005983DDB01FA");

                entity.ToTable("product_type");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.ServedAs)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("servedAs");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("store");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("owner_name");

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("store_address");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("store_name");

                entity.Property(e => e.StoreType).HasColumnName("store_type");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__store__admin_id__2F10007B");

                entity.HasOne(d => d.StoreTypeNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreType)
                    .HasConstraintName("FK__store__store_typ__5DCAEF64");
            });

            modelBuilder.Entity<StoreType>(entity =>
            {
                entity.ToTable("store_type");

                entity.Property(e => e.StoreTypeId).HasColumnName("store_type_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.StoreType1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("store_type");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__transact__85C600AF501D594E");

                entity.ToTable("transaction_history");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.TransactionStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("transaction_status");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("transaction_type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__transacti__user___60A75C0F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.RegNo, "UQ__users__74039E9C8B7DEE57")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__users__7C9273C4659A52B2")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNo, "UQ__users__E6BE36DC1CBAF099")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Active)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pass_word");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_no");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("reg_no");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_role")
                    .HasDefaultValueSql("('user')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

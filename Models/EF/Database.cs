using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Anemone.Models.EF
{
    public partial class Database : DbContext
    {
        internal object categorys;

        public Database()
            : base("name=Database")
        {
        }

        public virtual DbSet<cart_product> cart_product { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<menuType> menuTypes { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<orderdetail> orderdetails { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<slide> slides { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userType> userTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .Property(e => e.metaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.createBy)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.modifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.orderdetails)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.codePr)
                .IsFixedLength();

            modelBuilder.Entity<product>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.metaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.createBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.modifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.cart_product)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.produID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.orderdetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.pass)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.createBy)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.modifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.cart_product)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idID)
                .WillCascadeOnDelete(false);
        }
    }
}

using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities;

namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Directory : DbContext
    {
        public Directory()
            : base("name=Directory")
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Counterparties> Counterparties { get; set; }
        public virtual DbSet<Coworker> Coworker { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }

        public virtual DbSet<OrdersGoods> OrdersGoods { get; set; }
        //public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Loadig_Unloading_work> Loadig_Unloading_work { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>()
                .Property(e => e.type_of_car)
                .IsUnicode(false);

            modelBuilder.Entity<Cars>()
                .Property(e => e.trailer)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.checking_account)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.UNP)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.type_of_counterparty)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.legal_address)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.mailing_address)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.telephones)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.main_contract)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .Property(e => e.CBU)
                .IsUnicode(false);

            modelBuilder.Entity<Counterparties>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Counterparties)
                .HasForeignKey(e => e.Counterparties_key);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.sex)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.telephones)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.adress)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.document_type)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.passport_series)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.passport_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.who_issued)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<Coworker>()
                .Property(e => e.salary)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.nomenclature_type)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.item_type)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Nomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.purpose_of_acquisition)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.consignment_series)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.consignment_number)
                .IsUnicode(false);

            modelBuilder.Entity<Storage>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Storage>()
                .Property(e => e.addres)
                .IsUnicode(false);


            modelBuilder.Entity<Loadig_Unloading_work>()
                .Property(e => e.kind)
                .IsUnicode(false);

            modelBuilder.Entity<Loadig_Unloading_work>()
                .Property(e => e.executor)
                .IsUnicode(false);

            modelBuilder.Entity<Loadig_Unloading_work>()
                .Property(e => e.way)
                .IsUnicode(false);

            modelBuilder.Entity<Loadig_Unloading_work>()
                .Property(e => e.addres)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .HasRequired(x => x.Storage)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.storage_key)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Loadig_Unloading_work>()
                .HasOptional(x => x.Orders)
                .WithMany(x => x.Loadig_Unloading_work)
                .HasForeignKey(x => x.orders_key)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrdersGoods>()
     .HasKey(i => i.Id);

            modelBuilder.Entity<OrdersGoods>()
                .HasRequired(i => i.Order)
                .WithMany(i => i.Goods)
                .HasForeignKey(i => i.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrdersGoods>()
                .HasRequired(i => i.Goods)
                .WithMany()
                .HasForeignKey(i => i.GoodsId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Orders>()
                .HasOptional(x => x.Counterparties)
                .WithMany()
                .HasForeignKey(x => x.Counterparties_key);
               


        }
    }
}

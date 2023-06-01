using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models;

public partial class OderDrinkingContext : DbContext
{
    public OderDrinkingContext()
    {
    }

    public OderDrinkingContext(DbContextOptions<OderDrinkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DetailBill> DetailBills { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<HistoryBill> HistoryBills { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;uid=sa;password=1;database=Oder drinking;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("Bill");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerReceiveName).HasMaxLength(30);
            entity.Property(e => e.DateReceive).HasColumnType("smalldatetime");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.Bills)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bill_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Account)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.HistoryBillId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("HistoryBillID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);

            entity.HasOne(d => d.HistoryBill).WithMany(p => p.Customers)
                .HasForeignKey(d => d.HistoryBillId)
                .HasConstraintName("FK_Customer_HistoryBill");
        });

        modelBuilder.Entity<DetailBill>(entity =>
        {
            entity.HasKey(e => e.DetailId);

            entity.ToTable("DetailBill");

            entity.Property(e => e.DetailId)
                .ValueGeneratedNever()
                .HasColumnName("DetailID");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Bill).WithMany(p => p.DetailBills)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailBill_Bill1");

            entity.HasOne(d => d.Product).WithMany(p => p.DetailBills)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailBill_Products");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.GenreName).HasMaxLength(50);
        });

        modelBuilder.Entity<HistoryBill>(entity =>
        {
            entity.HasKey(e => e.BillId);

            entity.ToTable("HistoryBill");

            entity.Property(e => e.BillId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("BillID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.ProductsName).HasMaxLength(50);

            entity.HasOne(d => d.Genre).WithMany(p => p.HistoryBills)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_HistoryBill_Genres");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ProductID");
            entity.Property(e => e.Describe).HasMaxLength(100);
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImgPhoto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Genre).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Genres");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

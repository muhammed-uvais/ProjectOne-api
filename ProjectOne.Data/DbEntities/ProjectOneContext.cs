using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectOne.Data.DbEntities;

public partial class ProjectOneContext : DbContext
{
    public ProjectOneContext()
    {
    }

    public ProjectOneContext(DbContextOptions<ProjectOneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmnDoc> AdmnDocs { get; set; }

    public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }

    public virtual DbSet<CompanyMasterBankDetail> CompanyMasterBankDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<InvoiceContent> InvoiceContents { get; set; }

    public virtual DbSet<InvoiceCustomerDetail> InvoiceCustomerDetails { get; set; }

    public virtual DbSet<InvoiceHdr> InvoiceHdrs { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<TaxGroup> TaxGroups { get; set; }

    public virtual DbSet<TaxGroupChild> TaxGroupChilds { get; set; }

    public virtual DbSet<TaxType> TaxTypes { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ProjectOne;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmnDoc>(entity =>
        {
            entity.ToTable("AdmnDoc");

            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UploadFileName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompanyMaster>(entity =>
        {
            entity.ToTable("CompanyMaster");

            entity.Property(e => e.Email).HasMaxLength(160);
            entity.Property(e => e.Fax).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.VatNumber).HasMaxLength(160);
            entity.Property(e => e.Website).HasMaxLength(160);
        });

        modelBuilder.Entity<CompanyMasterBankDetail>(entity =>
        {
            entity.Property(e => e.BankAccountName).HasMaxLength(200);
            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankIfsc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BankIFSC");
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.Ibannumber)
                .HasMaxLength(50)
                .HasColumnName("IBANNumber");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Address1).HasMaxLength(200);
            entity.Property(e => e.Address2).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Trn)
                .HasMaxLength(50)
                .HasColumnName("TRN");
        });

        modelBuilder.Entity<InvoiceContent>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.TaxableValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Vatamount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VATAmount");
            entity.Property(e => e.Vatpercentage).HasColumnName("VATPercentage");
        });

        modelBuilder.Entity<InvoiceCustomerDetail>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(350);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Vatumber).HasMaxLength(50);
        });

        modelBuilder.Entity<InvoiceHdr>(entity =>
        {
            entity.ToTable("InvoiceHdr");

            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.NumberDisplay)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.ToTable("MenuMaster");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaxGroup>(entity =>
        {
            entity.ToTable("TaxGroup");

            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TaxGroupChild>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 3)");
        });

        modelBuilder.Entity<TaxType>(entity =>
        {
            entity.ToTable("TaxType");

            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.TaxType1)
                .HasMaxLength(100)
                .HasColumnName("TaxType");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("UserLogin");

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.ToTable("UserMaster");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

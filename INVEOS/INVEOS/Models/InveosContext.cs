using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace INVEOS.Models;

public partial class InveosContext : DbContext
{
    public InveosContext()
    {
    }

    public InveosContext(DbContextOptions<InveosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchaseorderdetail> Purchaseorderdetails { get; set; }

    public virtual DbSet<Purchaseorderheader> Purchaseorderheaders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Saleorderdetail> Saleorderdetails { get; set; }

    public virtual DbSet<Saleorderheader> Saleorderheaders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            var connectionString = configuration.GetConnectionString("InveosDB");
            optionsBuilder.UseMySQL(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.AddressDetail).HasMaxLength(60);
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.Municipality).HasMaxLength(60);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasColumnType("tinytext");
            entity.Property(e => e.Image).HasColumnType("blob");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Address, "Customer_fk0");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Address)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer_fk0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Role, "Employee_fk0");

            entity.HasIndex(e => e.Address, "Employee_fk1");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstLastname).HasMaxLength(15);
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.Gender).HasColumnType("tinyint(1)");
            entity.Property(e => e.SecondLastname).HasMaxLength(15);
            entity.Property(e => e.SecondName).HasMaxLength(15);

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Address)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_fk1");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_fk0");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idproduct).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.HasIndex(e => e.CategoryId, "Product_fk0");

            entity.HasIndex(e => e.SuplierId, "Product_fk1");

            entity.Property(e => e.Idproduct).HasColumnName("IDProduct");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Cost).HasPrecision(10);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image).HasColumnType("blob");
            entity.Property(e => e.Name).HasMaxLength(25);
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            entity.Property(e => e.SuplierId).HasColumnName("SuplierID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_fk0");

            entity.HasOne(d => d.Suplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SuplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_fk1");
        });

        modelBuilder.Entity<Purchaseorderdetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseOrderDetailId, e.PurchaseOrderHeaderId }).HasName("PRIMARY");

            entity.ToTable("purchaseorderdetail");

            entity.HasIndex(e => e.PurchaseOrderHeaderId, "PurchaseOrderDetail_fk0");

            entity.HasIndex(e => e.ProductId, "PurchaseOrderDetail_fk1");

            entity.Property(e => e.PurchaseOrderDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PurchaseOrderDetailID");
            entity.Property(e => e.PurchaseOrderHeaderId).HasColumnName("PurchaseOrderHeaderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Total).HasPrecision(10);
            entity.Property(e => e.UnitPrice).HasPrecision(10);

            entity.HasOne(d => d.Product).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PurchaseOrderDetail_fk1");

            entity.HasOne(d => d.PurchaseOrderHeader).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.PurchaseOrderHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PurchaseOrderDetail_fk0");
        });

        modelBuilder.Entity<Purchaseorderheader>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderHeaderId).HasName("PRIMARY");

            entity.ToTable("purchaseorderheader");

            entity.HasIndex(e => e.PurchaseNumber, "PurchaseNumber").IsUnique();

            entity.HasIndex(e => e.EmployeeId, "PurchaseOrderHeader_fk0");

            entity.HasIndex(e => e.SupplierId, "PurchaseOrderHeader_fk1");

            entity.Property(e => e.PurchaseOrderHeaderId).HasColumnName("PurchaseOrderHeaderID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.ShipDate).HasColumnType("date");
            entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            entity.Property(e => e.SubTotal).HasPrecision(10);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TaxAmt).HasPrecision(10);
            entity.Property(e => e.Total).HasPrecision(10);

            entity.HasOne(d => d.Employee).WithMany(p => p.Purchaseorderheaders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PurchaseOrderHeader_fk0");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchaseorderheaders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PurchaseOrderHeader_fk1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasColumnType("tinytext");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Saleorderdetail>(entity =>
        {
            entity.HasKey(e => new { e.SaleOrderDetailId, e.SaleOrderHeaderId }).HasName("PRIMARY");

            entity.ToTable("saleorderdetail");

            entity.HasIndex(e => e.SaleOrderHeaderId, "SaleOrderDetail_fk0");

            entity.HasIndex(e => e.ProductId, "SaleOrderDetail_fk1");

            entity.Property(e => e.SaleOrderDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SaleOrderDetailID");
            entity.Property(e => e.SaleOrderHeaderId).HasColumnName("SaleOrderHeaderID");
            entity.Property(e => e.Discount).HasPrecision(10);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Total).HasPrecision(10);
            entity.Property(e => e.UnitPrice).HasPrecision(10);

            entity.HasOne(d => d.Product).WithMany(p => p.Saleorderdetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SaleOrderDetail_fk1");

            entity.HasOne(d => d.SaleOrderHeader).WithMany(p => p.Saleorderdetails)
                .HasForeignKey(d => d.SaleOrderHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SaleOrderDetail_fk0");
        });

        modelBuilder.Entity<Saleorderheader>(entity =>
        {
            entity.HasKey(e => e.SaleOrderId).HasName("PRIMARY");

            entity.ToTable("saleorderheader");

            entity.HasIndex(e => e.CustomerId, "SaleOrderHeader_fk0");

            entity.HasIndex(e => e.SalesPersonId, "SaleOrderHeader_fk1");

            entity.Property(e => e.SaleOrderId).HasColumnName("SaleOrderID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.PaymentStatus).HasColumnType("tinyint(1)");
            entity.Property(e => e.SaleStatus).HasColumnType("tinyint(1)");
            entity.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");
            entity.Property(e => e.ShipDate).HasColumnType("date");
            entity.Property(e => e.SubTotal).HasPrecision(10);
            entity.Property(e => e.TaxAmt).HasPrecision(10);
            entity.Property(e => e.Total).HasPrecision(15);

            entity.HasOne(d => d.Customer).WithMany(p => p.Saleorderheaders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SaleOrderHeader_fk0");

            entity.HasOne(d => d.SalesPerson).WithMany(p => p.Saleorderheaders)
                .HasForeignKey(d => d.SalesPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SaleOrderHeader_fk1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.HasIndex(e => e.AddressId, "Supplier_fk0");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.Address).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Supplier_fk0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.EmployeeId, "User_fk0");

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            entity.Property(e => e.Username).HasMaxLength(10);

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

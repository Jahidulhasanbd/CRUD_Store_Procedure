using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation_with_store_procedure.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FJSE08F;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D88A06E02F");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public IQueryable<Customer>SearchCustomer(string name)
    {
        SqlParameter pName = new SqlParameter("@name", name);
        return this.Customers.FromSqlRaw("EXEC spSearchCustomer @name", pName);
    }
    public void InsertCustomer(Customer customer)
    {
        SqlParameter pName = new SqlParameter("@name", customer.Name);
        SqlParameter pCountry = new SqlParameter("@country", customer.Country);
        this.Database.ExecuteSqlRaw("EXEC spInsertCustomer @name,@country", pName, pCountry);
    }
    public void UpdateCustomer(Customer customer)
    {
        SqlParameter pId = new SqlParameter("@id", customer.CustomerId);
        SqlParameter pName = new SqlParameter("@name", customer.Name);
        SqlParameter pCountry = new SqlParameter("@country", customer.Country);
        this.Database.ExecuteSqlRaw("EXEC spUpdateCustomer @id,@name,@country", pId, pName, pCountry);
    }
    public void DeleteCustomer(int id)
    {
        SqlParameter pId=new SqlParameter("@id", id);
        this.Database.ExecuteSqlRaw("EXEC spDeleteCustomer @id", pId);
    }
}

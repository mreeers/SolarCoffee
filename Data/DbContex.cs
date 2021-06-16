using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbContex : IdentityDbContext
    {
        public DbContex()
        {

        }

        public DbContex(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddresses> CustomerAddresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SelesOrder> SelesOrders { get; set; }
        public virtual DbSet<SelesOrderItem> SelesOrderItems { get; set; }

    }
}

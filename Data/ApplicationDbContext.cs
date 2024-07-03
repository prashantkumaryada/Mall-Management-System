using MallRAj.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MallRAj.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CartModel> Carts { get; set; }

        public DbSet<CustomerModel> Customers { get; set; }

        public DbSet<OrderItemsModel> OrderItems { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<ProductItemModel> ProductItems { get; set; }

        public DbSet<ProductTypeModel> ProductTypes { get; set; }

        public DbSet<MallRAj.Models.StaffModel> StaffModel { get; set; }

        public DbSet<MallRAj.Models.BorrowModel> BorrowModel { get; set; }





    }
}

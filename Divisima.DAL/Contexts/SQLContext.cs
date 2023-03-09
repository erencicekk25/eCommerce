using Divisima.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Contexts
{
    public class SQLContext:DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> opt):base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

            //modelBuilder.Entity<ProductPicture>().HasOne(x => x.Product).WithMany(x => x.ProductPictures);

            modelBuilder.Entity<BlogBlogCategory>().HasKey(x=>new { x.BlogID,x.BlogCategoryID});

            modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<City>().HasOne(x => x.Country).WithMany(x => x.Cities).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<District>().HasOne(x => x.City).WithMany(x => x.Districts).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);

            //Data Seed
            modelBuilder.Entity<Admin>().HasData(new Admin {
                ID=1,
                NameSurname="Eren Çiçek",
                LastLoginDate=DateTime.Now,
                LastLoginIP="",
                Username="eren",
                Password= "202cb962ac59075b964b07152d234b70"
            });
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogBlogCategory> BlogBlogCategory { get; set; }
    }
}

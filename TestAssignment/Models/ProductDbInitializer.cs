using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestAssignment.Models
{
    public class ProductDbInitializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext db)
        {
            db.Products.Add(new Product { ItemName = "HDD 1TB", Quantiy = 55, Price = 220 });
            db.Products.Add(new Product { ItemName = "HDD SSD 512GB", Quantiy = 102, Price = 180 });
            db.Products.Add(new Product { ItemName = "RAM DDR4 16GB", Quantiy = 47, Price = 150 });

            db.Roles.Add(new Role { Id = 1, RoleName = "Admin" });
            db.Roles.Add(new Role { Id = 2, RoleName = "User" });

            db.Users.Add(new User { Id = 1, UserName = "Admin", Password = "123456", Email = "admin@testmail.com", FullName = "Tom Ford", RoleId = 1 });
            db.Users.Add(new User { Id = 2, UserName = "User", Password = "123456", Email = "user@testmail.com", FullName = "Harrison Ford", RoleId = 2 });

            base.Seed(db);
        }
    }
}
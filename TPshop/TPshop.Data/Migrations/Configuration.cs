namespace TPshop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TPshop.Data.TPshopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TPshop.Data.TPshopDbContext context)
        {
            CreateCategoryGroupSample(context);
            CreateCategorySample(context);
            CreateSupplierSample(context);
            CreateContactDetailSample(context);
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TPshopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TPshopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "Admin",
            //    Email = "admin@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Admin"
            //};

            //var user2 = new ApplicationUser()
            //{
            //    UserName = "Phong",
            //    Email = "thanhphong@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "TP shop"

            //};
            //manager.Create(user, "123456");
            //manager.Create(user2, "123456");
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateCategorySample(TPshop.Data.TPshopDbContext context)
        {
            if (context.Categories.Count() == 0)
            {
                List<Category> listCategory = new List<Category>()
            {
                new Category()
                {
                    CategoryGroupID=2,
                    Name="Business Laptop",
                    Alias="business-laptop",
                    Status = true,
                    Homeflag=true
                },
                new Category()
                {
                    CategoryGroupID=2,
                    Name="Gaming laptop",
                    Alias="gaming-laptop",
                    Status = true,
                    Homeflag=true
                },
                new Category()
                {
                    CategoryGroupID=2,
                    Name="2 in 1 laptop",
                    Alias="2in1-laptop",
                    Status = true,
                    Homeflag=true
                },
                new Category()
                {
                    CategoryGroupID=2,
                    Name="Tablet",
                    Alias="tablet",
                    Status = true,
                    Homeflag=true
                }
            };
                context.Categories.AddRange(listCategory);
                context.SaveChanges();
            }
        }

        private void CreateCategoryGroupSample(TPshop.Data.TPshopDbContext context)
        {
            if (context.CategoryGroups.Count() == 0)
            {
                List<CategoryGroup> listCategoryGroup = new List<CategoryGroup>()
            {
                new CategoryGroup()
                {
                    Name="Laptop"
                },
                new CategoryGroup()
                {
                    Name="Storage"
                },
                new CategoryGroup()
                {
                    Name="Ram"
                },
                new CategoryGroup()
                {
                    Name="Laptop"
                }
            };
                context.CategoryGroups.AddRange(listCategoryGroup);
                context.SaveChanges();
            }
        }

        private void CreateSupplierSample(TPshop.Data.TPshopDbContext context)
        {
            if (context.Suppliers.Count() == 0)
            {
                List<Supplier> listSupplier = new List<Supplier>()
            {
                new Supplier()
                {
                    Name="Dell",
                    SupplierAddress="sample address",
                    SupplierEmail="sample@gmail.com",
                    SupplierPhone="0123456789"
                },
                new Supplier()
                {
                    Name="Hp",
                    SupplierAddress="sample address",
                    SupplierEmail="sample@gmail.com",
                    SupplierPhone="0123456789"
                },
                new Supplier()
                {
                    Name="Lenovo",
                    SupplierAddress="sample address",
                    SupplierEmail="sample@gmail.com",
                    SupplierPhone="0123456789"
                },
                new Supplier()
                {
                    Name="Samsung",
                    SupplierAddress="sample address",
                    SupplierEmail="sample@gmail.com",
                    SupplierPhone="0123456789"
                }
            };
                context.Suppliers.AddRange(listSupplier);
                context.SaveChanges();
            }
        }

        private void CreateContactDetailSample(TPshop.Data.TPshopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var listContact = new TPshop.Model.Models.ContactDetail()
                {
                    Name = "Shop linh kiện Thanh Phong",
                    Address = "142 Phạm Phú Thứ",
                    Lat = 10.7441979,
                    Lng = 106.6420335,
                    Phone = "0123456789",
                    Website = "google.com",
                    Other = "",
                    Status = true
                };
                context.ContactDetails.Add(listContact);
                context.SaveChanges();
            }
        }
    }
}
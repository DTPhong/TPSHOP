using System;
using System.Collections;
using System.Collections.Generic;
using TPshop.Model.Models;
using TPshop.Web.Models;

namespace TPshop.Web.Infrastructure.Extensions
{
    public static class EntitiyExtension
    {
        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.CategoryID = productVm.CategoryID;
            product.SupplierID = productVm.SupplierID;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;
            product.Image = productVm.Image;
            product.MoreImage = productVm.MoreImage;
            product.Description = productVm.Description;
            product.Content = productVm.Content;
            product.Price = productVm.Price;
            product.Promotion = productVm.Promotion;
            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.Status = productVm.Status;
            product.HomeFlag = productVm.HomeFlag;
            product.ViewCount = productVm.ViewCount;
            product.Quantity = productVm.Quantity;
            
            product.CPU = productVm.CPU;
            product.Ram = productVm.Ram;
            product.Bus = productVm.Bus;
            product.RamMax = productVm.RamMax;
            product.VGA = productVm.VGA;
            product.Storage = productVm.Storage;
            product.StorageType = productVm.StorageType;
            product.Monitor = productVm.Monitor;
            product.Resolution = productVm.Resolution;
            product.BatteryCapacity = productVm.BatteryCapacity;
            product.BatteryCell = productVm.BatteryCell;
            product.Webcam = productVm.Webcam;
            product.Size = productVm.Size;
            product.Weight = productVm.Weight;
            product.OS = productVm.OS;
            product.Warranty = productVm.Warranty;
        }

        public static void UpdateCategory(this Category category, CategoryViewModel categoryVm)
        {
            category.ID = categoryVm.ID;
            category.CategoryGroupID = categoryVm.CategoryGroupID;
            category.Name = categoryVm.Name;
            category.Alias = categoryVm.Alias;
            category.Image = categoryVm.Image;
            category.Description = categoryVm.Description;
            category.CreatedDate = categoryVm.CreatedDate;
            category.Status = categoryVm.Status;
            category.Homeflag = categoryVm.Homeflag;
        }

        public static void UpdateSupplier(this Supplier supplier, SupplierViewModel supplierVm)
        {
            supplier.ID = supplierVm.ID;
            supplier.Name = supplierVm.Name;
            supplier.SupplierEmail = supplierVm.SupplierEmail;
            supplier.SupplierAddress = supplierVm.SupplierAddress;
            supplier.SupplierPhone = supplierVm.SupplierPhone;
        }

        public static void UpdateCategoryGroup(this CategoryGroup categoryGroup, CategoryGroupViewModel categoryGroupVm)
        {
            categoryGroup.ID = categoryGroupVm.ID;
            categoryGroup.Name = categoryGroupVm.Name;
        }

        public static void UpdateContactDetail(this ContactDetail contactDetail, ContactDetailViewModel contactDetailVm)
        {
            contactDetail.ID = contactDetailVm.ID;
            contactDetail.Name = contactDetailVm.Name;
            contactDetail.Phone = contactDetailVm.Phone;
            contactDetail.Address = contactDetailVm.Address;
            contactDetail.Website = contactDetailVm.Website;
            contactDetail.Lat = contactDetailVm.Lat;
            contactDetail.Lng = contactDetailVm.Lng;
            contactDetail.Other = contactDetailVm.Other;
            contactDetail.Email = contactDetailVm.Email;
            contactDetail.Status = contactDetailVm.Status;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVM)
        {
            feedback.Name = feedbackVM.Name;
            feedback.Email = feedbackVM.Email;
            feedback.Message = feedbackVM.Message;
            feedback.CreatedDate = DateTime.Now;
            feedback.Status = feedbackVM.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVM)
        {
            order.CustomerID = orderVM.CustomerID;
            order.CustomerName = orderVM.CustomerName;
            order.CustomerAddress = orderVM.CustomerAddress;
            order.CustomerPhone = orderVM.CustomerPhone;
            order.CustomerMessage = orderVM.CustomerMessage;
            order.CreateData = DateTime.Now;
            order.PaymentMethod = orderVM.PaymentMethod;
            order.PaymentStatus = orderVM.PaymentStatus;
            order.Status = orderVM.Status;
        }
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }
        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }
    }
}
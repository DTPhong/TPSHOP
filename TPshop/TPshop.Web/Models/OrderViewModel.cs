using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPshop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        
        public string CustomerID { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerPhone { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { get; set; }

        public DateTime? CreateData { get; set; }

        [MaxLength(256)]
        public string PaymentMethod { get; set; }

        [MaxLength(256)]
        public string PaymentStatus { get; set; }

        public bool? Status { get; set; }
        
        //public virtual ApplicationUser User { set; get; }

        public virtual IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
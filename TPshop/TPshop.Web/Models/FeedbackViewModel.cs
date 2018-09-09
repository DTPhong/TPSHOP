using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPshop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }
        [MaxLength(250,ErrorMessage ="Name only have 250 characters.")]
        [Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}
using DemoApp.Data.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoApp.Data.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public string GUID { get; set; }
        
        [IsUniqueEmailAddress]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        
    }
}
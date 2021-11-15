using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoApp.Data.DataModels
{
    public class User
    {
        [Key]
        public string GUID { get; set; }
       
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        
    }
}
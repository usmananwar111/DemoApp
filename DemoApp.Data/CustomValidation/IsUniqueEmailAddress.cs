using DemoApp.Data.DataModels;
using DemoApp.Data.Repository;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoApp.Data.CustomValidation
{
    public class IsUniqueEmailAddress: ValidationAttribute
    {
        private UserRepository _repoUser = new UserRepository();
        private Logger logger = LogManager.GetCurrentClassLogger();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Please enter an email address.");

            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("Please enter an email address.");

            string _value = value.ToString();

            _value = _value.ToUpper();

            var _data = _repoUser.CheckEmailExist(_value);

            if (_data)
                return new ValidationResult("Email already exist in the system. Please enter a unique email.");

            return ValidationResult.Success;
        }
    }
}
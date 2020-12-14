using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models{
    public class PastDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime)value > DateTime.Now)
                return new ValidationResult("Date must be in the past");
            else if((DateTime.Now.Subtract((DateTime)value)).TotalDays/365 <18)
                return new ValidationResult("Chef must be at least 18");
            return ValidationResult.Success;
        }
    }
    public class Chef{
        public int ChefId {get;set;}
        [Required]
        [Display(Name = "First Name")]
        public string FirstName {get;set;}
        [Required]
        [Display(Name = "Last Name")]
        public string LastName {get;set;}
        [Required]
        [PastDate]
        [DataType(DataType.Date)]
        public DateTime Birthday{get;set;}
        //Foreign Key
        public int DishId{get;set;}
        public List<Dish> CreatedDishes {get;set;}

        public int Age()
        {
            TimeSpan interval = DateTime.Now.Subtract(this.Birthday);
            int currentage = (int) Math.Floor(interval.TotalDays / 365);
            return currentage;
        }

    }
}
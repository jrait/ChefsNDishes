using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishID {get;set;}
        [Required]
        [Display(Name = "Name of Dish")]
        public string Name {get;set;}
        //Foreign Key
        [Required]
        [Display(Name = "Chef")]
        public int ChefId {get;set;}
        [Required]
        [Range(1,5)]
        public int Tastiness {get;set;}
        [Required]
        [Range(0,Int32.MaxValue)]
        [Display(Name = "# of Calories")]
        public int Calories {get;set;}
        [Required]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public Chef Creator {get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBrms.Models.Extended
{
    [MetadataType(typeof(MetadataRestaurant))]

    public partial class Restaurant
    {


    }

    public class MetadataRestaurant
        {
        
        
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public Nullable<int> LocationId { get; set; }
        [Required]
        public string PopularMenu { get; set; }
        [Required]
        [DisplayName("Cost Range")]
        public string CostPerOrder { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Cuisine { get; set; }
        [Required]
        public string Extra { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
  
        public Nullable<bool> IsActive { get; set; }
 
        public Nullable<bool> Visible { get; set; }
    }

}
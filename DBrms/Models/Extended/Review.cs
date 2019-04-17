using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBrms.Models.Extended
{
    [MetadataType(typeof(MetadataReview))]

    public partial class Review
    {
    }

    public class MetadataReview
    {
        [Required]
        public int RestaurantsId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Nullable<bool> IsActive { get; set; }
    }


}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBrms.Models.Extended
{
    [MetadataType(typeof(MetadataMagazine))]

    public partial class Magazine
    {
    }

    public class MetadataMagazine
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Details { get; set; }
      
        public Nullable<bool> IsActive { get; set; }
    }
}
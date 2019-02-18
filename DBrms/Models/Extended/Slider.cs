using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace DBrms.Models
{
    [MetadataType(typeof(MetadataSlider))]

    public partial class Slider
    {
    }

    public class MetadataSlider
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
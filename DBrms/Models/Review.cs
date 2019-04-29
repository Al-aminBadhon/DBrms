//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBrms.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Review
    {
        public int ReviewId { get; set; }
        public int RestaurantsId { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        [DisplayName("Active")]
        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> Rating { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}

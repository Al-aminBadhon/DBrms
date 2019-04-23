using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBrms.Models.Extended
{
    [MetadataType(typeof(MetadataItem))]

    public partial class Item
    {
    }

    public class MetadataItem
    {
        public Food Food
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }

    }
}
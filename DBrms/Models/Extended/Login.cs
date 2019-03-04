using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBrms.Models
{
    [MetadataType(typeof(MetadataLogin))]

    public partial class Login
    {
    }

    public class MetadataLogin
    {
        public int UserId { get; set; }
        [DisplayName("User Name") ]
        [Required(ErrorMessage ="Enter Name")]
        public string UserName { get; set; }
       
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        public int UserRole { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.Entities.Models
{
    public interface UserMetaData
    {
        [ScaffoldColumn(false)]
        int ID { get; set; }

        [Required(ErrorMessage = "User name is required"),
         StringLength(25, ErrorMessage = "User name must be less than 25 characters")]
        string UserName { get; set; }

        [Required(ErrorMessage = "Email is required"), StringLength(45)]
        string Email { get; set; }

        [StringLength(350, ErrorMessage = "Bio is limited to 350 characters")]
        string Bio { get; set; }

        [ScaffoldColumn(false)]
        string ProfileImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DataType(DataType.Date)]
        DateTime DateJoined { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DataType(DataType.Date)]
        DateTime DOB { get; set; }

        int GenderId { get; set; }

        int UserTypeID { get; set; }
        int NationalityID { get; set; }
    }
}

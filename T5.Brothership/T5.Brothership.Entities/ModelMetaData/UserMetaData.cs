﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.Entities.Models
{
    public interface UserMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        int ID { get; set; }

        [Required(ErrorMessage = "User name is required"),
         Display(Name = "Username"),
         StringLength(25, ErrorMessage = "User name must be less than 25 and more than 5 characters", MinimumLength = 5)]
        string UserName { get; set; }

        [Required(ErrorMessage = "Email is required"), StringLength(45), EmailAddress]
        string Email { get; set; }

        [StringLength(350, ErrorMessage = "Bio is limited to 350 characters"), DataType(DataType.MultilineText)]
        string Bio { get; set; }

        [ScaffoldColumn(false)]
        string ProfileImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DataType(DataType.Date), Display(Name ="Join Date")]
        DateTime DateJoined { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DataType(DataType.Date) ]
        DateTime DOB { get; set; }

        [Display(Name = "Gender")]
        int GenderId { get; set; }

        int UserTypeID { get; set; }

        [Display(Name = "Nationality")]
        int NationalityID { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.Entities.ModelMetaData
{
    internal interface UserLoginMetaData
    {
        [ScaffoldColumn(false)]
        int UserID { get; set; }

        [ScaffoldColumn(false), StringLength(64)]
        string PasswordHash { get; set; }

        [ScaffoldColumn(false), StringLength(64)]
        string Salt { get; set; }

    }
}

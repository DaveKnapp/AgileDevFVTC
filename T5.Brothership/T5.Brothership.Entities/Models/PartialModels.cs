using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.ModelMetaData;

namespace T5.Brothership.Entities.Models
{
    [MetadataTypeAttribute(typeof(UserMetaData))]
    public partial class User { }

    [MetadataTypeAttribute(typeof(UserLoginMetaData))]
    public partial class UserLogin { }
}

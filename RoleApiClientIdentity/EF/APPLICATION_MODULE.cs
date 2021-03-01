using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoleApiClientIdentity.EF
{
    public class APPLICATION_MODULE
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string URL { get; set; }
        public int PARENT_ID { get; set; }

    }
}
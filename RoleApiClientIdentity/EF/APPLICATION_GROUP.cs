using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleApiClientIdentity.EF
{
    [Table("APPLICATION_GROUPS")]
    public class APPLICATION_GROUP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(250)]
        public string NAME { set; get; }

        [StringLength(250)]
        public string DESCRIPTION { set; get; }

        public virtual List<APPLICATION_ROLE> APPLICATION_ROLES { get; set; }
    }
}
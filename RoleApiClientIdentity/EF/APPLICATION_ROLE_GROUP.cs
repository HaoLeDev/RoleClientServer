using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleApiClientIdentity.EF
{
    [Table("APPLICATION_ROLE_GROUPS")]
    public class APPLICATION_ROLE_GROUP
    {
        [Key]
        [Column(Order = 1)]
        public int GROUP_ID { set; get; }

        [Column(Order = 2)]
        [StringLength(128)]
        [Key]
        public string ROLE_ID { set; get; }

        [ForeignKey("ROLE_ID")]
        public virtual APPLICATION_ROLE APPLICATION_ROLE { set; get; }

        [ForeignKey("GROUP_ID")]
        public virtual APPLICATION_GROUP APPLICATION_GROUP { set; get; }
    }
}
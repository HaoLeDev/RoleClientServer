using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleApiClientIdentity.EF
{
    [Table("APPLICATION_USER_GROUPS")]
    public class APPLICATION_USER_GROUP
    {
        [StringLength(128)]
        [Key]
        [Column(Order = 1)]
        public string USER_ID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int GROUP_ID { set; get; }

        [ForeignKey("USER_ID")]
        public virtual APPLICATION_USER APPLICATION_USER { set; get; }

        [ForeignKey("GROUP_ID")]
        public virtual APPLICATION_GROUP APPLICATION_GROUP { set; get; }
    }
}
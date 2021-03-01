using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleApiClientIdentity.EF
{
    [Table("APPLICATION_ROLES")]
    public class APPLICATION_ROLE : IdentityRole
    {
        public APPLICATION_ROLE() : base()
        {
        }

        public int GroupId { get; set; }

        [StringLength(250)]
        public string Description { set; get; }

        [ForeignKey("GroupId")]
        public virtual APPLICATION_GROUP APPLICATION_GROUP { get; set; }
    }
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleApiClientIdentity.EF
{
    [Table("APPLICATION_USERS")]
    public class APPLICATION_USER : IdentityUser
    {
        public int? EM_ID { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<APPLICATION_USER> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public EMPLOYEE EMPLOYEES { get; set; }
    }
}
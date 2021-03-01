using RoleApiClientIdentity.EF;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RoleApiClientIdentity.Api
{
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        public RolesController() { }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> Create(APPLICATION_ROLE applicationRoleViewModel, HttpRequestMessage request = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (RoleUserDbContext db = new RoleUserDbContext())
                    {
                        db.Roles.Add(applicationRoleViewModel);
                        await db.SaveChangesAsync();
                    }
                    return request.CreateResponse(HttpStatusCode.OK, applicationRoleViewModel);
                }
                catch (Exception dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
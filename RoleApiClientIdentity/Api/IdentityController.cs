using RestSharp;
using RoleApiClientIdentity.EF;
using RoleApiClientIdentity.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TimeAttendency.WebApp.App_Start;

namespace RoleApiClientIdentity.Api
{
    [RoutePrefix("api/identity")]
    public class IdentityController : ApiController
    {
        private ApplicationUserManager _userManager;

        public IdentityController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [Route(nameof(Register))]
        [HttpPost]
        public async Task<HttpResponseMessage> Register(RegisterRequestViewModel viewModel, HttpRequestMessage request = null)
        {
            HttpResponseMessage response = null;
            try
            {
                APPLICATION_USER appUser = new APPLICATION_USER();
                appUser.UserName = viewModel.UserName;
                appUser.Email = viewModel.Email;
                appUser.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(appUser, viewModel.Password);
                if (result.Succeeded)
                {
                    response = request.CreateResponse(HttpStatusCode.Created, viewModel);
                }
            }
            catch (Exception ex)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return response;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public HttpResponseMessage Login([FromBody] LoginRequestModel requestModel, HttpRequestMessage httpRequest = null)
        {
            string host = HttpContext.Current.Request.Url.OriginalString;
            string[] host1 = host.Split('/');
            string url = host1[0] + "//" + host1[2] + "/oauth/token";

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.POST);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            request.AddHeader("Accept", "application/x-www-form-urlencoded");
            request.AddParameter("username", requestModel.userName, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "password", ParameterType.GetOrPost);
            request.AddParameter("password", requestModel.password, ParameterType.GetOrPost);
            IRestResponse<TokenModel> response = client.Execute<TokenModel>(request);
            HttpResponseMessage responseData = null;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                responseData = httpRequest.CreateResponse(response.StatusCode, response.Content);
            }
            else
                responseData = httpRequest.CreateResponse(response.StatusCode, response.Data.access_token);
            return responseData;
        }

    }
}
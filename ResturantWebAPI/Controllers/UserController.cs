using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Resturant.Model;
using Resturant.Service.Implementation;

namespace ResturantWebAPI.Controllers
{ 
    [AllowAnonymous]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService service)
        {
            this.userService = service;
        }

        public UserController()
        {
            this.userService = new UserService();
        }

        [Route("create_user")]
        [HttpPost]
        public HttpResponseMessage CreateUser(UserDTO user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                userService.CreateUser(user);
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
            
        }

        [Route("validate_user")]
        [HttpPost]
        public HttpResponseMessage ValidateUser(UserDTO user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                userService.CreateUser(user);
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;

        }
    }
}

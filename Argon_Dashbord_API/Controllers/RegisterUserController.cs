using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Argon_Dashbord_API.Models;

namespace Argon_Dashbord_API.Controllers
{
    public class RegisterUserController : ApiController
    {
        public HttpResponseMessage Get()
        {
            EcommerceEntities ecommerceEntities = new EcommerceEntities();

            var allUsers = from user in ecommerceEntities.RegisterUsers.AsEnumerable()
                              select user;
            
            return Request.CreateResponse(HttpStatusCode.OK, allUsers);
        }

        public string Post(RegisterUser registerUser)
        {
            try
            {
                EcommerceEntities ecommerceEntities = new EcommerceEntities();
                ecommerceEntities.RegisterUsers.Add(registerUser);
                ecommerceEntities.SaveChanges();
                return "User added successfully.";
            }
            catch (Exception e)
            {
                return "User not added. - " + e.Message;
            }
        }
    }
}

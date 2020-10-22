using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Argon_Dashbord_API.Models;

namespace Argon_Dashbord_API.Controllers
{
    public class BergerPersonController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                DBMethods dBMethods = new DBMethods();
                IEnumerable<VW_Person> person = dBMethods.GetPersonDetails();
                return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while retriving the Person detail list. Method(BergerPersonController.Get()) - " + e.Message);
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                DBMethods dBMethods = new DBMethods();
                IEnumerable<VW_Person> person = dBMethods.GetPersonDetails(id);
                if (person.First() == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User Not found with the BusinessEntityID - " + id);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while retriving the Person detail. Method(BergerPersonController.Get(BusinessEntityID)) - " + e.Message);
            }
        }

        public string Post(VW_Person person)
        {
            try
            {
                DBMethods dBMethods = new DBMethods();
                person.BusinessEntityID = dBMethods.PostBusinessEntity(person.ContactTypeName);
                dBMethods.PostPerson(person);
                if(person.PhoneNumber != null)
                    dBMethods.PostPhoneNumber(person);
                if (person.EmailAddress != null)
                    dBMethods.PostEmailAddress(person);
                if (person.PasswordHash != null)
                    dBMethods.PostPassword(person);

                return "Business Entities added successfully with Business Entity ID - " + person.BusinessEntityID.ToString();
            }
            catch(Exception e)
            {
                return "Exception occured." + e.Message;
            }
        }

        public string Put(VW_Person person)
        {
            try
            {
                DBMethods dBMethods = new DBMethods();
                person.BusinessEntityID = dBMethods.PostBusinessEntity(person.ContactTypeName);
                dBMethods.PostPerson(person);
                dBMethods.PostPhoneNumber(person);
                dBMethods.PostEmailAddress(person);
                dBMethods.PostPassword(person);

                return "Business Entities added successfully with Business Entity ID - " + person.BusinessEntityID.ToString();
            }
            catch (Exception e)
            {
                return "Exception occured." + e.Message;
            }
        }

    }
}

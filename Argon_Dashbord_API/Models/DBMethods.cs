using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Argon_Dashbord_API.Models
{
    public class DBMethods
    {
        /// <summary>
        /// This method will return the all user in the system irrespective of the customer type.
        /// </summary>
        /// <returns>IEnumerable<VW_Person></returns>
        public IEnumerable<VW_Person> GetPersonDetails()
        {
            BergerEntities ecommerceEntities = new BergerEntities();

            var allUsers = from user in ecommerceEntities.VW_Person.AsEnumerable()
                            select user;

            return allUsers;
        }

        /// <summary>
        /// This method will return the user details based on Business Entity ID.
        /// </summary>
        /// <param name="BusinessEntityID">Business Entity ID</param>
        /// <returns>IEnumerable<VW_Person></returns>
        public IEnumerable<VW_Person> GetPersonDetails(int BusinessEntityID)
        {
            BergerEntities userEntities = new BergerEntities();

            IEnumerable<VW_Person>person = (IEnumerable<VW_Person>)(from user in userEntities.VW_Person.AsEnumerable()
                                                                    where user.BusinessEntityID.Equals(BusinessEntityID)
                                                                    select user);
            yield return person.FirstOrDefault();
        }

        public int GetContactTypes(string contactTypeName)
        {
            BergerEntities userEntities = new BergerEntities();

            int ContactTypeID = (from contactType in userEntities.ContactTypes
                                    where contactType.Name.Equals(contactTypeName)
                                    select contactType.ContactTypeID).First();
            return ContactTypeID;
        }

        public BusinessEntity GetBusinessEntity(int businessEntityID)
        {
            BergerEntities userEntities = new BergerEntities();

            BusinessEntity businessEntities = (from businessEntity in userEntities.BusinessEntities
                                                where businessEntity.BusinessEntityID.Equals(businessEntityID)
                                                select businessEntity).First();
            return businessEntities;
        }
        
        public int PostBusinessEntity(string contactTypeName)
        {
            BergerEntities userEntities = new BergerEntities();

            BusinessEntity businessEntity = new BusinessEntity();
            businessEntity.ContactTypeID = GetContactTypes(contactTypeName);
            businessEntity.ModifiedDate = DateTime.Today;
            businessEntity = userEntities.BusinessEntities.Add(businessEntity);
            
            userEntities.SaveChanges();
            
            return businessEntity.BusinessEntityID;
        }

        public int PostPerson(VW_Person person)
        {
            BergerEntities userEntities = new BergerEntities();

            userEntities.People.Add(GetPerson(person));
            userEntities.SaveChanges();

            return person.BusinessEntityID;
        }

        public int PostPhoneNumber(VW_Person person)
        {
            BergerEntities userEntities = new BergerEntities();

            userEntities.PersonPhones.Add(GetPersonPhone(person));

            userEntities.SaveChanges();

            return person.BusinessEntityID;
        }

        public int PostEmailAddress(VW_Person person)
        {
            BergerEntities userEntities = new BergerEntities();

            userEntities.EmailAddresses.Add(GetEmailAddress(person));

            userEntities.SaveChanges();

            return person.BusinessEntityID;
        }

        public int PostPassword(VW_Person person)
        {
            BergerEntities userEntities = new BergerEntities();

            userEntities.Passwords.Add(GetPassword(person));

            userEntities.SaveChanges();

            return person.BusinessEntityID;
        }

        public Person GetPerson(VW_Person person)
        {
            Person newPerson = new Person();
            newPerson.BusinessEntityID = person.BusinessEntityID;
            newPerson.PersonType = "";
            newPerson.Title = person.Title;
            newPerson.FirstName = person.FirstName;
            newPerson.MiddleName = person.MiddleName;
            newPerson.LastName = person.LastName;
            newPerson.Suffix = person.Suffix;
            newPerson.ModifiedDate = DateTime.ParseExact(person.ModifiedDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            return newPerson;
        }

        public PersonPhone GetPersonPhone(VW_Person person)
        {
            PersonPhone personPhone = new PersonPhone();
            personPhone.BusinessEntityID = person.BusinessEntityID;
            personPhone.PhoneNumber = person.PhoneNumber;
            personPhone.ModifiedDate = DateTime.ParseExact(person.ModifiedDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            return personPhone;
        }

        public EmailAddress GetEmailAddress(VW_Person person)
        {
            EmailAddress emailAddress = new EmailAddress();
            emailAddress.BusinessEntityID = person.BusinessEntityID;
            emailAddress.EmailAddress1 = person.EmailAddress;
            emailAddress.ModifiedDate = DateTime.ParseExact(person.ModifiedDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            return emailAddress;
        }

        public Password GetPassword(VW_Person person)
        {
            Password password = new Password();
            password.BusinessEntityID = person.BusinessEntityID;
            password.PasswordHash = person.PasswordHash;
            password.PasswordSalt = person.PasswordHash;
            password.ModifiedDate = DateTime.ParseExact(person.ModifiedDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            return password;
        }

    }
}
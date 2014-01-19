using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EZ.Data;
using EZ.Domain;

namespace EZ.Web.Controllers
{
    public class PersonAddressesController : ApiControllerBase
    {
        public PersonAddressesController(IUoW uow)
        {
            Uow = uow;
        }

        // GET api/personaddresses
        public IEnumerable<PersonAddress> Get()
        {
            return Uow.PersonAddresses.GetAll();
        }

        //GET: api/personaddress/?personId=1&addressid=1
        public PersonAddress Get(long personId, long addressId)
        {
            var personAddress = Uow.PersonAddresses.GetByIds(personId, addressId);
            if (personAddress != null) return personAddress;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(PersonAddress personAddress)
        {
            Uow.PersonAddresses.Add(personAddress);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, personAddress);

            //Compose location header that tells how to get this personAddress
            // e.g. ~/api/personaddress/?personId=1&addressId=3
            var queryString = string.Format("?personId={0}&addressId={1}", personAddress.PersonId, personAddress.AddressId);
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerOnly, null) + queryString);

            return response;
        }

        public HttpResponseMessage Put(PersonAddress personAddress)
        {
            Uow.PersonAddresses.Update(personAddress);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(long personId, long addressId)
        {
            Uow.PersonAddresses.Delete(personId, addressId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

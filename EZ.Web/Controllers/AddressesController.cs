using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EZ.Domain;
using EZ.Data;

namespace EZ.Web.Controllers
{
    public class AddressesController : ApiControllerBase
    {
        public AddressesController(IEzUow uow)
        {
            Uow = uow;
        }

        public IEnumerable<Address> Get()
        {
            return Uow.Addresses.GetAll();
        }

        public Address Get(long id)
        {
            var address = Uow.Addresses.GetById(id);
            if (address != null) return address;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(Address address)
        {
            Uow.Addresses.Add(address);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, address);

            //compose location header that tells how to get this address
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerAndId, new { id = address.AddressId }));

            return response;
        }

        public HttpResponseMessage Put(Address address)
        {
            Uow.Addresses.Update(address);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(long addressId)
        {
            Uow.Addresses.Delete(addressId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

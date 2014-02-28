using EZ.Data;
using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EZ.Web.Controllers
{
    public class EzPersonsController : ApiControllerBase
    {
        public EzPersonsController(IEzUow uow)
        {
            Uow = uow;
        }

        public IEnumerable<EzPerson> Get()
        {
            return Uow.EzPersons.GetAll();
        }

        //GET: api/ezPersons/?personId=1&eventid=1
        public EzPerson Get(long personId, long eventId)
        {
            var ezPersons = Uow.EzPersons.GetByIds(personId, eventId);
            if (ezPersons != null) return ezPersons;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(EzPerson ezPerson)
        {
            Uow.EzPersons.Add(ezPerson);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, ezPerson);

            //Compose location header that tells how to get this EzPersons
            // e.g. ~/api/EzPersons/?personId=1&eventId=3
            var queryString = string.Format("?personId={0}&eventId={1}", ezPerson.PersonId, ezPerson.EzId);
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerOnly, null) + queryString);

            return response;
        }

        public HttpResponseMessage Put(EzPerson ezPerson)
        {
            Uow.EzPersons.Update(ezPerson);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(long personId, long eventId)
        {
            Uow.EzPersons.Delete(personId, eventId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

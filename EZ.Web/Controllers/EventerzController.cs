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
    public class EventerzController : ApiControllerBase
    {
        public EventerzController(IUoW uow)
        {
            Uow = uow;
        }

        public IEnumerable<Eventer> Get()
        {
            return Uow.Eventers.GetAll();
        }

        //GET: api/eventerz/?personId=1&eventid=1
        public Eventer Get(long personId, long eventId)
        {
            var eventerz = Uow.Eventers.GetByIds(personId, eventId);
            if (eventerz != null) return eventerz;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(Eventer eventer)
        {
            Uow.Eventers.Add(eventer);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, eventer);

            //Compose location header that tells how to get this Eventers
            // e.g. ~/api/Eventers/?personId=1&eventId=3
            var queryString = string.Format("?personId={0}&eventId={1}", eventer.PersonId, eventer.EzId);
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerOnly, null) + queryString);

            return response;
        }

        public HttpResponseMessage Put(Eventer eventer)
        {
            Uow.Eventers.Update(eventer);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(long personId, long eventId)
        {
            Uow.Eventers.Delete(personId, eventId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

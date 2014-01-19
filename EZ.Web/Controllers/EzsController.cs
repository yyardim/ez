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
    public class EzsController : ApiControllerBase
    {
        public EzsController(IUoW uow)
        {
            Uow = uow;
        }

        // GET api/event
        public IEnumerable<Ez> Get()
        {
            return Uow.Ezs.GetAll().OrderBy(e => e.DateTime);
        }

        // GET api/event/5
        public Ez Get(long id)
        {
            var eventz = Uow.Ezs.GetById(id);
            if (eventz != null) return eventz;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // POST api/event
        public HttpResponseMessage Post(Ez eventz)
        {
            Uow.Ezs.Add(eventz);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, eventz);

            //compose location header that tells how to get this event
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerAndId, new { id = eventz.EzId }));

            return response;
        }

        // PUT api/event/5
        public HttpResponseMessage Put(Ez eventz)
        {
            Uow.Ezs.Update(eventz);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE api/event/5
        public HttpResponseMessage Delete(long eventId)
        {
            Uow.Ezs.Delete(eventId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

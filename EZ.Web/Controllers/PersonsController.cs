using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EZ.Domain;
using EZ.Data;

namespace EZ.Web.Controllers
{
    public class PersonsController : ApiControllerBase
    {

        public PersonsController(IUoW uow)
        {
            Uow = uow;
        }

        //GET /api/persons
        public IEnumerable<Person> Get()
        {
            return Uow.Persons.GetAll();
        }

        public Person Get(long id)
        {
            var person = Uow.Persons.GetById(id);
            if (person != null) return person;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
        
        public HttpResponseMessage Post(Person person)
        {
            Uow.Persons.Add(person);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, person);

            //Compose location header that tells how to get this eventer
            response.Headers.Location = new Uri(Url.Link(WebApiConfig.ControllerAndId, new { id = person.PersonId }));

            return response;
        }

        //Update and existing person
        // PUT /api/persons/
        public HttpResponseMessage Put(Person person)
        {
            Uow.Persons.Update(person);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(long personId)
        {
            Uow.Persons.Delete(personId);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

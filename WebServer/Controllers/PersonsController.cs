using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;
using WebServer.Model;
using Newtonsoft.Json;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonBase _PersonBase;

        public PersonsController(IPersonBase PersonBase)
        {
            _PersonBase = PersonBase;
        }

        [HttpGet]
        [Route("Cities")]
        public IEnumerable<City> GetCities()
        {
            return _PersonBase.GetCitiesList();
        }

        [HttpPost]
        [Route("Filter")]
        public IEnumerable<Person> GetFilterPersons([FromBody] Person person)
        {
            return _PersonBase.GetFilterPersonsList(person);
        }

        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return _PersonBase.GetPersonsList();
        }

        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            return _PersonBase.GetPerson(id);
        }

        [HttpPost]
        public void Create([FromBody] Person person)
        {
            _PersonBase.Insert(person);
        }

        [HttpPut]
        public void Edit([FromBody] Person person)
        {
            _PersonBase.Update(person);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _PersonBase.Delete(id);
        }
    }
}

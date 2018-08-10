using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Model;

namespace WebServer.Interfaces
{
    public interface IPersonBase
    {
        List<Person> GetFilterPersonsList(Person person);
        List<City> GetCitiesList();
        List<Person> GetPersonsList();
        Person GetPerson(int id);
        void Insert(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}

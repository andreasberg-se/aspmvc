using System.Collections.Generic;

namespace AspMvc.Models
{

    public interface IPersonRepository
    {
        Person GetPersonById(int personId);

        public Person Create(Person person);
        public List<Person> Read();
        public Person Read(int personId);
        public Person Update(Person person);
        public bool Delete(int personId);

    }

}

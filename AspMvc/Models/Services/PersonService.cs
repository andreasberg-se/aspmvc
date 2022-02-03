using System.Collections.Generic;
using AspMvc.Models.ViewModels;
using AspMvc.Models.Repositories;

namespace AspMvc.Models.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService()
        {
            _personRepository = new PersonRepository();
        }

        public Person Add(CreatePersonViewModel createPersonViewModel)
        {
            Person newPerson = new Person()
            {
                FirstName = createPersonViewModel.FirstName,
                LastName = createPersonViewModel.LastName,
                City = createPersonViewModel.City,
                Phone = createPersonViewModel.Phone
            };
            return _personRepository.Create(newPerson);
        }

        public Person GetById(int personId)
        {
            return _personRepository.Read(personId);
        }

        public List<Person> GetList()
        {
            return _personRepository.Read();
        }

        public List<Person> SearchOR(string searchString)
        {
            List<Person> peopleList = _personRepository.Read();
            List<Person> matches = new List<Person>();
            if (string.IsNullOrWhiteSpace(searchString))
                return peopleList;

            string[] words = searchString.Split(" ");

            foreach (var person in peopleList)
            {
                foreach(var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word.Trim()))
                        continue;
                    if ((person.FirstName.ToLower().Contains(word.Trim().ToLower())) 
                    || (person.LastName.ToLower().Contains(word.Trim().ToLower()))
                    || (person.City.ToLower().Contains(word.Trim().ToLower())))
                    {
                        matches.Add(person);
                        break;
                    }   
                }
            }        
            return matches;
        }

        public List<Person> SearchAND(string searchString)
        {
            List<Person> peopleList = _personRepository.Read();
            List<Person> matches = new List<Person>();
            if (string.IsNullOrWhiteSpace(searchString))
                return peopleList;

            string[] words = searchString.Split(" ");

            foreach (var person in peopleList)
            {
                int wordCount = 0;
                int matchCount = 0;
                foreach(var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word.Trim()))
                        continue;
                    wordCount++;
                    if ((person.FirstName.ToLower().Contains(word.Trim().ToLower())) 
                    || (person.LastName.ToLower().Contains(word.Trim().ToLower()))
                    || (person.City.ToLower().Contains(word.Trim().ToLower())))
                        matchCount++;   
                }
                if ((wordCount > 0) && (wordCount == matchCount))
                    matches.Add(person);
            }        
            return matches;
        }

        public bool Delete(int personId)
        {
            return _personRepository.Delete(personId);
        }
    }
}
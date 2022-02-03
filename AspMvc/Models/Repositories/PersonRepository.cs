using System.Collections.Generic;
using System.Linq;

namespace AspMvc.Models.Repositories
{

    public class PersonRepository : IPersonRepository
    {
        public IEnumerable<Person> AllPersons =>
            new List<Person>
            {
                new Person {PersonId = 1, FirstName = "Andreas", LastName = "Berg", Address = "Main Street 50", ZipCode = 12345, City = "Sim City", 
                    Country = "Sweden", Phone = "+46 1234-567 890", Email = "mail [at] domain.com",
                    ProgrammingLanguages = new List<string>() {"Assembler", "Basic", "C#", "Java", "Pascal", "Python"},
                    WebDevelopmentTools = new List<string>() {"CSS", "HTML", "JavaScript", "PHP"},
                    Databases = new List<string>() {"Access", "MySQL", "Oracle SQL"},
                    Projects = new List<Project>()
                    {
                        new Project("https://github.com/andreasberg-se/calculator", "Calculator", "A simple calculator, written in C#."),
                        new Project("https://github.com/andreasberg-se/consoleproject", "Console Project", "A collection of 16 functions."),
                        new Project("https://github.com/andreasberg-se/css", "CSS", "A simple website using CSS."),
                        new Project("https://github.com/andreasberg-se/hangman", "Hangman", "Guess the word."),
                        new Project("https://github.com/andreasberg-se/html", "HTML", "Website without styling."),
                        new Project("https://github.com/andreasberg-se/sokoban", "Sokoban", "Push the blocks to the destinaton. Written in JavaScript."),
                        new Project("https://github.com/andreasberg-se/vendingmachine", "Vending Machine", "Deposit money and purchase stuff from the machine.")
                    }
                }
            };

        public Person GetPersonById(int personId)
        {
            return AllPersons.FirstOrDefault(p => p.PersonId == personId);
        }

        private static int idCounter = 0;
        private static List<Person> peopleList = new List<Person>();

        public Person Create(Person person)
        {
            Person newPerson = new Person();
            newPerson = person;
            newPerson.PersonId = ++idCounter;
            peopleList.Add(newPerson);
            return newPerson;
        }

        public List<Person> Read()
        {
            return peopleList;
        }

        public Person Read(int personId)
        {
            return peopleList.FirstOrDefault(p => p.PersonId == personId);
        }

        public Person Update(Person person)
        {
            int id = person.PersonId;
            Person updatedPerson = Read(id);
            if (updatedPerson == null)
                return null;

            updatedPerson = person;
            updatedPerson.PersonId = id;
            return updatedPerson;
        }

        public bool Delete(int personId)
        {
            Person deletePerson = Read(personId);
            if (deletePerson == null)
                return false;

            return peopleList.Remove(deletePerson);
        }
    }

}

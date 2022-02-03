using System.Collections.Generic;
using AspMvc.Models.ViewModels;

namespace AspMvc.Models.Services
{
    public interface IPersonService
    {
        public Person Add(CreatePersonViewModel createPersonViewModel);
        public Person GetById(int personId);
        public List<Person> GetList();
        List<Person> SearchOR(string searchString);
        List<Person> SearchAND(string searchString);
        public bool Delete(int personId);
    }
}
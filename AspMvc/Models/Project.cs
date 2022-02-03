namespace AspMvc.Models
{

    public class Project
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project(string url, string name, string description)
        {
            this.Url = url;
            this.Name = name;
            this.Description = description;
        }
    }

}

using Newtonsoft.Json;

namespace Nuget.Quickstart
{
    public class Account
    {
        public string Name {get;set;}
        public string Email {get;set;}
        public DateTime DOB {get;set;}
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account
            {
                Name = "Homer Simpson",
                Email = "homer@thesimpsons.com",
                DOB = new DateTime(1980, 2, 20, 0, 0, 0, DateTimeKind.Utc)
            };

            string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
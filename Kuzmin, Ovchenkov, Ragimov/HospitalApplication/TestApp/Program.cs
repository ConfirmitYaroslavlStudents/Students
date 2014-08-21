using HospitalLib.Database;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = new Storage();
            temp.DatabasePath = @"\test\History\";
            var person = new Person();
            person.FirstName = "Alex8";
            person.SecondName = "Lonely";
            //temp.Save(null, person);
          //  var templateProvider = new TemplateProvider();
         //   temp.Search(person, templateProvider);

            var personProvider = new PersonProvider();
            personProvider.DatabasePath = @"\test\Persons\";
           // personProvider.Save(person);
            personProvider.Load();

        }
    }
}

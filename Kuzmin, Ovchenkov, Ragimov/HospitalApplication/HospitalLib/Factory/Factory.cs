using System;
using HospitalLib.Data;
using HospitalLib.Loader;
using HospitalLib.Providers;

namespace HospitalLib.Factory
{
    public static class Factory
    {
        public static Person BuidPerson(string fistName, string lastName, string middleName, DateTime birthDate)
        {
            var databaseProvider = new DatabaseProvider();
            var idProvider = new NewIdProvider(databaseProvider);
            var person = new Person(idProvider, fistName, lastName, middleName, birthDate);

            return person;
        }

        public static HtmlLoader BuildHtmlLoader()
        {
            var databaseProvider = new DatabaseProvider();
            var idProvider = new NewIdProvider(databaseProvider);
            var templateProvider = new TemplateProvider(databaseProvider);
            var htmlLoader = new HtmlLoader(templateProvider, idProvider);

            return htmlLoader;
        }
    }
}

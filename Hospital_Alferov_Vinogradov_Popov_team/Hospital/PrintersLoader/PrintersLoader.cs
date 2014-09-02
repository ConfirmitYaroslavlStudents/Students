using System;
using System.Reflection;
using Shared;
using Shared.Interfaces;


namespace PrintersLoaderLibrary
{
    public static class PrintersLoader
    {
        public static IPrinter LoadPrinter(string pathToAssembly)
        {
            IPrinter printer = null;

            Assembly printerAssembly = Assembly.LoadFrom(pathToAssembly);
            Type[] printerTypes = printerAssembly.GetTypes();

            foreach (var type in printerTypes)
            {
                var printerInterface = type.GetInterface("IPrinter");

                if (printerInterface != null)
                {
                    printer = printerAssembly.CreateInstance(type.FullName) as IPrinter;
                }
            }

            return printer;
        }
    }
}
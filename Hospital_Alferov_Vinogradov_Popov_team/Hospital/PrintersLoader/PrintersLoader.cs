using System;
using System.Reflection;
using Shared;

namespace PrintersLoader
{
    public static class PrintersLoaderLib
    {
        public static IPrinter LoadPrinter(string pathToAssembly)
        {
            IPrinter printer = null;

            Assembly printerAssembly = Assembly.LoadFrom(pathToAssembly);
            Type[] printerTypes = printerAssembly.GetTypes();

            foreach (Type type in printerTypes)
            {
                Type printerInterface = type.GetInterface("IPrinter");

                if (printerInterface != null)
                {
                    printer = printerAssembly.CreateInstance(type.FullName) as IPrinter;
                }
            }

            return printer;
        }
    }
}

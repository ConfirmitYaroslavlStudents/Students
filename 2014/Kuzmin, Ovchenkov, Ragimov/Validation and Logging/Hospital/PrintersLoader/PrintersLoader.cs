using System;
using System.Reflection;
using LogService;
using Shared;

namespace PrintersLoaderLibrary
{
    public static class PrintersLoader
    {
        public static Printer LoadPrinter(string pathToAssembly)
        {
            Printer printer = null;

            Assembly printerAssembly = Assembly.LoadFrom(pathToAssembly);
            Type[] printerTypes = printerAssembly.GetTypes();

            foreach (Type type in printerTypes)
            {
                Type printerAbstractType = type.BaseType;

                if (printerAbstractType != null && printerAbstractType.IsAbstract &&
                    printerAbstractType.Name == "Printer")
                {
                    printer = printerAssembly.CreateInstance(type.FullName) as Printer;
                }
            }

            //LOGGING
            // TRY/ CATCH ??
            Logger.Info("Printer was loaded");
            return printer;
        }
    }
}
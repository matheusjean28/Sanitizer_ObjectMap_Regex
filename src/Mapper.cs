using System;
using System.Reflection;
using sanetizerUserInput;
using ObjectToMapSrc;

namespace sanetizerSrc
{
    public class Mapper
    {
        private readonly InputSanitizer sanitizer;

        public Mapper(InputSanitizer _sanitizer)
        {
            sanitizer = _sanitizer;
        }

        public void IterateProperties<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(obj);

                Console.WriteLine("------------------------");
                Console.WriteLine("\nProperties Without Clean: \n");
                Console.WriteLine($"Property: {propertyName}, \nValue: {propertyValue} \n");

                Console.WriteLine("------------------------");
                Console.WriteLine("Start clean items\n");

                string propertyNameClean = sanitizer.SanitizeInput(propertyName);
                string propertyValueClean = sanitizer.SanitizeInput(propertyValue?.ToString() ?? string.Empty);

                Console.WriteLine($"Clean Property: {propertyNameClean}, \nClean Value: {propertyValueClean}");
            }
        }
    }
}

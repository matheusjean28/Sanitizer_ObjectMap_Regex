using System;
using System.Reflection;
using ObjectToMapSrc;
using sanetizerUserInput;

namespace sanetizerSrc
{
    public class Mapper
    {
        private readonly InputSanitizer sanitizer;

        public Mapper(InputSanitizer _sanitizer)
        {
            sanitizer = _sanitizer;
        }

        public T IterateProperties<T>(T obj, bool ActiveLogginObject = false)
            where T : new()
        {
            T _newObjectGenericClean = new T();
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();

            //return new clean value as an object with all input properties
            foreach (
                PropertyInfo property in type.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                )
            )
            {
                string propertyName = property.Name;
                object? propertyValue = property.GetValue(obj);
                string? propertyNameClean = sanitizer.SanitizeInput(propertyName);
                string? propertyValueClean = sanitizer.SanitizeInput(
                    propertyValue?.ToString() ?? string.Empty
                );
                //take value to be set at place of dirty field
                property.SetValue(
                    _newObjectGenericClean,
                    Convert.ChangeType(propertyValueClean, property.PropertyType)
                );

                //if value true, then loggin is enable
                if (ActiveLogginObject)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("\nProperties Without Clean: \n");
                    Console.WriteLine($"Property: {propertyName}, \nValue: {propertyValue} \n");
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Start clean items\n");
                    Console.WriteLine(
                        $"Clean Property: {propertyNameClean}, \nClean Value: {propertyValueClean}"
                    );
                }
            }
            return _newObjectGenericClean;
        }
    }
}

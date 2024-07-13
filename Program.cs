using System;
using sanetizerUserInput;
using sanetizerSrc;
using ObjectToMapSrc;

class Program
{
    static void Main(string[] args)
    {
        var sanitizer = new InputSanitizer();
        var mapper = new Mapper(sanitizer);

        var testObjects = new[]
        {
            new ObjectToMap
            {
                Name = "John Doe <script>alert('xss')</script>",
                Age = 30,
                Email = "john.doe@example.com<script>"
            },
            new ObjectToMap
            {
                Name = "Jane #$@! Doe",
                Age = 25,
                Email = "jane.doe@exam$ple.com"
            },
            new ObjectToMap
            {
                Name = "Robert'); DROP TABLE Students;--",
                Age = 22,
                Email = "robert'--@example.com"
            },
            new ObjectToMap
            {
                Name = "Alice & Bob",
                Age = 28,
                Email = "alice&bob@example.com"
            },
            new ObjectToMap
            {
                Name = "Charlie 😊👍",
                Age = 35,
                Email = "charlie😊@example.com"
            },
            new ObjectToMap
            {
                Name = "Eve <img src='x' onerror='alert(1)'>",
                Age = 29,
                Email = "eve@example.com<img src='x' onerror='alert(1)'>"
            }
        };

        foreach (var person in testObjects)
        {
            Console.WriteLine("----- Testing new object -----");
            mapper.IterateProperties(person, true);
            Console.WriteLine(testObjects);
        }
    }
}

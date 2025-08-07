using System.Reflection;

namespace Specialist_Lab_4_1_TypeExplorer;

internal class Program
{
    private static void Main(string[] args)
    {
        Assembly typeExplorer = Assembly.LoadFrom("Specialist_Lab_4_1_ClassLib.dll");

        Console.WriteLine($"Загружена сборка {typeExplorer.FullName}");

        Type[] types = typeExplorer.GetTypes();

        Console.WriteLine("Сборка включает следующие доступные типы:");

        foreach (Type type in types)
        {

            Console.WriteLine($"\t{type}");
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine($"\tТип включает в себя следующие доступные свойства:");
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"\t\t- {property.Name} типа {property.PropertyType}");
            }
            Console.WriteLine();
        }
    }
}
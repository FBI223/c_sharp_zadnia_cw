

using System.Reflection;

namespace zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // 1 zadanie
            Console.WriteLine("Welcome to C# Reflection");
            Type customerType = typeof(Customer);
            

            DisplayFields(customerType);
            DisplayProperties(customerType);
            DisplayNestedTypes(customerType);
            DisplayMethods(customerType);
            DisplayConstructors(customerType);
            DisplayMembers(customerType);
        
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            
            // 2 zadanie 
            
            PropertyInfo[]? propertyInfos = customerType?.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            ConstructorInfo? customerConstructor = customerType?.GetConstructor(new Type[] {typeof(string) });
            object? customerObjectRefl = customerConstructor?.Invoke(new object[] { "John Doe" });

            propertyInfos?[0].SetValue(customerObjectRefl, "Na zjezdzie");
            propertyInfos?[1].SetValue(customerObjectRefl, 123 );
            FieldInfo? fieldInfo = customerType.GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fieldInfo?.SetValue(customerObjectRefl, "Jan Dan");

            
            object? v0 = propertyInfos?[0].GetValue(customerObjectRefl);
            object? v1 = propertyInfos?[1].GetValue(customerObjectRefl);
            object? v2 = propertyInfos?[2].GetValue(customerObjectRefl);
            Console.WriteLine(v0);
            Console.WriteLine(v1);
            Console.WriteLine(v2);
            
        }
        
        
        static void DisplayFields(Type? type)
        {
            Console.WriteLine("\nFields:");

            // Public fields
            Console.WriteLine("-- Public:");
            var publicFields = type?.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in publicFields)
            {
                Console.WriteLine($"Type: {field.FieldType.Name}; Name: {field.Name}");
            }

            // Non-public fields
            Console.WriteLine("-- Non Public:");
            var nonPublicFields = type?.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var field in nonPublicFields)
            {
                Console.WriteLine($"Type: {field.FieldType.Name}; Name: {field.Name}");
            }
        }

        static void DisplayProperties(Type? type)
        {
            Console.WriteLine("\nProperties:");
            var properties = type?.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                Console.WriteLine($"Type: {property.PropertyType.Name}; Name: {property.Name}");
            }
        }

        static void DisplayNestedTypes(Type? type)
        {
            Console.WriteLine("\nNested types:");
            var nestedTypes = type?.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var nestedType in nestedTypes)
            {
                Console.WriteLine($"Name: {nestedType.Name}; Base Type: {nestedType.BaseType?.Name}");
            }
        }

        static void DisplayMethods(Type? type)
        {
            Console.WriteLine("\nMethods:");
            var methods = type?.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                string parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                Console.WriteLine($"Name: {method.Name}; Return Type: {method.ReturnType.Name}; Params: [{parameters}]");
            }
        }

        
        static void DisplayConstructors(Type? type)
        {
            Console.WriteLine("\nConstructors:");
            var constructors = type?.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var constructor in constructors)
            {
                string parameters = string.Join(", ", constructor.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                Console.WriteLine($"Name: {constructor.Name}; Parameters: {parameters}");
            }
        }
        static void DisplayMembers(Type? type)
        {
            Console.WriteLine("\nMembers:");
            var members = type?.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var member in members)
            {
                Console.WriteLine($"Name: {member.Name}; Member Type: {member.MemberType}");
            }
        }
        
        
    }

    
    
    
    public class Customer
    {
        
        //fields
        private string _name;
        protected int _age;
        public bool isPreferred;
        
        //properties 
        public string Address { get; set; }
        public int SomeValue { get; set; }
        public string Name { get { return _name; } }

        //constructor
        public Customer(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Customer name!");
            _name = name;
        }
        
          
        // nested enum
        public enum SomeEnumeration
        {
            ValueOne = 1
            , ValueTwo = 2
        }
        //nested class
        public class SomeNestedClass
        {
            private string _someString;
        }
        
        //methods
        public int ImportantCalculation()
        {
            return 1000;
        }
        public void ImportantVoidMethod()
        {
        }
        
    }

}

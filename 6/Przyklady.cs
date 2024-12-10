

using System.Reflection;


namespace Foo
{
    public class Foo
    {
        public void hello()
        {
            Console.WriteLine("Hello World!");
        }
    }

}


namespace Refleksja
{

    using Foo;
    
    class Program
    {
        
        /*
         
        static void Main(string[] args)
        {
            System.Reflection.MemberInfo info = typeof(MyClassToGetAttributeInfo);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                Console.WriteLine(attributes[i]);
                ExampleAttribute ea = (ExampleAttribute)attributes[i];
                Console.WriteLine("Info: {0}  , topic :{1}", ea.message, ea.topic);
            }

            
            
            
            Foo foo = new Foo();
            foo.hello();
            
            var type = Assembly.GetExecutingAssembly().GetType("Foo.Foo"); // Ładowanie typu z assembly
            var fooRef = Activator.CreateInstance(type); // Inicjalizacja obiektu

            MethodInfo inf = type.GetMethod("hello");
            inf.Invoke(fooRef, null); // Wywołanie metody bez parametrów
            Console.ReadKey();
            
        }
    
        */
        
        
        
        [AttributeUsage(AttributeTargets.All)]
        public class ExampleAttribute : Attribute
        {
            public readonly string message;
            public string topic;

            public ExampleAttribute(string Message, string Topic)
            {
                this.message = Message;
                this.topic = Topic;
            }

            public string Topic
            {
                get { return topic; }
                set { topic = value; }
            }
        }


        [ExampleAttribute("Informacja o mojej klasie", "Informacja o moim topic")]
        class MyClassToGetAttributeInfo
        {
        }


        
        
        
        
    }
    
    
}

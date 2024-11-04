

class Person
{
    public string name;
    public string lastName;

    public Person(string name, string lastName)
    {
        this.name = name;
        this.lastName = lastName;
    }
}



class Program
{

    static void Method1(Person person)
    {
        person.name = person.name.ToUpper();
        person.lastName = person.lastName.ToUpper();
    }
    
    static Person Method2(Person person)
    {
        Person newPerson = new Person(person.name.ToLower(), person.lastName.ToLower());
        return newPerson;
    }
    
    static void Method22(Person person)
    {
        Person newPerson = new Person(person.name.ToLower(), person.lastName.ToLower());
        person = newPerson;
    }
    
    static void Method3(Person person)
    {
        person = null ; 
    }
    
    
    
    public static void Main(string[] args)
    {
        Person p1 = new Person("John", "Smith");
        
        
        
        Console.WriteLine("Przed pierwszą metodą: " + p1.name + " " + p1.lastName);
        Method1(p1);
        Console.WriteLine("Po pierwszej metodzie: " + p1.name + " " + p1.lastName);

        //p1 = Method2(p1);
        //Console.WriteLine("Po drugiej metodzie: " + p1.name + " " + p1.lastName);
        Method22(p1);
        Console.WriteLine("Po drugiej drugiej metodzie: " + p1.name + " " + p1.lastName);


        Method3(p1);
        Console.WriteLine("Po trzeciej metodzie: " + p1.name + " " + p1.lastName);
        
    }
}
    

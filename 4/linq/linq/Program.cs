namespace linq;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine();
        
        //ZADANIE NR 1 
        
        int n = 100;
        IEnumerable<int> query1 =  Enumerable.Range(1, n).Where(n => n != 5 && n != 9 && ( n % 7 == 0 || n % 2 == 1 )  ).Select(x => x*x);
        var naturalQuadraticList = query1.ToList();
        naturalQuadraticList.ForEach(x => Console.WriteLine($"Liczba: {Math.Sqrt(x)}, Kwadrat: {x}"));


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        
        
        var sumof = naturalQuadraticList.Sum();
        Console.WriteLine("Sum of collection " + sumof );
        Console.WriteLine();
        
        var countof = naturalQuadraticList.Count();
        Console.WriteLine("count of collection " + countof );
        Console.WriteLine();
        
        var firstelement = naturalQuadraticList.First();
        Console.WriteLine("First el " + firstelement );
        Console.WriteLine();
        
        var lastelement = naturalQuadraticList.Last();
        Console.WriteLine("Last el " + lastelement );
        Console.WriteLine();

        
        var third = naturalQuadraticList.Skip(2).First();
        Console.WriteLine("Third el " + third);

        
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine();
        
        
        
        //ZADANIE NR 2 
        
        // Wczytywanie N i M
        //Console.WriteLine("Podaj liczbę wierszy (N):");
        //int N = int.Parse(Console.ReadLine());
        int N = 8;
        //Console.WriteLine("Podaj liczbę kolumn (M):");
        //int M = int.Parse(Console.ReadLine());
        int M = 6;
        Random random = new Random();

        // Tworzenie listy list za pomocą LINQ
        List<List<int>> listaList = Enumerable
            .Range(0, N) 
            .Select(_ => Enumerable
                .Range(0, M)
                .Select(_ => random.Next(1, 101))
                .ToList())
            .ToList(); 

        
        Console.WriteLine("Wygenerowana lista:");
        listaList.ForEach(wiersz => Console.WriteLine(string.Join(" ", wiersz)) );
        
        
        Console.WriteLine("");
        Console.WriteLine("Suma wszystkich elementow");
        var suma1 = listaList.SelectMany(x=> x).Sum();
        var suma2 = listaList.Select(lista => lista.Sum()).Sum();
        
        Console.WriteLine("suma 1 : " + suma1);
        Console.WriteLine("suma 2 : " + suma2);
        
        
        
        
        
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine();
        
        
        
        //ZADANIE NR 3 
        Dictionary<char, String> dict = new Dictionary<char, string>();
        List<String> listaMiast = new List<String>();
        List<char> listaLiter = new List<char>();
        string? linia = "Y";
        
        listaMiast.Add("Paryz".ToUpper());
        listaMiast.Add("Praga".ToUpper());
        listaMiast.Add("Krakow".ToUpper());
        listaMiast.Add("Pcim".ToUpper());
        listaMiast.Add("Plock".ToUpper());
        

        while (linia != "X" && linia != "x" && linia != "\r" && linia != "\n" && linia != "" )
        {
            Console.WriteLine("Podaj miasto: ");
            linia = Console.ReadLine();

            if ( linia != null && linia.Length > 1 && !Char.IsNumber(linia[0]) && Char.IsAscii(linia[0]) && ( !listaMiast.Contains(linia.ToUpper()) ) )
            {
                if (!listaMiast.Contains(linia.ToUpper()))
                {
                    listaMiast.Add(linia.ToUpper());
                }
            }
        }
        
        
        var miastaSlownik = listaMiast
            .GroupBy(miasto => miasto[0])
            .ToDictionary(grupa => grupa.Key, grupa => grupa.OrderBy(x => x).ToList()); 

        

        
        linia = "X";
        while ( linia != "" && linia != "\r" && linia != "\n" )
        {
            Console.WriteLine("Podaj litere: ");
            linia = Console.ReadLine();
            char litera = 'Y';
            if (linia != null && linia.Length >= 1)
            {
                linia = linia.ToUpper();
                litera = linia[0];
            }

            if (linia != null && linia.Length == 1 && !Char.IsNumber(linia[0]) && Char.IsAscii(linia[0]))
            {
                if (miastaSlownik.ContainsKey(litera))
                {
                    Console.WriteLine( "klucz : "  +  litera);
                    Console.WriteLine("Wartości: " + string.Join(", ",  miastaSlownik[litera]));
                    Console.WriteLine(  );
                }
                else
                {
                    Console.WriteLine("PUSTE");
                    Console.WriteLine(  );
                }

            }

        }
        
        
        
        Console.WriteLine("KONIEC");
        
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine();



    }
    
    
}
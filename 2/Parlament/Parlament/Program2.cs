using System;
using System.Collections.Generic;

public class Glosowanie
{
    public string Temat { get; }
    public int GlosyZa { get; private set; }
    public int GlosyPrzeciw { get; private set; }

    public Glosowanie(string temat)
    {
        Temat = temat;
    }

    public void OddajGlos(bool za)
    {
        if (za)
        {
            GlosyZa++;
        }
        else
        {
            GlosyPrzeciw++;
        }
    }

    public void Wyniki()
    {
        Console.WriteLine($"Głosowanie nad: {Temat}");
        Console.WriteLine($"Głosów za: {GlosyZa}, Głosów przeciw: {GlosyPrzeciw}");
    }
}

public class Parlamentarzysta
{
    public string Imie { get; }
    
    public Parlamentarzysta(string imie)
    {
        Imie = imie;
    }

    public void OnPoczatekObrad(object sender, EventArgs e)
    {
        Console.WriteLine($"{Imie} jest gotowy do głosowania.");
    }

    public void OnKoniecObrad(object sender, EventArgs e)
    {
        Console.WriteLine($"{Imie} zakończył głosowanie.");
    }

    public void OddajGlos(Glosowanie glosowanie, Random rand)
    {
        bool za = rand.Next(0, 2) == 0; // Losowy głos: za lub przeciw
        glosowanie.OddajGlos(za);
        Console.WriteLine($"{Imie} oddał głos {(za ? "za" : "przeciw")}.");
    }
}

public class Parlament
{
    public event EventHandler PoczatekObrad; // Zdarzenie rozpoczęcia obrad
    public event EventHandler KoniecObrad;   // Zdarzenie zakończenia obrad

    private List<Parlamentarzysta> parlamentarzysci;
    private Glosowanie aktualneGlosowanie;

    public Parlament(List<Parlamentarzysta> parlamentarzysci)
    {
        this.parlamentarzysci = parlamentarzysci;
    }

    public void RozpocznijGlosowanie(string temat)
    {
        aktualneGlosowanie = new Glosowanie(temat);

        Console.WriteLine($"Rozpoczęto głosowanie nad: {temat}");
        PoczatekObrad?.Invoke(this, EventArgs.Empty); // Zgłoszenie zdarzenia rozpoczęcia obrad

        // Parlamentarzyści głosują
        Random rand = new Random();
        foreach (var parlamentarzysta in parlamentarzysci)
        {
            parlamentarzysta.OddajGlos(aktualneGlosowanie, rand);
        }

        ZakonczGlosowanie();
    }

    public void ZakonczGlosowanie()
    {
        Console.WriteLine("Zakończono obrady.");
        KoniecObrad?.Invoke(this, EventArgs.Empty); // Zgłoszenie zdarzenia zakończenia obrad
        aktualneGlosowanie.Wyniki(); // Wyświetlenie wyników głosowania
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Tworzymy listę parlamentarzystów
        List<Parlamentarzysta> parlamentarzysci = new List<Parlamentarzysta>
        {
            new Parlamentarzysta("Jan Kowalski"),
            new Parlamentarzysta("Anna Nowak"),
            new Parlamentarzysta("Piotr Wiśniewski")
        };

        // Tworzymy obiekt Parlamentu
        Parlament parlament = new Parlament(parlamentarzysci);

        // Rejestracja parlamentarzystów do nasłuchiwania na zdarzenia
        foreach (var parlamentarzysta in parlamentarzysci)
        {
            parlament.PoczatekObrad += parlamentarzysta.OnPoczatekObrad;
            parlament.KoniecObrad += parlamentarzysta.OnKoniecObrad;
        }

        // Rozpoczęcie głosowania
        parlament.RozpocznijGlosowanie("Nowa ustawa o edukacji");

        Console.ReadKey();
    }
}
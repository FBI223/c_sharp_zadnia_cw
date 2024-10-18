//Marcin Sztukowski

using System.Text.RegularExpressions;


public class NonPlayerCharacter 
{
    public string Name { get; set; }
    
}

public enum EHeroClass
{
    None = -1,    // Explicitly set None to an invalid class value.
    Barbarian = 0,  // Set Barbarian to 0
    Sorcerer = 1,   // Set Sorcerer to 1
    Rouge = 2       // Set Rouge to 2
}

public class Hero
{
    public string name;
    public EHeroClass klass;
    public int Gold { get; set; }
    public int Experience { get; set; }

    public Hero(string nazwa, EHeroClass klassa)
    {
        this.name = nazwa;
        this.klass = klassa;
        this.Gold = 0; // Złoto startowe
        this.Experience = 0; // Początkowe doświadczenie
    }
    
    
    
    public void GainExperience(int amount)
    {
        this.Experience += amount;
        Console.WriteLine($"Zyskałeś {amount} punktów doświadczenia! Obecne doświadczenie: {this.Experience}");
    }
    
    
}
public class Mission
{
    public string Name { get; set; }
    public bool IsCompleted { get; private set; } // Initially private setter

    public Mission(string name)
    {
        Name = name;
        IsCompleted = false; // When mission is created, it's not completed
    }

    // Public method to mark the mission as completed
    public void Complete()
    {
        IsCompleted = true;
    }
}


public class Region
{
    public string name;
    public int ile_locations;
    private List<Location> locations = new List<Location>();

    public void AddLocation(Location location)
    {
        locations.Add(location);
        ile_locations++;
    }

    public void RemoveLocation(Location location)
    {
        locations.Remove(location);
        ile_locations--;
    }

    public List<Location> Locations => locations; // Dodajemy getter, aby uzyskać dostęp do listy lokacji
}
public class Location
{
    public string name;
    public int ile_npc;
    
    // Lista przechowująca NPC-ów
    public List<NonPlayerCharacter> npcs = new List<NonPlayerCharacter>();

    // Getter, który udostępnia listę NPC-ów publicznie
    public List<NonPlayerCharacter> Npcs => npcs;

    // Dodanie NPC do lokalizacji
    public void AddNpc(NonPlayerCharacter npc)
    {
        npcs.Add(npc);
        ile_npc++;
    }

    public void RemoveNpc(NonPlayerCharacter npc)
    {
        npcs.Remove(npc);
        ile_npc--;
    }
}
public class LocationManager
{
    public static Region InitializeSanctuary(Hero hero)
    {
        Region sanctuary = new Region { name = "Khanduras" };

        // Tworzymy lokalizacje Tristram i Piekło
        Location tristram = new Location { name = "Tristram" };
        Location hell = new Location { name = "Piekło" };

        // Tworzymy NPC do Tristram i Piekło
        NonPlayerCharacter tyrael = new NonPlayerCharacter { Name = "Archanioł Tyrael" };
        NonPlayerCharacter gabriel = new NonPlayerCharacter { Name = "Archanioł Gabriel" }; // Nowy NPC
        NonPlayerCharacter baal = new NonPlayerCharacter { Name = "Baal" };
        NonPlayerCharacter mephisto = new NonPlayerCharacter { Name = "Mephisto" }; // Nowy NPC

        // Dodajemy NPC do lokalizacji
        tristram.AddNpc(tyrael);
        tristram.AddNpc(gabriel); // Dodajemy Gabriela w Tristram
        hell.AddNpc(baal);
        hell.AddNpc(mephisto); // Dodajemy Mephisto w Piekle

        // Dodajemy lokalizacje do regionu
        sanctuary.AddLocation(tristram);
        sanctuary.AddLocation(hell);

        return sanctuary;
    }
    
    
    
    
}
public class DialogManager
{

    /*
    public static void StartFightWithMephisto(Hero hero, NonPlayerCharacter mephisto, Location location)
    {
        Console.Clear();
        Console.WriteLine("Rozpoczęła się walka z Mephisto...");

        // Bohater traci doświadczenie


        if (hero.Experience >= 10000)
        {
            
        } else if (hero.Experience >= 1000)
        {
            hero.GainExperience(-500); // Tracisz 500 punktów doświadczenia

            // Mephisto pozostaje w lokacji, ponieważ bohater przegrywa
            Console.WriteLine("Przegrałeś walkę z Mephisto i straciłeś 500 punktów doświadczenia.");
            Console.ReadKey();
        }
        else
        {
            
            // Mephisto pozostaje w lokacji, ponieważ bohater przegrywa
            Console.WriteLine("Musisz uciekac bo masz za malo exp! Wroc w pozniejszym etapie gry!");
            Console.ReadKey();
        }

    }
    public static void StartFightWithBaal(Hero hero, NonPlayerCharacter baal, Location location)
    {
        Console.Clear();
        Console.WriteLine("Rozpoczęła się walka z Baalem...");

        // Hero gains experience after the fight
        hero.GainExperience(1000);

        // Remove Baal from the location after the fight
        location.RemoveNpc(baal);

        // Complete the mission
        hero.CompleteMission("Pokonaj Baala");

        Console.WriteLine("Zabiłeś Baala! Zyskałeś 1000 punktów doświadczenia.");
        Console.ReadKey();
    }
    
    */
    
public static DialogNode InitializeBaalDialog()
{
    DialogNode startNode = new DialogNode(new NpcDialogPart("Ośmieliłeś się stanąć przede mną, #HERONAME#? Przygotuj się na zniszczenie!"));
    

    // Opcja wyzwania do walki
    DialogNode challengeBaal = new DialogNode(new NpcDialogPart("Zobaczymy, jak długo wytrzymasz w walce przeciwko mnie."));

    // Ucieczka przed Baalem
    DialogNode escapeBaal = new DialogNode(new NpcDialogPart("Uciekasz? Słaby człowieku, nigdy nie będziesz godzien walki ze mną."));

    // Pytanie o Baala
    DialogNode askAboutBaal = new DialogNode(new NpcDialogPart("Jestem Baal, Pan Zniszczenia. Jednym z Trzech Pradawnych. To ja, wraz z moimi braćmi, sieję chaos na Ziemi."));
    
    // Rozwinięcie pytania o Baala
    DialogNode askAboutDestruction = new DialogNode(new NpcDialogPart("Destrukcja to prawdziwa natura wszechświata. Tylko ci, którzy zrozumieją chaos, mogą przetrwać."));
    askAboutBaal.AddResponse(new HeroDialogPart("Dlaczego siejesz zniszczenie?"), askAboutDestruction);
    
    // Pytanie o braci Baala (Diablo i Mephisto)
    DialogNode askAboutBrothers = new DialogNode(new NpcDialogPart("Moi bracia, Diablo i Mephisto, każdy ma swoją domenę. Razem dążymy do zapanowania nad światem."));
    askAboutBaal.AddResponse(new HeroDialogPart("Opowiedz mi o swoich braciach."), askAboutBrothers);
    
    askAboutBrothers.AddResponse(new HeroDialogPart("Wracam do pytan."), askAboutBaal);

    // Powrót do głównego pytania
    askAboutBaal.AddResponse(new HeroDialogPart("Wracam do pytania glownego."), startNode);

    askAboutDestruction.AddResponse(new HeroDialogPart("Wracam do pytan") , askAboutBaal );
    
    // Główne opcje dialogowe
    
    startNode.AddResponse(new HeroDialogPart("Zamierzam cię pokonać!"), challengeBaal);
    startNode.AddResponse(new HeroDialogPart("Muszę się wycofać."), escapeBaal);
    startNode.AddResponse(new HeroDialogPart("Kim jesteś, Baalu?"), askAboutBaal);
    startNode.AddResponse(new HeroDialogPart("Opuść dialog."), new DialogNode(new NpcDialogPart("Powracasz do lokacji...")));
    
    
    return startNode;
}
    
    
public static DialogNode InitializeMephistoDialog()
{
    DialogNode startNode = new DialogNode(new NpcDialogPart("Ah, #HERONAME#, przyszedłeś, by stawić czoła Panom Piekła? Jesteś gotów na swoją zgubę?"));

    // Opcja wyzwania do walki
    DialogNode challengeMephisto = new DialogNode(new NpcDialogPart("Bardzo dobrze! Zginiesz z ręki Pana Nienawiści, a twoja dusza będzie cierpieć przez wieczność."));


    // Opcja negocjacji z Mephisto
    DialogNode parleyMephisto = new DialogNode(new NpcDialogPart("Wielkie słowa, śmiertelniku. Jednakże, może byś się przyłączył do mojej władzy, zamiast ginąć?"));

    // Pytanie o Mephisto
    DialogNode askAboutMephisto = new DialogNode(new NpcDialogPart("Jestem Mephisto, Pan Nienawiści. Moja władza rozciąga się nad całym Piekłem."));
    
    // Rozwinięcie pytania o nienawiść
    DialogNode askAboutHatred = new DialogNode(new NpcDialogPart("Nienawiść jest siłą, która napędza ludzi do zniszczenia. To w niej tkwi prawdziwa moc."));
    askAboutMephisto.AddResponse(new HeroDialogPart("Dlaczego nienawiść?"), askAboutHatred);

    // Pytanie o plany Mephisto
    DialogNode askAboutPlans = new DialogNode(new NpcDialogPart("Mój plan to przejęcie władzy nad światem i zniszczenie każdej nadziei, którą ludzkość kiedykolwiek miała."));
    askAboutHatred.AddResponse(new HeroDialogPart("Jaki jest twój plan?"), askAboutPlans);

    askAboutPlans.AddResponse(new HeroDialogPart("Wracam do pytania głównego."), startNode);

    // Powrót do głównego pytania
    askAboutMephisto.AddResponse(new HeroDialogPart("Wracam do pytania głównego."), startNode);

    // Główne opcje dialogowe
    startNode.AddResponse(new HeroDialogPart("Pokonam cię, Mephisto!"), challengeMephisto);
    startNode.AddResponse(new HeroDialogPart("Nie musimy walczyć, możemy znaleźć inne wyjście."), parleyMephisto);
    startNode.AddResponse(new HeroDialogPart("Kim jesteś, Mephisto?"), askAboutMephisto);
    startNode.AddResponse(new HeroDialogPart("Opuść dialog."), new DialogNode(new NpcDialogPart("Powracasz do lokacji...")));

    return startNode;
}

public static DialogNode InitializeTyraelDialog()
{
    DialogNode startNode = new DialogNode(new NpcDialogPart("Witaj, #HERONAME#, jestem Archanioł Tyrael. Czy chcesz mi pomóc ocalić Tristram, pokonując Baala?"));

    DialogNode agreeHelp = new DialogNode(new NpcDialogPart("Dziękuję! Twoja pomoc będzie nieoceniona."));

    DialogNode refuseHelp = new DialogNode(new NpcDialogPart("Zrozumiałem, wróć, gdy będziesz gotowy na ostateczne starcie."));

    // Menu informacji o Tyraelu oraz aniołach
    DialogNode infoNode = new DialogNode(new NpcDialogPart("Jakie masz pytania o aniołach, Baalu lub mnie?"));

    DialogNode askAboutTyrael = new DialogNode(new NpcDialogPart("Jestem Tyrael, Archanioł Sprawiedliwości. Strzegę ludzkości od wieków, walcząc o zachowanie równowagi między światłem a ciemnością."));
    DialogNode askAboutAngels = new DialogNode(new NpcDialogPart("Aniołowie są obrońcami światła. Naszym zadaniem jest utrzymanie równowagi we wszechświecie i ochrona świata ludzi przed siłami ciemności."));
    DialogNode askAboutBaal = new DialogNode(new NpcDialogPart("Baal, Pan Zniszczenia, to jeden z najpotężniejszych demonów, który pragnie zniszczyć wszystko, co stworzone. Jego zniszczenie to jedyny sposób na ocalenie świata."));
    DialogNode askAboutMephisto = new DialogNode(new NpcDialogPart("Mephisto, Pan Nienawiści, to najstarszy z Trzech Pradawnych. Jest odpowiedzialny za rozprzestrzenianie nienawiści w sercach ludzi i demonów."));    

    // Dodanie opcji powrotu do głównego menu
    askAboutTyrael.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);
    askAboutAngels.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);
    askAboutBaal.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);
    askAboutMephisto.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);

    // Dodanie pytań w menu informacyjnym
    infoNode.AddResponse(new HeroDialogPart("Kim jesteś, Tyraelu?"), askAboutTyrael);
    infoNode.AddResponse(new HeroDialogPart("Opowiedz mi o aniołach."), askAboutAngels);
    infoNode.AddResponse(new HeroDialogPart("Kim jest Baal?"), askAboutBaal);
    infoNode.AddResponse(new HeroDialogPart("Kim jest Mephisto?"), askAboutMephisto);
    infoNode.AddResponse(new HeroDialogPart("Powrót do głównego dialogu."), startNode);

    // Standardowe odpowiedzi w dialogu
    startNode.AddResponse(new HeroDialogPart("Tak, pomogę."), agreeHelp);
    startNode.AddResponse(new HeroDialogPart("Nie, jestem zajęty."), refuseHelp);
    startNode.AddResponse(new HeroDialogPart("Pytania o informacje."), infoNode);  // Opcja pytania o informacje
    startNode.AddResponse(new HeroDialogPart("Opuść dialog."), new DialogNode(new NpcDialogPart("Powracasz do lokacji...")));

    return startNode;
}



public static DialogNode InitializeGabrielDialog()
{
    // Główne pytanie o misję Mephisto
    DialogNode startNode = new DialogNode(new NpcDialogPart("Witaj, #HERONAME#. Potrzebuję twojej pomocy w walce z Mephisto. Czy podejmiesz się tej misji?"));

    // Dodanie misji po zaakceptowaniu
    DialogNode acceptMission = new DialogNode(new NpcDialogPart("Doskonale! Twoja odwaga zostanie wynagrodzona."));


    // Odrzucenie misji
    DialogNode refuseMission = new DialogNode(new NpcDialogPart("Rozumiem, to nie jest łatwe zadanie. Powróć, gdy będziesz gotowy."));

    // Szczegóły misji
    DialogNode askDetails = new DialogNode(new NpcDialogPart("Musisz udać się do Piekła i pokonać Mephisto."));
    askDetails.AddResponse(new HeroDialogPart("Zrozumiałem. Wyruszam na misję."), acceptMission);
    askDetails.AddResponse(new HeroDialogPart("Nie, to zbyt niebezpieczne."), refuseMission);

    // Menu informacji o Gabriel oraz aniołach
    DialogNode infoNode = new DialogNode(new NpcDialogPart("Jakie masz pytania o aniołach lub mnie?"));

    DialogNode askAboutGabriel = new DialogNode(new NpcDialogPart("Jestem Gabriel, Archanioł Światła. Moim zadaniem jest ochrona ludzkości przed zagładą."));
    DialogNode askAboutAngels = new DialogNode(new NpcDialogPart("Aniołowie są obrońcami światła. Walczymy z demonami, aby zachować równowagę we wszechświecie."));
    DialogNode askAboutMephisto = new DialogNode(new NpcDialogPart("Mephisto, Pan Nienawiści, to potężny demon, który pragnie zniszczyć ludzkość. Musisz go pokonać, aby uratować świat."));

    // Dodanie odpowiedzi powrotu do menu informacyjnego
    askAboutGabriel.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);
    askAboutAngels.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);
    askAboutMephisto.AddResponse(new HeroDialogPart("Wracam do pytań."), infoNode);

    // Dodanie pytań w menu informacyjnym
    infoNode.AddResponse(new HeroDialogPart("Kim jesteś, Gabrielu?"), askAboutGabriel);
    infoNode.AddResponse(new HeroDialogPart("Opowiedz mi o aniołach."), askAboutAngels);
    infoNode.AddResponse(new HeroDialogPart("Kim jest Mephisto?"), askAboutMephisto);
    infoNode.AddResponse(new HeroDialogPart("Powrót do głównego dialogu."), startNode);

    // Główne pytanie w dialogu Gabriela
    startNode.AddResponse(new HeroDialogPart("Tak, pomogę ci!"), askDetails);
    startNode.AddResponse(new HeroDialogPart("Nie, to zbyt ryzykowne."), refuseMission);
    startNode.AddResponse(new HeroDialogPart("Mam pytania."), infoNode);
    startNode.AddResponse(new HeroDialogPart("Opuść dialog."), new DialogNode(new NpcDialogPart("Powracasz do lokacji...")));

    return startNode;
}
    
    
}


public class DialogNode
{
    public NpcDialogPart NpcPart { get; set; }  // Wypowiedź NPC
    public Dictionary<HeroDialogPart, DialogNode> HeroResponses { get; set; }  // Odpowiedzi bohatera -> Kolejne węzły
    public Action NodeAction { get; set; } // To store an action like starting a fight.

    public DialogNode(NpcDialogPart npcPart)
    {
        NpcPart = npcPart;
        HeroResponses = new Dictionary<HeroDialogPart, DialogNode>();
    }
    
    public void AddResponse(HeroDialogPart heroResponse, DialogNode nextNode)
    {
        HeroResponses.Add(heroResponse, nextNode);
    }
    
}

public class DialogParser
{
    private Hero hero;
    private NonPlayerCharacter npc;
    private int gold;
    private int experience;

    public DialogParser(Hero hero, NonPlayerCharacter npc, int gold, int experience)
    {
        this.hero = hero;
        this.npc = npc;
        this.gold = gold;
        this.experience = experience;
    }

    public string ParseDialog(IDialogPart dialogPart)
    {
        // Zastąpienie #HERONAME# imieniem bohatera, #NPCNAME# imieniem NPC, #GOLD# i #EXP# odpowiednimi wartościami
        string content = dialogPart.Content;
        content = content.Replace("#HERONAME#", hero.name);
        content = content.Replace("#NPCNAME#", npc.Name);
        content = content.Replace("#GOLD#", gold.ToString());
        content = content.Replace("#EXP#", experience.ToString());
        return content;
    }
}

public class DialogPart : IDialogPart
{
    public string Content { get; set; }

    // Konstruktor przyjmujący treść dialogu
    public DialogPart(string content)
    {
        Content = content;
    }

}



public interface IDialogPart
{
    string Content { get; }
}

public class NpcDialogPart : DialogPart
{
    // Konstruktor akceptujący treść dialogu
    public NpcDialogPart(string content) : base(content)
    {
    }
    
}

public class HeroDialogPart : DialogPart
{
    
    // Konstruktor przyjmujący treść dialogu i przekazujący ją do klasy bazowej
    public HeroDialogPart(string content) : base(content)
    {

    }

}


class Diablo
{
    
    public static Hero hero = new Hero("Not Specified", EHeroClass.None); // Zainicjalizowany bohater z domyślnymi wartościami

    static void Main()
    {
        MainMenu();
    }

    static void MainMenu()
    {
        string gameName = "Diablo V";
        bool exitGame = false;

        while (!exitGame)
        {
            Console.Clear(); // Czyści ekran konsoli
            Console.WriteLine($"Witaj w grze {gameName}!");
            Console.WriteLine("[1] Zacznij nową grę");
            Console.WriteLine("[X] Zamknij program");
            Console.Write("Wybierz opcję: ");

            string userChoice = Console.ReadLine().ToUpper();
            switch (userChoice)
            {
                case "1":
                    StartNewGame();  // Tworzenie nowej gry
                    break;
                case "X":
                    exitGame = true;
                    Console.WriteLine("Zamykanie programu...");
                    break;
                default:
                    Console.WriteLine("Niewłaściwa opcja! Spróbuj ponownie.");
                    Console.ReadKey(); // Czeka na naciśnięcie klawisza
                    break;
            }
        }
    }
    
    
    static void DisplayHeroProfile(Hero hero)
    {
        // Sprawdzamy, czy bohater istnieje (imię nie jest "Not Specified" i klasa nie jest None)
        if (hero == null || hero.name == "Not Specified" || hero.klass == EHeroClass.None)
        {
            Console.WriteLine("Nie utworzyłeś jeszcze bohatera.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("=== Profil Bohatera ===");
        Console.WriteLine($"Imię: {hero.name}");
        Console.WriteLine($"Klasa: {hero.klass}");
        Console.WriteLine($"Złoto: {hero.Gold}");
        Console.WriteLine($"Doświadczenie: {hero.Experience}");
        Console.WriteLine("=======================");

        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu.");
        Console.ReadKey();
    }
    
    static void StartNewGame()
    {
        Console.Clear();
        Console.WriteLine("Nowa gra rozpoczęta!");

        string heroName = VerifyHeroName(); // Weryfikacja imienia bohatera
        EHeroClass chosenClass = VerifyHeroClass(); // Weryfikacja wyboru klasy

        // Jeśli imię bohatera jest poprawne i klasa została wybrana, tworzymy bohatera
        if (!string.IsNullOrEmpty(heroName) && chosenClass != EHeroClass.None)
        {
            hero.name = heroName;
            hero.klass = chosenClass;
            hero.Gold = 100; // Dodanie początkowego złota
            hero.Experience = 2000; // Początkowe doświadczenie

            Console.Clear();
            Console.WriteLine($"Gra kontynuuje... Twój bohater to: {hero.name}, a klasa to: {chosenClass}. Wyruszasz na przygodę!");

            // Inicjalizacja gry i start podróży
            InitializeGame(hero);
        }
        else
        {
            Console.WriteLine("Nie udało się utworzyć postaci. Spróbuj ponownie.");
            Console.ReadKey();
        }
    }
    
    
    
    // Metoda do inicjalizacji gry
    static void InitializeGame(Hero hero)
    {
        Region sanctuary = LocationManager.InitializeSanctuary(hero);
        bool continueTravel = true;

        while (continueTravel)
        {
            Console.Clear();
            Console.WriteLine($"Witaj w regionie {sanctuary.name}. Wybierz lokalizację, do której chcesz podróżować:");
            Console.WriteLine("1. Tristram");
            Console.WriteLine("2. Piekło");
            Console.WriteLine("S. Zobacz swoje statystyki");
            Console.WriteLine("M. Zobacz dziennik misji");
            Console.WriteLine("X. Powrót do menu głównego");

            string locationChoice = Console.ReadLine().ToUpper();
            switch (locationChoice)
            {
                case "1":
                    TravelToLocation(sanctuary.Locations[0], hero); // Tristram
                    break;
                case "2":
                    TravelToLocation(sanctuary.Locations[1], hero); // Piekło
                    break;
                case "S":
                    DisplayHeroProfile(hero); // Wyświetlenie statystyk bohatera
                    break;
                case "X":
                    continueTravel = false;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                    break;
            }
        }
    }
    // Metoda podróży do wybranej lokalizacji
    static void TravelToLocation(Location location, Hero hero)
    {
        bool stayInLocation = true;

        while (stayInLocation)
        {
            Console.Clear();
            Console.WriteLine($"Jesteś teraz w {location.name}. Co chcesz zrobić?");
            Console.WriteLine("0. Powrót do mapy świata");

            // Wyświetlenie listy NPC
            for (int i = 0; i < location.Npcs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Rozmawiaj z {location.Npcs[i].Name}");
            }

            string choice = Console.ReadLine();
            
            if (choice == "0")
            {
                stayInLocation = false; // Powrót do wyboru lokalizacji
            }
            else if (int.TryParse(choice, out int npcIndex) && npcIndex > 0 && npcIndex <= location.Npcs.Count)
            {
                // Rozpocznij rozmowę z wybranym NPC
                NonPlayerCharacter selectedNpc = location.Npcs[npcIndex - 1];
                
                    StartDialogWithNpc(hero, selectedNpc, location);
                    Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
            }
        }
    }
    
    
    
    static void StartDialogWithNpc(Hero hero, NonPlayerCharacter npc, Location location)
    {
        Console.Clear();

        if (location.name == "Tristram")
        {
            if (npc.Name == "Archanioł Tyrael")
            {
                
                StartDialog(DialogManager.InitializeTyraelDialog(), hero, npc, 100, 500, location);
                
            }
            else if (npc.Name == "Archanioł Gabriel")
            {
                StartDialog(DialogManager.InitializeGabrielDialog(), hero, npc, hero.Gold, hero.Experience, location);
            }
        }
        else if (location.name == "Piekło")
        {
            if (npc.Name == "Baal")
            {
                StartDialog(DialogManager.InitializeBaalDialog(), hero, npc, 0, 1000, location);
            }
            else if (npc.Name == "Mephisto")
            {
                StartDialog(DialogManager.InitializeMephistoDialog(), hero, npc, 0, 1000, location);
            }
        }
    }
    
    
    // Metoda do rozpoczęcia dialogu
// Metoda do rozpoczęcia dialogu



    static void StartDialog(DialogNode node, Hero hero, NonPlayerCharacter npc, int gold, int experience, Location location)
    {
        bool dialogContinues = true;

        while (dialogContinues && node != null)
        {
            Console.Clear();

            // Parse the dialog and display it
            DialogParser parser = new DialogParser(hero, npc, gold, experience);
            string parsedDialog = parser.ParseDialog(node.NpcPart);
            Console.WriteLine(parsedDialog);
            

            // Sprawdzanie, czy dialog się kończy
            if (node.NodeAction != null || node.HeroResponses.Count == 0)
            {
                Console.WriteLine($"\nNaciśnij dowolny klawisz, aby wrócić do {location.name}.");
                Console.ReadKey();
                return;
            }

            // Wyświetlanie odpowiedzi bohatera
            Console.WriteLine("\nWybierz odpowiedź:");
            int index = 1;
            var responseList = new List<HeroDialogPart>();
            foreach (var response in node.HeroResponses.Keys)
            {
                Console.WriteLine($"{index}. {response.Content}");
                responseList.Add(response);
                index++;
            }

            // Get the player's choice
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= responseList.Count)
            {
                HeroDialogPart selectedResponse = responseList[choice - 1];
                node = node.HeroResponses[selectedResponse];  // Przejście do kolejnego węzła dialogu
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
            }
        }
    } 
    static string VerifyHeroName()
    {
        string heroName = "";
        bool validName = false;

        while (!validName)
        {
            Console.Write("Proszę podaj nazwę bohatera: ");
            heroName = Console.ReadLine().Trim();

            // Użycie wyrażenia regularnego do sprawdzenia poprawności imienia
            string pattern = @"^[A-Za-z\s]+$"; // Tylko litery alfabetu i spacje
            if (Regex.IsMatch(heroName, pattern) && !string.IsNullOrEmpty(heroName))
            {
                validName = true;
            }
            else
            {
                Console.WriteLine("Nazwa bohatera składa się tylko z liter alfabetu i ewentualnie spacji!");
            }
        }

        return heroName;
    }

    // Metoda do weryfikacji wyboru klasy bohatera
    static EHeroClass VerifyHeroClass()
    {
        EHeroClass chosenClass = EHeroClass.None;
        bool validChoice = false;

        while (!validChoice)
        {
            Console.WriteLine("Wybierz klasę bohatera:");
            Console.WriteLine("0 Barbarian");
            Console.WriteLine("1 Sorcerer");
            Console.WriteLine("2 Rouge");
            Console.Write("Wybieram: ");

            string userInput = Console.ReadLine();

            // Sprawdzenie, czy wybór jest poprawny (czy jest liczbą i czy mieści się w zakresie)
            if (int.TryParse(userInput, out int heroClassIndex) && Enum.IsDefined(typeof(EHeroClass), heroClassIndex))
            {
                chosenClass = (EHeroClass)heroClassIndex;
                validChoice = true;
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór klasy bohatera. Spróbuj ponownie.");
            }
        }
        

        return chosenClass;
    }
    
}

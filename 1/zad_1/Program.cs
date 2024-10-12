// See https://aka.ms/new-console-template for more information


using System.Text.RegularExpressions;
using System;



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
    private List<NonPlayerCharacter> npcs = new List<NonPlayerCharacter>();

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
    public static DialogNode InitializeTristramDialog(Hero hero, NonPlayerCharacter npc, int gold, int experience)
    {
        DialogNode startNode = new DialogNode(new NpcDialogPart("Witaj, #HERONAME#, jestem #NPCNAME#. Czy chcesz mi pomóc ocalić Tristram?"));

        DialogNode agreeHelp = new DialogNode(new NpcDialogPart("Dziękuję! Twoja pomoc będzie nieoceniona."));
        DialogNode refuseHelp = new DialogNode(new NpcDialogPart("Zrozumiałem, wróć, gdy będziesz gotowy na ostateczne starcie."));

        startNode.AddResponse(new HeroDialogPart("Tak, pomogę."), agreeHelp);
        startNode.AddResponse(new HeroDialogPart("Nie, jestem zajęty."), refuseHelp);

        return startNode;
    }

    public static void StartFightWithMephisto(Hero hero, NonPlayerCharacter mephisto, Location location)
    {
        Console.Clear();
        Console.WriteLine("Rozpoczęła się walka z Mephisto...");

        // Bohater traci doświadczenie
        hero.GainExperience(-500); // Tracisz 500 punktów doświadczenia

        // Mephisto pozostaje w lokacji, ponieważ bohater przegrywa
        Console.WriteLine("Przegrałeś walkę z Mephisto i straciłeś 500 punktów doświadczenia.");
        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do mapy świata.");
        Console.ReadKey();
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
        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do mapy świata.");
        Console.ReadKey();
    }
    public static DialogNode InitializeBaalDialog(Hero hero, NonPlayerCharacter baal, int gold, int experience, Location location)
    {
        DialogNode startNode = new DialogNode(new NpcDialogPart("Ośmieliłeś się stanąć przede mną, #HERONAME#? Przygotuj się na zniszczenie!"));

        DialogNode challengeBaal = new DialogNode(new NpcDialogPart("Zobaczymy, jak długo wytrzymasz w walce przeciwko mnie."));
    
        // Add the action to trigger the fight
        challengeBaal.SetAction(() => StartFightWithBaal(hero, baal, location));

        DialogNode escapeBaal = new DialogNode(new NpcDialogPart("Uciekasz? Słaby człowieku, nigdy nie będziesz godzien walki ze mną."));

        startNode.AddResponse(new HeroDialogPart("Zamierzam cię pokonać!"), challengeBaal);
        startNode.AddResponse(new HeroDialogPart("Muszę się wycofać."), escapeBaal);

        return startNode;
    }
    
    public static DialogNode InitializeTyraelDialog(Hero hero, NonPlayerCharacter tyrael, int gold, int experience)
    {
        DialogNode startNode = new DialogNode(new NpcDialogPart("Witaj, #HERONAME#, jestem Archanioł Tyrael. Czy chcesz mi pomóc ocalić Tristram, pokonując Baala?"));

        DialogNode agreeHelp = new DialogNode(new NpcDialogPart("Dziękuję! Twoja pomoc będzie nieoceniona."));
        agreeHelp.SetAction(() => hero.AddMission(new Mission("Pokonaj Baala"))); // Dodanie misji do dziennika

        DialogNode refuseHelp = new DialogNode(new NpcDialogPart("Zrozumiałem, wróć, gdy będziesz gotowy na ostateczne starcie."));

        startNode.AddResponse(new HeroDialogPart("Tak, pomogę."), agreeHelp);
        startNode.AddResponse(new HeroDialogPart("Nie, jestem zajęty."), refuseHelp);

        return startNode;
    }
public static DialogNode InitializeMephistoDialog(Hero hero, NonPlayerCharacter mephisto, int gold, int experience, Location location)
{
    DialogNode startNode = new DialogNode(new NpcDialogPart("Ah, #HERONAME#, słyszałem o tobie... Przyszedłeś, by stawić czoła Panom Piekła? Jesteś gotów na swoją zgubę?"));

    DialogNode challengeMephisto = new DialogNode(new NpcDialogPart("Bardzo dobrze! Zginiesz z ręki Pana Nienawiści, a twoja dusza będzie cierpieć przez wieczność."));
    challengeMephisto.SetAction(() => StartFightWithMephisto(hero, mephisto, location)); // Dodajemy akcję walki

    DialogNode parleyMephisto = new DialogNode(new NpcDialogPart("Wielkie słowa, śmiertelniku. Jednakże, może byś się przyłączył do mojej władzy, zamiast ginąć?"));

    startNode.AddResponse(new HeroDialogPart("Pokonam cię, Mephisto!"), challengeMephisto);
    startNode.AddResponse(new HeroDialogPart("Nie musimy walczyć, możemy znaleźć inne wyjście."), parleyMephisto);

    return startNode;
}
    
public static DialogNode InitializeGabrielDialog(Hero hero, NonPlayerCharacter gabriel, int gold, int experience)
{
    DialogNode startNode = new DialogNode(new NpcDialogPart("Witaj, #HERONAME#. Potrzebuję twojej pomocy w walce z Mephisto. Czy podejmiesz się tej misji?"));

    DialogNode acceptMission = new DialogNode(new NpcDialogPart("Doskonale! Twoja odwaga zostanie wynagrodzona."));
    acceptMission.SetAction(() => hero.AddMission(new Mission("Pokonaj Mephisto"))); // Dodanie misji do dziennika

    DialogNode askDetails = new DialogNode(new NpcDialogPart("Musisz udać się do Piekła i pokonać Mephisto."));
    DialogNode refuseMission = new DialogNode(new NpcDialogPart("Rozumiem, to nie jest łatwe zadanie. Powróć, gdy będziesz gotowy."));

    askDetails.AddResponse(new HeroDialogPart("Zrozumiałem. Wyruszam na misję."), acceptMission);
    askDetails.AddResponse(new HeroDialogPart("Nie, to zbyt niebezpieczne."), refuseMission);

    startNode.AddResponse(new HeroDialogPart("Tak, pomogę ci!"), askDetails);
    startNode.AddResponse(new HeroDialogPart("Nie, to zbyt ryzykowne."), refuseMission);

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

    // Dodawanie odpowiedzi bohatera i powiązanego węzła dialogowego
    // Add an action if needed
    public void SetAction(Action action)
    {
        NodeAction = action;
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
    public virtual void Speak()
    {
        Console.WriteLine(Content);
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
    public override void Speak()
    {
        Console.WriteLine("NPC: " + Content);
    }
}

public class HeroDialogPart : DialogPart
{
    
    // Konstruktor przyjmujący treść dialogu i przekazujący ją do klasy bazowej
    public HeroDialogPart(string content) : base(content)
    {
    }
    public override void Speak()
    {
        Console.WriteLine("Hero: " + Content);
    }
}
public class NonPlayerCharacter
{
    public string Name { get; set; }
    private List<NpcDialogPart> dialogParts = new List<NpcDialogPart>();

    public void AddDialog(NpcDialogPart part)
    {
        dialogParts.Add(part);
    }
    
    
    
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
    public List<Mission> MissionJournal { get; set; } // A list of missions

    public Hero(string nazwa, EHeroClass klassa)
    {
        this.name = nazwa;
        this.klass = klassa;
        this.Gold = 0; // Złoto startowe
        this.Experience = 0; // Początkowe doświadczenie
        this.MissionJournal = new List<Mission>(); // Make sure it's initialized
    }

    public void AddMission(Mission mission)
    {
        if (!MissionJournal.Any(m => m.Name == mission.Name)) // Sprawdzenie, czy misja już istnieje
        {
            MissionJournal.Add(mission);
        }
        else
        {
            Console.WriteLine($"Misja '{mission.Name}' jest już aktywna.");
        }
    }
    public void CompleteMission(string missionName)
    {
        Mission mission = MissionJournal.FirstOrDefault(m => m.Name == missionName);
        if (mission != null)
        {
            mission.Complete();
            Console.WriteLine($"Misja '{missionName}' została zakończona.");
        }
        else
        {
            Console.WriteLine($"Misja '{missionName}' nie została znaleziona w dzienniku.");
        }
    }
    public void GainExperience(int amount)
    {
        Experience += amount;
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

class Game
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
            if (hero != null && hero.name != "Not Specified")
            {
                Console.WriteLine("[2] Wczytaj grę");
            }
            Console.WriteLine("[X] Zamknij program");
            Console.Write("Wybierz opcję: ");

            string userChoice = Console.ReadLine().ToUpper();
            switch (userChoice)
            {
                case "1":
                    StartNewGame();  // Tworzenie nowej gry
                    break;
                case "2":
                    if (hero != null)
                    {
                        InitializeGame(hero); // Wczytanie istniejącej gry
                    }
                    else
                    {
                        Console.WriteLine("Nie masz jeszcze zapisanej gry.");
                        Console.ReadKey();
                    }
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
    
    
    
    static void DisplayMissionJournal(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("=== Dziennik Misji ===");

        if (hero.MissionJournal == null || hero.MissionJournal.Count == 0)
        {
            Console.WriteLine("Brak aktywnych misji.");
        }
        else
        {
            foreach (var mission in hero.MissionJournal)
            {
                string status = mission.IsCompleted ? "Zakończona" : "Aktywna";
                Console.WriteLine($"Misja: {mission.Name} - Status: {status}");
            }
        }

        Console.WriteLine("=======================");
        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić.");
        Console.ReadKey();
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
            hero = new Hero(heroName, chosenClass); // Tworzymy bohatera
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
                case "M":
                    DisplayMissionJournal(hero); // Wyświetlenie dziennika misji
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
                StartDialog(DialogManager.InitializeTyraelDialog(hero, npc, hero.Gold, hero.Experience), hero, npc, 100, 500, location);
            }
            else if (npc.Name == "Archanioł Gabriel")
            {
                StartDialog(DialogManager.InitializeGabrielDialog(hero, npc, hero.Gold, hero.Experience), hero, npc, hero.Gold, hero.Experience, location);
            }
        }
        else if (location.name == "Piekło")
        {
            if (npc.Name == "Baal")
            {
                StartDialog(DialogManager.InitializeBaalDialog(hero, npc, hero.Gold, hero.Experience, location), hero, npc, 0, 1000, location);
            }
            else if (npc.Name == "Mephisto")
            {
                StartDialog(DialogManager.InitializeMephistoDialog(hero, npc, hero.Gold, hero.Experience, location), hero, npc, 0, 1000, location);
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

            // Execute NodeAction if present
            node.NodeAction?.Invoke();

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

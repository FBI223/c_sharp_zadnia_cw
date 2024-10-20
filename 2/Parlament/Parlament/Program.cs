using System;
using System.Collections.Generic;

namespace cw01
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int numberOfDeputies = random.Next(2, 101); // Zakres [2, 100]
            Console.WriteLine($"Liczba posłów: {numberOfDeputies}");

            List<Representative> representatives = new List<Representative>();
            Parliment parliment_logic = new Parliment();

            // Dodawanie posłów i subskrybowanie zdarzeń
            for (int i = 0; i < numberOfDeputies; i++)
            {
                representatives.Add(new Representative($"Representative_{i}", parliment_logic));
            }

            // Rozpoczęcie głosowania
            parliment_logic.StartVoting();

            // Wywołanie komendy do oddania głosu (wszyscy posłowie oddadzą głos)
            parliment_logic.CommandToGiveVote();

            // Zakończenie głosowania
            parliment_logic.StopVoting();

            // Wyświetlenie wyników głosowania
            parliment_logic.DisplayVotingResults();
            Console.WriteLine("");
            Console.WriteLine("Session Ended!");
        }
    }

    public class Parliment
    {
        // Liczniki głosów
        private int yesVotes = 0;
        private int noVotes = 0;
        private int refrainVotes = 0;

        public event EventHandler VotingStarted;
        public event EventHandler VotingEnded;
        public event EventHandler VoteCommandGiven; // Event do oddania głosu przez wszystkich

        public void DisplayVotingResults()
        {
            Console.WriteLine("");
            Console.WriteLine("Wyniki głosowania:");
            Console.WriteLine($"Za: {yesVotes}");
            Console.WriteLine($"Przeciw: {noVotes}");
            Console.WriteLine($"Wstrzymujących się: {refrainVotes}");
            Console.WriteLine("");
        }

        public void StartVoting()
        {
            Console.WriteLine("");
            Console.WriteLine("StartVoting!");
            Console.WriteLine("");
            this.OnVotingStarted();
        }

        public void StopVoting()
        {
            Console.WriteLine("");
            Console.WriteLine("StopVoting!");
            Console.WriteLine("");
            this.OnVotingEnded();
        }

        public void CommandToGiveVote()
        {
            Console.WriteLine("");
            Console.WriteLine("Giving vote command!");
            Console.WriteLine("");
            this.OnGiveVote();
        }

        protected virtual void OnVotingStarted()
        {
            this.VotingStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGiveVote()
        {
            // Wywołanie zdarzenia, które powoduje oddanie głosu przez wszystkich
            this.VoteCommandGiven?.Invoke(this, EventArgs.Empty);
        }

        public void GiveVote(VotingEventArgs args)
        {
            switch (args.Vote)
            {
                case VoteOption.no:
                    this.noVotes++;
                    break;
                case VoteOption.yes:
                    this.yesVotes++;
                    break;
                case VoteOption.refrain:
                    this.refrainVotes++;
                    break;
            }
        }

        protected virtual void OnVotingEnded()
        {
            this.VotingEnded?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Representative
    {
        private string name;
        private static Random rand = new Random(); // Jednorazowa inicjalizacja Random
        private Parliment parliment;

        public Representative(string name, Parliment parliment)
        {
            this.name = name;
            this.parliment = parliment;

            // Subskrypcja na eventy głosowania
            parliment.VotingStarted += OnVotingStarted;
            parliment.VoteCommandGiven += OnGiveVote; // Subskrypcja na zdarzenie do oddania głosu
            parliment.VotingEnded += OnVotingEnded;
        }

        // Subskrypcja na zdarzenie rozpoczęcia głosowania
        public void OnVotingStarted(object sender, EventArgs e)
        {
            Console.WriteLine($"I {this.name} am ready to vote!");
        }

        // Subskrypcja na zdarzenie zakończenia głosowania
        public void OnVotingEnded(object sender, EventArgs e)
        {
            Console.WriteLine($"I {this.name} am leaving the parliment after voting!");
        }

        // Metoda oddania głosu przez posła w odpowiedzi na zdarzenie `VoteCommandGiven`
        public void OnGiveVote(object sender, EventArgs e)
        {
            VoteOption vote = GetVote(); // Losowanie głosu
            Console.WriteLine($"{name} zagłosował: {vote}");

            Parliment parliment = sender as Parliment;
            parliment.GiveVote(new VotingEventArgs { RepresentativeName = name, Vote = vote });
        }

        // Generowanie losowego głosu
        private VoteOption GetVote()
        {
            VoteOption[] possibleVotes = { VoteOption.yes, VoteOption.no, VoteOption.refrain };
            return possibleVotes[rand.Next(possibleVotes.Length)];
        }
    }

    // Definicja enum dla opcji głosowania
    public enum VoteOption
    {
        yes,
        no,
        refrain
    }

    public class VotingEventArgs : EventArgs
    {
        public string RepresentativeName { get; set; }
        public VoteOption Vote { get; set; }
    }
}

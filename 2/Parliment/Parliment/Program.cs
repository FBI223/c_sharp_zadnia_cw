using System;
using System.Collections.Generic;

namespace ParliamentSimulator
{
    public class VoteEventArgs : EventArgs
    {
        public int ParliamentarianId { get; }
        public bool Vote { get; }

        public VoteEventArgs(int id, bool vote)
        {
            ParliamentarianId = id;
            Vote = vote;
        }
    }

    public class Parliament
    {
        public event EventHandler<string> OnVotingStarted;
        public event EventHandler<string> OnVotingEnded;
        
        // Nowe zdarzenie, które symuluje przyciskanie przycisków
        public event EventHandler OnPressingButtons;

        private readonly List<Parliamentarian> parliamentarians;
        private int votesFor;
        private int votesAgainst;

        public Parliament(int numberOfParliamentarians)
        {
            parliamentarians = new List<Parliamentarian>();
            for (int i = 0; i < numberOfParliamentarians; i++)
            {
                Parliamentarian parliamentarian = new Parliamentarian(i);
                
                // Każdy poseł subskrybuje zdarzenie "Pressing Buttons"
                this.OnPressingButtons += parliamentarian.OnPressingButtonsHandler;

                // Parlament subskrybuje zdarzenie głosowania od każdego posła
                parliamentarian.VoteCast += HandleVote;
                
                parliamentarians.Add(parliamentarian);
            }
        }

        public void StartVoting(string topic)
        {
            Console.WriteLine($"Voting on '{topic}' has started.");
            OnVotingStarted?.Invoke(this, topic);
            
            votesFor = 0;
            votesAgainst = 0;

            // Wywołanie zdarzenia "Pressing Buttons" – posłowie oddadzą głos
            OnPressingButtons?.Invoke(this, EventArgs.Empty);
        }

        public void EndVoting(string topic)
        {
            OnVotingEnded?.Invoke(this, topic);
            Console.WriteLine($"Voting on '{topic}' has ended.");
            Console.WriteLine($"Votes for: {votesFor}, Votes against: {votesAgainst}");
        }

        private void HandleVote(object sender, VoteEventArgs e)
        {
            Console.WriteLine($"Parliamentarian {e.ParliamentarianId} voted {(e.Vote ? "for" : "against")}.");
            if (e.Vote)
                votesFor++;
            else
                votesAgainst++;
        }
    }

    public class Parliamentarian
    {
        public int Id { get; }
        private static Random random = new Random();

        // Zdarzenie głosowania z użyciem `VoteEventArgs`
        public event EventHandler<VoteEventArgs> VoteCast;

        public Parliamentarian(int id)
        {
            Id = id;
        }

        // Obsługa zdarzenia OnPressingButtons, wywołująca głosowanie
        public void OnPressingButtonsHandler(object sender, EventArgs e)
        {
            CastVote();
        }

        public void CastVote()
        {
            bool vote = random.Next(0, 2) == 1;
            // Wywołanie zdarzenia VoteCast z wynikiem głosowania
            VoteCast?.Invoke(this, new VoteEventArgs(Id, vote));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of parliamentarians: ");
            int numParliamentarians = int.Parse(Console.ReadLine());

            Console.Write("Enter the topic of voting: ");
            string topic = Console.ReadLine();

            Parliament parliament = new Parliament(numParliamentarians);

            // Subskrybowanie zdarzeń dla początku i końca głosowania
            parliament.OnVotingStarted += OnVotingStartedHandler;
            parliament.OnVotingEnded += OnVotingEndedHandler;

            // Rozpoczęcie głosowania
            parliament.StartVoting(topic);

            // Zakończenie głosowania
            parliament.EndVoting(topic);

            Console.ReadKey();
        }

        // Obsługa zdarzenia rozpoczęcia głosowania
        static void OnVotingStartedHandler(object sender, string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has started. Command: 'Pressing Buttons' sent.");
        }

        // Obsługa zdarzenia zakończenia głosowania
        static void OnVotingEndedHandler(object sender, string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has ended.");
        }
    }
}

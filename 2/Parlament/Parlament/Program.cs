using System;
using System.Collections.Generic;

namespace ParliamentSimulator
{
    // Define delegate for the voting events
    public delegate void VotingEventHandler(string topic);

    public class Parliament
    {
        public event VotingEventHandler OnVotingStarted;
        public event VotingEventHandler OnVotingEnded;
        
        private readonly List<Parliamentarian> parliamentarians;
        private int votesFor;
        private int votesAgainst;

        public Parliament(int numberOfParliamentarians)
        {
            parliamentarians = new List<Parliamentarian>();
            for (int i = 0; i < numberOfParliamentarians; i++)
            {
                Parliamentarian parliamentarian = new Parliamentarian(i);
                parliamentarian.OnVoteCast += HandleVote;
                parliamentarians.Add(parliamentarian);
            }
        }

        public void StartVoting(string topic)
        {
            Console.WriteLine($"Voting on the topic '{topic}' has started.");
            OnVotingStarted?.Invoke(topic);

            votesFor = 0;
            votesAgainst = 0;

            foreach (var parliamentarian in parliamentarians)
            {
                parliamentarian.CastVote();
            }
        }

        public void EndVoting(string topic)
        {
            Console.WriteLine($"Voting on the topic '{topic}' has ended.");
            OnVotingEnded?.Invoke(topic);
            Console.WriteLine($"Votes for: {votesFor}, Votes against: {votesAgainst}");
        }

        private void HandleVote(int parliamentarianId, bool vote)
        {
            Console.WriteLine($"Parliamentarian {parliamentarianId} voted {(vote ? "for" : "against")}.");
            if (vote)
                votesFor++;
            else
                votesAgainst++;
        }
    }

    public class Parliamentarian
    {
        public int Id { get; }
        private static Random random = new Random();

        // Define delegate for casting vote
        public delegate void VoteCastHandler(int id, bool vote);
        public event VoteCastHandler OnVoteCast;

        public Parliamentarian(int id)
        {
            Id = id;
        }

        public void CastVote()
        {
            bool vote = random.Next(0, 2) == 1;
            OnVoteCast?.Invoke(Id, vote);
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
            
            // Attach the event handlers
            parliament.OnVotingStarted += OnVotingStartedHandler;
            parliament.OnVotingEnded += OnVotingEndedHandler;

            // Start the voting process
            parliament.StartVoting(topic);

            // End the voting process
            parliament.EndVoting(topic);

            Console.ReadKey();
        }

        // Event handler for when voting starts
        static void OnVotingStartedHandler(string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has started.");
        }

        // Event handler for when voting ends
        static void OnVotingEndedHandler(string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has ended.");
        }
    }
}

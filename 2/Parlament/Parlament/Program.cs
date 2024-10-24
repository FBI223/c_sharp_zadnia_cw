using System;
using System.Collections.Generic;

namespace ParliamentSimulator
{
    // Define custom EventArgs for voting event
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

        private readonly List<Parliamentarian> parliamentarians;
        private int votesFor;
        private int votesAgainst;

        public Parliament(int numberOfParliamentarians)
        {
            parliamentarians = new List<Parliamentarian>();
            for (int i = 0; i < numberOfParliamentarians; i++)
            {
                Parliamentarian parliamentarian = new Parliamentarian(i);
                parliamentarian.VoteCast += HandleVote;
                parliamentarians.Add(parliamentarian);
            }
        }

        public void StartVoting(string topic)
        {
            OnVotingStarted?.Invoke(this, topic);

            votesFor = 0;
            votesAgainst = 0;

            foreach (var parliamentarian in parliamentarians)
            {
                parliamentarian.CastVote();
            }
        }

        public void EndVoting(string topic)
        {
            OnVotingEnded?.Invoke(this, topic);
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

        // Event using EventHandler with custom VoteEventArgs
        public event EventHandler<VoteEventArgs> VoteCast;

        public Parliamentarian(int id)
        {
            Id = id;
        }

        public void CastVote()
        {
            bool vote = random.Next(0, 2) == 1;
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
        static void OnVotingStartedHandler(object sender, string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has started.");
        }

        // Event handler for when voting ends
        static void OnVotingEndedHandler(object sender, string topic)
        {
            Console.WriteLine($"Event: Voting on '{topic}' has ended.");
        }
    }
}

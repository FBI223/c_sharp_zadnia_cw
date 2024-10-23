
namespace ParliamentSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj liczbę parlamentarzystów: ");
            int numberOfMembers = int.Parse(Console.ReadLine());
            Console.Write("Podaj temat głosowania: ");
            string votingTopic = Console.ReadLine();
            
            
            Parliament parliament = new Parliament(numberOfMembers, votingTopic);
            parliament.StartVoting();
            parliament.EndVoting(); 
            parliament.ShowResults();
            
        }
    }

    public class Parliament
    {
        public event EventHandler StartVoteEvent;
        public event EventHandler EndVoteEvent;
        private List<Parliamentarian> members;
        private string topic;
        private bool votingStarted;
        private bool votingEnded;
        private int votesFor;
        private int votesAgainst;

        public Parliament(int numberOfMembers, string votingTopic)
        {
            members = new List<Parliamentarian>();
            topic = votingTopic;
            votingStarted = false;
            votingEnded = false;
            votesFor = 0;
            votesAgainst = 0;

            for (int i = 0; i < numberOfMembers; i++)
            {
                Parliamentarian member = new Parliamentarian(i + 1);
                member.VoteEvent += OnMemberVoted;
                members.Add(member);
            }
        }

        public void StartVoting()
        {
            if (!votingStarted && !votingEnded)
            {
                Console.WriteLine($"Rozpoczęto głosowanie nad tematem: {topic}");
                votingStarted = true;
                OnStartVote();
                foreach (var member in members)
                {
                    member.Vote();
                }
            }
            else
            {
                Console.WriteLine("Głosowanie już się rozpoczęło lub zakończyło.");
            }
        }

        public void EndVoting()
        {
            if (votingStarted && !votingEnded)
            {
                Console.WriteLine("Zakończono głosowanie.");
                votingEnded = true;
                OnEndVote();
            }
            else
            {
                Console.WriteLine("Głosowanie nie zostało rozpoczęte lub już zakończone.");
            }
        }

        public void ShowResults()
        {
            Console.WriteLine($"Głosowanie nad {topic}. Głosów za: {votesFor}, Głosów przeciw: {votesAgainst}");
        }

        protected virtual void OnStartVote()
        {
            StartVoteEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEndVote()
        {
            EndVoteEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnMemberVoted(object sender, VoteEventArgs e)
        {
            if (e.Vote == Vote.For)
            {
                votesFor++;
            }
            else
            {
                votesAgainst++;
            }
        }
    }

    public class Parliamentarian
    {
        public event EventHandler<VoteEventArgs> VoteEvent;
        private int id;

        public Parliamentarian(int id)
        {
            this.id = id;
        }

        public void Vote()
        {
            Random rand = new Random();
            Vote vote = (Vote)rand.Next(2); // 0 - Against, 1 - For
            Console.WriteLine($"GŁOS {id}: {(vote != ParliamentSimulator.Vote.For ? "ZA" : "PRZECIW")}");
            OnVote(vote);
        }

        protected virtual void OnVote(Vote vote)
        {
            VoteEvent?.Invoke(this, new VoteEventArgs { Vote = vote });
        }
    }

    public enum Vote
    {
        Against,
        For
    }

    public class VoteEventArgs : EventArgs
    {
        public Vote Vote { get; set; }
    }
}

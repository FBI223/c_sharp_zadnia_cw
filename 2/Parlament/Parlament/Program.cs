//Marcin Sztukowski

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace cw01
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Random random = new Random();
            int numberOfDeputies = random.Next(10, 21); // Zakres [20, 100]
            Console.WriteLine($"Liczba posłów: {numberOfDeputies}");
            List<Representative> representatives = new List<Representative>();
            Parliment parliment_logic = new Parliment();
            
            
            for (int i = 0; i < numberOfDeputies; i++)
            {
                representatives.Add(new Representative($"Representative_{i}"));
            }
            for (int i = 0; i < numberOfDeputies; i++)
            {
                parliment_logic.VotingStarted += representatives[i].OnVotingStarted;
                parliment_logic.VotingEnded += representatives[i].OnVotingEnded;
                parliment_logic.VoteCommandGiven += representatives[i].OnGiveVote; // Subskrypcja na oddanie głosu
            }

            
            
            
            // Rozpoczęcie głosowania
            parliment_logic.StartVoting();
            
            parliment_logic.CommandToGiveVote();

            // Zakończenie głosowania
            parliment_logic.StopVoting();
            
            Console.WriteLine("");
            Console.WriteLine("Session Ended!");
            
            //Console.ReadKey();
        }
    
    }
    public class Parliment
    {
    
        public event EventHandler<VotingEventArgs> VoteSubmitted ;
        public event EventHandler VotingStarted;
        public event EventHandler VotingEnded;
        
        // Nowe zdarzenie, które wydaje komendę do oddania głosów
        public event EventHandler VoteCommandGiven;
        
        
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
            this.OnGiveVote(); // Wywołanie zdarzenia "oddaj głos"
        }
        
        
        public void GiveVote(VotingEventArgs args)
        {
            VoteSubmitted?.Invoke(this, args); // Zgłaszanie zdarzenia oddania głosu
        }

        
        
        
        protected virtual void OnVotingStarted() //Metoda protected virtual
        {
            this.VotingStarted?.Invoke(this , EventArgs.Empty);
        }
        
        protected virtual void OnGiveVote()
        {
            this.VoteCommandGiven?.Invoke(this, EventArgs.Empty); // Wywołanie zdarzenia "oddaj głos"
        }
        
        
        protected virtual void OnVotingEnded() //Metoda protected virtual
        {
            this.VotingEnded?.Invoke(this,EventArgs.Empty );
        }
        
        // Zgłaszanie zdarzenia oddania głosu


        
        
    }


    public class Representative
    {
        private string name;
        private VoteOption voteOption;
        
        public Representative(string name)
        {
            this.name = name;
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
        

        // Metoda oddania głosu przez posła
        // Posłowie oddają głos w odpowiedzi na VotingStarted
        public void OnGiveVote(object sender, EventArgs e)
        {
            Parliment parliment = sender as Parliment;
            VoteOption vote = GetVote();
            Console.WriteLine($"{name} zagłosował: {vote}");
            parliment.GiveVote(new VotingEventArgs { RepresentativeName = name, Vote = vote });
        }
        
        // Generowanie losowego głosu
        private VoteOption GetVote()
        {
            Random rand = new Random();
            VoteOption[] possibleVotes = { VoteOption.yes, VoteOption.no, VoteOption.refrain };
            return possibleVotes[rand.Next(0, possibleVotes.Length)];
        }
        
    }
    
    // Definicja enum dla opcji głosowania
    public enum VoteOption
    {
        yes,
        no,
        refrain // Zamiast "Wstrzymuję się" dla zgodności z konwencją C#
    }
    
    public class VotingEventArgs : EventArgs
    {
        public string RepresentativeName { get; set; }
        public VoteOption Vote { get; set; }
    }
    
    
    
}


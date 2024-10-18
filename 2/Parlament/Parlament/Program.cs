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
            ProcessBusinessLogic logic = new ProcessBusinessLogic();
            logic.ProcessCompleted+= LogicOnProcessCompleted;
            logic.ProcessCompleted2+=LogicOnProcessCompleted2;
            logic.ProcessCompleted3+= ProcessCompleted3;
            logic.StartProcess();
            Console.ReadKey();
        }
        private static void ProcessCompleted3(object sender, MyEventArgs e)
        {
            Console.WriteLine("event03");
            MyEventArgs args = e;
            Console.WriteLine(args.SomeArgs);
        }
        private static void LogicOnProcessCompleted2(object sender, EventArgs e)
        {
            Console.WriteLine("event02");
        }
        private static void LogicOnProcessCompleted()
        {
            Console.WriteLine("event01");
        }
    }
    public delegate void Notify(); // Delegata
    public class ProcessBusinessLogic
    {
        public event Notify ProcessCompleted; // Zdarzenie
        public event EventHandler ProcessCompleted2; // Zdarzenie
        public EventHandler<MyEventArgs> ProcessCompleted3; // Zdarzenie
        public void StartProcess()
        {
            Console.WriteLine("Process Started!");
// some code here..
            OnProcessCompleted();
            OnProcessCompleted2();
            OnProcessCompleted3();
        }
        protected virtual void OnProcessCompleted() //Metoda protected virtual
        {
//if ProcessCompleted is not null then call delegate
            ProcessCompleted?.Invoke(); //Zgłaszanie zdarzenia
        }
        public virtual void OnProcessCompleted2() //Metoda protected virtual
        {
//if ProcessCompleted is not null then call delegate
            ProcessCompleted2?.Invoke(this, EventArgs.Empty); //Zgłaszanie zdarzenia
        }
        protected virtual void OnProcessCompleted3() //Metoda protected virtual
        {
//if ProcessCompleted is not null then call delegate
            ProcessCompleted3?.Invoke(this, new MyEventArgs(){ SomeArgs = "hello"}); //Zgłaszanie zdarzenia
        }
    }
    public class MyEventArgs : EventArgs
    {
        public string SomeArgs { get; set; }
    }
}

public delegate void Vote(); // Delegata
public delegate void Beginning(); // Delegata
public delegate void Ending(); // Delegata

class Parlament
{
    
    
    
}


class Parlamentarzysta
{
    
    
}

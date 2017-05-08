using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SCR_anastaziak
{
    class Program
    {
        public static List<OperationAgent> ListOfAgents;

        public static List<OperationAgent> GenerateRunnables()
        {
            ListOfAgents = new List<OperationAgent>();

            for (int i = 0; i < 5; i++)
            {
                
                ListOfAgents.Add(new OperationAgent(i + 1, 1));
            }
            for (int i = 0; i < 5; i++) {

                ListOfAgents.Add(new OperationAgent(i + 6, 2));
            }
            

            return ListOfAgents;
        }

        public static void RunThreads(List<OperationAgent> ListOfAgents)
        {
            List<Thread> ListOfThreads = new List<Thread>();
            Thread BankThread = new Thread(new BankAcct().displayBalance);
            BankThread.Start();
            ListOfThreads.Add(BankThread);

            foreach (OperationAgent agent in ListOfAgents)
            {
                Thread Thr = new Thread(agent.acctOperation);
                Thr.Start();
                ListOfThreads.Add(Thr);
            }
            foreach (Thread thread in ListOfThreads)
            {
                thread.Join();
            }

            

            //Thread Bank = new Thread(Bank.disp)
            Console.WriteLine("\nAgents finished theirs work\n");

        }
        


        static void Main(string[] args)
        {
            RunThreads(GenerateRunnables());
            
            
        //RunFibers(GenerateRunnables());
        Console.Read();
        }
    }
}

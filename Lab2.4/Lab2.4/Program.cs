using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab2._4
{
    class Program
    {
        public static List<Agent> ListOfAgents;
        public static List<int> listOfWords;
        public static List<List<int>> arrayOfList;

        public static List<Agent> GenerateRunnables(string numberOfAgents)
        {
            int agentNumer = Int32.Parse(numberOfAgents);
            ListOfAgents = new List<Agent>();
            for (int i = 0; i < agentNumer; i++)
            {
                ListOfAgents.Add(new Agent(arrayOfList[i]));
            }
            return ListOfAgents;
        }

        public static List<int> generateWords(string stringNumber)
        {
            listOfWords = new List<int>();
            int number = Int32.Parse(stringNumber);
            Random rnd = new Random();
            for (int i = 0; i < number; i++)
            {
                int randomNumber = rnd.Next(1, 10);
                listOfWords.Add(randomNumber);
                Console.Write("{0} ", randomNumber);
            }
            Console.WriteLine();
            return listOfWords;

        }

        public static List<List<int>> divideList(string agents, List<int> numbers)
        {
            arrayOfList = new List<List<int>>();
            double agentsNumber = Double.Parse(agents);
            int modNumber = Convert.ToInt32(numbers.Count() % agentsNumber);
            int wholeNumber = Convert.ToInt32(Math.Floor(numbers.Count() / agentsNumber) + Math.Ceiling(modNumber / agentsNumber));
            Console.WriteLine("{0}  {1}", wholeNumber, modNumber);
            
            for (int i = 0; i < numbers.Count - modNumber; i += wholeNumber)
            {
                    arrayOfList.Add(numbers.GetRange(i, Math.Min(wholeNumber, numbers.Count - i)));

            }
            //Console.WriteLine("{0} {1} {2}", arrayOfList[0].Count, arrayOfList[1].Count, arrayOfList[2].Count);
            //arrayOfList.Add(words.GetRange((Double.Parse(agentsNumber) * intNumber) - 1, words.Count - 1));
            return arrayOfList;
        }
        public static void RunThreads(List<Agent> ListOfAgents)
        {
            List<Thread> ListOfThreads = new List<Thread>();


            foreach (Agent agent in ListOfAgents)
            {
                Thread Thr = new Thread(agent.Run);
                Thr.Start();
                ListOfThreads.Add(Thr);
            }
            foreach (Thread thread in ListOfThreads)
            {
                thread.Join();
            }
            Console.WriteLine("\nAgents finished theirs work\n");

        }

        static void Main(string[] args)
        {
            Console.WriteLine("How many numbers to count:");
            string numberOfWords = Console.ReadLine();
            Console.WriteLine();
            //generateWords(numberOfWords);
            /*Console.WriteLine("How many stages:");
            string numberOfStages = Console.ReadLine();
            */
            Console.WriteLine("How many agents:");
            string numberOfAgents = Console.ReadLine();
            Console.WriteLine();
            divideList(numberOfAgents, generateWords(numberOfWords));

            RunThreads(GenerateRunnables(numberOfAgents));

            




        }
    }
}

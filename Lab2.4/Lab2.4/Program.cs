﻿using System;
using System.IO;
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
        public static int stage = 0;
        public static Dictionary<string, int> openWith;


        public static string LoadText(string path)
        {
            string text = null;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                    Console.WriteLine(text);
                    string[] words = text.Split(' ');
                    Console.WriteLine(words[1]);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return text;
        }
        public static List<Agent> GenerateRunnables(int numberOfAgents, int lastStage)
        {
            if ( lastStage == stage + 1)
            {
                numberOfAgents = 1;
            }
            double agents = Convert.ToDouble(numberOfAgents);
            numberOfAgents = Convert.ToInt32(Math.Ceiling(agents / Math.Pow(2, stage)));
            ListOfAgents = new List<Agent>();
            for (int i = 0; i < numberOfAgents; i++)
            {
                ListOfAgents.Add(new Agent(arrayOfList[i]));
            }

            return ListOfAgents;
        }

        public static List<int> GenerateNumbers(int numbersCount)
        {
            listOfWords = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < numbersCount; i++)
            {
                int randomNumber = rnd.Next(1, 10);
                listOfWords.Add(randomNumber);
            }
            Console.WriteLine();
            return listOfWords;

        }

        public static List<int> GenerateSummedNumbers(List<Agent> agents)
        {
            List<int> summedNumberList = new List<int>();

            foreach (Agent agent in agents)
            {
                summedNumberList.Add(agent.Sum);
            }

            return summedNumberList;
        }

        public static List<List<int>> DivideList(int agents, int lastStage, List<int> numbers)
        {
            arrayOfList = new List<List<int>>();
           


            if (lastStage == stage + 1)
            {
                arrayOfList.Add(numbers);
            }
            else
            {
                double agentsNumber = Convert.ToDouble(agents);
                agentsNumber = Math.Ceiling(agentsNumber / Math.Pow(2, stage));
                agents = Convert.ToInt32(agentsNumber);
                int modNumber = numbers.Count % agents;
                int wholeNumber = Convert.ToInt32(Math.Floor(numbers.Count / agentsNumber) + (modNumber != 0 ? 1 : 0));

                for (int i = 0; i < numbers.Count; i += wholeNumber)
                {
                    arrayOfList.Add(numbers.GetRange(i, Math.Min(wholeNumber, numbers.Count - i)));

                }
            }
            
            
            //Console.WriteLine("{0} {1} {2}", arrayOfList[0].Count, arrayOfList[1].Count, arrayOfList[2].Count);
            //arrayOfList.Add(words.GetRange((Double.Parse(agentsNumber) * intNumber) - 1, words.Count - 1));
            Console.WriteLine();
            Console.WriteLine("{0} agents counting", arrayOfList.Count);
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Stage: {0} \n{1} agents finished work with numbers: ", stage, ListOfAgents.Count);
            foreach (Agent agent in ListOfAgents)
            {
                Console.Write("{0} ", agent.Sum);
            }
            stage++;
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            Console.WriteLine();

        }

        static void Main(string[] args)
        {
            Console.Write("How many numbers to count: ");
            int numberOfWords = Int32.Parse(Console.ReadLine());
            Console.Write("How many agents: ");
            int numberOfAgents = Int32.Parse(Console.ReadLine());
            Console.Write("How many stages: ");
            int numberOfStages = Int32.Parse(Console.ReadLine());

            DivideList(numberOfAgents, numberOfStages, GenerateNumbers(numberOfWords));
            RunThreads(GenerateRunnables(numberOfAgents, numberOfStages));

            for (int i = 1; i < numberOfStages; i++)
            {
                DivideList(numberOfAgents, numberOfStages, GenerateSummedNumbers(ListOfAgents));
                RunThreads(GenerateRunnables(numberOfAgents, numberOfStages));
            }
            Console.WriteLine();
            

            //LoadText("data.txt");

        }
    }
}

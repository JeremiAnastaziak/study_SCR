using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab2._4
{
    public class Agent
    {
        public bool HasFinished { get { return HasFinished; } set { HasFinished = value; } }
        public List<int> List { get; set; }
        public int Sum = 0;

        public void Run()
        {
            foreach (int element in List)
            {
                Sum += element;
                Console.Write("{0} ", Sum);
                Thread.Sleep(100);
            }

        }

        public Agent(List<int> list = null)
        {
            List = list;
        }
    }
}

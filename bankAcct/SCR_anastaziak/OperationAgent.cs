using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SCR_anastaziak {
    class OperationAgent : BankAcct {
        public int Id { get; set; }
        public int SpecifyOperation { get; set; }
        private static Object acctLock = new Object();

        public OperationAgent(int id = 0, int specifyOperation = 0) {
            Id = id;
            SpecifyOperation = specifyOperation;
        }

        public void acctOperation() {

            while (true)
            {
                int specifyValueOfAddition = 10;
                int specifyValueOfSubstraction = 5;
                if ((BankAcct.cash - 10) < 0 && SpecifyOperation == 2)
                {
                    Console.WriteLine("Could not finalize operation, balance is: {0}", BankAcct.cash);
                    return;
                }

                lock (acctLock)
                {

                    int agentCash = BankAcct.cash;


                    switch (SpecifyOperation)
                    {
                        case 1:
                            {
                                agentCash += specifyValueOfAddition;
                                Console.WriteLine("Agent with id: {0} added {1} and left: {2} cash in the bank", Id, specifyValueOfAddition, agentCash);
                                
                                break;
                            }
                        case 2:
                            {
                                agentCash -= specifyValueOfSubstraction;
                                Console.WriteLine("Agent with id: {0} withdrawed {1} and left: {2} cash in the bank", Id, specifyValueOfSubstraction, agentCash);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("dont know what to do");
                                break;
                            }
                    }
                    BankAcct.cash = agentCash;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

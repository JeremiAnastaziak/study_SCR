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
        public BankAcct Bank;




        public OperationAgent(int id = 0, int specifyOperation = 0, BankAcct bank = null) {
            Id = id;
            SpecifyOperation = specifyOperation;
            Bank = bank;
        }

        public void acctOperation() {
            bool gotLock = false;
            while (true)
            {
                int specifyValueOfAddition = 10;
                int specifyValueOfSubstraction = 5;

                //anulowanie operacji po nieudanej probie
                try
                {
                    Bank.sl.TryEnter(ref gotLock);
                    if (gotLock)
                    {
                        if ((BankAcct.cash - 10) < 0 && SpecifyOperation == 2)
                        {
                            Console.WriteLine("Could not finalize operation, balance is: {0}", BankAcct.cash);
                            return;
                        }
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

                        Thread.Sleep(100);

                    }
                    else
                    {
                        break;
                    }

                }

                finally
                {
                    // Only give up the lock if you actually acquired it
                    if (gotLock) Bank.sl.Exit();
                   
                    gotLock = false;

                }


                //ciagle ponawianie proby
                /*try
                {
                       Bank.sl.Enter(ref gotLock);
                        if ((BankAcct.cash - 10) < 0 && SpecifyOperation == 2)
                        {
                            Console.WriteLine("Could not finalize operation, balance is: {0}", BankAcct.cash);
                            return;
                        }
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

                   
                    Thread.Sleep(100);

                }
                finally
                {

                    // Only give up the lock if you actually acquired it
                    if (gotLock) Bank.sl.Exit();

                   gotLock = false;

                }
                */


                //ponawianie proby co cykl pracy agenta
                /*try
                {
                    Bank.sl.TryEnter(ref gotLock);

                    if (gotLock)
                    { 
                        //Bank.sl.Enter(ref gotLock);

                        if ((BankAcct.cash - 10) < 0 && SpecifyOperation == 2)
                        {
                            Console.WriteLine("Could not finalize operation, balance is: {0}", BankAcct.cash);
                            return;
                        }
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
                   
                    }
                    Thread.Sleep(1000);

                }
                finally
                {
                   
                    // Only give up the lock if you actually acquired it
                    //Thread.Sleep(100);
                    if (gotLock) Bank.sl.Exit();
                    
                    //Console.WriteLine("udalo sie");
                    gotLock = false;

                    
                }
                */




                /*lock (acctLock)
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
                }*/
            }
        }
    }
}

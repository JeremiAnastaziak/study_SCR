using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCR_anastaziak {
    class BankAcct {
        //public static Object acctLock = new Object();
        public static int cash = 1;
        //string Name { get; set; }

        public BankAcct() { }

        public void displayBalance()
        {
            while(true)
            {
                Console.WriteLine("Bank balance: {0}", cash);
                Thread.Sleep(2000);
            }
            
        }

        /*public BankAcct(double bal) {
            Balance = bal;
        }

        public double Withdraw(double amt) {
            if ((Balance - amt) < 0) {
                Console.WriteLine($"Sorry ${Balance} in Account");
                return Balance;
            }

            lock (acctLock) {
                if (Balance >= amt) {
                    Console.WriteLine("Removed {0} and {1} left in Account",
                    amt, (Balance - amt));
                    Balance -= amt;
                }

                return Balance;

            }
        }
        public void IssueWithdraw() {
            Withdraw(1);
        }*/

    }
}

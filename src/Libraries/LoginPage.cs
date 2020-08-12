using src.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Libraries
{
    public static class LoginPage
    {
        /// <summary>
        /// Checks if the supplied account number exists with the bank
        /// </summary>
        /// <param name="passedNUBAN"></param>
        /// <returns>true if account found else false</returns>
        public static bool Login(string passedNUBAN)
        {
            foreach (var item in BankData.Accounts)
            {
                if (Int32.Parse(passedNUBAN) == item.AccountNumber)
                {
                    return true;
                }
            }

            
            Console.WriteLine();
            Console.WriteLine("User profile not found.");
            Console.WriteLine("....");
            return false;            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace src.Libraries
{
    public static class ValidateRegistration
    {
        public static string FirstName;
        public static string LastName;
        public static string Email;

        /// <summary>
        /// Validates user's input while creating account
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="accountType"></param>
        /// <param name="amount"></param>
        /// <returns>true if fields are compliant, else false</returns>
        public static bool Customer(string firstName, string lastName, string email, string accountType, string amount)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            //AccountType = accountType.ToLower();
            //Amount = amount;

            if (ValidateTypeAndAmmount(accountType, amount) 
                && ValidateField())
            {
                BankAccount.AddNewAccount(firstName, lastName, email, accountType, Convert.ToDecimal(amount));
                Console.WriteLine();
                Console.WriteLine();
                return true;
            }
            else
            {
                return false;
            }               
        }

        /// <summary>
        /// Checks for the supplied account type and amount if valid
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="amount"></param>
        /// <returns>true if valid else false</returns>
        public static bool ValidateTypeAndAmmount(string accountType, string amount)
        {
            if (double.TryParse(amount, out var money))
            {
               if ((accountType.Equals("s", StringComparison.OrdinalIgnoreCase)
                && money >= 100)
                ||
                (accountType.Equals("c", StringComparison.OrdinalIgnoreCase)
                && money >= 1000))
                {
                    return true;
                }
            }            

            Console.WriteLine("...");
            Console.WriteLine("Incorrect Account Type OR Amount.");
            Console.WriteLine();
            return false;
        }

        /// <summary>
        /// Method is called by the Customer method for validation of input field
        /// </summary>
        /// <returns></returns>
        public static bool ValidateField()
        {
            if (FirstName != string.Empty &&
                LastName != string.Empty &&
                Email != string.Empty)
            {
                return true;
            }

            Console.WriteLine("...");
            Console.WriteLine("Don't leave any field empty.");
            Console.WriteLine();
            return false;
        }

        /// <summary>
        /// Validates supplied account number if is an integer
        /// </summary>
        /// <param name="nuban"></param>
        /// <returns></returns>
        public static bool ValidateAccountNumber(string nuban)
        {
            if (nuban.Length == 10)
            {
                if (int.TryParse(nuban, out var money))
                {
                    return true;
                }
            }

            Console.WriteLine("...");
            Console.WriteLine("Incorrect Nuban.");
            Console.WriteLine();
            return false;
        }
    }
}

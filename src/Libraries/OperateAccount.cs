using src.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Libraries
{
    static class OperateAccount
    {
        /// <summary>
        /// This is the login interface
        /// </summary>
        /// <param name="passedNUBAN"></param>
        public static void AccountOperations(string passedNUBAN)
        {
            var account = new Account();

            // Checks if the passed account number is registered with the bank
            foreach (var item in BankData.Accounts)
            {
                if (Int32.Parse(passedNUBAN) == item.AccountNumber)
                {
                    account = item;
                }
            }

            do
            {
                Console.WriteLine($@"


                    Welcome to  Mr./Mrs. {account.Owner}");
                Console.WriteLine();
                Console.WriteLine(@$"
        Account Portal for [ {account.AccountNumber} ]

");
                Console.WriteLine(@"
                     What would you like to do today:");
                Console.WriteLine();
                Console.WriteLine("Create another account  -------  Press 1");
                Console.WriteLine();
                Console.WriteLine("Make deposit ------------------  Press 2");
                Console.WriteLine();
                Console.WriteLine("Make withdrawal  --------------  Press 3");
                Console.WriteLine();
                Console.WriteLine("Get balance  ------------------  Press 4");
                Console.WriteLine();
                Console.WriteLine("Transfer to another account  --  Press 5");
                Console.WriteLine();
                Console.WriteLine("Transfer to another customer  -  Press 6");
                Console.WriteLine();
                Console.WriteLine("Get Account History  ----------  Press 7");
                Console.WriteLine();
                Console.WriteLine("Logout  -----------------------  Press 8");
                Console.WriteLine();
                    Console.Write("                         Your reply here: ");
                var response_two = Console.ReadLine();

                // Operations a user can perform in bank app
                switch (response_two)
                {
                    case "1":
                        Console.WriteLine();
                        Console.Write("Input Account Type: S for Savings and C for Current.");
                        var acctType = Console.ReadLine();
                        Console.Write("Input Deposit Amount: ");
                        var initialDeposit = Console.ReadLine();
                        if (ValidateRegistration.ValidateTypeAndAmmount(acctType, initialDeposit))
                        {
                            BankAccount.AddAccount(account, acctType.ToLower(), Convert.ToDecimal(initialDeposit));
                            Console.WriteLine();
                            break;
                        }
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.Write("Input Deposit Amount: ");
                        var deposit = Console.ReadLine();
                        var personalNote = "Self deposit";
                        account.MakeDeposit(Convert.ToDecimal(deposit), personalNote);
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.Write("Input Withdrawal Amount: ");
                        var withdraw = Console.ReadLine();
                        Console.Write("Input Transaction Note: ");
                        var withdrawNote = Console.ReadLine();
                        account.MakeWithdrawal(Convert.ToDecimal(withdraw), withdrawNote);
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        account.GetBalance();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        Console.Write("Amount To Transfer: ");
                        var transferSelf = Console.ReadLine();
                        Console.Write("Input Account Number: ");
                        var myNumber = Console.ReadLine();
                        account.MakeTransferToSelf(Convert.ToInt32(myNumber), Convert.ToDecimal(transferSelf));
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        Console.Write("Amount To Transfer: ");
                        var transferOther = Console.ReadLine();
                        Console.Write("Input Account Number: ");
                        var otherNumber = Console.ReadLine();
                        Console.Write("Transaction Note: ");
                        var transactionDetails = Console.ReadLine();
                        account.MakeTransferToOthers(Convert.ToInt32(otherNumber), Convert.ToDecimal(transferOther), transactionDetails);
                        Console.WriteLine();
                        break;
                    case "7":
                        Console.WriteLine();
                        Console.WriteLine(account.GetAccountHistory());
                        //account.GetAccountHistory();
                        Console.WriteLine();
                        break;
                    case "8":
                        Console.WriteLine("Logout");
                        break;
                    default:
                        break;
                }
                if (response_two == "8")
                {
                    break;
                }
            } while (true);
        }
    }
}

using src.Data;
using System;
using System.Collections.Generic;

namespace src.Libraries
{
    public class BankAccount
    {
		public static int accountNumberSeed = 1111111111;
		public static int idNumber = 0001;

		/// <summary>
		/// Creates new customer and also a bank account for the customer
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="email"></param>
		/// <param name="type"></param>
		/// <param name="amount"></param>
		public static void AddNewAccount(string firstName, string lastName, string email, string type, decimal amount)
        {
			var owner = $"{firstName} {lastName}";
			var acctNumber = accountNumberSeed++;
			var id = idNumber++;
			var note = "Initial Deposit";

			var account = new Account(id, type, acctNumber, owner, amount, note);
			BankData.Accounts.Add(account);

			var customer = new Customer(id, owner, email, acctNumber);
			BankData.Customers.Add(customer);

			Console.WriteLine();
			Console.WriteLine(@$"
				Account Successfully Created

			-----------------------------------
			KINDLY REMEMBER YOUR ACCOUNT NUMBER
			-----------------------------------
			Your NUBAN is -- {acctNumber}.
			-----------------------------------
");

			Console.WriteLine("Thank you for banking with us.");
            Console.WriteLine();
		}

		/// <summary>
		/// Creates a new Bank account for an existing customer,
		/// and updates the customers details with new account number
		/// </summary>
		/// <param name="account"></param>
		/// <param name="type"></param>
		/// <param name="amount"></param>
		public static void AddAccount(Account account, string type, decimal amount)
		{
			var acctNumber = accountNumberSeed++;
			var note = "Initial Deposit";

			var newAccount = new Account(account.CustomerID, type, acctNumber, account.Owner, amount, note);
			BankData.Accounts.Add(newAccount);

			foreach (var item in BankData.Customers)
			{
				if (item.ID == account.CustomerID)
				{
					item.Numbers.Add(acctNumber);
					break;
				}
			}

			Console.WriteLine();
			Console.WriteLine($@"
				Account Successfully Created

			-----------------------------------
			KINDLY REMEMBER YOUR ACCOUNT NUMBER
			-----------------------------------
			Your NUBAN is -- {acctNumber}.
			-----------------------------------
");

			Console.WriteLine("Thank you for banking with us.");
            Console.WriteLine();
			
			return;
		}
	}
}

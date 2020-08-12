using System;
using System.Collections.Generic;
using System.Text;
using src.Data;

namespace src.Libraries
{
    public class Account
    {
        public Account()
        {

        }
        public string Type { get; set; }
        public int AccountNumber { get; set; }
        public string Owner { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }
        public int CustomerID { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Transaction> allTransactions = new List<Transaction>();

        /// <summary>
        /// Bank Account Contructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="acctNumber"></param>
        /// <param name="owner"></param>
        /// <param name="amount"></param>
        /// <param name="note"></param>
        public Account(int id, string type, int acctNumber, string owner, decimal amount, string note)
        {
			this.CustomerID = id;
            this.Type = type;
            this.AccountNumber = acctNumber;
            this.Owner = owner;
			this.MakeDeposit(amount, note);
			this.Note = note;
        }

		/// <summary>
		/// Makes deposit into account and updates
		/// the balance and trasaction list
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="note"></param>
		public void MakeDeposit(decimal amount, string note)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException("Amount of deposit must be positive");
			}
			var deposit = new Transaction(amount, DateTime.Now, note);
			allTransactions.Add(deposit);
			this.Balance += amount;
			if(note != "Initial Deposit") Console.WriteLine("Deposit successful.");
		}

		/// <summary>
		/// Makes withdraw from account and updates
		/// the balance and trasaction list
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="note"></param>
		public void MakeWithdrawal(decimal amount, string note)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException("Amount of withdrawal must be positive");
			}
			if ((Balance - amount < 100 && this.Type == "s") || (Balance - amount < 0 && this.Type == "c"))
			{
				throw new InvalidOperationException("No sufficient funds for this withdrawal");
			}
			var withdrawal = new Transaction(-amount, DateTime.Now, note);
			allTransactions.Add(withdrawal);
			this.Balance -= amount;
            Console.WriteLine("Withdrawal successful.");
		}

		/// <summary>
		/// Prints out the Current Balance of Account
		/// </summary>
		public void GetBalance()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Your balance is: {this.Balance}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
		}

		/// <summary>
		/// Makes transfer to another of customer's account
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="note"></param>
		public bool MakeTransferToSelf(int number, decimal amount)
        {
			foreach (var item in BankData.Accounts)
			{
				if (item.AccountNumber == number && item.CustomerID == this.CustomerID && item.AccountNumber != this.AccountNumber)
				{

                    this.MakeWithdrawal(amount, $"Amount sent to {number}");
					item.MakeDeposit(amount, $"Amount received from {this.AccountNumber}");
                    Console.WriteLine("Money successfully transferred.");
                    Console.WriteLine();
                    return true;
				}
			}
			Console.WriteLine("Account not found.");
			return false;
		}

		/// <summary>
		/// Makes transfer to another customer's account
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="note"></param>
		public bool MakeTransferToOthers(int number, decimal amount, string note)
		{
            foreach (var item in BankData.Accounts)
            {
				if (item.AccountNumber == number && item.AccountNumber != this.AccountNumber)
				{
					this.MakeWithdrawal(amount, note);
					item.MakeDeposit(amount, $"Amount received from {this.AccountNumber} for {note}");
					Console.WriteLine("Money successfully transferred.");
					return true;
				}
			}
			Console.WriteLine("Account not found.");
			return false;
		}

		/// <summary>
		/// returns the account statement containing all transactions of account
		/// </summary>
		/// <returns></returns>
		public string GetAccountHistory()
		{
			var report = new StringBuilder();

			//HEADER
			report.AppendLine("Date\t\tAmount\tNote");
			foreach (var item in allTransactions)
			{
				//ROWS
				report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");
			}
			return report.ToString();
		}

	}
}

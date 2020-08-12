using System;
using System.Collections.Generic;
using System.Text;

namespace src.Libraries
{
	/// <summary>
	/// A type to hold the transaction details on an account
	/// </summary>
	public class Transaction
	{
		public decimal Amount { get; }
		public DateTime Date { get; }
		public string Notes { get; }

		public Transaction(decimal amount, DateTime date, string note)
		{
			this.Amount = amount;
			this.Date = date;
			this.Notes = note;
		}
	}
}

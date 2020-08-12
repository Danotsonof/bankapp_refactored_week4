using src.Libraries;
using System.Collections.Generic;

namespace src.Data
{
    public static class BankData
    {
        public static List<Customer> Customers = new List<Customer>();
        public static List<Account> Accounts = new List<Account>();
        public static List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}

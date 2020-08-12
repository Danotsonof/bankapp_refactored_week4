using NUnit.Framework;
using src.Data;
using src.Libraries;
using System;
using System.Text;

namespace tests
{
    public class AccountOperations
    {
        // Fields used in the Test Class
        // Arrange
        static string firstName = "Dare";
        static string lastName = "More";
        static string email = "dare.more@pepsi.com";
        static string owner = $"{firstName} {lastName}";
        static string accountType = "s";

        static string firstName2 = "Rice";
        static string lastName2 = "Chops";
        static string email2 = "rice.chops@belly.com";
        static string accountType2 = "c";
        static string amount2 = "1000";

        static int nuban = 1111111111;
        static int amount = 1000;
        static int id = 0001;


        /// <summary>
        /// This clears the Customers' List and Accounts' List of Banks
        /// each Test Method Runs.
        /// </summary>
        [TearDown]
        public void ClearAccountsList()
        {
            BankData.Accounts.Clear();
            BankData.Customers.Clear();
        }

        /// <summary>
        /// Confirms the Balance of Customer after making Deposit
        /// </summary>
        [Test]
        [Category("Deposit")]
        public void UpdateBalanceAfterDeposit()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal deposit = 12;
            var note2 = "Self deposit";
            // Act
            account.MakeDeposit(deposit, note2);
            var actual = account.Balance;
            // Assert
            Assert.AreEqual(1012, actual);
        }

        /// <summary>
        /// Reject Deposits with negative amount as input
        /// </summary>
        [Test]
        [Category("Deposit")]
        public void RejectInvalidDepositAmount()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal deposit = -12;
            var note2 = "Self deposit";
            //Assert
            Assert.That(() => account.MakeDeposit(deposit, note2), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        /// <summary>
        /// Customers with savings account can Withdraw money
        /// </summary>
        [Test]
        [Category("Withdrawal")]
        public void CustomersCanWithdrawMoney_SavingsAccount()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal withdraw = 200;
            var note2 = "Shawarma";
            // Act
            account.MakeWithdrawal(withdraw, note2);
            var actual = account.Balance;
            // Assert
            Assert.AreEqual(800, actual);
        }

        /// <summary>
        /// Dont perform withdrawals with negative amounts
        /// </summary>
        [Test]
        [Category("Withdrawal")]
        public void RejectNegativeNumberWithdrawal()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal withdraw = -200;
            var note2 = "Shawarma";
            // Assert
            Assert.That(() => account.MakeWithdrawal(withdraw, note2), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        /// <summary>
        /// For withdrawals, balance left must not be less than 
        /// the minimun current account balance (#0)
        /// </summary>
        [Test]
        [Category("Withdrawal")]
        public void RejectWithdrawalWithBalanceLessThanZeroBalance_CurrentAccount()
        {
            // Arrange
            var accountType = "c";
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal withdraw = 1200;
            var note2 = "Shawarma";
            // Assert
            Assert.That(() => account.MakeWithdrawal(withdraw, note2), Throws.TypeOf<InvalidOperationException>());
        }

        /// <summary>
        /// For withdrawals, balance left must not be less than 
        /// the minimun savings account balance (#100)
        /// </summary>
        [Test]
        [Category("Withdrawal")]
        public void RejectWithdrawalWithBalanceLessThanMinimumBalance_SavingsAccount()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal withdraw = 920;
            var note2 = "Shawarma";
            // Assert
            Assert.That(() => account.MakeWithdrawal(withdraw, note2), Throws.TypeOf<InvalidOperationException>());
        }

        /// <summary>
        /// Check for accurate senders balance after transfer
        /// </summary>
        [Test]
        [Category("Money Transfer")]
        public void AccurateBalanceAfterTransferToAnotherPersonalAccount()
        {
            // Arrange
            var nuban = 1111111110;
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            var customer = new Customer(id, owner, email, nuban);
            BankData.Customers.Add(customer);
            var acctType = "s";
            var initialDeposit = "1000";
            BankAccount.AddAccount(account, acctType.ToLower(), Convert.ToDecimal(initialDeposit));
            Account firstAccount = BankData.Accounts[0];
            Account secondAccount = BankData.Accounts[1];
            // Act
            firstAccount.MakeTransferToSelf(secondAccount.AccountNumber, 300);
            var actual = firstAccount.Balance;
            // Assert
            Assert.AreEqual(700, actual);
        }

        /// <summary>
        /// Check for accurate senders balance after transfer
        /// </summary>
        [Test]
        [Category("Money Transfer")]
        public void ValidateTransactionRecords()
        {
            // Arrange
            var nuban = 1111111110;
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);

            var customer = new Customer(id, owner, email, nuban);
            BankData.Customers.Add(customer);

            var acctType = "s";
            var initialDeposit = "1000";
            BankAccount.AddAccount(account, acctType.ToLower(), Convert.ToDecimal(initialDeposit));

            Account firstAccount = BankData.Accounts[0];
            Account secondAccount = BankData.Accounts[1];
            // Act
            firstAccount.MakeTransferToSelf(secondAccount.AccountNumber, 300);
            var actual = firstAccount.allTransactions.Count;
            //Assert
            Assert.AreEqual(2, actual);
        }

        /// <summary>
        /// Check for accurate receipt balance after transfer
        /// </summary>
        [Test]
        [Category("Money Transfer")]
        public void UpdateRecipientAccountAfterTransfer()
        {
            // Arrange
            BankAccount.AddNewAccount(firstName, lastName, email, accountType, Convert.ToDecimal(amount));

            BankAccount.AddNewAccount(firstName2, lastName2, email2, accountType2, Convert.ToDecimal(amount2));

            Account firstAccount = BankData.Accounts[0];
            Account secondAccount = BankData.Accounts[1];
            var note = "Payment for books";
            // Act
            firstAccount.MakeTransferToOthers(secondAccount.AccountNumber, 300, note);
            var actual = secondAccount.Balance;
            // Assert
            Assert.AreEqual(1300, actual);
        }

        /// <summary>
        /// Should not transfer money to the sending account number
        /// </summary>
        [Test]
        [Category("Money Transfer")]
        public void CustomerCanNotTrasferMoneyToTheSendingAccountNumber()
        {
            // Arrange
            BankAccount.AddNewAccount(firstName, lastName, email, accountType, Convert.ToDecimal(amount));
            // Act
            Account firstAccount = BankData.Accounts[0];
            var actual = firstAccount.MakeTransferToSelf(firstAccount.AccountNumber, 300);
            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Return false if recipient account number not found
        /// </summary>
        [Test]
        [Category("Money Transfer")]
        public void UnsuccessfulTransferIfAccountNotFound()
        {
            // Arrange
            BankAccount.AddNewAccount(firstName, lastName, email, accountType, Convert.ToDecimal(amount));
            Account firstAccount = BankData.Accounts[0];
            var note = "Payment for books";
            // Act
            var actual = firstAccount.MakeTransferToOthers(2111111111, 300, note);
            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Confirms the Balance of Customer after making Deposit
        /// </summary>
        [Test]
        [Category("Deposit")]
        public void ReturnsBankStatement()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            decimal deposit = 12;
            var note2 = "Self deposit";
            // Act
            account.MakeDeposit(deposit, note2);
            string actual = account.GetAccountHistory();
            // Assert
            Assert.AreEqual(typeof(String), actual.GetType());
        }

        /// <summary>
        /// Checks for valid login details
        /// </summary>
        [Test]
        public void ReturnTrueForAValidLogin()
        {
            // Arrange
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            // Act
            var expected = LoginPage.Login(nuban.ToString());
            // Assert
            Assert.IsTrue(expected);
        }

        /// <summary>
        /// Checks for invalid login details
        /// </summary>
        [Test]
        public void ReturnFalseForInvalidLoginDetail()
        {
            // Act
            var expected = LoginPage.Login(nuban.ToString());
            // Assert
            Assert.IsFalse(expected);
        }

        /// <summary>
        /// Checks the number of customers registered with a bank
        /// </summary>
        [Test]
        public void ReturnTheNumberOfCustomersABankHas()
        {
            // Arrange
            var nuban = 1111111110;
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);

            var customer = new Customer(id, owner, email, nuban);
            BankData.Customers.Add(customer);

            BankAccount.AddNewAccount(firstName2, lastName2, email2, accountType2, Convert.ToDecimal(amount2));
            // Act
            var actual = BankData.Customers.Count;
            // Assert
            Assert.AreEqual(2, actual); ;
        }

        /// <summary>
        /// Checks the number of customers registered with a bank
        /// </summary>
        [Test]
        public void ReturnTheNumberOfAccountsInABank()
        {
            // Arrange
            BankAccount.AddNewAccount(firstName, lastName, email, accountType, Convert.ToDecimal(amount));

            BankAccount.AddNewAccount(firstName2, lastName2, email2, accountType2, Convert.ToDecimal(amount2));
            // Act
            var actual = BankData.Accounts.Count;
            // Assert
            Assert.AreEqual(2, actual); ;
        }

        /// <summary>
        /// Checks the number of accounts owned by a Customer
        /// </summary>
        [Test]
        public void ReturnTheRightNumberOfAccountsForACustomer_WithMoreThanOneAccount()
        {
            // Arrange
            var nuban = 1111111110;
            var note = "short note";
            var account = new Account(id, accountType, nuban, owner, amount, note);
            BankData.Accounts.Add(account);
            var customer = new Customer(id, owner, email, nuban);
            BankData.Customers.Add(customer);

            var acctType = "s";
            var initialDeposit = "1000";
            BankAccount.AddAccount(account, acctType.ToLower(), Convert.ToDecimal(initialDeposit));

            var acctType2 = "s";
            var initialDeposit2 = "1000";
            BankAccount.AddAccount(account, acctType2.ToLower(), Convert.ToDecimal(initialDeposit2));
            // Act
            var actual = BankData.Customers[0].Numbers.Count;
            // Assert
            Assert.AreEqual(3, actual); ;
        }
    }
}
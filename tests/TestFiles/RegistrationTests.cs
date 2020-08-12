using NUnit.Framework;
using src.Libraries;

namespace tests
{
    public class RegistrationTests
    {
        // Fields used in the Test Class
        // Arrange
        static string firstName = "Dare";
        static string lastName = "More";
        static string email = "dare.more@pepsi.com";

        /// <summary>
        /// Checks if opening account balance is a valid input
        /// </summary>
        [Test]
        public void ReturnFalseForInvalidOpeningBalance()
        {
            // Arrange
            var accountType = "s";
            var amount = "";
            // Act
            var expected = ValidateRegistration.ValidateTypeAndAmmount(accountType, amount);
            // Assert
            Assert.IsFalse(expected);
        }

        /// <summary>
        /// Checks if amount for opening Savings Account
        /// is greater than or equal to the minimum opening balance
        /// </summary>
        [Test]
        public void RejectLowOpeningBalanceForSavingsAccount()
        {
            // Arrange
            var accountType = "s";
            var amount = "99";
            // Act
            var expected = ValidateRegistration.Customer(firstName, lastName, email, accountType, amount);
            // Assert
            Assert.IsFalse(expected);
        }

        /// <summary>
        /// Checks if amount for opening Current Account
        /// is greater than or equal to the minimum opening balance
        /// </summary>
        [Test]
        public void RejectLowOpeningBalanceForCurrentAccount()
        {
            // Arrange
            var accountType = "c";
            var amount = "900";
            // Act
            var expected = ValidateRegistration.Customer(firstName, lastName, email, accountType, amount);
            // Assert
            Assert.IsFalse(expected);
        }
    }
}
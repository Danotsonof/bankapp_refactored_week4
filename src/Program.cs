using System;
using System.Linq;

/// <summary>
/// Console Bank App Entry Point
/// </summary>
namespace src.Libraries
{
    public class Program
    {
        /// <summary>
        /// The Logic here involves asking the user for preferred options:
        /// Login
        /// Create Account
        /// Exit App
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool launchApp = true;
            do
            {
                Console.Write(@"
                                                ***************
                                                Welcome to Bank
                                               -----------------

    To LOGIN                press L
    To CREATE ACCOUNT       press C 
    To EXIT APP             press E

                Your Input here: ");
                var response_one = Console.ReadLine().ToLower();

                if (response_one == "c")
                {
                    var response_two = string.Empty;
                    do
                    {
                        Console.WriteLine();
                        Console.Write(@"
Are you a New Customer or an Existing Customer?


    New Customer,           press N
    Existing Customer,      press E
    To go Back,             press B

                Your Input here: ");
                        response_two = Console.ReadLine();
                        if (response_two.Equals("n", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Input the your details accordingly:");
                            Console.WriteLine("...");
                            Console.Write("Firstname: ");
                            var firstName = Console.ReadLine();

                            Console.Write("Lastname: ");
                            var lastName = Console.ReadLine();

                            Console.Write("Email address: ");
                            var email = Console.ReadLine();

                            Console.Write("Account Type (S for savings or C for current): ");
                            string accountType = Console.ReadLine();

                            Console.Write("Opening Amount: ");
                            var amount = Console.ReadLine();

                            var check = ValidateRegistration.Customer(firstName, lastName, email, accountType, amount);

                            if (check)
                            {
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (response_two.Equals("e", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Kindly Login to open another account.");
                            Console.WriteLine();
                            Console.WriteLine("...");
                            Console.Write("Input your NUBAN: ");
                            var passedNUBAN = Console.ReadLine();
                            if (LoginPage.Login(passedNUBAN)) OperateAccount.AccountOperations(passedNUBAN);
                        }
                        if (response_two.Equals("b", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            break;
                        }
                    } while (!response_two.Equals("n", StringComparison.OrdinalIgnoreCase) &&
                            !response_two.Equals("e", StringComparison.OrdinalIgnoreCase) &&
                            !response_two.Equals("b", StringComparison.OrdinalIgnoreCase));

                }
                else if (response_one == "l")
                {
                    Console.WriteLine("...");
                    Console.Write("Input your NUBAN: ");
                    var passedNUBAN = Console.ReadLine();
                    if(LoginPage.Login(passedNUBAN)) OperateAccount.AccountOperations(passedNUBAN);
                }
                else if (response_one == "e")
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Kindly select the right option.");
                    Console.WriteLine();
                    continue;
                }
            } while (launchApp);

        }
    }
}

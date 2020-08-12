using System;
using System.Collections.Generic;
using System.Text;

namespace src.Libraries
{
    /// <summary>
    /// The Customer Class for the Console app
    /// </summary>
    public class Customer
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public List<int> Numbers = new List<int>();

        public Customer()
        {

        }

        /// <summary>
        /// The constructor for the Customer class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <param name="email"></param>
        /// <param name="number"></param>
        public Customer(int id, string owner, string email, int number)
        {
            this.ID = id;
            this.Owner = owner;
            this.Email = email;
            this.Numbers.Add(number);
        }
    }
}

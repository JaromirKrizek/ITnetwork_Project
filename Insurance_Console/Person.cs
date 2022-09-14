using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance_Console
{
    //#############################################################################################
    internal class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }    // Je možné místo věku??? 
        public ulong PhoneNumber { get; private set; }     // Může být jako ulong ???

        //-----------------------------------------------------------------------------------------
        public Person(string firstName, string lastName, DateTime birthDate, ulong phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        //-----------------------------------------------------------------------------------------
        public override string ToString()
        {
            return String.Format($"{FirstName}\t{LastName}\t{BirthDate.ToString("d.M.yyyy")}\t{PhoneNumber}");
        }

    }
}
